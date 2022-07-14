using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAAddIn.Interfaces.DbSchema;

namespace EAAddIn.Applications.SQLServerScriptGenerator
{
    public class DbSchemaHelper
    {
        public static IDbTable FindTable(List<IDbTable> tables, string table)
        {
            if (tables == null) return null;

            var result =
                from IDbTable t in tables
                where t.Name == table
                select t;

            if (result.Count() == 1)
            {
                return result.First();
            }

            return null;
        }
        public static IDbIndex FindIndex(List<IDbIndex> indices, string name)
        {
            if (indices == null) return null;

            IEnumerable<IDbIndex> result =
                from IDbIndex i in indices
                where i.Name == name
                select i;

            if (result.Count() == 1)
            {
                return result.First();
            }

            return null;
        }

        public static IDbColumn FindColumn(List<IDbColumn> columns, string name)
        {
            if (columns == null) return null;

            IEnumerable<IDbColumn> result =
                from IDbColumn c in columns
                where c.Name == name
                select c;

            if (result.Count() == 1)
            {
                return result.First();
            }

            return null;
        }

        public static bool AdjustColumnProperties(ref IDbColumn column)
        {
            bool lengthsCleared = false;

            IDbColumn refColumn = column;

            if (new[] { "smallint", "tinyint", "int", "bigint", "float", "real", "binary", "image", "ntext", "text", "bool", "bit", "timestamp", "datetime", "smalldatetime", "money", "xml", "sql_variant", "uniqueidentifier" }.Contains(refColumn.DataType))
            {
                column.Length = null;
                column.Precision = null;
                column.Scale = null;
                
                lengthsCleared =  true;
            }
            else if (new[] { "nchar", "char", "nvarchar", "varchar", "varbinary" }.Contains(refColumn.DataType))
            {
                column.Precision = null;
                column.Scale = null;
            }
            return lengthsCleared;
        }

        public static IDbFunction FindFunction(List<IDbFunction> functions, string function)
        {
            if (functions == null) return null;

            IEnumerable<IDbFunction> result =
                from IDbFunction i in functions
                where i.Name == function
                select i;

            if (result.Count() == 1)
            {
                return result.First();
            }

            return null;
        }

        internal static IDbStoredProcedure FindStoredProcedure(List<IDbStoredProcedure> storedProcedures, string storedProcedure)
        {
            if (storedProcedures == null) return null;

            IEnumerable<IDbStoredProcedure> result =
                from IDbStoredProcedure i in storedProcedures
                where i.Name == storedProcedure
                select i;

            if (result.Count() == 1)
            {
                return result.First();
            }

            return null;
        }

        internal static IDbView FindView(List<IDbView> views, string view)
        {
            if (view == null) return null;

            IEnumerable<IDbView> result =
                from IDbView i in views
                where i.Name == view
                select i;

            if (result.Count() == 1)
            {
                return result.First();
            }

            return null;
        }
    }
}
