using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EA;
using EAAddIn.Interfaces.DbSchema;

namespace EAAddIn.Applications.SQLServerScriptGenerator
{
    public class EADbSchema: IDbSchema
    {
        public delegate void ShowElementParsing(string type, string item);

        #region IDbSchema Members

        public event ShowElementParsing ParsingElement;

        public string Database
        {
            get;
            set;
        }

        public string Type
        {
            get { return "EA"; }
        }

        public List<IDbTable> Tables
        {
            get;
            set;
        }

        public List<IDbStoredProcedure> StoredProcedures
        {
            get;
            set;
        }

        public List<IDbFunction> Functions
        {
            get;
            set;
        }



        public List<IDbView> Views
        {
            get;
            set;
        }

        #endregion

        public void Load()
        {
            if (Tables == null)
            {
                Tables = new List<IDbTable>();
            }
            else
            {
                Tables.Clear();
            }

            Package package = AddInRepository.Instance.Repository.GetTreeSelectedPackage();

            if (package != null)
            {
                Database = package.Name;

                var tables =
                    from Element t in package.Elements
                    where t.Stereotype == "table"
                    orderby t.Name
                    select t;

                //foreach (Element element in package.Elements)
                //{
                //    if (element.Stereotype == "table")

                foreach (Element element in tables)
                {
                    if (ParsingElement != null)
                        ParsingElement("table", element.Name);

                    var table = new EADbTable
                                    {
                                        Name = element.Name
                                    };
                    Tables.Add(table);

                    #region Load columns

                    table.Columns = new List<IDbColumn>();

                    foreach (EA.Attribute attrib in element.Attributes)
                    {
                        if (ParsingElement != null)
                            ParsingElement("attribute", attrib.Name);

                        if (attrib.Stereotype == "column")
                        {
                            var columnDefinition = new EADbColumn
                                                       {
                                                           Name = attrib.Name,
                                                           DataType = attrib.Type,
                                                           Length =
                                                               string.IsNullOrEmpty(attrib.Length)
                                                                   ? 0
                                                                   : Convert.ToInt32(attrib.Length),
                                                           Precision =
                                                               string.IsNullOrEmpty(attrib.Precision)
                                                                   ? 0
                                                                   : Convert.ToInt32(attrib.Precision),
                                                           Scale =
                                                               string.IsNullOrEmpty(attrib.Scale)
                                                                   ? 0
                                                                   : Convert.ToInt32(attrib.Scale),
                                                           PK = Convert.ToBoolean(attrib.IsOrdered),
                                                           NotNull = Convert.ToBoolean(attrib.AllowDuplicates),
                                                           Position = attrib.Pos + 1,
                                                           DefaultValue = attrib.Default,
                                                           FK = Convert.ToBoolean(attrib.IsCollection)
                                                       };

                            IDbColumn column = columnDefinition;

                            //clear out length, precision & scale for these datatypes
                            DbSchemaHelper.AdjustColumnProperties(ref column);

                            table.Columns.Add(column);
                        }
                    }

                    #endregion

                    #region Load indexes

                    table.Indices = new List<IDbIndex>();

                    foreach (Method method in element.Methods)
                    {
                        if (ParsingElement != null)
                            ParsingElement("method", method.Name);

                        var indexDefinition = new EADbIndex
                                                  {
                                                      Name = method.Name,
                                                      Type = method.Stereotype,
                                                      Constraint = method.Code
                                                  };

                        table.Indices.Add(indexDefinition);
                    }

                    #endregion
                }

            }

            #region Views

            if (Views == null)
            {
                Views = new List<IDbView>();
            }
            else
            {
                Views.Clear();
            }

            var views =
                    from Element t in package.Elements
                    where t.Stereotype == "view"
                    orderby t.Name
                    select t;

            foreach (Element element in views)
            {
                if (ParsingElement != null)
                    ParsingElement("view", element.Name);

                var view = new EADbView()
                                {
                                    Name = element.Name
                                };

                foreach (Property property in element.Properties)
                {
                    Debug.WriteLine(property.Name);
                    Debug.WriteLine(property.ObjectType.ToString());
                    Debug.WriteLine(property.Validation);
                    Debug.WriteLine(property.Value.ToString());
                }

                Views.Add(view);

                #region Load columns

                view.Columns = new List<IDbColumn>();

                foreach (EA.Attribute attrib in element.Attributes)
                {
                    if (ParsingElement != null)
                        ParsingElement("attribute", attrib.Name);

                    if (attrib.Stereotype == "column")
                    {
                        var columnDefinition = new EADbColumn
                                                   {
                                                       Name = attrib.Name,
                                                       DataType = attrib.Type,
                                                       Length =
                                                           string.IsNullOrEmpty(attrib.Length)
                                                               ? 0
                                                               : Convert.ToInt32(attrib.Length),
                                                       Precision =
                                                           string.IsNullOrEmpty(attrib.Precision)
                                                               ? 0
                                                               : Convert.ToInt32(attrib.Precision),
                                                       Scale =
                                                           string.IsNullOrEmpty(attrib.Scale)
                                                               ? 0
                                                               : Convert.ToInt32(attrib.Scale),
                                                       PK = Convert.ToBoolean(attrib.IsOrdered),
                                                       NotNull = Convert.ToBoolean(attrib.AllowDuplicates),
                                                       Position = attrib.Pos + 1,
                                                       DefaultValue = attrib.Default,
                                                       FK = Convert.ToBoolean(attrib.IsCollection)
                                                   };

                        IDbColumn column = columnDefinition;

                        //clear out length, precision & scale for these datatypes
                        DbSchemaHelper.AdjustColumnProperties(ref column);

                        view.Columns.Add(column);
                    }
                }

                #endregion
            }

            #endregion


            #region Stored Procedures

            if (StoredProcedures == null)
            {
                StoredProcedures = new List<IDbStoredProcedure>();
            }
            else
            {
                StoredProcedures.Clear();
            }

            var storedProcedures =
                    from Element t in package.Elements
                    where t.Stereotype == "stored procedure"
                    || t.Stereotype == "stored procedures"
                    orderby t.Name
                    select t;

            foreach (Element element in storedProcedures)
            {
                if (ParsingElement != null)
                    ParsingElement("stored procedure", element.Name);

                if (element.Methods.Count > 0)
                {
                    // stored procedures are stored as methods
                    foreach (Method method in element.Methods)
                    {
                        StoredProcedures.Add(new EADbStoredProcedure()
                                                 {
                                                     Name = method.Name
                                                 });

                        if (ParsingElement != null)
                            ParsingElement("stored procedure", element.Name);
                    }
                }
                else
                {

                    StoredProcedures.Add(new EADbStoredProcedure()
                                              {
                                                  Name = element.Name
                                              });
                }
             }

            #endregion



            #region Functions

            if (Functions == null)
            {
                Functions = new List<IDbFunction>();
            }
            else
            {
                Functions.Clear();
            }


            var functions =
                    from Element t in package.Elements
                    where t.Stereotype == "function"
                    || t.Stereotype == "functions"
                    orderby t.Name
                    select t;

            foreach (Element element in functions)
            {
                if (ParsingElement != null)
                    ParsingElement("function", element.Name);

                if (element.Methods.Count > 0)
                {
                    // stored procedures are stored as methods
                    foreach (Method method in element.Methods)
                    {
                        Functions.Add(new EADbFunction()
                        {
                            Name = method.Name
                        });

                        if (ParsingElement != null)
                            ParsingElement("function", element.Name);
                    }
                }
                else
                {
                    Functions.Add(new EADbFunction()
                    {
                        Name = element.Name
                    });
                }
            }

            #endregion
        }



        public IDbTable FindTable(string table)
        {
            return DbSchemaHelper.FindTable(Tables, table);
        }

        public IDbFunction FindFunction(string function)
        {
            return DbSchemaHelper.FindFunction(Functions, function);
        }

        public IDbStoredProcedure FindStoredProcedure(string storedProcedure)
        {
            return DbSchemaHelper.FindStoredProcedure(StoredProcedures, storedProcedure);
        }

        public IDbView FindView(string view)
        {
            return DbSchemaHelper.FindView(Views, view);
        }
    }
}
