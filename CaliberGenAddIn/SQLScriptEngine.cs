using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EA;
using EAAddIn.Applications.SQLServerScriptGenerator;

namespace EAAddIn
{
    public class SQLScriptEngine
    {
        //public Dictionary<string, DBTableDefinition> EATableDefinitions = new Dictionary<string, DBTableDefinition>();
        public string Database { get; set;}

        public event EADbSchema.ShowElementParsing ParsingElement;

        public EADbSchema eaSchema = new EADbSchema();
        public SQLServerDbSchema sqlServerSchema = new SQLServerDbSchema();
    
        public bool loadEASchemaDefinitions()
        {
            bool ok = true;
            
            eaSchema.Load();

            return ok;
        }

        public bool loadSQLServerSchemaDefinitions()
        {
            bool ok = true;

            sqlServerSchema.Load();

            return ok;
        }
        public string BuildCreateScript(string table)
        {
            var builder = new StringBuilder();

            builder.AppendLine("USE [" + Database + "]");
            builder.AppendLine("GO");
            builder.AppendLine(string.Format("/****** Object:  Table [dbo].[{0}]    Script Date: {1} ******/", Database, DateTime.Now.ToString()));
            builder.AppendLine("SET ANSI_NULLS ON");
            builder.AppendLine("GO");
            builder.AppendLine("SET QUOTED_IDENTIFIER ON");
            builder.AppendLine("GO");
            builder.AppendLine("SET ANSI_PADDING ON");
            builder.AppendLine("GO");
            builder.AppendLine(string.Format("CREATE TABLE [dbo].[{0}] (", eaSchema.Database));

            foreach (var columnDefinition in eaSchema.FindTable(table).Columns)
            {
                builder.Append(string.Format("  [{0}] [{1}]",
                                                 columnDefinition.Name,
                                                 columnDefinition.DataType));

                #region Add length/scale/precision

                #endregion

                if (columnDefinition.NotNull.Value)
                {
                    builder.Append(" NOT NULL");
                }
                else
                {
                    builder.Append(" NULL");
                }

                builder.AppendLine(",");

            }

            builder.AppendLine();
            builder.AppendLine("GO");
            builder.AppendLine("SET ANSI_PADDING OFF");

            return builder.ToString();
        }


        internal void SetSqlServerConnectionString(string p)
        {
            sqlServerSchema.ConnectionString = p;
        }

        internal List<DbDifference> Compare()
        {
            return new DbSchemaComparer(eaSchema, sqlServerSchema).Compare();
        }
    }
}
