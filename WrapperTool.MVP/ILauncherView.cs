using System;
using CICSWeb.Net;
using WrapperTool.Config;

namespace WrapperTool.MVP
{
    public interface ILauncherView : IView<WrapperConfig>
    {
        bool NeedsToRestoreInput(string text);
        void UpdateAfterLoadTemplate();
        void UpdateAfterLoadData();
        void UpdateAfterRefresh();
        void ShowResponse(string response);
        void ShowRequest(string request);
        void UpdateAfterExecute();
        void UpdateBeforeExecute();
        void UpdateAfterClear();
        void KillGroups();
        string FileName { get; }

        WrapperToolSetting CurrentSettings { get; }


        event EventHandler ClearEmpty;
        event EventHandler Execute;
        event EventHandler<SaveEventArgs> SaveAs;
        event EventHandler RefreshData;
        event EventHandler Clear;
        event EventHandler KillGroupsOnly;
        event EventHandler LoadTemplate;
        event EventHandler LoadData;


    }
}
