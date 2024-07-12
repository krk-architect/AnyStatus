using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using AnyStatus.API.Widgets;

namespace AnyStatus.Apps.Windows.Infrastructure.Converters;

public class StatusIconConverter : IValueConverter
{
    private static readonly Dictionary<string, object> _iconCache = new();

    public virtual object Convert(object      value
                                , Type        targetType
                                , object      parameter
                                , CultureInfo culture)
    {
        if (value is null)
        {
            return null;
        }

        var key = Status.Icon(value.ToString());

        if (string.IsNullOrEmpty(key))
        {
            return null;
        }

        if (_iconCache.ContainsKey(key))
        {
            return _iconCache[key];
        }

        var keyParts = key.Split('.');

        if (keyParts.Length == 2 && Enum.TryParse(SupportedIconPacks.IconPacks[keyParts[0]], keyParts[1], out var kind))
        {
            _iconCache.TryAdd(key, kind);

            return _iconCache[key];
        }

        return null;
    }

    public object ConvertBack(object      value
                            , Type        targetType
                            , object      parameter
                            , CultureInfo culture) => throw new NotSupportedException();
}