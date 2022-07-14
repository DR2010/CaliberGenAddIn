
namespace WrapperTool.MVP
{
    public interface IView<TModel>
    {
        IPresenter<TModel> Presenter { get; }
    }

}
