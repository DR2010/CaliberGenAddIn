using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using EA;

namespace EAAddIn
{
    public class ObjectDefinition
    {
        public Element Element { get; set; }
        public int Id { get; set; }
        public string Guid { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public Package Package { get; set; }
        public string Status { get; set; }
        public string Stereotype { get; set; }
        public string Type { get; set; }

    }
    class ObjectList : BindingList<ObjectDefinition>
    {
        public ObjectList( )
        {
            
        }
        private bool m_Sorted = false;
        private ListSortDirection m_SortDirection =
        ListSortDirection.Ascending;
        private PropertyDescriptor m_SortProperty = null;

        public ObjectList(List<ObjectDefinition> definitions) :base (definitions)
        {
        }


        protected override bool SupportsSearchingCore
        {
            get
            {
                return true;
            }
        }


        protected override bool SupportsSortingCore
        {
            get
            {
                return true;
            }
        }


        protected override bool IsSortedCore
        {
            get
            {
                return m_Sorted;
            }
        }


        protected override ListSortDirection SortDirectionCore
        {
            get
            {
                return m_SortDirection;
            }
        }


        protected override PropertyDescriptor SortPropertyCore
        {
            get
            {
                return m_SortProperty;
            }
        }


        protected override void ApplySortCore(PropertyDescriptor prop,
        ListSortDirection direction)
        {
            m_SortDirection = direction;
            m_SortProperty = prop;
            BOSortComparer<ObjectDefinition> comparer = new
            BOSortComparer<ObjectDefinition>(prop, direction);
            ApplySortInternal(comparer);
        }

        /// <summary>
        /// Helper class to do the actual sorting work.
        /// </summary>
        /// 
        private void ApplySortInternal(BOSortComparer<ObjectDefinition> comparer)
        {

            List<ObjectDefinition> listRef = this.Items as List<ObjectDefinition>;
            if (listRef == null)
                return;

            //let List<T> do the actual sorting based on your comparer
            listRef.Sort(comparer);
            m_Sorted = true;

            OnListChanged(new
            ListChangedEventArgs(ListChangedType.Reset, -1));
        }


        class BOSortComparer<T> : IComparer<T>
        {
            private PropertyDescriptor m_PropDesc = null;
            private ListSortDirection m_Direction = ListSortDirection.Ascending;

            public BOSortComparer(PropertyDescriptor propDesc, ListSortDirection
            direction)
            {
                m_PropDesc = propDesc;
                m_Direction = direction;
            }

            int IComparer<T>.Compare(T x, T y)
            {
                object xValue = m_PropDesc.GetValue(x);
                object yValue = m_PropDesc.GetValue(y);
                return CompareValues(xValue, yValue, m_Direction);
            }

            private int CompareValues(object xValue, object yValue,
            ListSortDirection direction)
            {
                int retValue = 0;
                if (xValue is IComparable) //can ask the x value
                {
                    retValue = ((IComparable)xValue).CompareTo(yValue);
                }
                else if (yValue is IComparable) //can ask the y value
                {
                    retValue = ((IComparable)yValue).CompareTo(xValue);
                }
                //not comparable, compare string representations
                else if (!xValue.Equals(yValue))
                {
                    retValue = xValue.ToString().CompareTo(yValue.ToString());
                }
                if (direction == ListSortDirection.Ascending)
                    return retValue;
                else
                    return retValue * -1;

            }
        }
    }
}
