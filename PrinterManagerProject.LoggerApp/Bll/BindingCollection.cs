using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterManagerProject.LoggerApp
{
    /// <summary>
    /// 提供支持数据绑定的泛型集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BindingCollection<T> : BindingList<T>
    {
        private bool _isSortedCore = true;
        private ListSortDirection _sortDirectionCore = ListSortDirection.Ascending;
        private PropertyDescriptor _sortPropertyCore = null;

        /// <summary>
        /// 是否已排序
        /// </summary>
        protected override bool IsSortedCore
        {
            get { return _isSortedCore; }
        }
        /// <summary>
        /// 获取列表的排序方向
        /// </summary>
        protected override ListSortDirection SortDirectionCore
        {
            get { return _sortDirectionCore; }
        }
        /// <summary>
        /// 获取用于对列表排序的属性说明符
        /// </summary>
        protected override PropertyDescriptor SortPropertyCore
        {
            get { return _sortPropertyCore; }
        }
        /// <summary>
        /// 是否支持排序
        /// </summary>
        protected override bool SupportsSortingCore
        {
            get { return true; }
        }
        /// <summary>
        /// 是否支持搜索
        /// </summary>
        protected override bool SupportsSearchingCore
        {
            get { return true; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BindingCollection()
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="list"></param>
        public BindingCollection(IList<T> list)
            : base(list)
        { }

        /// <summary>
        /// 对项排序
        /// </summary>
        /// <param name="property"></param>
        /// <param name="direction"></param>
        protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
        {
            List<T> items = this.Items as List<T>;

            if (items != null)
            {
                ObjectPropertyCompare<T> pc = new ObjectPropertyCompare<T>(property, direction);
                items.Sort(pc);
                _isSortedCore = true;
                _sortDirectionCore = direction;
                _sortPropertyCore = property;
            }
            else
            {
                _isSortedCore = false;
            }

            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        /// <summary>
        /// 移除排序
        /// </summary>
        protected override void RemoveSortCore()
        {
            _isSortedCore = false;
            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }
    }
}
