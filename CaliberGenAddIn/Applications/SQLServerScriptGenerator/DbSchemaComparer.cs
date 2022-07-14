using System.Collections.Generic;
using System.Linq;
using EAAddIn.Interfaces.DbSchema;

namespace EAAddIn.Applications.SQLServerScriptGenerator
{
    public class DbSchemaComparer
    {
        public DbSchemaComparer(IDbSchema from, IDbSchema to)
        {
            From = from;
            To = to;
        }

        private IDbSchema From { get; set; }

        private IDbSchema To { get; set; }

        public List<DbDifference> Compare()
        {
            var differences = new List<DbDifference>();

            //new tables
            IEnumerable<IDbTable> newTables =
                from f in From.Tables
                where !(from t in To.Tables
                        where t.Name == f.Name
                        select t).Any()
                select f;

            foreach (IDbTable dbTable in newTables)
            {
                differences.Add(new DbDifference
                                    {
                                        CreateScript = true,
                                        Location = From.Type,
                                        Item = "Table",
                                        Name = dbTable.Name,
                                        Type = "New"
                                    });
            }

            //dropped tables
            IEnumerable<IDbTable> droppedTables =
                from t in To.Tables
                where !(from f in From.Tables
                        where f.Name == t.Name
                        select f).Any()
                select t;

            foreach (IDbTable dbTable in droppedTables)
            {
                differences.Add(new DbDifference
                                    {
                                        CreateScript = true,
                                        Location = To.Type,
                                        Item = "Table",
                                        Name = dbTable.Name,
                                        Type = "Dropped"
                                    });
            }

            //analyse each table
            var tables =
                from f in From.Tables
                from t in To.Tables
                where t.Name == f.Name
                select new {From = f, To = t};

            foreach (var tablePair in tables)
            {
                var fromTable = tablePair.From;
                var toTable = tablePair.To;

                #region Columns
                //new columns
                IEnumerable<IDbColumn> newColumns =
                    from f in fromTable.Columns
                    where !(from t in toTable.Columns
                            where t.Name == f.Name
                            select t).Any()
                    select f;

                foreach (IDbColumn column in newColumns)
                {
                    if (column.NotNull.Value)
                    {
                        differences.Add(new DbDifference
                                            {
                                                Location = From.Type,
                                                Item = "Column",
                                                Name = fromTable.Name + "." + column.Name,
                                                Type = "New (Not Null)"
                                            });
                    }
                    else
                    {
                        differences.Add(new DbDifference
                        {
                            CreateScript = true,
                            Location = From.Type,
                            Item = "Column",
                            Name = fromTable.Name + "." + column.Name,
                            Type = "New"
                        });
 
                    }
                }

                //dropped columns
                IEnumerable<IDbColumn> droppedColumns =
                    from t in tablePair.To.Columns
                    where !(from f in fromTable.Columns
                            where f.Name == t.Name
                            select f).Any()
                    select t;

                foreach (IDbColumn column in droppedColumns)
                {
                    differences.Add(new DbDifference
                                        {
                                            CreateScript = true,
                                            Location = To.Type,
                                            Item = "Column",
                                            Name = fromTable.Name + "." + column.Name,
                                            Type = "Dropped"
                                        });
                }

                //analyse each column
                var columns =
                    from f in fromTable.Columns
                    from t in toTable.Columns
                    where t.Name == f.Name &&
                          (t.DataType != f.DataType
                           || t.Length != f.Length
                           || t.NotNull != f.NotNull
                           || t.PK != f.PK
                           || t.Precision != f.Precision
                           || t.Scale != f.Scale)

                    select new {From = f, To = t};

                foreach (var columnPair in columns)
                {
                    var fromColumn = columnPair.From;
                    var toColumn = columnPair.To;

                    if (toColumn.DataType != fromColumn.DataType)
                    {
                        differences.Add(new DbDifference
                                            {
                                                Location = From.Type + "/" + To.Type,
                                                Item = "Column",
                                                Name = fromTable.Name + "." + fromColumn.Name,
                                                Type = "DataType change",
                                                OldValue = toColumn.DataType,
                                                NewValue = fromColumn.DataType
                                            });

                    }
                    if (toColumn.Length != fromColumn.Length)
                    {
                        bool canChange = false;
                        if (toColumn.Length > fromColumn.Length)
                        {
                            canChange = true;
                        }

                        differences.Add(new DbDifference
                                            {
                                                CreateScript = canChange,
                                                Location = From.Type + "/" + To.Type,
                                                Item = "Column",
                                                Name = fromTable.Name + "." + fromColumn.Name,
                                                Type = "Length change",
                                                OldValue = toColumn.Length.ToString(),
                                                NewValue = fromColumn.Length.ToString(),
                                            });

                    }
                    if (toColumn.NotNull != fromColumn.NotNull)
                    {
                        bool canChange = false;
                        if (!toColumn.NotNull.Value)
                        {
                            canChange = true;
                        }

                        differences.Add(new DbDifference
                                            {
                                                CreateScript = canChange,
                                                Location = From.Type + "/" + To.Type,
                                                Item = "Column",
                                                Name = fromTable.Name + "." + fromColumn.Name,
                                                Type = "Not null change",
                                                OldValue = toColumn.NotNull.ToString(),
                                                NewValue = fromColumn.NotNull.ToString()
                                            });

                    }
                    if (toColumn.PK != fromColumn.PK)
                    {
                        differences.Add(new DbDifference
                                            {
                                                Location = From.Type + "/" + To.Type,
                                                Item = "Column",
                                                Name = fromTable.Name + "." + fromColumn.Name,
                                                Type = "PK change",
                                                OldValue = toColumn.PK.ToString(),
                                                NewValue = fromColumn.PK.ToString()
                                            });

                    }
                    if (toColumn.Precision != fromColumn.Precision)
                    {
                        bool canChange = false;
                        if (toColumn.Precision > fromColumn.Precision)
                        {
                            canChange = true;
                        }
                        differences.Add(new DbDifference
                                            {
                                                CreateScript = canChange,
                                                Location = From.Type + "/" + To.Type,
                                                Item = "Column",
                                                Name = fromTable.Name + "." + fromColumn.Name,
                                                Type = "Precision change",
                                                OldValue = toColumn.Precision.ToString(),
                                                NewValue = fromColumn.Precision.ToString()
                                            });

                    }
                    if (toColumn.Scale != fromColumn.Scale)
                    {
                        differences.Add(new DbDifference
                                            {
                                                Location = From.Type + "/" + To.Type,
                                                Item = "Column",
                                                Name = fromTable.Name + "." + fromColumn.Name,
                                                Type = "Scale change",
                                                OldValue = toColumn.Scale.ToString(),
                                                NewValue = fromColumn.Scale.ToString()
                                            });

                    }

                }
                #endregion

                #region Indexes
                //new indexes
                IEnumerable<IDbIndex> newIndices =
                    from f in fromTable.Indices
                    where !(from t in toTable.Indices
                            where t.Name == f.Name
                            select t).Any()
                    select f;

                foreach (IDbIndex dbIndex in newIndices)
                {
                    differences.Add(new DbDifference
                    {
                        CreateScript = true,
                        Location = From.Type,
                        Item = "Index",
                        Name = dbIndex.Name,
                        Type = "New"
                    });
                }

                //dropped indexes
                IEnumerable<IDbIndex> droppedIndices =
                    from t in toTable.Indices
                    where !(from f in fromTable.Indices
                            where f.Name == t.Name
                            select f).Any()
                    select t;

                foreach (IDbIndex dbIndex in droppedIndices)
                {
                    differences.Add(new DbDifference
                    {
                        CreateScript = true,
                        Location = To.Type,
                        Item = "Index",
                        Name = dbIndex.Name,
                        Type = "Dropped"
                    });
                }
                #endregion
            }

            return differences;
        }
    }

    public class DbDifference
    {
        public bool CreateScript { get; set; }
        public string Location { get; set; }
        public string Item { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}