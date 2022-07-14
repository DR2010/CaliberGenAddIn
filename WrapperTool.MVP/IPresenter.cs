using System.Collections.Generic;
using System.Data;
using WrapperTool.Config;

namespace WrapperTool.MVP
{
    public interface IPresenter<TModel>
    {
        TModel Model { get; }
        IView<TModel> View {get;}
        IEnumerable<WrapperToolSetting> Settings { get; }
    }

    public interface IGroupPresenter<TModel> : IPresenter<TModel>
    {
        IEnumerable<TModel> BuildListItems(DataTable dt);
    }
}
