using System;

namespace WrapperTool.MVP
{
    public class SaveEventArgs : EventArgs
    {
        private SaveEventArgs() { }
        public SaveEventArgs(string fileName) { FileName = fileName; }
        public string FileName { get; private set; }
    }

}
