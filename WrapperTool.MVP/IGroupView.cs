using System;
using System.Data;
using CICSWeb.Net;

namespace WrapperTool.MVP
{
    public interface IGroupView : IView<IWrapperConfigItem>
    {
        void Loaded(DataTable data);

        event EventHandler LoadGroup;
    }
}
