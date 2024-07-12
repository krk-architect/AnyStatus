using System.Collections.Generic;
using AnyStatus.API.Attributes;

namespace AnyStatus.Core.Themes;

internal class ThemeSource : IItemsSource
{
    private static readonly NameValueItem[] _items =
    [
        new ("Dark", "Dark"), new ("Light", "Light")
    ];

    public IEnumerable<NameValueItem> GetItems(object source) => _items;
}