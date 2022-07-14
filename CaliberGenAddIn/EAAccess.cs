using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using EA;
using EAAddIn.Applications.DatabaseReleaseManager;
using Attribute=EA.Attribute;
using Connector=EA.Connector;
using Element=EA.Element;

namespace EAAddIn
{
    internal class EaAccess
    {
        private readonly string _mappingTable;
        public readonly string EaRepository;
        private SqlConnection _eadbConnection;
        private SqlConnection _mappingConnection;
        public string Author;
        public string EA_GUID;
        public string EaStatus;
        public string Module;
        public string Name;


        public EaAccess()
        {
            var dbcon = new dbConnections();

            //if (
            //    AddInRepository.Instance.Repository.ConnectionString.ToUpper().Contains(
            //        dbcon.EARep1ConnectionString.ToUpper()))
            //    EaRepository = dbcon.CSEARepository;
            //else if (
            //    AddInRepository.Instance.Repository.ConnectionString.ToUpper().Contains(
            //        dbcon.EASecureConnectionString.ToUpper()))
            //    EaRepository = dbcon.CSSecureEARepository;

            //else if (
            //    AddInRepository.Instance.Repository.ConnectionString.ToUpper().Contains(
            //        dbcon.EADMLocalConnectionString.ToUpper()))
            //    EaRepository = dbcon.CSDMLocalRepository;

            EaRepository = AddInRepository.Instance.ConnectionStringshort;

            _mappingTable = dbcon.csCaliberEAMapping;
        }

        // -----------------------------------------------------
        // Retrieve Composite Diagram related to an element
        // 
        // ------------------------------------------------------
        public sEAElement GetCompositeDiagram(string EA_GUID, string returnType)
        {
            _eadbConnection = new SqlConnection(EaRepository);
            _eadbConnection.Open();

            var ret = new sEAElement();
            string condition = "";

            // Client is an Element (guid)
            // Supplier is a Diagram (guid)

            if (returnType == "Diagram")
                condition = "    AND [Client] = '{0}'";

            if (returnType == "Element")
                condition = "    AND [Supplier] = '{0}'";

            SqlCommand sqlCommand1 = _eadbConnection.CreateCommand();

            sqlCommand1.CommandText = string.Format(
                " SELECT [XrefID] " +
                "       ,[Name] " +
                "       ,[Type] " +
                "       ,[Visibility] " +
                "       ,[Namespace] " +
                "       ,[Requirement] " +
                "       ,[Constraint] " +
                "       ,[Behavior] " +
                "       ,[Partition] " +
                "       ,[Description] " +
                "       ,[Client] " +
                "       ,[Supplier] " +
                "       ,[Link] " +
                "   FROM t_xref " +
                "  WHERE " +
                "        [Name]   = 'DefaultDiagram' " +
                condition, EA_GUID);

            SqlDataReader reader;

            try
            {
                reader = sqlCommand1.ExecuteReader();

                if (reader != null)
                {
                    if (reader.Read())
                    {
                        if (returnType == "Diagram")
                        {
                            ret.EA_GUID = reader["Supplier"].ToString();
                            ret.Type = "Diagram";
                        }

                        if (returnType == "Element")
                        {
                            ret.EA_GUID = reader["Client"].ToString();
                            ret.Type = "Element";
                        }

                        ret.Stereotype = "";
                        ret.Name = reader["Name"].ToString();
                        ret.Status = "";
                        ret.ElementID = "";
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                ret.Name = "Error retrieving diagram. " + ex;
            }

            _eadbConnection.Close();

            return ret;
        }

        public string GetPathGuidForReference(string reference)
        {
            return GetPathGuidForReference(reference, false);
        }

        public string GetPathGuidForReference(string reference, bool ignoreLast)
        {
            string returnValue = string.Empty;

            if (!reference.Contains(".")) return string.Empty;

            var parms = new List<string>();

            string sql = GetSqlForFindingReference(reference, ref parms, ignoreLast);

            using (var conn = new SqlConnection(EaRepository))
            {
                var command = new SqlCommand(sql, conn);

                int i = 0;
                foreach (string parm in parms)
                {
                    i++;
                    command.Parameters.AddWithValue("@" + i, parm);
                }


                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    if (reader.Read())
                    {
                        if (ignoreLast)
                        {
                            returnValue = reader.GetInt32(0).ToString();
                        }
                        else
                        {
                            returnValue = reader.GetString(0);
                        }
                    }
                }

                finally
                {
                    reader.Close();
                }
            }
            return returnValue;
        }

        public string GetElementGuidForReference(string reference)
        {
            return GetGuidForReference(reference, true);
        }

        public string GetDiagramGuidForReference(string reference)
        {
            return GetGuidForReference(reference, false);
        }

        public string GetGuidForReference(string reference, bool element)
        {
            int packageId = 0;
            string guid = string.Empty;

            if (!reference.Contains(".")) return string.Empty;

            string[] nodes = reference.Split(new char[1] {'.'});


            string package = GetPathGuidForReference(reference, true);

            if (!Int32.TryParse(package, out packageId))
                return string.Empty;

            string table = element ? "t_object" : "t_diagram";

            string sql = "SELECT ea_guid  " + Environment.NewLine +
                         " FROM [dbo].[" + table + "]" + Environment.NewLine +
                         " WHERE name = @1" + Environment.NewLine +
                         " and package_id = " + packageId;

            using (var conn = new SqlConnection(EaRepository))
            {
                var command = new SqlCommand(sql, conn);
                conn.Open();

                command.Parameters.AddWithValue("@1", nodes[nodes.Length - 1]);

                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    if (reader.Read())
                    {
                        guid = reader.GetString(0);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            return guid;
        }

        //public string GetDiagramGuidForReference(string reference)
        //{
        //    var packageId = 0;
        //    var guid = string.Empty;

        //    if (!reference.Contains(".")) return string.Empty;

        //    var nodes = reference.Split(new char[1] { '.' });

        //    var sql = GetSqlForFindingReference(reference, true);

        //    using (var conn = new SqlConnection(EaRepository))
        //    {


        //        var command = new SqlCommand(sql, conn);
        //        conn.Open();

        //        var reader = command.ExecuteReader();

        //        try
        //        {
        //            if (reader.Read())
        //            {
        //                packageId = reader.GetInt32(0);
        //            }

        //        }
        //        finally
        //        {
        //            reader.Close();
        //        }

        //        if (packageId > 0)
        //        {
        //            sql = "SELECT ea_guid  " + Environment.NewLine +
        //            " FROM [dbo].[t_diagram]" + Environment.NewLine +
        //            " WHERE name = '" + nodes[nodes.Length-1] + "'" + Environment.NewLine +
        //            " and package_id = " + packageId;


        //            command = new SqlCommand(sql, conn);

        //            reader = command.ExecuteReader();

        //            try
        //            {
        //                if (reader.Read())
        //                {
        //                    guid = reader.GetString(0);
        //                }

        //            }
        //            finally
        //            {
        //                reader.Close();
        //            }
        //        }
        //    }

        //    return guid;
        //}


        private string GetSqlForFindingReference(string reference, ref List<string> parms, bool ignoreLast)
        {
            string[] nodes = reference.Split(new char[1] {'.'});

            string tableSql = string.Empty;
            string whereSql = string.Empty;

            int i = 0;

            foreach (string node in nodes)
            {
                i++;

                if (ignoreLast && i == nodes.Length) continue;

                tableSql +=
                    (tableSql.Length > 0 ? ", " : string.Empty)
                    + " [t_package] [" + i + "]";

                whereSql += (whereSql.Length > 0 ? " and " : string.Empty)
                            + " [" + i + "].name = @" + i;

                parms.Add(node);

                if (i > 1)
                {
                    whereSql += Environment.NewLine
                                + " and [" + i + "].parent_id = [" + (i - 1) + "].package_id";
                }
            }

            string sql = "SELECT ";

            if (ignoreLast)
            {
                sql += "[" + (nodes.Length - 1) + "].package_id ";
            }
            else
            {
                sql += "[" + nodes.Length + "].ea_guid ";
            }

            sql += Environment.NewLine +
                   " FROM " + tableSql + Environment.NewLine +
                   " WHERE " + whereSql;

            return sql;
        }

        // ----------------------------------------------------------
        // Update EA - set status to UnderDevelopment
        // ----------------------------------------------------------
        public string UpdateStatus()
        {
            // Connect to EA
            // Get element by guid
            // update status to UnderDevelopment
            // Close Connection
            string ret = "Ok.";

            if (String.IsNullOrEmpty(EA_GUID))
                return "GUID not provided";

            if (String.IsNullOrEmpty(Module))
                return "CAB not provided";

            //
            // Update status in EA.
            //

            // The status won't be updated here... to hard
            // to instantiate the com object takes ages...


            //
            // Create link in mapping table
            //
            _eadbConnection = new SqlConnection(_mappingTable);
            _eadbConnection.Open();

            new SqlCommand();
            SqlCommand sqlCommand = _eadbConnection.CreateCommand();

            EaStatus = "UnderDevelopment";

            sqlCommand.CommandText =
                string.Format(
                    "INSERT into CABMapping " +
                    "(CAB, EA_GUID, EaStatus, CABName) " +
                    "VALUES ('{0}', '{1}', '{2}', '{3}')",
                    Module,
                    EA_GUID,
                    EaStatus,
                    Name);

            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                ret = ex.ToString();
            }

            _eadbConnection.Close();

            return ret;
        }

        //-----------------------------------------------------
        // Retrieve EA Element
        //-----------------------------------------------------
        public sEAElement RetrieveEaElement(string EA_GUID)
        {
            //
            // EA SQL database
            //
            _eadbConnection = new SqlConnection(EaRepository);
            _eadbConnection.Open();

            sEAElement ret;

            // ret = RetrieveEaElement(_eadbConnection, EA_GUID);

            ret = GetEaElement(_eadbConnection, EA_GUID);

            _eadbConnection.Close();

            return ret;
        }

        //-----------------------------------------------------
        // Retrieve EA Element with existing SQL Connection
        //-----------------------------------------------------
        public sEAElement RetrieveEaElement(SqlConnection eadbConnection, string EA_GUID)
        {
            var ret = new sEAElement();

            SqlCommand sqlCommand1 = eadbConnection.CreateCommand();

            sqlCommand1.CommandText = string.Format(
                "SELECT " +
                "   ea_guid, Object_ID, Object_Type, " +
                "   Diagram_ID, Name, Package_ID, Stereotype, " +
                "   Status, Author " +
                "FROM t_object " +
                " WHERE " +
                " ea_guid = '{0}'", EA_GUID);

            SqlDataReader reader;

            try
            {
                reader = sqlCommand1.ExecuteReader();

                if (reader != null)
                {
                    if (reader.Read())
                    {
                        ret.EA_GUID = reader["ea_guid"].ToString();
                        ret.Type = reader["Object_Type"].ToString();
                        ret.Stereotype = reader["Stereotype"].ToString();
                        ret.Name = reader["Name"].ToString();
                        ret.Status = reader["Status"].ToString();
                        ret.ElementID = reader["Object_ID"].ToString();
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                ret.Name = "Error retrieving element. " + ex;
            }


            return ret;
        }


        //-----------------------------------------------------
        // Retrieve EA Package
        //-----------------------------------------------------
        public static string getCurrentPath()
        {
            string ret;
            string textElementID = "";
            string separator = "\\";

            // Get current element
            object selectedObject = AddInRepository.Instance.Repository.GetTreeSelectedObject();
            Package selectedPackage = AddInRepository.Instance.Repository.GetTreeSelectedPackage();

            // Get path
            string path = "";
            var treeitem = new ArrayList();

            Package walkbypath;
            walkbypath = AddInRepository.Instance.Repository.GetPackageByGuid(selectedPackage.PackageGUID);
            treeitem.Add(walkbypath.Name);

            while (walkbypath.ParentID > 0)
            {
                walkbypath = AddInRepository.Instance.Repository.GetPackageByID(walkbypath.ParentID);
                treeitem.Add(walkbypath.Name);
            }

            for (int i = treeitem.Count - 1; i >= 0; i--)
            {
                path += treeitem[i] + separator;
            }


            if (selectedObject is Element)
            {
                var selectedElement = (Element) selectedObject;
                path += "(E)" + selectedElement.Name;
                textElementID = "E" + selectedElement.ElementID.ToString().PadLeft(10, '0');
                ;
            }

            if (selectedObject is Diagram)
            {
                var selectedElement = (Diagram) selectedObject;
                path += "(D)" + selectedElement.Name;
                textElementID = "D" + selectedElement.DiagramID.ToString().PadLeft(10, '0');
                ;
            }

            if (selectedObject is Package)
            {
                var selectedElement = (Package) selectedObject;
                path += "(P)" + selectedElement.Name;
                textElementID = "P" + selectedElement.PackageID.ToString().PadLeft(10, '0');
            }

            path = textElementID + separator + path;

            ret = "#" + path;

            return ret;
        }


        //-----------------------------------------------------
        // Retrieve EA Package
        //-----------------------------------------------------
        private static sEAElement RetrieveEaPackage(SqlConnection eadbConnection, string EA_GUID)
        {
            var ret = new sEAElement();

            SqlCommand sqlCommand1;
            sqlCommand1 = eadbConnection.CreateCommand();

            sqlCommand1.CommandText = string.Format(
                " SELECT " +
                "[Package_ID], [Name], [Parent_ID], [CreatedDate], [ModifiedDate]" +
                ",[Notes],[ea_guid],[XMLPath],[IsControlled],[LastLoadDate],[LastSaveDate]" +
                ",[Version],[Protected],[PkgOwner],[UMLVersion],[UseDTD],[LogXML],[CodePath]" +
                ",[Namespace],[TPos],[PackageFlags],[BatchSave],[BatchLoad]" +
                " FROM [t_package] " +
                " WHERE " +
                " ea_guid = '{0}'", EA_GUID);

            SqlDataReader reader;

            try
            {
                reader = sqlCommand1.ExecuteReader();

                if (reader != null)
                {
                    if (reader.Read())
                    {
                        ret.EA_GUID = reader["ea_guid"].ToString();
                        ret.Name = reader["Name"].ToString();
                        ret.Type = "Package";
                        ret.Stereotype = "?";
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                ret.Name = "Error retrieving element. " + ex;
            }


            //_eadbConnection.Close();

            return ret;
        }

        //-----------------------------------------------------
        // Retrieve EA Diagram
        //-----------------------------------------------------
        private sEAElement retrieveEADiagram(SqlConnection EADBConnection, string EA_GUID)
        {
            //
            // EA SQL database
            //
            //_eadbConnection = new SqlConnection(EaRepository);
            //_eadbConnection.Open();
            var ret = new sEAElement();

            var sqlCommand1 = new SqlCommand();
            sqlCommand1 = EADBConnection.CreateCommand();

            sqlCommand1.CommandText = string.Format(
                " SELECT " +
                "[Diagram_ID], [Package_ID] ,[ParentID] ,[Diagram_Type] ,[Name]" +
                ",[Version] ,[Author] ,[ShowDetails] ,[Notes] ,[Stereotype],[AttPub]" +
                ",[AttPri] ,[AttPro] ,[Orientation] ,[cx] ,[cy] ,[Scale] ,[CreatedDate]" +
                ",[ModifiedDate],[HTMLPath],[ShowForeign],[ShowBorder],[ShowPackageContents]" +
                ",[PDATA],[Locked],[ea_guid],[TPos],[Swimlanes],[StyleEx]" +
                " FROM [t_diagram] " +
                " WHERE " +
                " ea_guid = '{0}'", EA_GUID);

            SqlDataReader reader;

            try
            {
                reader = sqlCommand1.ExecuteReader();

                if (reader != null)
                {
                    if (reader.Read())
                    {
                        ret.EA_GUID = reader["ea_guid"].ToString();
                        ret.Name = reader["Name"].ToString();
                        ret.Type = "Diagram";
                        ret.Stereotype = reader["Diagram_Type"].ToString();
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                ret.Name = "Error retrieving element. " + ex;
            }

            //_eadbConnection.Close();

            return ret;
        }


        //-----------------------------------------------------
        // Retrieve EA Attribute
        //-----------------------------------------------------
        private sEAElement retrieveEAAttribute(SqlConnection EADBConnection, string EA_GUID)
        {
            //
            // EA SQL database
            //
            //_eadbConnection = new SqlConnection(EaRepository);
            //_eadbConnection.Open();

            var ret = new sEAElement();

            var sqlCommand1 = new SqlCommand();
            sqlCommand1 = EADBConnection.CreateCommand();

            sqlCommand1.CommandText = string.Format(
                " SELECT " +
                "[Object_ID],[Name],[Scope],[Stereotype],[Containment],[IsStatic]" +
                ",[IsCollection],[IsOrdered],[AllowDuplicates],[LowerBound],[UpperBound]" +
                ",[Container],[Notes],[Derived],[ID],[Pos],[GenOption],[Length],[Precision]" +
                ",[Scale],[Const],[Style],[Classifier],[Default],[Type],[ea_guid],[StyleEx]" +
                "FROM [t_attribute]" +
                " WHERE " +
                " ea_guid = '{0}'", EA_GUID);

            SqlDataReader reader;

            try
            {
                reader = sqlCommand1.ExecuteReader();

                if (reader != null)
                {
                    if (reader.Read())
                    {
                        ret.EA_GUID = reader["ea_guid"].ToString();
                        ret.Name = reader["Name"].ToString();
                        ret.Type = reader["Type"].ToString();
                        ret.Stereotype = reader["Stereotype"].ToString();
                        ret.Status = "N/A";
                        ret.ElementID = reader["ID"].ToString();
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                ret.Name = "Error retrieving element. " + ex;
            }


            //_eadbConnection.Close();

            return ret;
        }

        //
        // Retrieve EA Element using SQL instead of API
        // Using EA_GUID
        //
        public sEAElement GetEaElement(SqlConnection eadbConnection,
                                       string EA_GUID)
        {
            sEAElement ret;
            sEAElement eaElem;

            eaElem.EA_GUID = null;
            eaElem.Name = "N/A";
            eaElem.Stereotype = "N/A";
            eaElem.Type = "N/A";
            eaElem.Status = "N/A";
            eaElem.ElementID = "0";

            // Try to retrieve Class or Use Case
            eaElem = RetrieveEaElement(eadbConnection, EA_GUID);
            if (eaElem.EA_GUID == null)
            {
                // Try package
                eaElem = RetrieveEaPackage(eadbConnection, EA_GUID);
                if (eaElem.EA_GUID == null)
                {
                    // Try attribute
                    eaElem = retrieveEAAttribute(eadbConnection, EA_GUID);
                }
            }

            ret.EA_GUID = eaElem.EA_GUID;
            ret.Name = eaElem.Name;
            ret.Type = eaElem.Type;
            ret.Stereotype = eaElem.Stereotype;
            ret.Status = eaElem.Status;
            ret.ElementID = eaElem.ElementID;


            return ret;
        }


        //
        // Retrieve EA Element using SQL instead of API
        //
        public sEAElement GetEaElement(SqlConnection eadbConnection,
                                       object selectedObject)
        {
            sEAElement ret;
            sEAElement eaElem;

            eaElem.EA_GUID = null;
            eaElem.Name = "N/A";
            eaElem.Stereotype = "N/A";
            eaElem.Type = "N/A";
            eaElem.Status = "N/A";
            eaElem.ElementID = "0";


            try
            {
                var element = (Element) AddInRepository.Instance.Repository.GetTreeSelectedObject();
                eaElem = RetrieveEaElement(eadbConnection, element.ElementGUID);
            }
            catch (Exception)
            {
                try
                {
                    var element = (Package) AddInRepository.Instance.Repository.GetTreeSelectedObject();
                    eaElem = RetrieveEaPackage(eadbConnection, element.PackageGUID);
                }
                catch (Exception)
                {
                    try
                    {
                        var element = (Diagram) AddInRepository.Instance.Repository.GetTreeSelectedObject();
                        eaElem = retrieveEADiagram(eadbConnection, element.DiagramGUID);
                    }
                    catch (Exception)
                    {
                        try
                        {
                            // Check how the current selected attribute can be retrieved.
                            // 
                            var element = (Attribute) AddInRepository.Instance.Repository.GetTreeSelectedObject();
                            eaElem = retrieveEAAttribute(eadbConnection, element.AttributeGUID);
                        }
                        catch (Exception)
                        {
                            eaElem.Name = "Error.";
                        }
                    }
                }
            }

            ret.EA_GUID = eaElem.EA_GUID;
            ret.Name = eaElem.Name;
            ret.Type = eaElem.Type;
            ret.Stereotype = eaElem.Stereotype;
            ret.Status = eaElem.Status;
            ret.ElementID = eaElem.ElementID;

            return ret;
        }


        // ----------------------------------------------------------
        //  Check user access
        // ----------------------------------------------------------
        public bool UserAccess(string user, string usergroup)
        {
            ArrayList groups = SecurityGroupList(user);
            bool ret = false;

            foreach (string group in groups)
            {
                if (group == usergroup)
                {
                    ret = true;
                }
            }

            return ret;
        }

        // ----------------------------------------------------------
        //               Retrieve Groups for user
        // ----------------------------------------------------------
        public ArrayList SecurityGroupList(string user)
        {
            var retArray = new ArrayList();
            //
            // EA SQL database
            //
            _eadbConnection = new SqlConnection(EaRepository);
            _eadbConnection.Open();

            SqlCommand sqlCommand1;
            sqlCommand1 = _eadbConnection.CreateCommand();

            sqlCommand1.CommandText = string.Format(
                "  SELECT                  " +
                "           usr.UserID     " +
                "          ,usr.UserLogin  " +
                "          ,usr.FirstName  " +
                "          ,usr.Surname    " +
                "          ,grp.GroupID    " +
                "          ,grp.GroupName  " +
                "    FROM  dbo.t_secusergroup as usergroup, " +
                "          dbo.t_secuser as usr,  " +
                "          dbo.t_secgroup as grp " +
                "   WHERE " +
                "          grp.GroupID =  usergroup.GroupID " +
                "     AND  usergroup.UserID = usr.UserID " +
                "     AND  usr.UserLogin = '{0}'", user);

            SqlDataReader reader = sqlCommand1.ExecuteReader();

            if (reader != null)
            {
                while (reader.Read())
                {
                    retArray.Add(reader["GroupName"].ToString());
                }

                reader.Close();
            }

            _eadbConnection.Close();

            return retArray;
        }

        // ----------------------------------------------------------
        //               List appliesTo stereotype
        // ----------------------------------------------------------

        public ArrayList GetAppliesToList()
        {
            var retArray = new ArrayList();
            //
            // EA SQL database
            //
            _eadbConnection = new SqlConnection(EaRepository);
            _eadbConnection.Open();

            SqlCommand sqlCommand1 = _eadbConnection.CreateCommand();

            sqlCommand1.CommandText = string.Format(
                "SELECT DISTINCT AppliesTo " +
                "FROM [EA_Release1].[dbo].[t_stereotypes]");

            SqlDataReader reader = sqlCommand1.ExecuteReader();

            if (reader != null)
            {
                while (reader.Read())
                {
                    retArray.Add(reader["AppliesTo"].ToString());
                }

                reader.Close();
            }

            _eadbConnection.Close();

            return retArray;
        }

        // ----------------------------------------------------------
        //       List stereotypes for a given element type
        // ----------------------------------------------------------

        public ArrayList GetStereotypeList(string appliesTo)
        {
            var retArray = new ArrayList();
            //
            // EA SQL database
            //
            _eadbConnection = new SqlConnection(EaRepository);
            _eadbConnection.Open();

            SqlCommand sqlCommand1 = _eadbConnection.CreateCommand();

            sqlCommand1.CommandText = string.Format(
                "SELECT [Stereotype] " +
                "FROM [EA_Release1].[dbo].[t_stereotypes] " +
                "WHERE [AppliesTo] = '{0}'", appliesTo);

            SqlDataReader reader = sqlCommand1.ExecuteReader();

            if (reader != null)
            {
                while (reader.Read())
                {
                    retArray.Add(reader["Stereotype"].ToString());
                }

                reader.Close();
            }

            _eadbConnection.Close();

            return retArray;
        }

        // ----------------------------------------------------------
        //       List status 
        // ----------------------------------------------------------

        public ArrayList GetStatusList()
        {
            var retArray = new ArrayList();
            //
            // EA SQL database
            //
            _eadbConnection = new SqlConnection(EaRepository);
            _eadbConnection.Open();

            SqlCommand sqlCommand1 = _eadbConnection.CreateCommand();

            sqlCommand1.CommandText = string.Format(
                "SELECT [Status] " +
                "FROM [t_statustypes] ");

            SqlDataReader reader = sqlCommand1.ExecuteReader();

            if (reader != null)
            {
                while (reader.Read())
                {
                    retArray.Add(reader["Status"].ToString());
                }

                reader.Close();
            }

            _eadbConnection.Close();

            return retArray;
        }


        // ----------------------------------------------------------
        //       Get Attribute Details
        // ----------------------------------------------------------
        private stEAAttribute GetEaAttribute(string EA_GUID)
        {
            //
            // EA SQL database
            //
            var eadbConn = new SqlConnection(EaRepository);
            eadbConn.Open();

            var ret = new stEAAttribute();

            SqlCommand sqlgetAttribute = eadbConn.CreateCommand();

            sqlgetAttribute.CommandText = string.Format(
                "SELECT " +
                "   [Object_ID] ,[Name] ,[Scope] ,[Stereotype] ,[Containment] " +
                "  ,[IsStatic]  ,[IsCollection]  ,[IsOrdered]  ,[AllowDuplicates] " +
                "  ,[LowerBound],[UpperBound]    ,[Container]  ,[Notes]  " +
                "  ,[Derived]   ,[ID]  ,[Pos]    ,[GenOption]  ,[Length]  " +
                "  ,[Precision] ,[Scale] ,[Const],[Style]      ,[Classifier] " +
                "  ,[Default]   ,[Type]  ,[ea_guid]  ,[StyleEx]   " +
                " FROM [EA_Release1].[dbo].[t_attribute] " +
                " WHERE " +
                " [ea_guid] = '{0}' ", EA_GUID);

            SqlDataReader readerAttribute;

            try
            {
                readerAttribute = sqlgetAttribute.ExecuteReader();

                if (readerAttribute != null)
                {
                    if (readerAttribute.Read())
                    {
                        ret.Object_ID = Convert.ToInt32(readerAttribute["Object_ID"]);
                        ret.Name = readerAttribute["Name"].ToString();
                        ret.Scope = readerAttribute["Scope"].ToString();

                        if (readerAttribute["Stereotype"] is DBNull)
                            ret.Stereotype = "";
                        else
                            ret.Stereotype = readerAttribute["Stereotype"].ToString();

                        ret.Notes = readerAttribute["Notes"].ToString();
                        ret.Type = readerAttribute["Type"].ToString();
                        ret.ea_guid = readerAttribute["ea_guid"].ToString();

                        if (readerAttribute["StyleEx"] is DBNull)
                            ret.StyleEx = "";
                        else
                            ret.StyleEx = readerAttribute["StyleEx"].ToString();

                        if (readerAttribute["Containment"] is DBNull)
                            ret.Containment = "";
                        else
                            ret.Containment = readerAttribute["Containment"].ToString();

                        if (readerAttribute["IsStatic"] is DBNull)
                            ret.IsStatic = 0;
                        else
                            ret.IsStatic = Convert.ToInt32(readerAttribute["IsStatic"]);

                        if (readerAttribute["Length"] is DBNull)
                            ret.Length = 0;
                        else
                            ret.Length = Convert.ToInt32(readerAttribute["Length"]);

                        //ret.IsCollection = Convert.ToInt32( readerAttribute["IsCollection"]);
                        //ret.IsOrdered = Convert.ToInt32( readerAttribute["IsOrdered"] );
                        //ret.AllowDuplicates = Convert.ToInt32( readerAttribute["AllowDuplicates"] ) ;
                        //ret.LowerBound = readerAttribute["LowerBound"].ToString();
                        //ret.UpperBound = readerAttribute["UpperBound"].ToString();
                        //ret.Container = readerAttribute["Container"].ToString();
                        //ret.Derived = readerAttribute["Derived"].ToString();
                        //ret.ID = Convert.ToInt32( readerAttribute["ID"].ToString());
                        //ret.Pos = Convert.ToInt32( readerAttribute["Pos"].ToString());
                        //ret.GenOption = readerAttribute["GenOption"].ToString();
                        //ret.Precision = Convert.ToInt32( readerAttribute["Precision"].ToString());
                        //ret.Scale = Convert.ToInt32( readerAttribute["Scale"].ToString());
                        //ret.Const = Convert.ToInt32( readerAttribute["Const"].ToString()); 
                        //ret.Style = readerAttribute["Style"].ToString();
                        //ret.Classifier = readerAttribute["Classifier"].ToString();
                        //ret.Default = readerAttribute["Default"].ToString();
                    }

                    readerAttribute.Close();
                }
            }
            catch (Exception ex)
            {
                ret.Name = "Error retrieving element. " + ex;
            }

            eadbConn.Close();

            return ret;
        }

        // ----------------------------------------------------------
        //       Get Operation Details
        // ----------------------------------------------------------
        private stEAOperation GetEaOperation(string EA_GUID)
        {
            //
            // EA SQL database
            //
            var EADBConn = new SqlConnection(EaRepository);
            EADBConn.Open();

            var ret = new stEAOperation();

            var sqlCommand1 = new SqlCommand();
            sqlCommand1 = EADBConn.CreateCommand();

            sqlCommand1.CommandText = string.Format(
                "SELECT " +
                "  [OperationID] ,[Object_ID] ,[Name]  ,[Scope]  ,[Type]  ,[ReturnArray]  " +
                "  ,[Stereotype]  ,[IsStatic]  ,[Concurrency]     ,[Notes] ,[Behaviour] " +
                "  ,[Abstract]    ,[GenOption] ,[Synchronized]    ,[Pos]   ,[Const] " +
                "  ,[Style] ,[Pure]  ,[Throws] ,[Classifier]  ,[Code] ,[IsRoot] " +
                "  ,[IsLeaf] ,[IsQuery] ,[StateFlags] ,[ea_guid]  ,[StyleEx] " +
                " FROM [EA_Release1].[dbo].[t_operation] " +
                " WHERE " +
                " [ea_guid] = '{0}'", EA_GUID);

            SqlDataReader reader = null;

            try
            {
                reader = sqlCommand1.ExecuteReader();

                if (reader.Read())
                {
                    ret.OperationID = Convert.ToInt32(reader["OperationID"].ToString());
                    ret.Object_ID = Convert.ToInt32(reader["Object_ID"].ToString());
                    ret.Name = reader["Name"].ToString();
                    ret.Scope = reader["Scope"].ToString();
                    ret.Type = reader["Type"].ToString();
                    ret.ReturnArray = reader["ReturnArray"].ToString();
                    ret.Stereotype = reader["Stereotype"].ToString();
                    ret.IsStatic = reader["IsStatic"].ToString();
                    ret.Concurrency = reader["Concurrency"].ToString();
                    ret.Notes = reader["Notes"].ToString();
                    ret.Behaviour = reader["Behaviour"].ToString();
                    ret.Abstract = reader["Abstract"].ToString();
                    ret.GenOption = reader["GenOption"].ToString();
                    ret.Synchronized = reader["Synchronized"].ToString();
                    ret.StateFlags = reader["StateFlags"].ToString();
                    ret.ea_guid = reader["ea_guid"].ToString();
                    ret.StyleEx = reader["StyleEx"].ToString();

                    if (reader["Pos"] is DBNull)
                        ret.Pos = 0;
                    else
                        ret.Pos = Convert.ToInt32(reader["Pos"]);

                    if (reader["Const"] is DBNull)
                        ret.Const = 0;
                    else
                        ret.Const = Convert.ToInt32(reader["Const"]);

                    ret.Style = reader["Style"].ToString();

                    if (reader["Pure"] is DBNull)
                        ret.Pure = 0;
                    else
                        ret.Pure = Convert.ToInt32(reader["Pure"]);

                    ret.Throws = reader["Throws"].ToString();
                    ret.Classifier = reader["Classifier"].ToString();
                    ret.Code = reader["Code"].ToString();
                    ret.IsRoot = Convert.ToInt32(reader["IsRoot"].ToString());
                    ret.IsLeaf = Convert.ToInt32(reader["IsLeaf"].ToString());
                    ret.IsQuery = Convert.ToInt32(reader["IsQuery"].ToString());
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                ret.Name = "Error retrieving element. " + ex;
            }

            EADBConn.Close();

            return ret;
        }

        // --------------------------------------------------
        //      Get metadata value
        // --------------------------------------------------
        public static string getMetadataValue(string metadata,
                                              string toBeSearched,
                                              int objLength)
        {
            string ret = "";

            if (toBeSearched.Length < metadata.Length + objLength)
                return ret;

            for (int i = 0; i < toBeSearched.Length - metadata.Length - objLength; i++)
            {
                string nextN = toBeSearched.Substring(i, metadata.Length);

                if (nextN == metadata)
                {
                    ret = toBeSearched.Substring(i + metadata.Length, objLength);
                }
            }

            return ret;
        }


        // ----------------------------------------------------------
        //       List elements linked to an element
        // ----------------------------------------------------------
        public List<ConnectorList> getLinkedElementsSQL(Element inElement,
                                                        string filterStereotype,
                                                        string filterType,
                                                        bool calls,
                                                        bool calledBy,
                                                        List<string> connectorTypes,
                                                        string[] includedStereotypes,
                                                        string[] excludedStereotypes,
                                                        int elementID
            )
        {
            string filterToStereotypes = string.Empty;

            if ((inElement == null) && elementID == 0)
                return null;

            // Gives the caller the option to send the element ID or the element
            if (inElement != null)
                elementID = inElement.ElementID;

            var ConnectorList = new List<ConnectorList>();

            //
            // EA SQL database
            //
            _eadbConnection = new SqlConnection(EaRepository);
            _eadbConnection.Open();

            string sterSearch = "";
            if (!String.IsNullOrEmpty(filterStereotype))
            {
                sterSearch = " t_object.Stereotype like  '%{1}%' AND ";
            }
            string typeSearch = "";
            if (!String.IsNullOrEmpty(filterStereotype) && filterType != "All")
            {
                typeSearch = " t_object.Object_Type like  '%{2}%' AND ";
            }
            string filterConnectorType = "";

            if (connectorTypes != null)
            {
                if (connectorTypes.Count > 0 && connectorTypes[0] != "All")
                {
                    filterConnectorType = "(t_connector.Connector_Type IN ('" + connectorTypes[0] + "'";

                    for (int i = 1; i < connectorTypes.Count; i++)
                    {
                        filterConnectorType += ",'" + connectorTypes[i] + "'";
                    }

                    filterConnectorType += ")) AND ";
                }
            }
            if (includedStereotypes != null)
            {
                if (includedStereotypes.Length > 0)
                {
                    filterToStereotypes = "(t_object.Stereotype IN ('" + includedStereotypes[0] + "'";

                    for (int i = 1; i < includedStereotypes.Length; i++)
                    {
                        filterToStereotypes += ",'" + includedStereotypes[i] + "'";
                    }

                    filterToStereotypes += ")) AND ";
                }
            }
            if (excludedStereotypes != null)
            {
                if (excludedStereotypes.Length > 0)
                {
                    filterToStereotypes = "(t_object.Stereotype NOT IN ('" + excludedStereotypes[0] + "'";

                    for (int i = 1; i < excludedStereotypes.Length; i++)
                    {
                        filterToStereotypes += ",'" + excludedStereotypes[i] + "'";
                    }

                    filterToStereotypes += ")) AND ";
                }
            }


            //
            // Get Calls
            //
            // The object information is for the end point (destination)
            //
            if (calls)
            {
                var sqlCommand1 = new SqlCommand();
                sqlCommand1 = _eadbConnection.CreateCommand();

                sqlCommand1.CommandText = string.Format(
                    " SELECT t_connector.[Connector_ID] ConnectorId" +
                    " ,t_connector.[Name]               ConnectorName" +
                    " ,t_connector.[Direction]          ConnectorDirection" +
                    " ,t_connector.[Connector_Type]     ConnectorType" +
                    " ,t_connector.[Start_Object_ID]    ConnectorStartId" +
                    " ,t_connector.[End_Object_ID]      ConnectorEndId" +
                    " ,t_connector.[StyleEx]            ConnectorStyleEx" +
                    " ,t_object.Object_Type             ObjectType" +
                    " ,t_object.ea_guid                 ObjectGuid" +
                    " ,t_object.Stereotype              ObjectStereotype" +
                    " ,t_object.Name                    ObjectName" +
                    " ,t_object.Alias                   ObjectAlias" +
                    " ,t_object.Note                    ObjectNote" +
                    " FROM t_connector, t_object " +
                    " WHERE " +
                    sterSearch +
                    typeSearch +
                    filterToStereotypes +
                    filterConnectorType +
                    " (t_connector.[End_Object_ID] = t_object.Object_ID " +
                    "  AND [Start_Object_ID] = {0}) ",
                    elementID,
                    filterStereotype,
                    filterType);

                SqlDataReader reader = sqlCommand1.ExecuteReader();

                while (reader.Read())
                {
                    var connList = new ConnectorList();
                    connList.ObjectElementId =
                        Convert.ToInt32(reader["ConnectorEndId"].ToString());

                    connList.ObjectGuid = reader["ObjectGuid"].ToString();

                    connList.ConnectorId = Convert.ToInt32(reader["ConnectorId"].ToString());
                    connList.ConnectorStartId = Convert.ToInt32(reader["ConnectorStartId"].ToString());
                    connList.ConnectorEndId = Convert.ToInt32(reader["ConnectorEndId"].ToString());
                    connList.ConnectorName = reader["ConnectorName"].ToString();
                    connList.ConnectorType = reader["ConnectorType"].ToString();
                    connList.ObjectStereotype = reader["ObjectStereotype"].ToString();
                    connList.ConnectorDirection = reader["ConnectorDirection"].ToString();
                    connList.ObjectName = reader["ObjectName"].ToString();
                    connList.ConnectorStyleEx = reader["ConnectorStyleEx"].ToString();

                    // Check specific messages for GEN
                    //
                    if (connList.ObjectStereotype.Contains("GEN"))
                    {
                        connList.ConnectorDirection = "O---->> (Calls)";
                        connList.ConnectorName = "";
                    }

                    connList.ObjectType = reader["ObjectType"].ToString();
                    connList.ObjectAlias = reader["ObjectAlias"].ToString();
                    connList.ObjectNote = reader["ObjectNote"].ToString();

                    connList.UsesUsedBy = " o---> ";
                    if (connList.ConnectorType == "Realisation")
                        connList.UsesUsedBy = " Implements ";

                    // Export Start Element (From) and End Element (To)
                    //
                    if (inElement != null)
                    {
                        connList.FromElementId = inElement.ElementID;
                        connList.FromElementName = inElement.Name;
                        connList.FromElementType = inElement.Type;
                        connList.FromElementStereotype = inElement.Stereotype;

                        connList = getConnectorFeatureInfo(connList, inElement);
                    }

                    connList.To_ElementID = connList.ObjectElementId;
                    connList.To_ElementType = connList.ObjectType;
                    connList.To_ElementName = connList.ObjectName;
                    connList.To_ElementStereotype = connList.ObjectStereotype;

                    // Other end of the relationship
                    //
                    connList.OtherEndObjectElementId = connList.To_ElementID;
                    connList.OtherEndObjectName = connList.To_ElementName;
                    connList.OtherEndObjectType = connList.To_ElementType;
                    connList.OtherEndObjectStereotype = connList.To_ElementStereotype;

                    ConnectorList.Add(connList);
                }

                reader.Close();
            }

            //
            // Get Called By
            //
            if (calledBy)
            {
                var sqlcmdCalledBy = new SqlCommand();
                sqlcmdCalledBy = _eadbConnection.CreateCommand();

                sqlcmdCalledBy.CommandText = string.Format(
                    " SELECT t_connector.[Connector_ID] ConnectorId" +
                    " ,t_connector.[Name]               ConnectorName" +
                    " ,t_connector.[Direction]          ConnectorDirection" +
                    " ,t_connector.[Connector_Type]     ConnectorType" +
                    " ,t_connector.[Start_Object_ID]    ConnectorStartId" +
                    " ,t_connector.[End_Object_ID]      ConnectorEndId" +
                    " ,t_connector.[StyleEx]            ConnectorStyleEx" +
                    " ,t_object.Object_Type             ObjectType" +
                    " ,t_object.ea_guid                 ObjectGuid" +
                    " ,t_object.Stereotype              ObjectStereotype" +
                    " ,t_object.Name                    ObjectName" +
                    " ,t_object.Alias                   ObjectAlias" +
                    " ,t_object.Note                    ObjectNote" +
                    " FROM t_connector, t_object " +
                    " WHERE " +
                    sterSearch +
                    typeSearch +
                    filterToStereotypes +
                    filterConnectorType +
                    " (t_connector.[Start_Object_ID] = t_object.Object_ID AND [End_Object_ID]   = {0}) ",
                    elementID,
                    filterStereotype,
                    filterType);

                SqlDataReader readerCalledBy = sqlcmdCalledBy.ExecuteReader();

                while (readerCalledBy.Read())
                {
                    var connList = new ConnectorList();

                    connList.ObjectElementId =
                        Convert.ToInt32(readerCalledBy["ConnectorStartId"].ToString());

                    connList.ObjectGuid = readerCalledBy["ObjectGuid"].ToString();
                    connList.ConnectorId = Convert.ToInt32(readerCalledBy["ConnectorId"].ToString());
                    connList.ConnectorStartId = Convert.ToInt32(readerCalledBy["ConnectorStartId"].ToString());
                    connList.ConnectorEndId = Convert.ToInt32(readerCalledBy["ConnectorEndId"].ToString());
                    connList.ObjectStereotype = readerCalledBy["ObjectStereotype"].ToString();
                    connList.ConnectorDirection = readerCalledBy["ConnectorDirection"].ToString();
                    connList.ConnectorName = readerCalledBy["ConnectorName"].ToString();
                    connList.ConnectorStyleEx = readerCalledBy["ConnectorStyleEx"].ToString();

                    // Check specific messages for GEN
                    //
                    if (connList.ObjectStereotype.Contains("GEN"))
                    {
                        connList.ConnectorDirection = "<<----O (Called By)";
                        connList.ConnectorName = "";
                    }

                    connList.ConnectorType = readerCalledBy["ConnectorType"].ToString();
                    connList.ObjectType = readerCalledBy["ObjectType"].ToString();
                    connList.ObjectName = readerCalledBy["ObjectName"].ToString();
                    connList.ObjectAlias = readerCalledBy["ObjectAlias"].ToString();
                    connList.ObjectNote = readerCalledBy["ObjectNote"].ToString();

                    connList.UsesUsedBy = " <---o ";
                    if (connList.ConnectorType == "Realisation")
                        connList.UsesUsedBy = " is Realised by ";

                    // Export Start Element (From) and End Element (To)
                    //
                    connList.FromElementId = connList.ObjectElementId;
                    connList.FromElementType = connList.ObjectType;
                    connList.FromElementName = connList.ObjectName;

                    if (inElement != null)
                    {
                        connList.To_ElementID = inElement.ElementID;
                        connList.To_ElementType = inElement.Type;
                        connList.To_ElementName = inElement.Name;
                        connList.To_ElementStereotype = inElement.Stereotype;

                        connList = getConnectorFeatureInfo(connList, inElement);
                    }

                    // Other end of the relationship
                    //
                    connList.OtherEndObjectElementId = connList.FromElementId;
                    connList.OtherEndObjectType = connList.FromElementType;
                    connList.OtherEndObjectName = connList.FromElementName;
                    connList.OtherEndObjectStereotype = connList.FromElementStereotype;


                    ConnectorList.Add(connList);
                }

                readerCalledBy.Close();
            }

            _eadbConnection.Close();

            return ConnectorList;
        }

        // -----------------------------------------
        // Get connector feature
        // -----------------------------------------
        private ConnectorList getConnectorFeatureInfo(ConnectorList connList, Element element)
        {
            // Check for Feature connection
            //
            if (connList.ConnectorStyleEx.Contains("LFSP="))
            {
                // Connection to an element feature
                //
                string featureguid = getMetadataValue("LFSP=", connList.ConnectorStyleEx, 38);

                var stea = new stEAAttribute();
                stea = GetEaAttribute(featureguid);

                if (stea.Name != null && stea.Name != "")
                {
                    if (element.ElementID == connList.FromElementId)
                    {
                        connList.FromConnectorFeatureType = "Attribute";
                        connList.FromConnectorFeature = stea.Name;
                    }
                    else
                    {
                        connList.To_ConnectorFeatureType = "Attribute";
                        connList.To_ConnectorFeature = stea.Name;
                    }
                }
                else
                {
                    var stop = new stEAOperation();
                    stop = GetEaOperation(featureguid);

                    if (stop.Name != "")
                    {
                        if (element.ElementID == connList.FromElementId)
                        {
                            connList.FromConnectorFeatureType = "Operation";
                            connList.FromConnectorFeature = stop.Name;
                        }
                        else
                        {
                            connList.To_ConnectorFeatureType = "Operation";
                            connList.To_ConnectorFeature = stop.Name;
                        }
                    }
                }
            }

            if (connList.ConnectorStyleEx.Contains("LFEP="))
            {
                // Connection to an element feature
                //
                string featureguid = getMetadataValue("LFEP=", connList.ConnectorStyleEx, 38);

                var stea = new stEAAttribute();
                stea = GetEaAttribute(featureguid);

                if (stea.Name != null && stea.Name != "")
                {
                    if (connList.ObjectElementId == connList.To_ElementID)
                    {
                        connList.To_ConnectorFeatureType = "Attribute";
                        connList.To_ConnectorFeature = stea.Name;
                    }
                    else
                    {
                        connList.FromConnectorFeatureType = "Attribute";
                        connList.FromConnectorFeature = stea.Name;
                    }
                }
                else
                {
                    var stop = new stEAOperation();
                    stop = GetEaOperation(featureguid);

                    if (stop.Name != "" && stop.Name != "")
                    {
                        if (connList.ObjectElementId == connList.To_ElementID)
                        {
                            connList.To_ConnectorFeatureType = "Operation";
                            connList.To_ConnectorFeature = stop.Name;
                        }
                        else
                        {
                            connList.FromConnectorFeatureType = "Operation";
                            connList.FromConnectorFeature = stop.Name;
                        }
                    }
                }
            }

            return connList;
        }


        public void GetEaElements(ref List<Element> elements, ref List<DiagramObject> diagramObjects)
        {
            GetEaElements(ref elements, ref diagramObjects, false);
        }

        public void GetEaElements(ref List<Element> elements, ref List<DiagramObject> diagramObjects, bool diagramOnly)
        {
            // Get selected elements from current diagram
            Diagram diag = AddInRepository.Instance.Repository.GetCurrentDiagram();

            if (diag != null && diag.SelectedObjects.Count > 0)
            {
                foreach (DiagramObject diagobj in diag.SelectedObjects)
                {
                    if (diagobj.ObjectType.ToString() != "Package")
                    {
                        Element element =
                            AddInRepository.Instance.Repository.GetElementByID(
                                diagobj.ElementID);

                        if (element == null || element.ElementID <= 0 || element.Type == "Package")
                            break;

                        elements.Add(element);
                        diagramObjects.Add(diagobj);
                    }
                }
            }

                // Get selected elements from project browser
            else if (AddInRepository.Instance.Repository.GetTreeSelectedObject() is Element && !diagramOnly)
            {
                elements.Add((Element) AddInRepository.Instance.Repository.GetTreeSelectedObject());
                diagramObjects.Add(null);
            }
        }

        public Connector GetEaConnector()
        {
            var elements = new List<Element>();

            // Get selected elements from current diagram
            Diagram diag = AddInRepository.Instance.Repository.GetCurrentDiagram();

            if (diag != null)
            {
                return diag.SelectedConnector;
            }
            return null;
        }

        // ----------------------------------------------------------
        // Add br's to tree
        // ----------------------------------------------------------
        public void getElementsFromPackages(sqlPackage package, DataTable dt,
                                            string eltype, string connectedTo,
                                            string includeSubPackages,
                                            string ignoreDeletedRules)
        {
            //foreach (EA.Element element in package.Elements)

            foreach (sqlElement element in sqlElements(package, eltype,
                                                       ignoreDeletedRules))
            {
                var item = new brItem();
                item.name = element.Name;
                item.BRStatus = element.Status;
                // item.linkNames = "";
                // item.placeddiagrams = "";
                item.brnotes = element.Note.Trim();
                item.elementID = element.Object_ID.ToString();

                int elID = Convert.ToInt32(item.elementID);

                // get related items
                List<ConnectorList> linked = getLinkedElementsSQL(null, null, connectedTo,
                                                                  true, true, new List<string>(), null, null, elID);

                foreach (ConnectorList el in linked)
                {
                    if (item.linkNames == null)
                        item.linkNames = el.ObjectName;
                    else
                        item.linkNames = item.linkNames.Trim() + ";" + el.ObjectName;
                }


                // get related diagrams
                var linkeddiagrams = new List<DiagramStruct>();
                linkeddiagrams = getDiagramList(elID);

                foreach (DiagramStruct el in linkeddiagrams)
                {
                    if (item.placeddiagrams == null)
                        item.placeddiagrams = el.DiagramName;
                    else
                        item.placeddiagrams = item.placeddiagrams.Trim() + ";" + el.DiagramName;
                }

                DataRow elementRow = dt.NewRow();

                elementRow["packageName"] = package.Name;
                elementRow["businessRule"] = item.name;
                elementRow["BRStatus"] = item.BRStatus;
                elementRow["linkedTo"] = item.linkNames;
                elementRow["placeddiagrams"] = item.placeddiagrams;
                elementRow["brnotes"] = item.brnotes;
                elementRow["elementID"] = item.elementID;

                dt.Rows.Add(elementRow);
            }


            if (includeSubPackages == "Yes")
            {
                var listOfPackages = new ArrayList();
                listOfPackages = sqlPackages(package);

                foreach (sqlPackage pkg in listOfPackages)
                {
                    getElementsFromPackages(pkg, dt, eltype, connectedTo,
                                            includeSubPackages, ignoreDeletedRules);
                }
            }

            return;
        }

        // ----------------------------------------------------------
        // Retrieve packages from a package
        // ----------------------------------------------------------
        public ArrayList sqlPackages(sqlPackage package)
        {
            var retArray = new ArrayList();
            //
            // EA SQL database
            //
            _eadbConnection = new SqlConnection(EaRepository);
            _eadbConnection.Open();

            var sqlCommand1 = new SqlCommand();
            sqlCommand1 = _eadbConnection.CreateCommand();

            sqlCommand1.CommandText = string.Format(
                "  SELECT " +
                "       [Package_ID] " +
                "      ,[Name] " +
                "      ,[Parent_ID] " +
                "      ,[Notes] " +
                "      ,[ea_guid] " +
                "      ,[PkgOwner] " +
                "  FROM [EA_Release1].[dbo].[t_package] " +
                " WHERE Parent_ID = {0} ",
                package.Package_ID);

            SqlDataReader reader = sqlCommand1.ExecuteReader();

            while (reader.Read())
            {
                var pack = new sqlPackage();

                pack.ea_guid = reader["ea_guid"].ToString();
                pack.Name = reader["Name"].ToString();
                pack.Notes = reader["Notes"].ToString();
                pack.Package_ID = Convert.ToInt32(reader["Package_ID"].ToString());
                pack.Parent_ID = Convert.ToInt32(reader["Parent_ID"].ToString());
                pack.PkgOwner = reader["PkgOwner"].ToString();

                retArray.Add(pack);
            }

            reader.Close();

            _eadbConnection.Close();

            return retArray;
        }

     

        public Package GetPackageByName(string packageName)
        {
            int packageId = -1;

            using (var conn = new SqlConnection(EaRepository))
            {
                string sql =
                    "select package_id " +
                    "from t_package " +
                    "where Name = " + "'" + packageName + "'";

                var command = new SqlCommand(sql, conn);
                conn.Open();

                SqlDataReader reader = command.ExecuteReader();


                if (reader.Read() && !reader.IsDBNull(0))
                {
                    packageId = reader.GetInt32(0);
                }
            }

            if (packageId == -1) return null;

            Package package = AddInRepository.Instance.Repository.GetPackageByID(packageId);

            return package;
        }

        //Wayne - return the packages under a package
        public List<IDualPackage> ListPacakgeswithinpackage(IDualPackage package)
        {
            var projectList = new List<IDualPackage>();
        
            _eadbConnection = new SqlConnection(EaRepository);
            _eadbConnection.Open();

            var sqlCommand1 = new SqlCommand();
            sqlCommand1 = _eadbConnection.CreateCommand();

            sqlCommand1.Parameters.AddWithValue("@parentPackage", package.PackageID);

            sqlCommand1.CommandText = "SELECT [Package_ID] FROM [EA_Release1].[dbo].[t_package] WHERE [Parent_ID] = @parentPackage";

            SqlDataReader reader = sqlCommand1.ExecuteReader();

            while (reader.Read())
            {               
                projectList.Add(AddInRepository.Instance.Repository.GetPackageByID(reader.GetInt32(0)));            
            }

            reader.Close();
            _eadbConnection.Close();
            return projectList;
        }

        //Wayne - return elements of a certain type (passed in) for a certain stereoptype (passed in).
        public List<IDualElement> ListProjects(string projectStereotype, string objecttype)
        {

            var projectList = new List<IDualElement>();
            //
            // EA SQL database
            //
            _eadbConnection = new SqlConnection(EaRepository);
            _eadbConnection.Open();

            var sqlCommand1 = new SqlCommand();
            sqlCommand1 = _eadbConnection.CreateCommand();

            sqlCommand1.Parameters.AddWithValue("@projectStereotype", projectStereotype);
            sqlCommand1.Parameters.AddWithValue("@objectType", objecttype);

            sqlCommand1.CommandText = "SELECT [Object_ID] FROM [EA_Release1].[dbo].[t_object] WHERE Stereotype = @projectStereotype AND Object_Type = @objectType";


            SqlDataReader reader = sqlCommand1.ExecuteReader();

            while (reader.Read())
            {
                // you will need to use this!
                projectList.Add(AddInRepository.Instance.Repository.GetElementByID(reader.GetInt32(0)));
                
                //projectList.Add(reader.GetString(0));
            
            }

            reader.Close();

            _eadbConnection.Close();

            return projectList;
        }

        //Wayne - return the packages under a package
        public List<IDualElement> ListElementsWithinPackage(IDualPackage package)
        {
            var projectList = new List<IDualElement>();
            foreach (Element element in package.Elements)
            {
                projectList.Add(element);
            }


            //_eadbConnection = new SqlConnection(EaRepository);
            //_eadbConnection.Open();

            //var sqlCommand1 = new SqlCommand();
            //sqlCommand1 = _eadbConnection.CreateCommand();

            //sqlCommand1.Parameters.AddWithValue("@parentPackage", package.PackageID);

            //sqlCommand1.CommandText = "SELECT [Package_ID] FROM [EA_Release1].[dbo].[t_package] WHERE [Parent_ID] = @parentPackage";

            //SqlDataReader reader = sqlCommand1.ExecuteReader();

            //while (reader.Read())
            //{
            //    projectList.Add(AddInRepository.Instance.Repository.GetPackageByID(reader.GetInt32(0)));
            //}

            //reader.Close();
            //_eadbConnection.Close();
            return projectList;
        }

        //tbd
        public List<IDualElement> ListProjects(string projectStereotype, string objecttype, IDualPackage parentpackage)
        {

            var projectList = new List<IDualElement>();
            //
            // EA SQL database
            //
            _eadbConnection = new SqlConnection(EaRepository);
            _eadbConnection.Open();

            var sqlCommand1 = new SqlCommand();
            sqlCommand1 = _eadbConnection.CreateCommand();

            sqlCommand1.Parameters.AddWithValue("@projectStereotype", projectStereotype);
            sqlCommand1.Parameters.AddWithValue("@objectType", objecttype);
            sqlCommand1.Parameters.AddWithValue("@parentId", parentpackage.PackageID);

            sqlCommand1.CommandText = "SELECT [Object_ID] FROM [EA_Release1].[dbo].[t_object] WHERE Stereotype = @projectStereotype AND Object_Type = @objectType and ParentID = @parentId";

            SqlDataReader reader = sqlCommand1.ExecuteReader();

            while (reader.Read())
            {
                projectList.Add(AddInRepository.Instance.Repository.GetElementByID(reader.GetInt32(0)));
            }

            reader.Close();

            _eadbConnection.Close();

            return projectList;
        }

        // ----------------------------------------------------------
        // Retrieve elements from a package
        // ----------------------------------------------------------
        public ArrayList sqlElements(sqlPackage package, string elementType,
                                     string ignoreDeletedRules)
        {
            var retArray = new ArrayList();

            string eltypecondition = "";
            string deletedRules = "";

            if (elementType == null || elementType == "" || elementType == "All")
                eltypecondition = "";
            else
                eltypecondition = " AND Object_Type is like '%{1}%' ";

            deletedRules = "";
            if (ignoreDeletedRules == "Yes")
                deletedRules = " AND (Status <> 'Deleted' AND Status <> '[Category]')";


            //
            // EA SQL database
            //
            _eadbConnection = new SqlConnection(EaRepository);
            _eadbConnection.Open();

            var sqlCommand1 = new SqlCommand();
            sqlCommand1 = _eadbConnection.CreateCommand();

            sqlCommand1.CommandText = string.Format(
                "SELECT  " +
                "      [Object_ID] " +
                "      ,[Object_Type] " +
                "      ,[Name] " +
                "      ,[Alias] " +
                "      ,[Author] " +
                "      ,[Note] " +
                "      ,[Package_ID] " +
                "      ,[ea_guid] " +
                "      ,[Status] " +
                " FROM [EA_Release1].[dbo].[t_object] " +
                " WHERE " +
                "     Package_ID = {0} " +
                deletedRules +
                eltypecondition +
                " ",
                package.Package_ID,
                elementType);

            SqlDataReader reader = sqlCommand1.ExecuteReader();

            while (reader.Read())
            {
                var pack = new sqlElement();

                pack.Alias = reader["Alias"].ToString();
                pack.Author = reader["Author"].ToString();
                pack.ea_guid = reader["ea_guid"].ToString();
                pack.Name = reader["Name"].ToString();
                pack.Note = reader["Note"].ToString();
                pack.Package_ID = Convert.ToInt32(reader["Package_ID"].ToString());
                pack.Object_ID = Convert.ToInt32(reader["Object_ID"].ToString());
                pack.Object_Type = reader["Object_Type"].ToString();
                pack.Package_ID = Convert.ToInt32(reader["Package_ID"].ToString());
                pack.Status = reader["Status"].ToString();

                retArray.Add(pack);
            }

            reader.Close();

            _eadbConnection.Close();

            return retArray;
        }


        // ----------------------------------------------------------
        // Retrieve elements from a package and their link
        // ----------------------------------------------------------
        public ArrayList getElementsFromPackage(Package package)
        {
            var ret = new ArrayList();

            foreach (Element element in package.Elements)
            {
                // I am only interested in Requirements
                //
                if (element.Type != "Requirement")
                    continue;

                var item = new brItem
                               {
                                   name = element.Name,
                                   BRStatus = element.Status,
                                   linkNames = "",
                                   brnotes = element.Notes.Trim(),
                                   elementID = element.ElementID.ToString()
                               };

                // get related items
                List<ConnectorList> linked = getLinkedElementsSQL(element, null,
                                                                  "Class", true, true, new List<string>(), null, null, 0);

                foreach (ConnectorList linkeditem in linked)
                {
                    item.linkNames = item.linkNames.Trim() + ";" + linkeditem.ObjectName;
                }

                ret.Add(item);
            }
            return ret;
        }

        // ----------------------------------------------
        // Place selected elements
        // ----------------------------------------------
        public static void placeElementInDiagram(int elementID)
        {
            // Get selected element from current diagram
            Diagram diag = AddInRepository.Instance.Repository.GetCurrentDiagram();

            if (diag == null)
                return;

            if (elementID <= 0)
                return;

            //
            // Check if the element selected is already placed
            //
            bool elementAlreadyPlaced = false;
            foreach (DiagramObject diagobj in diag.DiagramObjects)
            {
                if (diagobj.ElementID == elementID)
                {
                    elementAlreadyPlaced = true;
                }
            }

            if (!elementAlreadyPlaced)
            {
                object objElement = diag.DiagramObjects.AddNew("", "");
                var diagObject = (DiagramObject) objElement;


                if (elementID > 0)
                {
                    diagObject.ElementID = elementID;
                    diagObject.Update();
                }
            }

            diag.DiagramObjects.Refresh();
            diag.Update();
            AddInRepository.Instance.Repository.SaveDiagram(diag.DiagramID);
            AddInRepository.Instance.Repository.ReloadDiagram(diag.DiagramID);
        }

        // ----------------------------------------------
        //  Get selected element
        // ----------------------------------------------
        public static Element getSelectedElement()
        {
            // Get selected element from current diagram
            Diagram diag = AddInRepository.Instance.Repository.GetCurrentDiagram();

            DiagramObject objectSelected = null;
            foreach (DiagramObject diagobj in diag.SelectedObjects)
            {
                objectSelected = diagobj;
                break;
            }

            Element elementSelect = null;

            if (objectSelected != null)
            {
                try
                {
                    elementSelect =
                        AddInRepository.Instance.Repository.GetElementByID(
                            objectSelected.ElementID);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                try
                {
                    elementSelect =
                        (Element) AddInRepository.Instance.Repository.GetTreeSelectedObject();
                }
                catch (Exception)
                {
                    return null;
                }
            }


            if (elementSelect == null || elementSelect.ElementID <= 0)
                return null;

            return elementSelect;
        }


        // ----------------------------------------------------------
        // Retrieve a list of diagrams where the element is placed
        // ----------------------------------------------------------
        public List<DiagramStruct> getDiagramList(int objectID)
        {
            var diagramList = new List<DiagramStruct>();
            //
            // EA SQL database
            //
            _eadbConnection = new SqlConnection(EaRepository);
            _eadbConnection.Open();

            var sqlCommand1 = new SqlCommand();
            sqlCommand1 = _eadbConnection.CreateCommand();

            sqlCommand1.CommandText = string.Format(
                " SELECT " +
                "  diagobj.Diagram_ID     Diagram_ID" +
                " ,diagobj.Object_ID      Object_ID" +
                " ,diag.Package_ID        Package_ID" +
                " ,diag.ParentID          ParentID" +
                " ,diag.Name              DiagramName" +
                " ,diag.Diagram_Type      DiagramType" +
                " ,diag.ea_guid           DiagramEA_GUID" +
                " FROM " +
                " t_diagramobjects diagobj, " +
                " t_diagram        diag " +
                " WHERE " +
                "       (diagobj.Diagram_ID = diag.Diagram_ID ) " +
                "   AND (diagobj.Object_ID     = {0} ) ",
                objectID
                );

            SqlDataReader reader = sqlCommand1.ExecuteReader();

            while (reader.Read())
            {
                var diagram = new DiagramStruct
                                  {
                                      Diagram_ID = Convert.ToInt32(reader["Diagram_ID"]),
                                      Object_ID = Convert.ToInt32(reader["Object_ID"]),
                                      Package_ID = Convert.ToInt32(reader["Package_ID"]),
                                      ParentID = Convert.ToInt32(reader["ParentID"]),
                                      DiagramName = reader["DiagramName"].ToString(),
                                      DiagramType = reader["DiagramType"].ToString(),
                                      DiagramEA_GUID = reader["DiagramEA_GUID"].ToString()
                                  };

                diagramList.Add(diagram);
            }

            reader.Close();

            _eadbConnection.Close();

            return diagramList;
        }

        // ----------------------------------------------------------
        //       Get element by tagged value
        // ----------------------------------------------------------
        public sElementTag getElementByTaggedValue(string tag, string value, string[] statusesToIgnore)
        {
            //
            // EA SQL database
            //
            _eadbConnection = new SqlConnection(EaRepository);
            _eadbConnection.Open();

            // Get object by tagged value
            //

            string statusWhereClause = string.Empty;

            if (statusesToIgnore != null && statusesToIgnore.Length > 0)
            {
                statusWhereClause = " AND obj.status not in ('" + statusesToIgnore[0] + "'";
                for (int i = 0; i < statusesToIgnore.Length; i++)
                {
                    statusWhereClause += ", '" + statusesToIgnore[i] + "'";
                }

                statusWhereClause += ")";
            }
            var ret = new sElementTag();

            var sqlCommand1 = new SqlCommand();
            sqlCommand1 = _eadbConnection.CreateCommand();

            sqlCommand1.CommandText = string.Format(
                "SELECT " +
                "   obj.ea_guid elemguid" +
                "  ,obj.Object_ID " +
                "  ,obj.Object_Type " +
                "  ,obj.Diagram_ID " +
                "  ,obj.Name " +
                "  ,obj.Package_ID " +
                "  ,obj.Stereotype " +
                "  ,obj.Status " +
                "  ,obj.Author " +
                "  ,obj.ParentID " +
                "  ,tag.PropertyID " +
                "  ,tag.Value " +
                "  ,tag.Notes " +
                "  ,tag.ea_guid tagguid " +
                " FROM t_object obj, " +
                "     t_objectproperties tag" +
                " WHERE " +
                "      tag.[Object_ID] = obj.[Object_ID] " +
                " AND  tag.[Property]  = '{0}' " +
                " AND  tag.[Value]     = '{1}' " +
                statusWhereClause
                , tag
                , value.Trim());

            SqlDataReader reader = null;

            try
            {
                reader = sqlCommand1.ExecuteReader();

                if (reader.Read())
                {
                    ret.elemguid = reader["elemguid"].ToString();
                    ret.Object_ID = reader["Object_ID"].ToString();
                    ret.Object_Type = reader["Object_Type"].ToString();
                    ret.Diagram_ID = Convert.ToInt32(reader["Diagram_ID"]);
                    ret.Name = reader["Name"].ToString();
                    ret.Package_ID = Convert.ToInt32(reader["Package_ID"]);
                    ret.Stereotype = reader["Stereotype"].ToString();
                    ret.Status = reader["Status"].ToString();
                    ret.Author = reader["Author"].ToString();
                    ret.PropertyID = Convert.ToInt32(reader["PropertyID"]);
                    ret.Value = reader["Value"].ToString();
                    ret.Notes = reader["Notes"].ToString();
                    ret.tagguid = reader["tagguid"].ToString();
                    ret.ParentID = Convert.ToInt32(reader["ParentID"]);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                ret.Name = "Error retrieving element. " + ex;
            }
            _eadbConnection.Close();

            return ret;
        }

        // ----------------------------------------------------------
        //       Get element by tagged value
        // ----------------------------------------------------------
        public sElementTag getElementByStereotype(string stereotype, string name)
        {
            //
            // EA SQL database
            //
            _eadbConnection = new SqlConnection(EaRepository);
            _eadbConnection.Open();

            var ret = new sElementTag();

            var sqlCommand1 = new SqlCommand();
            sqlCommand1 = _eadbConnection.CreateCommand();

            sqlCommand1.CommandText = string.Format(
                "SELECT " +
                "   obj.ea_guid elemguid" +
                "  ,obj.Object_ID " +
                "  ,obj.Object_Type " +
                "  ,obj.Diagram_ID " +
                "  ,obj.Name " +
                "  ,obj.Package_ID " +
                "  ,obj.Stereotype " +
                "  ,obj.Status " +
                "  ,obj.Author " +
                "  ,obj.ParentID " +
                "  ,tag.PropertyID " +
                "  ,tag.Value " +
                "  ,tag.Notes " +
                "  ,tag.ea_guid tagguid " +
                " FROM t_object obj" +
                " WHERE obj.Name = {0}" +
                " AND  obj.Stereotype like '{1}' ", name, stereotype);

            SqlDataReader reader = null;

            try
            {
                reader = sqlCommand1.ExecuteReader();

                if (reader.Read())
                {
                    ret.elemguid = reader["elemguid"].ToString();
                    ret.Object_ID = reader["Object_ID"].ToString();
                    ret.Object_Type = reader["Object_Type"].ToString();
                    ret.Diagram_ID = Convert.ToInt32(reader["Diagram_ID"]);
                    ret.Name = reader["Name"].ToString();
                    ret.Package_ID = Convert.ToInt32(reader["Package_ID"]);
                    ret.Stereotype = reader["Stereotype"].ToString();
                    ret.Status = reader["Status"].ToString();
                    ret.Author = reader["Author"].ToString();
                    ret.PropertyID = Convert.ToInt32(reader["PropertyID"]);
                    ret.Value = reader["Value"].ToString();
                    ret.Notes = reader["Notes"].ToString();
                    ret.tagguid = reader["tagguid"].ToString();
                    ret.ParentID = Convert.ToInt32(reader["ParentID"]);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                ret.Name = "Error retrieving element. " + ex;
            }
            _eadbConnection.Close();

            return ret;
        }

        // ----------------------------------------------------------
        //       Retrieve tagged value for an element
        // ----------------------------------------------------------
        public string getTaggedValue(int elementID, string tag)
        {
            //
            // EA SQL database
            //
            _eadbConnection = new SqlConnection(EaRepository);
            _eadbConnection.Open();

            // Get object by tagged value
            //
            string ret = "";

            var sqlCommand1 = new SqlCommand();
            sqlCommand1 = _eadbConnection.CreateCommand();

            sqlCommand1.CommandText = string.Format(
                "SELECT " +
                "   obj.ea_guid elemguid" +
                "  ,obj.Object_ID " +
                "  ,obj.Object_Type " +
                "  ,obj.Diagram_ID " +
                "  ,obj.Name " +
                "  ,obj.Package_ID " +
                "  ,obj.Stereotype " +
                "  ,obj.Status " +
                "  ,obj.Author " +
                "  ,obj.ParentID " +
                "  ,tag.PropertyID " +
                "  ,tag.Value " +
                "  ,tag.Notes " +
                "  ,tag.ea_guid tagguid " +
                " FROM t_object obj, " +
                "     t_objectproperties tag" +
                " WHERE " +
                "      tag.[Object_ID] = obj.[Object_ID] " +
                " AND  tag.[Object_ID] = '{0}' " +
                " AND  tag.[Property]  = '{1}' "
                , elementID
                , tag);

            SqlDataReader reader = null;

            try
            {
                reader = sqlCommand1.ExecuteReader();

                if (reader.Read())
                {
                    ret = reader["Value"].ToString();
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                ret = "Error retrieving element. " + ex;
            }
            _eadbConnection.Close();

            return ret;
        }


        // ----------------------------------------------------------
        //       Retrieve Element Classifier
        // ----------------------------------------------------------
        public int GetClassifier(Element element)
        {
            //
            // EA SQL database
            //
            _eadbConnection = new SqlConnection(EaRepository);
            _eadbConnection.Open();

            // Get object by tagged value
            //
            int ret = 0;

            var sqlCommand1 = new SqlCommand();
            sqlCommand1 = _eadbConnection.CreateCommand();

            sqlCommand1.CommandText = string.Format(
                "SELECT " +
                "   Object_ID " +
                "  ,Object_Type " +
                "  ,Classifier " +
                " FROM t_object " +
                " WHERE " +
                "       Object_ID = '{0}' "
                , element.ElementID
                );

            SqlDataReader reader = null;

            try
            {
                reader = sqlCommand1.ExecuteReader();

                if (reader.Read())
                {
                    ret = Convert.ToInt32(reader["Classifier"].ToString());
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                ret = 0;
            }
            _eadbConnection.Close();

            return ret;
        }


        // ----------------------------------------------------------
        //       Retrieve Element Classifier
        // ----------------------------------------------------------
        public List<sEAElement> ListElementInstances(Element element)
        {
            var ret = new List<sEAElement>();

            //
            // EA SQL database
            //
            _eadbConnection = new SqlConnection(EaRepository);
            _eadbConnection.Open();

            var sqlCommand1 = new SqlCommand();
            sqlCommand1 = _eadbConnection.CreateCommand();

            sqlCommand1.CommandText = string.Format(
                "SELECT " +
                "   Object_ID " +
                "  ,Object_Type " +
                "  ,Classifier " +
                "  ,Name " +
                "  ,Package_ID " +
                "  ,Stereotype " +
                "  ,Status " +
                "  ,Author " +
                "  ,ParentID " +
                " FROM t_object " +
                " WHERE " +
                "       Classifier = '{0}' "
                , element.ElementID
                );

            SqlDataReader reader = null;

            try
            {
                reader = sqlCommand1.ExecuteReader();

                while (reader.Read())
                {
                    var classifier = new sEAElement();

                    classifier.EA_GUID = reader["Object_ID"].ToString();
                    classifier.ElementID = reader["Object_ID"].ToString();
                    classifier.Name = reader["Name"].ToString();
                    classifier.Stereotype = reader["Stereotype"].ToString();
                    classifier.Status = reader["Status"].ToString();

                    ret.Add(classifier);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
            }
            _eadbConnection.Close();

            return ret;
        }


        // ----------------------------------------------------------
        //       Show the last id of a tagged value
        // ----------------------------------------------------------
        public sElementTag GetLastTaggedValueByStereotype(string tag, string stereotype)
        {
            //
            // EA SQL database
            //
            _eadbConnection = new SqlConnection(EaRepository);
            _eadbConnection.Open();

            // Get object by tagged value
            //

            var ret = new sElementTag();

            var sqlCommand1 = new SqlCommand();
            sqlCommand1 = _eadbConnection.CreateCommand();

            sqlCommand1.CommandText = string.Format(
                "SELECT " +
                "   obj.ea_guid elemguid" +
                "  ,obj.Object_ID " +
                "  ,obj.Object_Type " +
                "  ,obj.Diagram_ID " +
                "  ,obj.Name " +
                "  ,obj.Package_ID " +
                "  ,obj.Stereotype " +
                "  ,obj.Status " +
                "  ,obj.Author " +
                "  ,obj.ParentID " +
                "  ,tag.PropertyID " +
                "  ,tag.Value " +
                "  ,tag.Notes " +
                "  ,tag.ea_guid tagguid " +
                " FROM t_object obj, " +
                "     t_objectproperties tag" +
                " WHERE " +
                "      tag.[Object_ID] = obj.[Object_ID] " +
                " AND  tag.[Property]  = '{0}' " +
                " AND  obj.Stereotype  = '{1}' " +
                " ORDER BY tag.Value DESC "
                , tag
                , stereotype
                );


            /*
             *                 " WHERE " +
                "      tag.[Object_ID] = obj.[Object_ID] " +
                " AND  tag.[Property]  = '{0}' " +
                " AND  tag.[Value]     = '{1}' " +
            */

            SqlDataReader reader = null;

            try
            {
                reader = sqlCommand1.ExecuteReader();

                while (reader.Read())
                {
                    ret.elemguid = reader["elemguid"].ToString();
                    ret.Object_ID = reader["Object_ID"].ToString();
                    ret.Object_Type = reader["Object_Type"].ToString();
                    ret.Diagram_ID = Convert.ToInt32(reader["Diagram_ID"]);
                    ret.Name = reader["Name"].ToString();
                    ret.Package_ID = Convert.ToInt32(reader["Package_ID"]);
                    ret.Stereotype = reader["Stereotype"].ToString();
                    ret.Status = reader["Status"].ToString();
                    ret.Author = reader["Author"].ToString();
                    ret.PropertyID = Convert.ToInt32(reader["PropertyID"]);
                    ret.Value = reader["Value"].ToString();
                    ret.Notes = reader["Notes"].ToString();
                    ret.tagguid = reader["tagguid"].ToString();
                    ret.ParentID = Convert.ToInt32(reader["ParentID"]);
                    break;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                ret.Name = "Error retrieving element. " + ex;
            }
            _eadbConnection.Close();

            return ret;
        }


        // ----------------------------------------------
        //        Add tagged value
        // ----------------------------------------------
        public static bool addTaggedValue(Element EAElement,
                                          string tagString,
                                          string value
            )
        {
            bool ret = true;

            // Element is mandatory
            if (EAElement == null)
                return false;


            var ea = new EadbUpdate();
            var sqlTagValue = new EadbUpdate.SqlTaggedValue();

            sqlTagValue.XElementId = EAElement.ElementID;
            sqlTagValue.XProperty = tagString;
            sqlTagValue.XValue = value;

            ea.SqlSaveTaggedValue(sqlTagValue);

            return ret;
        }


        // ----------------------------------------------
        //        Add tagged value (Old)
        // ----------------------------------------------
        public static bool addTaggedValueOLD(Element EAElement,
                                             string tagString,
                                             string value
            )
        {
            bool ret = true;

            // Element is mandatory
            if (EAElement == null)
                return false;

            // Add tagged value if necessary
            //
            object tagVal = EAElement.TaggedValues.GetByName(tagString);

            if (tagVal == null)
            {
                // tag not found
                //
                TaggedValue tagToAdd;
                object tagLoadModule =
                    EAElement.TaggedValues.AddNew(tagString, value);
                tagToAdd = (TaggedValue) tagLoadModule;
                tagToAdd.Update();
                EAElement.TaggedValues.Refresh();
            }
            else
            {
                var tv = (TaggedValue) tagVal;

                tv.Value = value;
                tv.Update();
                EAElement.TaggedValues.Refresh();
            }

            return ret;
        }


        // ----------------------------------------------
        //        Delete tagged value
        // ----------------------------------------------
        public static bool deleteTaggedValue(Element EAElement,
                                             string tagString)
        {
            bool ret = true;

            // Delete tagged value if necessary
            //
            for (short i = 0; i < EAElement.TaggedValues.Count; i++)
            {
                object tagobject = EAElement.TaggedValues.GetAt(i);
                var tv = (TaggedValue) tagobject;

                if (tv.Name == tagString)
                {
                    EAElement.TaggedValues.DeleteAt(i, true);
                    break;
                }
            }

            return ret;
        }

        // ----------------------------------------------------------
        //       Fix tagged value
        // ----------------------------------------------------------
        public void fixLoadModuleTaggedValue()
        {
            // EA SQL database
            //
            _eadbConnection = new SqlConnection(EaRepository);
            _eadbConnection.Open();

            //
            // Mapping table database
            //
            _mappingConnection = new SqlConnection(_mappingTable);
            _mappingConnection.Open();

            // Retrieve all load modules
            //
            var cabList = new mtCABMappingList();
            cabList.getCabList(_mappingConnection, "");

            foreach (DataRow cab in cabList.cabMapList.Rows)
            {
                string EA_GUID = cab["EA_GUID"].ToString();
                string cablm = cab["CAB"].ToString();

                // Retrieve EA Element
                //
                Element EAElement = AddInRepository.Instance.Repository.GetElementByGuid(EA_GUID);

                // Add tagged value if necessary
                //
                if (EAElement != null)
                {
                    addTaggedValue(EAElement, "Load Module", cablm);
                }
            }
            _mappingConnection.Close();
            _eadbConnection.Close();
        }


        // ----------------------------------------------------------
        //       Fix tagged value
        // ----------------------------------------------------------
        public void fixTableTaggedValue()
        {
            // EA SQL database
            //
            _eadbConnection = new SqlConnection(EaRepository);
            _eadbConnection.Open();

            //
            // Mapping table database
            //
            _mappingConnection = new SqlConnection(_mappingTable);
            _mappingConnection.Open();

            // Retrieve all load modules
            //
            var tableList = new mtTableMappingList();
            tableList.getTableList(_mappingConnection);

            foreach (DataRow table in tableList.tableMapping.Rows)
            {
                string EA_GUID = table["EA_GUID"].ToString();
                string tableName = table["tableName"].ToString();
                string alternateName = table["alternateName"].ToString();

                // Retrieve EA Element
                //
                Element EAElement = AddInRepository.Instance.Repository.GetElementByGuid(EA_GUID);

                // Add tagged value if necessary
                //
                if (EAElement != null)
                {
                    addTaggedValue(EAElement, "GenLogicalName", alternateName);
                    addTaggedValue(EAElement, "DB2PhysicalName", tableName);
                }
            }
            _mappingConnection.Close();
            _eadbConnection.Close();
        }

        // ---------------------------------------------------------------
        //              Promote a class to another class
        // - It replicates the relationships from the source classs
        //   in the destination
        // ---------------------------------------------------------------
        public List<Message> promoteClass(Element source,
                                          Element destination,
                                          bool replaceInDiagr,
                                          bool addAttributes,
                                          bool addMethods,
                                          bool duplicateConnectors,
                                          bool verifyOnly)
        {
            var messages = new List<Message>();

            if (source.Type != destination.Type)
            {
                messages.Add(
                    new ErrorMessage("Elements must be of the same type."));

                return messages;
            }

            if (source.Type != "Class" && source.Type != "UseCase")
            {
                messages.Add(
                    new ErrorMessage("Element not supported yet."));

                return messages;
            }

            if (source.Stereotype == "table")
            {
                string user2 = AddInRepository.Instance.Repository.GetCurrentLoginUser(false);

                if (! UserAccess(user2, "DBA"))
                {
                    messages.Add(
                        new ErrorMessage("User must be a DBA."));

                    return messages;
                }
            }

            // Add attributes
            if (addAttributes)
            {
                messages.AddRange(copyAttributes(source, destination, verifyOnly));
            }

            // Add methods
            if (addMethods)
            {
                messages.AddRange(copyMethods(source, destination, verifyOnly));
            }

            Package currentPackage = AddInRepository.Instance.Repository.GetTreeSelectedPackage();

            if (!verifyOnly)
            {
                currentPackage.Elements.Refresh();
                currentPackage.Diagrams.Refresh();
                currentPackage.Packages.Refresh();
                currentPackage.Connectors.Refresh();
            }

            if (duplicateConnectors)
            {
                #region Duplicate connectors

                // Duplicate connectors/ associations
                //
                foreach (Connector sourceCon in source.Connectors)
                {
                    int SupplierElementId = 0;
                    int ClientElementId = 0;

                    if (source.ElementID == sourceCon.SupplierID)
                    {
                        SupplierElementId = destination.ElementID;
                        ClientElementId = sourceCon.ClientID;
                    }

                    if (source.ElementID == sourceCon.ClientID)
                    {
                        ClientElementId = destination.ElementID;
                        SupplierElementId = sourceCon.SupplierID;
                    }

                    if (!verifyOnly)
                    {
                        var destCon = (Connector)
                                      destination.Connectors.AddNew(sourceCon.Name, sourceCon.Type);


                        destCon.SupplierID = SupplierElementId;
                        destCon.ClientID = ClientElementId;


                        destCon.Alias = sourceCon.Alias;
                        destCon.Name = sourceCon.Name;
                        destCon.Notes = sourceCon.Notes;
                        destCon.Direction = sourceCon.Direction;
                        destCon.Stereotype = sourceCon.Stereotype;
                        destCon.Type = sourceCon.Type;
                        destCon.SequenceNo = sourceCon.SequenceNo;
                        destCon.Update();
                        destination.Update();
                        destination.Refresh();
                    }
                    messages.Add(
                        new InformationMessage("Connector from '"
                                               + new EaAccess().GetElementNameByObjectID(ClientElementId)
                                               + "' to '"
                                               + new EaAccess().GetElementNameByObjectID(SupplierElementId) + "'"
                                               + (verifyOnly ? " will be created" : " created")));
                }
                #endregion Duplicate connectors

                //#region Duplicate realisations


                //foreach (EA.Realization sourceCon in source.Realizes)
                //{
                //    int SupplierElementId = 0;
                //    int ClientElementId = 0;

                //    if (source.ElementID == sourceCon.SupplierID)
                //    {
                //        SupplierElementId = destination.ElementID;
                //        ClientElementId = sourceCon.ClientID;
                //    }

                //    if (source.ElementID == sourceCon.ClientID)
                //    {
                //        ClientElementId = destination.ElementID;
                //        SupplierElementId = sourceCon.SupplierID;
                //    }

                //    if (!verifyOnly)
                //    {
                //        var destCon = (Connector)
                //                      destination.Realizes.AddNew(sourceCon.Name, sourceCon.Type);


                //        destCon.SupplierID = SupplierElementId;
                //        destCon.ClientID = ClientElementId;


                //        destCon.Alias = sourceCon.Alias;
                //        destCon.Name = sourceCon.Name;
                //        destCon.Notes = sourceCon.Notes;
                //        destCon.Direction = sourceCon.Direction;
                //        destCon.Stereotype = sourceCon.Stereotype;
                //        destCon.Type = sourceCon.Type;
                //        destCon.SequenceNo = sourceCon.SequenceNo;
                //        destCon.Update();
                //        destination.Update();
                //        destination.Refresh();
                //    }
                //    messages.Add(
                //        new InformationMessage("Realisation from '"
                //                               + new EaAccess().GetElementNameByObjectID(ClientElementId)
                //                               + "' to '"
                //                               + new EaAccess().GetElementNameByObjectID(SupplierElementId) + "'"
                //                               + (verifyOnly ? " will be created" : " created")));
                //}
                //#endregion Duplicate realisations

                #region Replace in Diagrams

                if (replaceInDiagr)
                {
                    // Replace in diagrams

                    var eac = new EaAccess();
                    List<DiagramStruct> objDiagList = eac.getDiagramList(source.ElementID);

                    foreach (DiagramStruct objDiag in objDiagList)
                    {
                        if (objDiag.DiagramType == "Sequence")
                        {
                            var warning =
                                new WarningMessage(objDiag.DiagramName +
                                                   " is a sequence diagram and must be manually updated.");
                            ;
                            messages.Add(warning);

                            continue;
                        }

                        DiagramStruct ds = objDiag;
                        Diagram diagram = AddInRepository.Instance.Repository.GetDiagramByID(ds.Diagram_ID);

                        int top = 1;
                        int left = 1;
                        int right = 1;
                        int bottom = 1;

                        // Add new element to diagram
                        //

                        #region add new element to diagram

                        bool elementAlreadyPlaced = false;
                        foreach (DiagramObject diagobject in diagram.DiagramObjects)
                        {
                            if (diagobject.ElementID == destination.ElementID)
                            {
                                elementAlreadyPlaced = true;
                            }

                            // Get coordinates
                            if (diagobject.ElementID == source.ElementID)
                            {
                                top = diagobject.top;
                                left = diagobject.left;
                                right = diagobject.right;
                                bottom = diagobject.bottom;
                            }
                        }

                        if (!elementAlreadyPlaced)
                        {
                            string message = "'" + destination.Name + "'"
                                             + (verifyOnly ? " will be" : string.Empty)
                                             + " added to diagram '"
                                             + diagram.Name + "'";
                            string messageType = MessageType.Information;

                            if (!verifyOnly)
                            {
                                object newObject = diagram.DiagramObjects.AddNew("", "");
                                var newDiagObject = (DiagramObject) newObject;

                                if (destination.ElementID > 0)
                                {
                                    newDiagObject.ElementID = destination.ElementID;
                                    newDiagObject.top = top;
                                    newDiagObject.left = left;
                                    newDiagObject.right = right;
                                    newDiagObject.bottom = bottom;

                                    try
                                    {
                                        newDiagObject.Update();
                                    }
                                    catch (Exception ex)
                                    {
                                        message = "Error: " + newDiagObject.GetLastError() + " " +
                                                  ex;
                                        messageType = MessageType.Error;
                                    }
                                }
                                diagram.DiagramObjects.Refresh();
                            }

                            if (messageType == MessageType.Information)
                            {
                                messages.Add(new InformationMessage(message));
                            }
                            else
                            {
                                messages.Add(new ErrorMessage(message));
                            }
                        }

                        #endregion add new element to diagram

                        // Delete Element from diagram
                        //

                        #region delete element from diagram

                        for (short i = 0; i < diagram.DiagramObjects.Count; i++)
                        {
                            var oldDiagObject = (DiagramObject) diagram.DiagramObjects.GetAt(i);

                            if (oldDiagObject.ElementID == source.ElementID)
                            {
                                messages.Add(
                                    new InformationMessage("'" + source.Name + "'"
                                                           + (verifyOnly ? " will be" : string.Empty)
                                                           + " removed from diagram '"
                                                           + diagram.Name + "'"));

                                if (!verifyOnly)
                                {
                                    diagram.DiagramObjects.DeleteAt(i, false);
                                }
                                break;
                            }
                        }
                        if (!verifyOnly)
                        {
                            destination.Update();
                            destination.Refresh();
                            diagram.DiagramObjects.Refresh();
                        }

                        #endregion delete element from diagram
                    }
                }

                #endregion Replace in Diagrams
            }


            if (!verifyOnly)
            {
                currentPackage.Elements.Refresh();
                currentPackage.Diagrams.Refresh();
                currentPackage.Packages.Refresh();
                currentPackage.Connectors.Refresh();

                AddInRepository.Instance.Repository.RefreshOpenDiagrams(true);
            }

            return messages;
        }


        // ---------------------------------------------------------------
        //              Promote a class to another class
        // - It replicates the relationships from the source classs
        //   in the destination
        // ---------------------------------------------------------------
        public List<Message> MergeTable(Element source, Element destination)
        {
            return null;

            var messages = new List<Message>();

            if (source.Type != destination.Type)
            {
                messages.Add(
                    new ErrorMessage("Elements must be of the same type."));

                return messages;
            }

            if (source.Stereotype != "table")
            {
                messages.Add(
                    new ErrorMessage("Only tables can be selected."));

                return messages;
            }

            Package currentPackage = AddInRepository.Instance.Repository.GetTreeSelectedPackage();

            currentPackage.Elements.Refresh();
            currentPackage.Diagrams.Refresh();
            currentPackage.Packages.Refresh();
            currentPackage.Connectors.Refresh();


            // Duplicate connectors/ associations
            //
            foreach (Connector sourceCon in source.Connectors)
            {
                int SupplierElementId = 0;
                int ClientElementId = 0;

                if (source.ElementID == sourceCon.SupplierID)
                {
                    SupplierElementId = destination.ElementID;
                    ClientElementId = sourceCon.ClientID;
                }

                if (source.ElementID == sourceCon.ClientID)
                {
                    ClientElementId = destination.ElementID;
                    SupplierElementId = sourceCon.SupplierID;
                }

                // Retrieve other side of the connector
                // If it is a table, don't add the connector

                Element otherEnd = AddInRepository.Instance.Repository.GetElementByID(SupplierElementId);
                if (otherEnd.Type == "table")
                    continue;


                // Find destination element associations
                //
                // For each dest.associations if the end
                //
                // << work in progress >>


                var destCon = (Connector)
                              destination.Connectors.AddNew(sourceCon.Name, sourceCon.Type);

                destCon.SupplierID = SupplierElementId;
                destCon.ClientID = ClientElementId;

                destCon.Alias = sourceCon.Alias;
                destCon.Name = sourceCon.Name;
                destCon.Notes = sourceCon.Notes;
                destCon.Direction = sourceCon.Direction;
                destCon.Stereotype = sourceCon.Stereotype;
                destCon.Type = sourceCon.Type;
                destCon.SequenceNo = sourceCon.SequenceNo;
                destCon.Update();
                destination.Update();
                destination.Refresh();

                messages.Add(
                    new InformationMessage("Connector from '"
                                           + new EaAccess().GetElementNameByObjectID(ClientElementId)
                                           + "' to '"
                                           + new EaAccess().GetElementNameByObjectID(SupplierElementId) + "'"
                                           + " created"));
            }

            currentPackage.Elements.Refresh();
            currentPackage.Diagrams.Refresh();
            currentPackage.Packages.Refresh();
            currentPackage.Connectors.Refresh();

            AddInRepository.Instance.Repository.RefreshOpenDiagrams(true);

            return messages;
        }


        // ---------------------------------------------------------------
        //              Apply template to new element
        // ---------------------------------------------------------------
        public string applyTemplate(Element element)
        {
            string ret = "Ok";
            string tempGUID = "{A4A6D605-1208-4354-A668-890FD93EC952}";

            // look for template in EA
            Package templatePackage = AddInRepository.Instance.Repository.GetPackageByGuid(tempGUID);
            Element templateElement = null;

            if (templatePackage == null)
            {
                ret = "Package not found.";
                return ret;
            }

            // Retrieve template
            foreach (Element currentElement in templatePackage.Elements)
            {
                if (element.Stereotype == currentElement.Stereotype &&
                    element.Type == currentElement.Type)
                {
                    templateElement = currentElement;
                    break;
                }
            }


            if (templateElement == null)
            {
                return ret;
            }

            // Replicate Tags
            copyTags(templateElement, element);

            // Replicate Attributes
            copyAttributes(templateElement, element, false);

            // Replicate Methods
            copyMethods(templateElement, element, false);

            Package currentPackage = AddInRepository.Instance.Repository.GetTreeSelectedPackage();
            currentPackage.Elements.Refresh();
            currentPackage.Diagrams.Refresh();
            currentPackage.Packages.Refresh();
            currentPackage.Connectors.Refresh();

            AddInRepository.Instance.Repository.RefreshOpenDiagrams(true);

            return ret;
        }

        // ---------------------------------------------------------------
        //              Copy methods
        // ---------------------------------------------------------------
        public List<Message> copyMethods(Element source,
                                         Element destination,
                                         bool verifyOnly)
        {
            var messages = new List<Message>();
            // Duplicate methods
            //
            foreach (Method fromMethod in source.Methods)
            {
                // Check if method exists
                //
                bool methodAlreadyCreated = false;

                foreach (Method met in destination.Methods)
                {
                    if (met.Name == fromMethod.Name)
                    {
                        methodAlreadyCreated = true;
                    }
                }

                if (!methodAlreadyCreated)
                {
                    if (!verifyOnly)
                    {
                        var toMethod =
                            (Method) destination.Methods.AddNew(
                                         fromMethod.Name, fromMethod.ReturnType);

                        toMethod.Abstract = fromMethod.Abstract;
                        toMethod.IsStatic = fromMethod.IsStatic;
                        toMethod.Notes = fromMethod.Notes;
                        toMethod.Stereotype = fromMethod.Stereotype;
                        toMethod.Visibility = fromMethod.Visibility;
                        toMethod.Update();
                        destination.Update();
                        destination.Refresh();
                    }
                    messages.Add(
                        new InformationMessage("Operation '" + fromMethod.Name + "'" +
                                               (verifyOnly ? " will be created" : " created")));
                }
            }
            return messages;
        }

        // ---------------------------------------------------------------
        //              Apply template to new element
        // ---------------------------------------------------------------
        public List<Message> copyAttributes(Element source, Element destination, bool verifyOnly)
        {
            var Messages = new List<Message>();
            var attributes = new SortedList<int, Attribute>();
            //
            // Duplicate attributes
            //
            foreach (Attribute fromAttribute in source.Attributes)
            {
                bool exists = false;

                foreach (Attribute atr in destination.Attributes)
                {
                    if (atr.Name == fromAttribute.Name)
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    attributes.Add(fromAttribute.Pos, fromAttribute);
                }
            }
            int pos = 0;

            foreach (var attributePair in attributes)
            {
                Attribute fromAttribute = attributePair.Value;

                if (!verifyOnly)
                {
                    var toAttribute =
                        (Attribute) destination.Attributes.AddNew(
                                        fromAttribute.Name, fromAttribute.Type);

                    toAttribute.AllowDuplicates = fromAttribute.AllowDuplicates;
                    toAttribute.Length = fromAttribute.Length;
                    toAttribute.Notes = fromAttribute.Notes;
                    toAttribute.Type = fromAttribute.Type;
                    toAttribute.IsConst = fromAttribute.IsConst;
                    toAttribute.IsDerived = fromAttribute.IsDerived;
                    toAttribute.Scale = fromAttribute.Scale;
                    toAttribute.Containment = fromAttribute.Containment;
                    toAttribute.Stereotype = fromAttribute.Stereotype;
                    toAttribute.Visibility = fromAttribute.Visibility;
                    toAttribute.Pos = pos++;

                    toAttribute.Update();
                    destination.Update();
                    destination.Refresh();
                }
                Messages.Add(
                    new InformationMessage("Attribute '" + fromAttribute.Name + "' of type '" + fromAttribute.Type +
                                           "'" +
                                           (verifyOnly ? " will be created" : " created")));
            }
            return Messages;
        }

        // ---------------------------------------------------------------
        //              Copy Tags
        // ---------------------------------------------------------------
        public void copyTags(Element source,
                             Element destination)
        {
            //
            // copy tagged value
            //
            foreach (TaggedValue fromTag in source.TaggedValues)
            {
                // Check if attribute exists
                //
                var tagInDestination =
                    (TaggedValue) destination.TaggedValues.GetByName(fromTag.Name);

                if (tagInDestination == null)
                {
                    addTaggedValue(destination, fromTag.Name, fromTag.Value);

                    destination.Update();
                    destination.Refresh();
                }
            }
            return;
        }

        // ---------------------------------------------------------------
        //              Just after a new element is added
        // ---------------------------------------------------------------

        public bool postNewElement(Element newElement)
        {
            string retStr = applyTemplate(newElement);
            bool ret = false;

            if (retStr == "Ok")
                ret = true;

            return ret;
        }

        // >>>>>>>>>>>>>>>>>>>>>>>>>> <<<<<<<<<<<<<<<<<<<<<<<<<<<<< //
        //                      STRUCTURES                          //
        // >>>>>>>>>>>>>>>>>>>>>>>>>> <<<<<<<<<<<<<<<<<<<<<<<<<<<<< //

        internal int RemoveAttributeMultipleStereotypes(int elementId)
        {
            int rows = 0;

            using (var conn = new SqlConnection(EaRepository))
            {
                string sql =
                    "delete from t_xref " + Environment.NewLine +
                    "where XrefID in  " + Environment.NewLine +
                    "( " + Environment.NewLine +
                    "select distinct x.XrefID " + Environment.NewLine +
                    "from dbo.t_object as o " + Environment.NewLine +
                    "inner join t_attribute as a on o.Object_ID = a.Object_ID " + Environment.NewLine +
                    "inner join t_xref x on Client = a.ea_guid " + Environment.NewLine +
                    "where x.Type = 'attribute property' " + Environment.NewLine +
                    "and o.Object_ID = " + elementId + Environment.NewLine +
                    ")";

                var command = new SqlCommand(sql, conn);
                conn.Open();

                rows = command.ExecuteNonQuery();
            }

            return rows;
        }

        internal int RemoveElementStereotypes(int elementId, string stereotype)
        {
            int rows = 0;

            using (var conn = new SqlConnection(EaRepository))
            {
                string sql =
                    "delete from t_xref " + Environment.NewLine +
                    "where XrefID in  " + Environment.NewLine +
                    "( " + Environment.NewLine +
                    "select distinct x.XrefID " + Environment.NewLine +
                    "from dbo.t_object as o " + Environment.NewLine +
                    "inner join t_xref x on Client = o.ea_guid " + Environment.NewLine +
                    "where x.Name = 'Stereotypes' " + Environment.NewLine +
                    "and x.Description like @P1 " + Environment.NewLine +
                    "and o.Object_ID = " + elementId + Environment.NewLine +
                    ")";

                var command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("P1", "%" + stereotype + "%");
                conn.Open();

                rows = command.ExecuteNonQuery();
            }

            return rows;
        }


        internal string GetReleaseGuid(string release, string releaseDate)
        {
            string guid = string.Empty;

            using (var conn = new SqlConnection(EaRepository))
            {
                string sql =
                    "select ea_guid " +
                    "from t_object o " +
                    "where stereotype = 'release' " +
                    "and object_type = 'Class' " +
                    "and ( name = @P1 or " +
                    "exists ( select * from t_objectproperties t2 where o.object_id = t2.object_id and t2.property = 'Release Date' and t2.value = @P2)) ";

                var command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@P1", release);
                command.Parameters.AddWithValue("@P2", releaseDate);
                conn.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        guid = reader.GetString(0);
                    }
                }
            }
            return guid;
        }

        internal DateTime? GetReleaseDate(string release)
        {
            string releaseDate = DateTime.MinValue.ToString("dd/MM/yyyy");

            using (var conn = new SqlConnection(EaRepository))
            {
                string sql =
                    "select t2.value " +
                    "from t_object o, t_objectproperties t2 " +
                    "where stereotype = 'release' " +
                    "and object_type = 'Class' " +
                    "and o.Name = @P1 " +
                    "and o.object_id = t2.object_id and t2.property = 'Release Date' ";

                var command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@P1", release);
                conn.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        releaseDate = reader.GetString(0);
                    }
                }
                else
                {
                    return null;
                }
            }
            return DateTime.ParseExact(releaseDate, "dd/MM/yyyy", CultureInfo.CurrentCulture);
        }

        internal bool DeleteRelease(string release, string releaseDate)
        {
            string guid = GetReleaseGuid(release, releaseDate);

            if (string.IsNullOrEmpty(guid)) return false;

            Element element = AddInRepository.Instance.Repository.GetElementByGuid(guid);

            if (element == null) return false;

            return DeleteElement(element);
        }

        internal bool DeleteElement(Element element)
        {
            Package package = AddInRepository.Instance.Repository.GetPackageByID(element.PackageID);

            short i = 0;
            bool found = false;
            bool itemsDeleted = false;

            foreach (Element packageElement in package.Elements)
            {
                if (element.ElementGUID == packageElement.ElementGUID)
                {
                    found = true;
                    break;
                }
                i++;
            }
            if (found)
            {
                package.Elements.DeleteAt(i, false);
                itemsDeleted = true;
            }

            return itemsDeleted;
        }

        internal bool DeleteAttribute(Attribute attribute)
        {
            Element element = AddInRepository.Instance.Repository.GetElementByID(attribute.ParentID);

            short i = 0;
            bool found = false;
            bool attributeDeleted = false;

            foreach (Attribute elementAttribute in element.Attributes)
            {
                if (attribute.AttributeGUID == elementAttribute.AttributeGUID)
                {
                    found = true;
                    break;
                }
                i++;
            }
            if (found)
            {
                element.Attributes.DeleteAt(i, false);
                attributeDeleted = true;
            }

            return attributeDeleted;
        }

        internal List<Release> ListReleases()
        {
            var releaseInfo = new List<Release>();
            var sortedReleases = new SortedList<DateTime, Release>();

            using (var conn = new SqlConnection(EaRepository))
            {
                string sql =
                    "select o.name [Name], t1.value [Stream],  t2.value [Release Date] " +
                    "from t_object o left outer join t_objectproperties t1 on o.object_id = t1.object_id and t1.property = 'Stream' " +
                    "left outer join t_objectproperties t2 on o.object_id = t2.object_id and t2.property = 'Release Date' " +
                    "where o.stereotype = 'release' " +
                    "and o.object_type = 'Class' ";

                var command = new SqlCommand(sql, conn);
                conn.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var release = new Release
                                      {
                                          Name = reader.GetString(0)
                                      };

                    if (!reader.IsDBNull(1))
                    {
                        release.Stream = reader.GetString(1);
                    }

                    if (reader.IsDBNull(2))
                    {
                        string day = "01";
                        string month = release.Name.Substring(0, 3);
                        string year = release.Name.Substring(4, 4);

                        var date = new DateTime(2000, 1, 1);
                        DateTime.TryParse(day + "-" + month + "-" + year, out date);

                        release.ReleaseDate = date;
                    }
                    else
                    {
                        int day = 1;
                        int month = 1;
                        int year = 2000;

                        string date = reader.GetString(2);

                        Int32.TryParse(date.Substring(0, 2), out day);
                        Int32.TryParse(date.Substring(3, 2), out month);
                        Int32.TryParse(date.Substring(6, 4), out year);

                        release.ReleaseDate = new DateTime(year, month, day);
                    }

                    if (!sortedReleases.ContainsKey(release.ReleaseDate))
                        sortedReleases.Add(release.ReleaseDate, release);
                }
            }
            IEnumerable<Release> releases = from r in sortedReleases
                                            select r.Value;

            foreach (Release release in releases)
            {
                releaseInfo.Add(release);
            }
            return releaseInfo;
        }

        internal int GetReleaseStereotypeUsageCount(string release)
        {
            int usage = 0;

            using (var conn = new SqlConnection(EaRepository))
            {
                release = "%" + release + "%";

                string sql =
                    "select count(*) " +
                    "from t_object o " +
                    "where o.stereotype like @P1 ";

                var command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@P1", release);
                conn.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        usage = reader.GetInt32(0);
                    }
                }
                reader.Close();

                // extended stereotpyes
                sql =
                    "select  count(*) " +
                    "from dbo.t_object as o " +
                    "inner join t_xref x on Client = o.ea_guid " +
                    "where x.Name = 'Stereotypes' " +
                    "and x.Description like @P1";

                command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@P1", release);

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        usage += reader.GetInt32(0);
                    }
                }

                reader.Close();
                //

                sql =
                    "select count(*) " +
                    "from t_attribute a " +
                    "where a.stereotype like @P1 ";

                command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@P1", release);

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        usage += reader.GetInt32(0);
                    }
                }

                reader.Close();

                // extended attribute stereotpyes
                sql =
                    "select  count(*) " +
                    "from dbo.t_object as o " +
                    "inner join t_attribute as a on o.Object_ID = a.Object_ID " +
                    "inner join t_xref x on Client = a.ea_guid " +
                    "where x.Type = 'attribute property' " +
                    "and x.Description like @P1 ";

                command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@P1", release);

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        usage += reader.GetInt32(0);
                    }
                }

                reader.Close();

                //

                sql =
                    "select count(*) " +
                    "from t_operation o " +
                    "where o.stereotype like @P1 ";

                command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@P1", release);

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        usage += reader.GetInt32(0);
                    }
                }

                reader.Close();

                // extended attribute stereotpyes
                sql =
                    "select  count(*) " +
                    "from dbo.t_object as o " +
                    "inner join t_operation as a on o.Object_ID = a.Object_ID " +
                    "inner join t_xref x on Client = a.ea_guid " +
                    "where x.Type = 'operation property' " +
                    "and x.Description like @P1 ";

                command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@P1", release);

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        usage += reader.GetInt32(0);
                    }
                }

                reader.Close();

                //


                sql =
                    "select count(*) " +
                    "from t_connector c " +
                    "where c.stereotype like @P1 or c.stereotype like @P1";

                command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@P1", release);

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        usage += reader.GetInt32(0);
                    }
                }
            }

            return usage;
        }

        internal Message CreateStereotype(string stereotype, string appliesTo)
        {
            Message message = new InformationMessage(appliesTo + " stereotype " + stereotype + " created successfully.");

            try
            {
                using (var conn = new SqlConnection(EaRepository))
                {
                    string sql =
                        "select stereotype " +
                        "from t_stereotypes " +
                        "where stereotype = @P1 " +
                        "and appliesto = @P2 ";

                    var command = new SqlCommand(sql, conn);
                    command.Parameters.AddWithValue("@P1", stereotype);
                    command.Parameters.AddWithValue("@P2", appliesTo);
                    conn.Open();

                    SqlDataReader reader = command.ExecuteReader();


                    if (!reader.HasRows)
                    {
                        reader.Close();


                        sql =
                            "INSERT INTO t_stereotypes " +
                            " (Stereotype,AppliesTo,Description,ea_guid) " +
                            " VALUES (@P1,@P2,@P3,@P4)";

                        command.CommandText = sql;
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@P1", stereotype);
                        command.Parameters.AddWithValue("@P2", appliesTo);
                        command.Parameters.AddWithValue("@P3",
                                                        "Auto-generated " + DateTime.Now.ToShortDateString() + " by " +
                                                        AddInRepository.Instance.Repository.GetCurrentLoginUser(false));
                        command.Parameters.AddWithValue("@P4", "{" + Guid.NewGuid() + "}");

                        if (command.ExecuteNonQuery() == 0)
                            message = new ErrorMessage("Unknown error creating stereotype " + stereotype);
                    }
                    else
                    {
                        reader.Close();
                        message =
                            new WarningMessage(appliesTo + " stereotype " + stereotype +
                                               " not created. Stereotype already exists.");
                    }
                }
            }
            catch (Exception ex)
            {
                message = new ErrorMessage("Database error: " + ex.Message);
            }


            return message;
        }

        internal Message DeleteStereotype(string stereotype, string appliesTo)
        {
            Message message = new InformationMessage(appliesTo + " stereotype " + stereotype + " deleted successfully.");
            try
            {
                using (var conn = new SqlConnection(EaRepository))
                {
                    string sql =
                        "delete " +
                        "from t_stereotypes " +
                        "where stereotype = @P1 " +
                        "and appliesto = @P2 ";

                    var command = new SqlCommand(sql, conn);
                    command.Parameters.AddWithValue("@P1", stereotype);
                    command.Parameters.AddWithValue("@P2", appliesTo);
                    conn.Open();

                    int affectedRows = command.ExecuteNonQuery();


                    if (affectedRows <= 0)
                    {
                        message =
                            new WarningMessage(appliesTo + " stereotype " + stereotype +
                                               " not deleted. Stereotype may not exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                message = new ErrorMessage("Database error: " + ex.Message);
            }


            return message;
        }

        public Package GetMasterReleasePackage()
        {
            int packageId = -1;

            using (var conn = new SqlConnection(EaRepository))
            {
                string sql =
                    "select package_id " +
                    "from t_package " +
                    "where Name = 'Master Releases' ";

                var command = new SqlCommand(sql, conn);
                conn.Open();

                SqlDataReader reader = command.ExecuteReader();


                if (reader.Read() && !reader.IsDBNull(0))
                {
                    packageId = reader.GetInt32(0);
                }
            }

            if (packageId == -1) return null;

            Package package = AddInRepository.Instance.Repository.GetPackageByID(packageId);

            return package;
        }

        internal List<string[]> ListClassesContainingStereotype(string release)
        {
            release = "%" + release + "%";
            var classes = new List<string[]>();

            using (var conn = new SqlConnection(EaRepository))
            {
                string sql =
                    "select o.ea_guid, o.name " +
                    "from t_object o " +
                    "where o.stereotype like @P1 " +
                    "and o.object_type = 'Class' " +
                    "and o.name IS NOT NULL " +
                    "UNION " +
                    "select  o.ea_guid, o.name " +
                    "from dbo.t_object as o  " +
                    "inner join t_xref x on Client = o.ea_guid  " +
                    "where x.Name = 'Stereotypes'  " +
                    "and o.name IS NOT NULL " + 
                    "and x.Description like @P1 ";


                var command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@P1", release);

                conn.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    classes.Add(new[] {reader.GetString(0), reader.GetString(1)});
                }
            }
            return classes;
        }

        internal List<string[]> ListAttributesContainingStereotype(string release)
        {
            release = "%" + release + "%";
            var ElementAndAttributes = new List<string[]>();

            using (var conn = new SqlConnection(EaRepository))
            {
                string sql =
                    "select a.ea_guid, a.name, o.name " +
                    "from t_attribute a, t_object o " +
                    "where a.object_id = o.object_id " +
                    "and a.stereotype like @P1 " +
                    "UNION " +
                    "select a.ea_guid, a.name, o.name " +
                    "from dbo.t_object as o  " +
                    "inner join t_attribute as a on o.Object_ID = a.Object_ID  " +
                    "inner join t_xref x on Client = a.ea_guid  " +
                    "where  " +
                    "x.Type = 'attribute property' and  " +
                    "x.Description like @P1 ";

                var command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@P1", release);

                conn.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ElementAndAttributes.Add(new[] {reader.GetString(0), reader.GetString(1), reader.GetString(2)});
                }
            }
            return ElementAndAttributes;
        }

        public void UpdateAddInVersion()
        {
            var updater = new EadbUpdate();

            updater.ElementID = 187075;
            updater.Note = string.Format("<b>Current CalibreGEN Addin:</b> {0} (Released {1})", new EAAddIn().Version,
                                         DateTime.Now.ToShortDateString());

            updater.UpdateEaElementNotes();
        }

        #region structures

        // ----------------------------------------------------------
        //       Structure for ConnectorList
        // ----------------------------------------------------------

        public static ConnectorList GetConnectorListForElement(Element element)
        {
            var myConnectorList = new ConnectorList();

            myConnectorList.ObjectElement = element;
            myConnectorList.ObjectType = element.Type;
            myConnectorList.ObjectName = element.Name;
            myConnectorList.ObjectStereotype = element.Stereotype;
            myConnectorList.ObjectElementId = element.ElementID;
            myConnectorList.ObjectGuid = element.ElementGUID;
            myConnectorList.ObjectModified = element.Modified;
            myConnectorList.ObjectStatus = element.Status;

            return myConnectorList;
        }

        public static ConnectorList GetConnectorListForPackage(Package package)
        {
            var myConnectorList = new ConnectorList();

            myConnectorList.ObjectElement = null;
            myConnectorList.ObjectPackage = package;
            myConnectorList.ObjectName = package.Name;
            myConnectorList.ObjectStereotype = string.Empty;
            myConnectorList.ObjectType = "Package";
            myConnectorList.ObjectElementId = package.PackageID;
            myConnectorList.ObjectGuid = package.PackageGUID;
            myConnectorList.ObjectModified = package.Modified;
            myConnectorList.ObjectStatus = string.Empty;

            return myConnectorList;
        }

        public string getViewDefinitionByObjectID(int ObjectId)
        {
            string select = "select notes from t_objectproperties where Object_ID = " + ObjectId;
            string definition = string.Empty;

            using (var connection = new SqlConnection(EaRepository))
            {
                using (var command = new SqlCommand(select, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader != null && reader.Read())
                    {
                        definition = reader[0].ToString();
                    }
                }
            }
            return definition;
        }

        public string GetElementNameByObjectID(int ObjectId)
        {
            string select = "select isnull(name,'') + isnull((select ':' + name from t_object where object_id = t.classifier),'') from t_object t where Object_ID = " + ObjectId;
            string name = string.Empty;

            using (var connection = new SqlConnection(EaRepository))
            {
                using (var command = new SqlCommand(select, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader != null && reader.Read())
                    {
                        name = reader[0].ToString();
                    }
                }
            }
            return name;
        }

        #region Nested type: cab

        public struct cab
        {
            public string Author;
            public string EA_GUID;
            public string EAStatus;
            public string EAStereotype;
            public string Module;
            public string Name;
        }

        #endregion

        #region Nested type: ConnectorList

        public struct ConnectorList
        {
            public string ConnectorDirection;
            public int ConnectorEndId;
            public int ConnectorId;
            public string ConnectorName;
            public int ConnectorStartId;
            public string ConnectorStyleEx;
            public string ConnectorType;
            public string FromConnectorFeature;
            public string FromConnectorFeatureType;
            public int FromElementId;
            public string FromElementName;
            public string FromElementStereotype;
            public string FromElementType;
            public string ObjectAlias;


            public Element ObjectElement;
            public int ObjectElementId;
            public string ObjectGuid;
            public DateTime ObjectModified;
            public string ObjectName;
            public string ObjectNote;
            public Package ObjectPackage;
            public string ObjectStatus;
            public string ObjectStereotype;
            public string ObjectType;


            public int OtherEndObjectElementId;
            public string OtherEndObjectName;
            public string OtherEndObjectStereotype;
            public string OtherEndObjectType;

            public string To_ConnectorFeature;
            public string To_ConnectorFeatureType;
            public int To_ElementID;
            public string To_ElementName;
            public string To_ElementStereotype;
            public string To_ElementType;
            public string UsesUsedBy;
        }

        #endregion

        // ----------------------------------------------------------
        //       Structure for ConnectorList
        // ----------------------------------------------------------

        #region Nested type: DiagramStruct

        public struct DiagramStruct
        {
            public int Diagram_ID;
            public string DiagramEA_GUID;
            public string DiagramName;
            public string DiagramType;
            public int Object_ID;
            public int Package_ID;
            public int ParentID;
        }

        #endregion

        #region Nested type: sEAElement

        public struct sEAElement
        {
            public string EA_GUID;
            public string ElementID;
            public string Name;
            public string Status;
            public string Stereotype;
            public string Type;
        }

        #endregion

        // ----------------------------------------------------------
        //       Structure for a tagged element
        // ----------------------------------------------------------

        #region Nested type: sElementTag

        public struct sElementTag
        {
            public string Author;
            public int Diagram_ID;
            public string elemguid;
            public string Name;
            public string Notes;
            public string Object_ID;
            public string Object_Type;
            public int Package_ID;
            public int ParentID;
            public int PropertyID;
            public string Status;
            public string Stereotype;
            public string tagguid;
            public string Value;
        }

        #endregion

        // ----------------------------------------------------------
        //       Structure for a cab
        // ----------------------------------------------------------

        // ----------------------------------------------------------
        //       Structure for an attribute
        // ----------------------------------------------------------

        #region Nested type: stEAAttribute

        public struct stEAAttribute
        {
            public int AllowDuplicates;
            public string Classifier;
            public int Const;
            public string Container;
            public string Containment;
            public string Default;
            public string Derived;
            public string ea_guid;
            public string GenOption;
            public int ID;
            public int IsCollection;
            public int IsOrdered;
            public int IsStatic;
            public int Length;
            public string LowerBound;
            public string Name;
            public string Notes;
            public int Object_ID;
            public int Pos;
            public int Precision;
            public int Scale;
            public string Scope;
            public string Stereotype;
            public string Style;
            public string StyleEx;
            public string Type;
            public string UpperBound;
        }

        #endregion

        // ----------------------------------------------------------
        //       Structure for an operation
        // ----------------------------------------------------------

        #region Nested type: stEAOperation

        public struct stEAOperation
        {
            public string Abstract;
            public string Behaviour;
            public string Classifier;
            public string Code;
            public string Concurrency;
            public int Const;
            public string ea_guid;
            public string GenOption;
            public int IsLeaf;
            public int IsQuery;
            public int IsRoot;
            public string IsStatic;
            public string Name;
            public string Notes;
            public int Object_ID;
            public int OperationID;
            public int Pos;
            public int Pure;
            public string ReturnArray;
            public string Scope;
            public string StateFlags;
            public string Stereotype;
            public string Style;
            public string StyleEx;
            public string Synchronized;
            public string Throws;
            public string Type;
        }

        #endregion

        // ----------------------------------------------------------
        //                    Structure
        // ----------------------------------------------------------

        #endregion structures

        #region Nested type: brItem

        public struct brItem
        {
            public string brnotes;
            public string BRStatus;
            public string elementID;
            public string linkNames;
            public string name;
            public string placeddiagrams;
        }

        #endregion

        #region Nested type: sqlElement

        public struct sqlElement
        {
            public string Alias;
            public string Author;
            public string ea_guid;
            public string Name;
            public string Note;
            public int Object_ID;
            public string Object_Type;
            public int Package_ID;
            public string Status;
        }

        #endregion

        #region Nested type: sqlPackage

        public struct sqlPackage
        {
            public string ea_guid;
            public string Name;
            public string Notes;
            public int Package_ID;
            public int Parent_ID;
            public string PkgOwner;
        }

        #endregion
    }

    public class ReleaseInfo
    {
        public string Name { get; set; }
        public string Stream { get; set; }

        public string FullName
        {
            get
            {
                if (String.IsNullOrEmpty(Stream))
                    return Name + " (Release)";
                else
                {
                    return Name + " (" + Stream + ")";
                }
            }
        }
    }
}
