using System.Globalization;

namespace Stasevich353502.UI.ValueConverters;

public class IdToImageSourceConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        string imagesFolderPath;
        try
        {
            imagesFolderPath = Path.Combine(FileSystem.AppDataDirectory, "Images");

            if (!Directory.Exists(imagesFolderPath))
            {
                Directory.CreateDirectory(imagesFolderPath);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(
                $"Error getting AppDataDirectory or creating Images folder: {ex.Message}");
            return ImageSource.FromFile("placeholder.png");
        }

        if (value is not Guid id || id == Guid.Empty)
        {
            System.Diagnostics.Debug.WriteLine("SushiToImageSourceConverter: Value is not Sushi or Id is invalid. Using placeholder.");
            return ImageSource.FromFile("placeholder.png");
        }

        string fileName = $"{id}.png";
        string imagePath = Path.Combine(imagesFolderPath, fileName);

        if (File.Exists(imagePath))
        {
            // System.Diagnostics.Debug.WriteLine($"SushiToImageSourceConverter: Loading image from {imagePath}");
            // Чтобы избежать проблем с кэшированием, особенно на Android при обновлении файла:
            // return ImageSource.FromStream(() => File.OpenRead(imagePath));
            // Или, если это вызывает проблемы с производительностью на больших списках,
            // можно попробовать FileImageSource с выставленным CachingEnabled=false,
            // но FromStream надежнее для избежания кэша.
            // Пока оставим FromFile, но имейте в виду кэширование.
            return ImageSource.FromFile(imagePath);
        }
        else
        {
            System.Diagnostics.Debug.WriteLine($"SushiToImageSourceConverter: File not found {imagePath}. Using placeholder.");
            return ImageSource.FromFile("placeholder.png");
        }
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}