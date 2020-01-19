using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterManagerProject.LoggerApp
{

    public class ObjectPropertyCompare<T> : IComparer<T>
    {
        /// <summary>
        /// 属性
        /// </summary>
        public PropertyDescriptor Property { get; set; }
        /// <summary>
        /// 排序方向
        /// </summary>
        public ListSortDirection Direction { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ObjectPropertyCompare()
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="property"></param>
        /// <param name="direction"></param>
        public ObjectPropertyCompare(PropertyDescriptor property, ListSortDirection direction)
        {
            Property = property;
            Direction = direction;
        }

        /// <summary>
        /// 比较两个对象
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(T x, T y)
        {
            object xValue = x.GetType().GetProperty(Property.Name).GetValue(x, null);
            object yValue = y.GetType().GetProperty(Property.Name).GetValue(y, null);

            int returnValue;

            if (xValue == null && yValue == null)
            {
                returnValue = 0;
            }
            else if (xValue == null)
            {
                returnValue = -1;
            }
            else if (yValue == null)
            {
                returnValue = 1;
            }
            else if (xValue is IComparable)
            {
                returnValue = ((IComparable)xValue).CompareTo(yValue);
            }
            else if (xValue.Equals(yValue))
            {
                returnValue = 0;
            }
            else
            {
                returnValue = xValue.ToString().CompareTo(yValue.ToString());
            }

            if (Direction == ListSortDirection.Ascending)
            {
                return returnValue;
            }
            else
            {
                return returnValue * -1;
            }
        }
    }
}
