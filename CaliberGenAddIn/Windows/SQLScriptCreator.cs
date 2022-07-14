using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EA;
using EAAddIn.Applications.SQLServerScriptGenerator;
using EAAddIn.Interfaces.DbSchema;

namespace EAAddIn.Windows
{
    public partial class SQLScriptCreator : Form
    {
        private SQLScriptColumnAttributes columnAttributes;
        private SQLScriptIndexAttributes indexAttributes;
        private SQLScriptFunctionAttributes functionAttributes;
        private SQLScriptStoredProcedureAttributes storedProcedureAttributes;
        private SQLScriptViewAttributes viewAttributes;
        private SQLScriptEngine scriptGenerator = new SQLScriptEngine(); 

        public SQLScriptCreator()
        {
            InitializeComponent();


            #region Add user controls to bottom panel
            columnAttributes = new SQLScriptColumnAttributes();
            columnAttributes.Dock = DockStyle.Fill;
            columnAttributes.Visible = false;

            indexAttributes = new SQLScriptIndexAttributes();
            indexAttributes.Dock = DockStyle.Fill;
            indexAttributes.Visible = false;


            storedProcedureAttributes = new SQLScriptStoredProcedureAttributes();
            storedProcedureAttributes.Dock = DockStyle.Fill;
            storedProcedureAttributes.Visible = false;

            functionAttributes = new SQLScriptFunctionAttributes();
            functionAttributes.Dock = DockStyle.Fill;
            functionAttributes.Visible = false;



            viewAttributes = new SQLScriptViewAttributes();
            viewAttributes.Dock = DockStyle.Fill;
            viewAttributes.Visible = false;


            BottomPanel.Controls.Add(columnAttributes);
            BottomPanel.Controls.Add(indexAttributes);
            BottomPanel.Controls.Add(storedProcedureAttributes);
            BottomPanel.Controls.Add(functionAttributes);
            BottomPanel.Controls.Add(viewAttributes);
            #endregion

            ThisImageList.Images.Add(Properties.Resources.ImageTable); // image 0 
            ThisImageList.Images.Add(Properties.Resources.ImageAttribute); // image 1 
            ThisImageList.Images.Add(Properties.Resources.ImageOperation); // image 2 
            ThisImageList.Images.Add(Properties.Resources.ImageViewSQL); // image 3 
            ThisImageList.Images.Add(Properties.Resources.ImageStoredProcedure); // image 4 
            ThisImageList.Images.Add(Properties.Resources.ImageFunction); // image 5 

            EASchemaTreeView.ImageList = ThisImageList;

            scriptGenerator.eaSchema.ParsingElement += new EADbSchema.ShowElementParsing(eaSchema_ParsingElement);
            scriptGenerator.sqlServerSchema.ParsingElement += new SQLServerDbSchema.ShowElementParsing(sqlServerSchema_ParsingElement);
        }

        void sqlServerSchema_ParsingElement(string type, string item)
        {
  
            progressToolStripLabel.Text = "Parsing SQL Server " + type + " '" + item + "'...";
            Application.DoEvents();

        }
        void eaSchema_ParsingElement(string type, string item)
        {
            progressToolStripLabel.Text = "Parsing EA " + type + " '" + item + "'...";
            Application.DoEvents();
        }


        private void LoadEATables()
        {
            EASchemaTreeView.Nodes.Clear();

            DisplayToolstrip();

            if (scriptGenerator.loadEASchemaDefinitions())
            {
                DatabaseTextBox.Text = scriptGenerator.eaSchema.Database;

                UpdateTreeView(EASchemaTreeView, scriptGenerator.eaSchema);

                toolStripProgressBar.PerformStep();
            }

            HideToolstrip();
        }

        private void UpdateTreeView(TreeView treeView, IDbSchema schema)
        {
            #region Tables, columns, indexes
            foreach (var table in schema.Tables)
            {
                //var table = tableDefintion.Value;
                var tableNode = new TreeNode(table.Name, 0, 0) { Tag = "table" };

                treeView.Nodes.Add(tableNode);

                #region Load columns

                if (table.Columns != null)
                {
                    var columns = new List<TreeNode>();

                    table.Columns.Sort(CompareColumnsByPosition);

                    foreach (var column in table.Columns)
                    {
                        //var column = columnDefiniton.Value;

                        var columnNode = new TreeNode(column.Name, 1, 1) { Tag = "column" };

                        columns.Add(columnNode);
                    }
                    tableNode.Nodes.AddRange(columns.ToArray());
                }

                #endregion

                #region Load indexes

                var indexes = new List<TreeNode>();

                if (table.Indices != null)
                {
                    foreach (var index in table.Indices)
                    {
                        //var index = indexDefiniton.Value;


                        var indexNode = new TreeNode(index.Name, 2, 2) { Tag = "index" };

                        indexes.Add(indexNode);

                        //var indexDefinition = new DBIndexDefinition
                        //                          {
                        //                              Name = index.Name,
                        //                              Type = index.Type,
                        //                              Constraint = index.Constraint
                        //                          };

                    }
                    tableNode.Nodes.AddRange(indexes.ToArray());
                }

                #endregion
            }
            #endregion

            #region Views

            foreach (var view in schema.Views)
            {
                //var table = tableDefintion.Value;
                var viewNode = new TreeNode(view.Name, 3, 3) { Tag = "view" };
                ;
                treeView.Nodes.Add(viewNode);

                #region Load columns

                if (view.Columns != null)
                {
                    var columns = new List<TreeNode>();

                    foreach (var column in view.Columns)
                    {
                        //var column = columnDefiniton.Value;

                        var columnNode = new TreeNode(column.Name, 1, 1)
                            {Tag = "column"};

                        columns.Add(columnNode);
                    }
                    viewNode.Nodes.AddRange(columns.ToArray());
                }

                #endregion

            }

            #endregion
            
            #region Stored Procedures

            foreach (var storedProcedure in schema.StoredProcedures)
            {
                //var table = tableDefintion.Value;
                var storedProcedureNode = new TreeNode(storedProcedure.Name, 4, 4) { Tag = "stored procedure" };
                ;
                treeView.Nodes.Add(storedProcedureNode);
            }

            #endregion

            #region Functions

            foreach (var function in schema.Functions)
            {
                //var table = tableDefintion.Value;
                var functionNode = new TreeNode(function.Name, 5, 5) { Tag = "function" };
                ;
                treeView.Nodes.Add(functionNode);
            }

            #endregion
        }

        private static int CompareColumnsByPosition(IDbColumn x, IDbColumn y)
        {
            if (x.Position < y.Position) return -1;
            
            if (x.Position == y.Position) return 0;
            
            return 1;
        }

        private void HideToolstrip()
        {
            progressToolStripLabel.Visible = false;
        }
        private void DisplayToolstrip()
        {
            progressToolStripLabel.Text = "Refreshing EA tables...";
            progressToolStripLabel.Visible = true;
            toolStrip.Invalidate(true);
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            LoadEATables();
            scriptGenerator.SetSqlServerConnectionString(SqlServerConnectionTextBox.Text);
            LoadSQLServerTables();
        }

        private void LoadSQLServerTables()
        {
            SQLSchemaTreeView.Nodes.Clear();

            DisplayToolstrip();

            if (scriptGenerator.loadSQLServerSchemaDefinitions())
            {
                UpdateTreeView(SQLSchemaTreeView, scriptGenerator.sqlServerSchema);

                toolStripProgressBar.PerformStep();
            }

            HideToolstrip();
        }

        private void EATablesTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNodeSelected(e, scriptGenerator.eaSchema);
        }

        private void SQLTablesTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNodeSelected(e, scriptGenerator.sqlServerSchema);
        }

        private void TreeNodeSelected(TreeViewEventArgs e, IDbSchema schema)
        {
            HideIndexDefinition();
            HideColumnDefinition();
            HideFunctionDefinition();
            HideStoredProcedureDefinition();
            HideViewDefinition();

            if (e.Node.Tag.ToString() == "column" || e.Node.Tag.ToString() == "index")
            {
                SetCurrentObject(e.Node.Parent);
            }
            else
            {
                SetCurrentObject(e.Node);
            }
            SetControlView(e.Node, schema);
        }

        private void SetCurrentObject(TreeNode treeNode)
        {
            

            switch (treeNode.Tag.ToString())
            {
                case "table":
                    ObjectLabel.Text = "Table";
                    break;
                case "view":
                    ObjectLabel.Text = "View";
                    break;
                case "stored procedure":
                    ObjectLabel.Text = "Stored Proc";
                   break;
                case "function":
                    ObjectLabel.Text = "Function";
                    break;
            }
            ObjectTextBox.Text = treeNode.Text;
        }

        private void SetControlView(TreeNode treeNode, IDbSchema schema)
        {

            switch (treeNode.Tag.ToString())
            {
                case "column":
                    ShowColumnDefinition(schema, treeNode.Parent.Text, treeNode.Text); 
                    break;
                case "index":
                    ShowIndexDefinition(schema, treeNode.Parent.Text, treeNode.Text);
                    break;
                case "stored procedure":
                    ShowStoredProcedureDefinition(schema, treeNode.Text); 
                    break;
                case "function":
                    ShowFunctionDefinition(schema, treeNode.Text);
                    break;
                case "view":
                    ShowViewDefinition(schema, treeNode.Text);
                    break;
            }
        }

        private void ShowIndexDefinition(IDbSchema schema, string table, string index)
        {
            indexAttributes.Visible = true;

            var tableDefinition = schema.FindTable(table);
            var indexDefinition = tableDefinition.FindIndex(index);
            
            if (indexDefinition == null) return;

            indexAttributes.NameTextBox.Text = indexDefinition.Name;

            if (string.IsNullOrEmpty(indexDefinition.InternalType))
            {
                indexAttributes.TypeTextBox.Text = indexDefinition.Type;
            }
            else
            {
                indexAttributes.TypeTextBox.Text = indexDefinition.InternalType + " " + indexDefinition.Type;
            }
            indexAttributes.TypeTextBox.Text = indexDefinition.InternalType + " " + indexDefinition.Type;
            indexAttributes.ConstraintTextBox.Text = indexDefinition.Constraint;
            indexAttributes.ColumnsListView.Items.Clear();
            if (indexDefinition.Columns.Select(c => new ListViewItem(c.Name)).Count() > 0)
            {
                indexAttributes.ColumnsListView.Items.AddRange(indexDefinition.Columns.Select(c => new ListViewItem(c.Name)).ToArray());
            }
        }

        private void ShowColumnDefinition(IDbSchema schema, string table, string column)
        {
            columnAttributes.Visible = true;
            
            var tableDefinition = schema.FindTable(table);
            if (tableDefinition == null) return;

            var columnDefinition = tableDefinition.FindColumn(column);
            if (columnDefinition == null) return;

            columnAttributes.NameTextBox.Text = columnDefinition.Name;
            columnAttributes.TypeTextBox.Text = columnDefinition.DataType;
            columnAttributes.LengthTextBox.Text = columnDefinition.Length.ToString();
            columnAttributes.PrecisionTextBox.Text = columnDefinition.Precision.ToString();
            columnAttributes.ScaleTextBox.Text = columnDefinition.Scale.ToString();
            columnAttributes.NotNullCheckBox.Checked = columnDefinition.NotNull.Value;
            columnAttributes.PKCheckBox.Checked = columnDefinition.PK;
            columnAttributes.FKCheckBox.Checked = columnDefinition.FK;
            columnAttributes.UniqueCheckBox.Checked = columnDefinition.Unique;
            columnAttributes.DefaultValueTextBox.Text = columnDefinition.DefaultValue;
            columnAttributes.PositionTextBox.Text = columnDefinition.Position.ToString();
        }
        private void ShowFunctionDefinition(IDbSchema schema, string function)
        {
            functionAttributes.Visible = true;

            var functionDefinition = schema.FindFunction(function);

            if (functionDefinition == null) return;

            functionAttributes.CodeTextBox.Text = functionDefinition.Code;
        }
        private void ShowStoredProcedureDefinition(IDbSchema schema, string storedProcedure)
        {
            storedProcedureAttributes.Visible = true;

            var storedProcedureDefinition = schema.FindStoredProcedure(storedProcedure);

            if (storedProcedureDefinition == null) return;

            storedProcedureAttributes.ConstraintTextBox.Text = storedProcedureDefinition.Code;
        }

        private void ShowViewDefinition(IDbSchema schema, string view)
        {
            viewAttributes.Visible = true;

            var viewDefinition = schema.FindView(view);

            if (viewDefinition == null) return;

            viewAttributes.CodeTextBox.Text = viewDefinition.Code;
            viewAttributes.ColumnsListView.Items.Clear();
            viewAttributes.ColumnsListView.Items.AddRange(viewDefinition.Columns.Select( c => new ListViewItem(c.Name)).ToArray()); 
        }


        private void HideIndexDefinition()
        {
            indexAttributes.Visible = false;
        }
        private void HideColumnDefinition()
        {
            columnAttributes.Visible = false;
        }
        private void HideFunctionDefinition()
        {
            functionAttributes.Visible = false;
        }
        private void HideStoredProcedureDefinition()
        {
            storedProcedureAttributes.Visible = false;
        }
        private void HideViewDefinition()
        {
            viewAttributes.Visible = false;
        }

        private void CreateScriptButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ObjectTextBox.Text))
            {
                string text = scriptGenerator.BuildCreateScript(ObjectTextBox.Text);

                var viewer = new TextViewer
                {
                    ViewerText = text
                };
                viewer.Show();


            }
        }

        private void SQLScriptCreator_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void EATablesTreeView_MouseLeave(object sender, EventArgs e)
        {
            EASchemaTreeView.SelectedNode = null;
        }

        private void SQLTablesTreeView_MouseLeave(object sender, EventArgs e)
        {
            SQLSchemaTreeView.SelectedNode = null;
        }

        private void CompareButton_Click(object sender, EventArgs e)
        {
            var differences = scriptGenerator.Compare();

            if (differences != null)
            {
                var viewer = new SQLSchemaDifferencesViewer(differences);
                viewer.GetColumn(0).Width = 100;
                viewer.GetColumn(0).Frozen = true;
                viewer.GetColumn(0).HeaderText = "Create Script?";

                viewer.GetColumn(3).Width = 210;
                viewer.Width = 700;
                viewer.Show();
            }

        }

    }
}
