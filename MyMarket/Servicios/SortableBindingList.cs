using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MyMarket.Servicios;

/// <summary>
///     Una implementación de BindingList que soporta ordenamiento.
/// </summary>
public class SortableBindingList<T> : BindingList<T>
{
    private bool _isSorted;
    private PropertyDescriptor? _sortProperty;
    private ListSortDirection _sortDirection;

    public SortableBindingList()
    {
    }

    public SortableBindingList(IList<T> list) : base(list)
    {
    }

    protected override bool SupportsSortingCore => true;

    protected override bool IsSortedCore => _isSorted;

    protected override PropertyDescriptor? SortPropertyCore => _sortProperty;

    protected override ListSortDirection SortDirectionCore => _sortDirection;

    protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
    {
        // Note: BindingList<T>.Items is typically a List<T>, but we verify the type first
        var items = Items as List<T>;
        if (items == null)
        {
            // If the underlying collection is not a List<T>, we cannot sort
            throw new NotSupportedException("The underlying collection must be a List<T> to support sorting.");
        }

        var propertyInfo = typeof(T).GetProperty(prop.Name);
        if (propertyInfo == null)
        {
            return;
        }

        var comparer = new PropertyComparer<T>(propertyInfo, direction);
        items.Sort(comparer);

        _sortProperty = prop;
        _sortDirection = direction;
        _isSorted = true;

        OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
    }

    protected override void RemoveSortCore()
    {
        _isSorted = false;
        _sortProperty = null;
        _sortDirection = ListSortDirection.Ascending;

        OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
    }

    private class PropertyComparer<TItem> : IComparer<TItem>
    {
        private readonly System.Reflection.PropertyInfo _propertyInfo;
        private readonly ListSortDirection _direction;

        public PropertyComparer(System.Reflection.PropertyInfo propertyInfo, ListSortDirection direction)
        {
            _propertyInfo = propertyInfo;
            _direction = direction;
        }

        public int Compare(TItem? x, TItem? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return _direction == ListSortDirection.Ascending ? -1 : 1;
            if (y == null) return _direction == ListSortDirection.Ascending ? 1 : -1;

            // Note: Using reflection for property access. For very large datasets,
            // consider using compiled expression trees for better performance.
            var xValue = _propertyInfo.GetValue(x, null);
            var yValue = _propertyInfo.GetValue(y, null);

            if (xValue == null && yValue == null) return 0;
            if (xValue == null) return _direction == ListSortDirection.Ascending ? -1 : 1;
            if (yValue == null) return _direction == ListSortDirection.Ascending ? 1 : -1;

            int result;
            if (xValue is IComparable comparableX)
            {
                result = comparableX.CompareTo(yValue);
            }
            else if (xValue is string strX && yValue is string strY)
            {
                result = string.Compare(strX, strY, StringComparison.CurrentCultureIgnoreCase);
            }
            else
            {
                result = string.Compare(xValue.ToString(), yValue.ToString(), StringComparison.CurrentCultureIgnoreCase);
            }

            return _direction == ListSortDirection.Ascending ? result : -result;
        }
    }
}
