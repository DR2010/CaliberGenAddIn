using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace EAAddIn
{
    public class EABPMN
    {
        public string EARepository;
        public static SqlConnection EADBConnection;

        public EABPMN()
        {

            EARepository =
              "Data Source=DRPSQL007\\SQL07;" +
              "Initial Catalog=EA_Release1;"+
              "Persist Security info=false;"+
              "integrated security=sspi;";

            //            "Connect=provider=sqloledb.1;integrated security=sspi;"+

        }

        //
        // Report for package indicated
        // 
        public void report(EA.Package RootPackage, DataTable bpmnDataTable)
        {
            bpmnDataTable.Rows.Clear();

            if (RootPackage == null)
            {
                return;
            }

            foreach (EA.Diagram diagram in RootPackage.Diagrams)
            {
                DataRow dr = bpmnDataTable.NewRow();
                dr["PackageName"] = RootPackage.Name;
                dr["DiagramName"] = diagram.Name;
                dr["DiagramVersion"] = diagram.Version;
                dr["DiagramStereotype"] = diagram.Stereotype;
                dr["DiagramNotes"] = diagram.Notes;
                dr["DiagramModifiedDate"] = diagram.ModifiedDate;
                dr["PackageEAGUID"] = diagram.DiagramGUID;
                dr["DiagramParentID"] = diagram.ParentID;
                dr["DiagramPackageID"] = diagram.PackageID;

                bpmnDataTable.Rows.Add(dr);
            }


            addNodetoDataTable(RootPackage, bpmnDataTable);
        }

        //
        // Add node to DataTable EA
        //
        private void addNodetoDataTable(EA.Package pk, DataTable bpmnDataTable)
        {
            foreach (EA.Package package in pk.Packages)
            {
                foreach (EA.Diagram diagram in pk.Diagrams)
                {
                    DataRow dr = bpmnDataTable.NewRow();
                    dr["PackageName"] = package.Name;
                    dr["DiagramName"] = diagram.Name;
                    dr["DiagramVersion"] = diagram.Version;
                    dr["DiagramStereotype"] = diagram.Stereotype;
                    dr["DiagramNotes"] = diagram.Notes;
                    dr["DiagramModifiedDate"] = diagram.ModifiedDate;
                    dr["PackageEAGUID"] = diagram.DiagramGUID;
                    dr["DiagramParentID"] = diagram.ParentID;
                    dr["DiagramPackageID"] = diagram.PackageID;
                    bpmnDataTable.Rows.Add(dr);
                }

                addNodetoDataTable(package, bpmnDataTable);
            }
        }


        public void BPMNUserList(DataTable bpmnDataTable)
        {

            //
            // EA SQL database
            //
            EADBConnection = new SqlConnection(EARepository);
            EADBConnection.Open();

            SqlCommand sqlCommand1 = new SqlCommand();
            sqlCommand1 = EADBConnection.CreateCommand();

            sqlCommand1.CommandText =
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
            "     AND    ( GroupName = 'Business Process Modeller' OR GroupName like 'BPM%')";

            SqlDataReader reader = sqlCommand1.ExecuteReader();

            bpmnDataTable.Clear();

            while (reader.Read())
            {
                DataRow userRow = bpmnDataTable.NewRow();
                userRow["UserLogin"] = reader["UserLogin"].ToString();
                userRow["FirstName"] = reader["FirstName"].ToString();
                userRow["Surname"] = reader["Surname"].ToString();
                userRow["GroupName"] = reader["GroupName"].ToString();

                bpmnDataTable.Rows.Add(userRow);
            }

            reader.Close();

            EADBConnection.Close();

        }


        // ----------------------------------------------------------
        //                    Structure
        // ----------------------------------------------------------
        public struct sUser
        {
            public string UserLogin;
            public string FirstName;
            public string Surname;
            public string GroupID;
            public string GroupName;
        }
    }
}
