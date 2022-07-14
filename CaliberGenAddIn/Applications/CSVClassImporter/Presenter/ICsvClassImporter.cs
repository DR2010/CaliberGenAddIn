
using System;
using System.Windows.Forms;

namespace EAAddIn.Windows.Interfaces {
	public interface ICsvClassImporter {

		void Show();
        event EventHandler<EventArgs> SetPackageToCurrentRequested;
        event EventHandler<EventArgs> ImportCsvRequested;
        event EventHandler<EventArgs> CreateClassesRequested;
        BindingSource ClassesBindingSource { get; set; }
        string CsvFileName { get; set; }
	    string PackageName { set; }
	    void BindClasses();
    }

}