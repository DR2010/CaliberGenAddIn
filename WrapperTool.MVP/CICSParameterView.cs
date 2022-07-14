using System.ComponentModel;
using System.Linq;
using CICSWeb.Net;

namespace WrapperTool.MVP
{
    public sealed class CICSParameterCollectionView 
    {
        public CICSParameterCollectionView(CICSParameterCollection collection)
        {
            Collection = collection;
        }
        public BindingList<ICICSParameterView<IWrapperConfigItem>> List()
        {
            var list = new BindingList<ICICSParameterView<IWrapperConfigItem>>();
            if (Collection == null) return list;
            Collection.Dictionary.ToList().ForEach
                (item => list.Add(new CICSParameterView(item.Value)));
            return list;
        }
        public CICSParameterCollection Collection
        {
            get;
            private set;
        }

    }
    public class CICSParameterView : ICICSParameterView<IWrapperConfigItem>
    {
        private CICSParameterView() { }
        public CICSParameterView(IWrapperConfigItem item) 
        {
            ItemData = item;
        }
        public IWrapperConfigItem ItemData
        {
            get;
            private set;
        }
        #region ICICSParameterView Members

        public string Name
        {
            get { return ItemData.Name; }
        }

        public string Value
        {
            get
            {
                return ItemData.Value;
            }
            set
            {
                ItemData.Value = value;
                OnNotifyPropertiesChanged();
            }
        }

        public string ArrayKey
        {
            get { return ItemData.ArrayKey; }
        }

        public IWrapperConfigItem Source
        {
            get { return ItemData; }
        }


        #endregion
        public void OnNotifyPropertiesChanged()
        {
            PropertyChanged(this, new PropertyChangedEventArgs("Value"));
        }


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion
    }
    public interface ICICSParameterView<TSource> : INotifyPropertyChanged
    {
        string Name { get; }
        string Value { get; set; }
        string ArrayKey { get; }
        TSource Source { get; }
    }

}
