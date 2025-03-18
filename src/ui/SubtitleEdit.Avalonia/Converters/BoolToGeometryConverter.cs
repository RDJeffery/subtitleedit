using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace SubtitleEdit.Avalonia.Converters
{
    public class BoolToGeometryConverter : IMultiValueConverter
    {
        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            if (values.Count != 3 || values[0] is not bool isPlaying || values[1] is not StreamGeometry playIcon || values[2] is not StreamGeometry pauseIcon)
                return null;

            return isPlaying ? pauseIcon : playIcon;
        }
    }
} 