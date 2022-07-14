


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using EAAddIn.Applications;
using Message=EAAddIn.Message;

public class PreviewActiveReportPresenter
{
    public IMaintainReleases View
    {
        get;
        set;
    }

    public event EventHandler<EventArgs> PreviewFormClosed;

    //List<Release> Releases { get; set;} 


    public PreviewActiveReportPresenter(IMaintainReleases view, DataView data, Type report)
    {
        //View.PreviewFormClosed += View_PreviewFormClosed;

        View.StreamsBindingSource.CancelEdit();

        View.Show();

    }

    void View_ReleaseFormClosed(object sender, EventArgs e)
    {
        //if (ReleasesClosed != null)
        //{
        //    ReleasesClosed(this, EventArgs.Empty);
        //}
    }

   



    
    internal void ShowView()
    {
        View.BringToFront();
    }
}//end MaintainReleasesPresenter