using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EAAddIn.Interfaces.DbSchema;

namespace EAAddIn.Applications.SQLServerScriptGenerator
{
    public class SQLServerDbSchema : IDbSchema
    {
        public string ConnectionString
        {
            get; set;
        }

        public delegate void ShowElementParsing(string type, string item);
        public event ShowElementParsing ParsingElement;

        #region IDbSchema Members
        public List<IDbTable> Tables { get; set; }
        public List<IDbStoredProcedure> StoredProcedures { get; set; }
        public List<IDbFunction> Functions { get; set; }
        public List<IDbView> Views { get; set; }
        public string Database { get; set; }
        public string Type
        {
            get { return "Sql Server";}
        }

        public void Load()
        {
            var sys = new SystemTablesDataContext(ConnectionString);

            #region Tables, Columns and Indexes

            if (Tables == null)
            {
                Tables = new List<IDbTable>();
            }
            else
            {
                Tables.Clear();
            }


            

                var tables =
                    from t in sys.tables
                    where t.type == "U"
                    orderby t.name
                    select new SQLServerDbTable {Id = t.object_id, Name = t.name};

                foreach (SQLServerDbTable table in tables)
                {
                    Tables.Add(table);

                    if (ParsingElement != null)
                        ParsingElement("table", table.Name);

                    table.Columns = new List<IDbColumn>();

                    var columns =
                        from c in sys.columns
                        from t in sys.types
                        where c.object_id == table.Id
                              && c.system_type_id == t.system_type_id
                              && c.user_type_id == t.user_type_id
                        orderby c.column_id

                        select new SQLServerDbColumn
                                   {
                                       Name = c.name,
                                       Length = c.max_length,
                                       Precision = c.precision,
                                       Scale = c.scale,
                                       DataType = t.name,
                                       NotNull = !c.is_nullable,
                                       Position = c.column_id
                                   };

                    foreach (SQLServerDbColumn column in columns)
                    {
                        IDbColumn thisColumn = column;

                        //clear out length, precision & scale for these datatypes
                        if (!DbSchemaHelper.AdjustColumnProperties(ref thisColumn))
                        {
                            thisColumn.Length = thisColumn.Length/2;
                        }
                        table.Columns.Add(thisColumn);
                    }

                    table.Indices = new List<IDbIndex>();

                    var uniqueIndices =
                        from i in sys.indexes
                        from k in sys.key_constraints
                        where i.object_id == table.Id
                              && i.object_id == k.parent_object_id
                              && i.is_unique == true
                              && k.unique_index_id == i.index_id

                        select new SQLServerDbIndex
                                   {
                                       Id = i.index_id,
                                       ObjectId = i.object_id,
                                       Name = i.name,
                                       Type = k.type,
                                       Constraint = k.type_desc,
                                       InternalType = i.type_desc
                                   };

                    foreach (SQLServerDbIndex uniqueIndex in uniqueIndices)
                    {
                        table.Indices.Add(uniqueIndex);
                        uniqueIndex.Columns = new List<IDbColumn>();

                        var keyColumns =
                            from ic in sys.index_columns
                            from c in sys.columns
                            from o in sys.objects
                            where ic.index_id == uniqueIndex.Id
                                  && ic.object_id == uniqueIndex.ObjectId
                                  && ic.column_id == c.column_id
                                  && ic.object_id == c.object_id
                                  && ic.object_id == o.parent_object_id
                                  && o.type == "PK"
                            select c.name;

                        foreach (string keyColumn in keyColumns)
                        {
                            uniqueIndex.Columns.Add(new SQLServerDbColumn
                                                        {
                                                            Name = keyColumn
                                                        });
                            table.Columns.Where(n => n.Name == keyColumn).First().PK = true;
                        }
                    }


                    var fkIndices =
                        from o in sys.objects
                        from f in sys.foreign_keys
                        from i in sys.indexes
                        where f.parent_object_id == table.Id
                            && f.parent_object_id == o.object_id
                            && f.referenced_object_id == i.object_id
                            && f.key_index_id == i.index_id

                        select new SQLServerDbIndex
                                   {
                                       Id = f.key_index_id.Value,
                                       ObjectId = f.object_id,
                                       Name = f.name,
                                       Type = "FK",
                                       // Constraint = o.type_desc,
                                       InternalType = i.type_desc
                                   };

                    foreach (var fkIndex in fkIndices)
                    {
                        table.Indices.Add(fkIndex);
                        fkIndex.Columns = new List<IDbColumn>();

                        var keyColumns =
                            from fc in sys.foreign_key_columns
                            from c in sys.columns
                            where fc.constraint_object_id == fkIndex.ObjectId
                                  && fc.parent_column_id == c.column_id
                                  && fc.parent_object_id == c.object_id
                            select c.name;

                        foreach (string keyColumn in keyColumns)
                        {
                            fkIndex.Columns.Add(new SQLServerDbColumn
                                                    {
                                                        Name = keyColumn
                                                    });
                            table.Columns.Where(n => n.Name == keyColumn).First().FK = true;
                        }
                    }

                    var nonuniqueIndices =
                        from i in sys.indexes
                        where i.object_id == table.Id
                              && i.is_unique == false
                              && i.name != null

                        select new SQLServerDbIndex
                                   {
                                       Id = i.index_id,
                                       ObjectId = i.object_id,
                                       Name = i.name,
                                       Type = "index",
                                       InternalType = i.type_desc
                                   };
                    foreach (var nonuniqueIndex in nonuniqueIndices)
                    {
                        nonuniqueIndex.Columns = new List<IDbColumn>();

                        var nonuniqueColumns =
                            from c in sys.columns
                            from i in sys.index_columns
                            where i.object_id == nonuniqueIndex.ObjectId
                            && i.index_id == nonuniqueIndex.Id
                            && i.object_id == c.object_id
                            && i.column_id == c.column_id
                            orderby i.index_column_id
                            select c.name;

                        foreach (string nonuniqueColumn in nonuniqueColumns)
                        {
                            nonuniqueIndex.Columns.Add(new SQLServerDbColumn
                            {
                                Name = nonuniqueColumn
                            });
                        }

                        table.Indices.Add(nonuniqueIndex);

                    }


                    var checkConstraints =
                        from c in sys.check_constraints
                        from t in sys.tables
                        where c.parent_object_id == t.object_id
                              && t.object_id == table.Id

                        select new SQLServerDbIndex
                                   {
                                       ObjectId = c.object_id,
                                       Name = c.name,
                                       Type = "check constraint",
                                       Constraint = c.definition
                                   };
                    foreach (var checkConstraint in checkConstraints)
                    {
                        table.Indices.Add(checkConstraint);

                        checkConstraint.Columns = new List<IDbColumn>();
                        if (table.Columns.Where(n => n.Position == checkConstraint.DefaultColumnId).Count() > 0)
                        {
                            checkConstraint.Columns.Add(table.Columns.Where(n => n.Position == checkConstraint.DefaultColumnId).First());
                        }

                    }

                    var defaultConstraints =
                        from d in sys.default_constraints
                        where d.parent_object_id == table.Id

                        select new SQLServerDbIndex
                                   {
                                       ObjectId = d.parent_object_id,
                                       DefaultColumnId = d.parent_column_id,
                                       Name = d.name,
                                       Type = "default constraint",
                                       Constraint = d.definition
                                   };

                    foreach (SQLServerDbIndex defaultConstraint in defaultConstraints)
                    {
                        defaultConstraint.Columns = new List<IDbColumn>();
                        defaultConstraint.Columns.Add(table.Columns.Where(n => n.Position == defaultConstraint.DefaultColumnId).First());
                        table.Indices.Add(defaultConstraint);
                        table.Columns.Where(n => n.Position == defaultConstraint.DefaultColumnId).First().DefaultValue =
                            defaultConstraint.Constraint;
                    }

                    var triggers =
                        from tr in sys.triggers
                        from s in sys.sql_modules
                        where tr.object_id == s.object_id
                              && tr.parent_id == table.Id

                        select new SQLServerDbIndex
                                   {
                                       Name = tr.name,
                                       Type = "trigger",
                                       Constraint = s.definition
                                   };
                    foreach (SQLServerDbIndex trigger in triggers)
                    {
                        table.Indices.Add(trigger);
                    }

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
                    from p in sys.procedures
                    from s in sys.sql_modules
                    where p.object_id == s.object_id
                    orderby p.name
                    select new SQLServerDbStoredProcedure() {Name = p.name, Code = s.definition};

            foreach (SQLServerDbStoredProcedure storedProcedure in storedProcedures)
            {
                if (ParsingElement != null)
                    ParsingElement("stored procedure", storedProcedure.Name);

                StoredProcedures.Add(storedProcedure);
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
                    from f in sys.objects
                    from s in sys.sql_modules
                    where f.object_id == s.object_id
                    && f.type == "FN"
                    orderby f.name
                    select new SQLServerDbFunction() { Name = f.name, Code = s.definition };

            foreach (SQLServerDbFunction function in functions)
            {
                if (ParsingElement != null)
                    ParsingElement("function", function.Name);

                Functions.Add(function);
            }
            #endregion

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
                    from v in sys.views
                    from s in sys.sql_modules
                    where v.object_id == s.object_id
                    && v.type == "V"
                    orderby v.name
                    select new SQLServerDbView() { Id = v.object_id, Name = v.name, Code = s.definition };

            foreach (SQLServerDbView view in views)
            {
                Views.Add(view);
                
                if (ParsingElement != null)
                    ParsingElement("view", view.Name);

                view.Columns = new List<IDbColumn>();

                var columns =
                    from c in sys.columns
                    from t in sys.types
                    where c.object_id == view.Id
                          && c.system_type_id == t.system_type_id
                          && c.user_type_id == t.user_type_id
                    orderby c.column_id

                    select new SQLServerDbColumn
                               {
                                   Name = c.name,
                                   Length = c.max_length,
                                   Precision = c.precision,
                                   Scale = c.scale,
                                   DataType = t.name,
                                   NotNull = !c.is_nullable,
                                   Position = c.column_id
                               };

                foreach (SQLServerDbColumn column in columns)
                {
                    view.Columns.Add(column);
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

        #endregion
    }
}
