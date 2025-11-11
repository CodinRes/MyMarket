using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MyMarket.Servicios;

/// <summary>
///     Proporciona funcionalidad de ayuda para DataGridView, incluyendo ordenamiento por columnas.
/// </summary>
public static class DataGridViewHelper
{
    /// <summary>
    ///     Habilita el ordenamiento por click en el encabezado de columna para un DataGridView.
    /// </summary>
    public static void HabilitarOrdenamientoPorColumna(DataGridView dgv)
    {
        if (dgv == null)
        {
            throw new ArgumentNullException(nameof(dgv));
        }

        dgv.ColumnHeaderMouseClick += (sender, e) =>
        {
            if (sender is not DataGridView grid)
            {
                return;
            }

            var column = grid.Columns[e.ColumnIndex];
            
            // Verificar si la columna es ordenable
            if (column.SortMode == DataGridViewColumnSortMode.NotSortable)
            {
                return;
            }

            // Intentar ordenar via BindingSource si está disponible
            if (grid.DataSource is BindingSource bindingSource)
            {
                OrdenarBindingSource(bindingSource, column, grid);
            }
            else
            {
                // Fallback a ordenamiento directo del grid
                OrdenarDataGridView(grid, e.ColumnIndex);
            }
        };
    }

    private static void OrdenarDataGridView(DataGridView grid, int columnIndex)
    {
        var column = grid.Columns[columnIndex];
        var direction = ListSortDirection.Ascending;

        // Determinar la dirección del ordenamiento
        if (grid.Tag is string lastSort && lastSort == column.Name + "_asc")
        {
            direction = ListSortDirection.Descending;
            grid.Tag = column.Name + "_desc";
        }
        else
        {
            grid.Tag = column.Name + "_asc";
        }

        // Cambiar los glifos de ordenamiento en las columnas
        foreach (DataGridViewColumn col in grid.Columns)
        {
            col.HeaderCell.SortGlyphDirection = SortOrder.None;
        }

        column.HeaderCell.SortGlyphDirection = direction == ListSortDirection.Ascending
            ? SortOrder.Ascending
            : SortOrder.Descending;

        grid.Sort(column, direction);
    }

    private static void OrdenarBindingSource(BindingSource bindingSource, DataGridViewColumn column, DataGridView grid)
    {
        var direction = ListSortDirection.Ascending;
        var propertyName = column.DataPropertyName;

        if (string.IsNullOrEmpty(propertyName))
        {
            return;
        }

        // Determinar la dirección del ordenamiento usando el Tag del grid
        var sortKey = $"{propertyName}_asc";
        if (grid.Tag is string lastSort && lastSort == sortKey)
        {
            direction = ListSortDirection.Descending;
            grid.Tag = $"{propertyName}_desc";
        }
        else
        {
            grid.Tag = sortKey;
        }

        // Cambiar los glifos de ordenamiento solo en las columnas ordenables
        foreach (DataGridViewColumn col in grid.Columns)
        {
            if (col.SortMode != DataGridViewColumnSortMode.NotSortable)
            {
                col.HeaderCell.SortGlyphDirection = SortOrder.None;
            }
        }

        if (column.SortMode != DataGridViewColumnSortMode.NotSortable)
        {
            column.HeaderCell.SortGlyphDirection = direction == ListSortDirection.Ascending
                ? SortOrder.Ascending
                : SortOrder.Descending;
        }

        // Aplicar el ordenamiento
        // Primero intentar con IBindingList.ApplySort para soportar SortableBindingList
        if (bindingSource.List is IBindingList bindingList && bindingList.SupportsSorting)
        {
            // Obtener el tipo de elemento de la lista
            var listType = bindingSource.List.GetType();
            Type? itemType = null;
            
            if (listType.IsGenericType)
            {
                var genericArgs = listType.GetGenericArguments();
                if (genericArgs.Length > 0)
                {
                    itemType = genericArgs[0];
                }
            }
            
            if (itemType != null)
            {
                var props = TypeDescriptor.GetProperties(itemType);
                var property = props.Find(propertyName, true);
                if (property != null)
                {
                    bindingList.ApplySort(property, direction);
                    return;
                }
            }
        }
        
        // Fallback a ordenamiento por string para listas que soporten IBindingListView
        try
        {
            bindingSource.Sort = $"{propertyName} {(direction == ListSortDirection.Ascending ? "ASC" : "DESC")}";
        }
        catch (NotSupportedException)
        {
            // La lista no soporta ordenamiento. Ignorar el error silenciosamente.
        }
        catch (ArgumentException)
        {
            // Nombre de propiedad inválido. Ignorar el error silenciosamente.
        }
    }
}
