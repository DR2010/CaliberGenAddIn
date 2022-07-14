

using System;
using System.Windows.Forms;


public interface IPreviewActiveReport
{

	void Show();
    void BringToFront();

    event EventHandler<EventArgs> PreviewFormClosed;
}