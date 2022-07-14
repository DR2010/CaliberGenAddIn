


using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EA;

namespace EAAddIn.Windows.Interfaces
{
    public interface IDatabaseReleaseToolbar
    {

        void Show();
        string ReleasesDisplayMember { set; }
        string ReleasesValueMember { set; }
        string ChangeType { get; }
        Object SelectedRelease { get; set; }
        event EventHandler<EventArgs> ReleaseSelected;
        void BindReleases();
        event EventHandler<EventArgs> ApplyStereotypeToTableRequested;
        event EventHandler<EventArgs> ApplyStereotypeToColumnsRequested;
        event EventHandler<EventArgs> GetColumnsRequested;
        BindingSource ReleasesBindingSource { get; }
        BindingSource ColumnsBindingSource { get; set; }
        List<string> SelectedColumns { get; }
        void BindColumns();
        EA.Element Element { set; get; }
        DiagramObject DiagramObject { get; set; }
        event EventHandler<EventArgs> HideUnchangedColumnsRequested;
    }
}