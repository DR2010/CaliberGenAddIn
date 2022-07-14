using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using EA;

namespace EAAddIn.Applications.CSVClassImporter
{
    public class CSVImportEngine
    {
        int columnCount = 0;
        private List<string> columnNames = new List<string>();

        public DataTable CreateDataTableFromCSV(string csvFile)
        {
            var csvDataTable = new DataTable();

            try
            {
                var csvStream = System.IO.File.OpenRead(csvFile);
                csvDataTable = PopulateDataTableFromUploadedFile(csvStream);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error processing file: " + ex.Message, "Import File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return csvDataTable;
        }
        private DataTable PopulateDataTableFromUploadedFile(System.IO.Stream strm)
        {
            var dataTable = new DataTable();

            var srdr = new System.IO.StreamReader(strm);
            var strLine = String.Empty;
            var iLineCount = 0;

            bool moreToProcess = true;

            do
            {
                strLine = srdr.ReadLine();
                if (strLine == null)
                {
                    moreToProcess = false;
                }
                else
                {
                    if (0 == iLineCount++)
                    {
                        dataTable = CreateDataTableForCSVData(strLine);
                    }
                    else
                    {
                        AddDataRowToTable(strLine, dataTable);
                    }
                }
            } while (moreToProcess);

            return dataTable;
        }

        private DataTable CreateDataTableForCSVData(String line)
        {
            columnNames = new List<string>();
            DataTable dt = new DataTable("CSVTable");
            String[] values = line.Split(new char[] { ',' });
            columnCount = values.Length;
            int index = 0;
            foreach (String strVal in values)
            {
                var columnName = strVal;
                columnNames.Add(columnName);

                dt.Columns.Add(columnName, Type.GetType("System.String"));
            }
            return dt;
        }

        private string GetColumnName(int index)
        {
            return columnNames[index];
        }

        private DataRow AddDataRowToTable(String line, DataTable dt)
        {
            var values = line.Split(new [] { ',' });
            var numberOfValues = values.Length;
            // If number of values in this line are more than the columns
            // currently in table, then we need to add more columns to table.
            if (numberOfValues > columnCount)
            {
                var difference = numberOfValues - columnCount;
                for (var i = 0; i < difference; i++)
                {
                    var columnName = GetColumnName(columnCount + i);
                    dt.Columns.Add(columnName, Type.GetType("System.String"));
                }
                columnCount = numberOfValues;
            }
            var idx = 0;
            var drow = dt.NewRow();
            foreach (var stringValue in values)
            {
                var columnName = GetColumnName(idx++);
                drow[columnName] = stringValue.Trim();
            }
            dt.Rows.Add(drow);
            return drow;
        }


        internal void CreateElementsFromDataTable(EA.Package package, DataTable csvDataTable)
        {
            for (int i = 0; i < csvDataTable.Rows.Count; i++)
            {
                var row = csvDataTable.Rows[i];

                    var elementType = "Class";
                    if (columnNames.Contains("Type"))
                    {
                        elementType = row[columnNames.IndexOf("Type")].ToString();
                    }

                    var elementName = row[columnNames.IndexOf("Name")].ToString();

                    var newElement = (Element)package.Elements.AddNew(elementName, elementType);

                    if (columnNames.Contains("Stereotype"))
                    {
                        newElement.Stereotype = row[columnNames.IndexOf("Stereotype")].ToString();
                    }
                    newElement.Update();

                    AddAttributesToClass(ref newElement, i + 1, csvDataTable);

                    newElement.Update();
            }
            package.Elements.Refresh();
        }

        private void AddAttributesToClass(ref Element newClass, int rowIndex, DataTable csvDataTable)
        {

            while (rowIndex < csvDataTable.Rows.Count 
                && csvDataTable.Rows[rowIndex][columnNames.IndexOf("Type")].ToString() == "Attribute" )
            {
                var row = csvDataTable.Rows[rowIndex];

                var attribute = (EA.Attribute)newClass.Attributes.AddNew(row[columnNames.IndexOf("Name")].ToString(), row[columnNames.IndexOf("Type")].ToString());

                attribute.Update();

                //for (int columnIndex = 2; columnIndex < row.ItemArray.Count(); columnIndex++)
                //{
                //    var newTag = attribute.TaggedValues.AddNew(csvDataTable.Rows[0][columnIndex].ToString(),
                //                                  csvDataTable.Rows[rowIndex][columnIndex].ToString());
                //    var tv = (AttributeTag) newTag;
                //    tv.Update();
                //}
                rowIndex++;
            }
        }

    }
}
