using System.Globalization;

namespace Stasevich353502.UI.ValueConverters;

public class RateToColorValueConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if ((int)value < 4)
            return Color.FromRgb(255, 0, 0);
        return Color.FromArgb("#FFD3D3D3");
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}