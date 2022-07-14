using System;
using System.ComponentModel;

using System.Windows.Forms;
using EA;
using EAAddIn.Applications.DatabaseReleaseManager;
using EAAddIn.Windows;
using EAAddIn.Windows.Interfaces;
using DatabaseReleaseManager = EAAddIn.Applications.DatabaseReleaseManager.DatabaseReleaseManager;
using System.Collections.Generic;
using Element=EA.Element;

namespace EAAddIn.Presenters
{
    public class DatabaseReleaseToolbarPresenter
    {
        public DatabaseReleaseManager Model
        {
            get;
            set;
        }
        public IDatabaseReleaseToolbar View
        {
            get;
            set;
        }

        private readonly BindingSource columns = new BindingSource();
        private BindingList<Column> results = new BindingList<Column>();

        private List<Release> Releases
        {
            get;
            set;
        }
        /// 
        /// <param name="view"></param>
        public DatabaseReleaseToolbarPresenter(IDatabaseReleaseToolbar view)
        {
            View = view;
            Model = new DatabaseReleaseManager();
            UpdateReleases();
            //View.ReleasesValueMember = "Name";

            view.ReleaseSelected += view_ReleaseSelected;
            view.ApplyStereotypeToTableRequested += view_ApplyStereotypeToTablesRequested;
            view.GetColumnsRequested += view_GetColumnsRequested;
            view.ApplyStereotypeToColumnsRequested += view_ApplyStereotypeToColumnsRequested;
            view.HideUnchangedColumnsRequested += view_HideUnchangedColumnsRequested;
            }

        void view_HideUnchangedColumnsRequested(object sender, EventArgs e)
        {
            var selectedElements = new List<Element>();
            var selectedDiagramObjects = new List<DiagramObject>();

            new EaAccess().GetEaElements(ref selectedElements, ref selectedDiagramObjects, true);

            Diagram diagram = AddInRepository.Instance.Repository.GetCurrentDiagram();

            foreach (var diagramObject in selectedDiagramObjects)
            {
                Model.ShowPKsAndChangedColumnsOnlyOnDiagramElement(diagram, diagramObject);
            }
        }

        void view_ApplyStereotypeToColumnsRequested(object sender, EventArgs e)
        {
            Diagram diag = AddInRepository.Instance.Repository.GetCurrentDiagram();
            if (diag != null)
            {
                AddInRepository.Instance.Repository.SaveDiagram(diag.DiagramID);
            }

            Model.Release = (Release)View.SelectedRelease;
            Model.ChangeType = View.ChangeType;

            var newNames = new List<string>();
            var selectedColumns = View.SelectedColumns;
            var renamedSelectedColumns = new List<string>();

            if (View.ChangeType == "renamed")
            {
                foreach (var selectedColumn in selectedColumns)
                {
                    var renameForm = new RenamedItemForm();
                    renameForm.OldName = "column " + selectedColumn;

                    var result = renameForm.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        newNames.Add(renameForm.NewName);
                        renamedSelectedColumns.Add(selectedColumn);
                    }
                }
                selectedColumns = renamedSelectedColumns;
            }
            Model.ApplyStereotypeToSelectedColumns(View.Element, View.DiagramObject, selectedColumns, newNames);

            if (diag != null)
            {
                AddInRepository.Instance.Repository.ReloadDiagram(diag.DiagramID);
            }

        }

        void view_GetColumnsRequested(object sender, EventArgs e)
        {
            var tableColumns = Model.GetColumnsForSelectedTable();

            UpdateColumns(tableColumns);

            View.Element = Model.GetSelectedTableElement();
            View.DiagramObject = Model.GetSelectedTableDiagramObject();
        }

        void view_ApplyStereotypeToTablesRequested(object sender, EventArgs e)
        {
            Model.Release = (Release) View.SelectedRelease;
            Model.ChangeType = View.ChangeType;

            var newNames = new List<string>();
            var selectedElements = new List<Element>();
            var selectedDiagramObjects = new List<DiagramObject>();

            new EaAccess().GetEaElements(ref selectedElements, ref selectedDiagramObjects, true);
            var renamedSelectedElements = new List<EA.Element>();

            Diagram diag = AddInRepository.Instance.Repository.GetCurrentDiagram();

            if (diag != null && ( selectedElements.Count > 0 || new EaAccess().GetEaConnector() != null))
            {
                AddInRepository.Instance.Repository.SaveDiagram(diag.DiagramID);

                if (View.ChangeType == "renamed")
                {
                    foreach (var element in selectedElements)
                    {
                        var renameForm = new RenamedItemForm();
                        renameForm.OldName = "table " + element.Name;

                        var result = renameForm.ShowDialog();

                        if (result == DialogResult.OK)
                        {
                            newNames.Add(renameForm.NewName);
                            renamedSelectedElements.Add(element);
                        }
                    }
                    selectedElements = renamedSelectedElements;
                }
                else if (View.ChangeType == "to be deleted")
                {
                    foreach (var element in selectedElements)
                    {
                        if (element.Name.StartsWith("TBD_"))
                        {
                            element.Name = element.Name.Substring(4);
                        }

                        newNames.Add("TBD_" + element.Name);
                        renamedSelectedElements.Add(element);
                    }
                    selectedElements = renamedSelectedElements;
                }
                Model.ApplyStereotypeToSelectedTables(selectedElements, selectedDiagramObjects, newNames);

                var connector = new EaAccess().GetEaConnector();

                if (connector != null)
                {
                    Model.ApplyStereotypeToSelectedConnector(connector);
                }
                //AddInRepository.Instance.Repository.SaveDiagram(diag.DiagramID);
                AddInRepository.Instance.Repository.ReloadDiagram(diag.DiagramID);
            }
        }
    



        void view_ReleaseSelected(object sender, System.EventArgs e)
        {
        }

        private void UpdateReleases()
        {
            Releases = new Release().List();

            View.ReleasesBindingSource.DataSource = Releases;
            View.ReleasesDisplayMember = "FullName";

            View.BindReleases();

            Release latestRelease = new Release();
            latestRelease.ReleaseDate = DateTime.MinValue;

            foreach (var release in Releases)
            {
                if (latestRelease.ReleaseDate < release.ReleaseDate)
                {
                    latestRelease = release;
                }
            }
            if (latestRelease.ReleaseDate != DateTime.MinValue)
            {
                View.SelectedRelease = latestRelease;
            }
            columns.DataSource = results;
            View.ColumnsBindingSource = columns;
        }
        private void UpdateColumns(IList<Column> myColumns)
        {
            results = new BindingList<Column>(myColumns);
            columns.DataSource = results;
            View.BindColumns();
        }

        #region View Events

       
        #endregion
    }
}