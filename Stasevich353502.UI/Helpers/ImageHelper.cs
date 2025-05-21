namespace Stasevich353502.UI.Helpers;

public static class ImageHelper
{
    public static string GetImagesFolderPath()
    {
        string appDataDir = FileSystem.AppDataDirectory;

        string targetImagesPath = Path.Combine(appDataDir, "Images");

        if (!Directory.Exists(targetImagesPath))
        {
            Directory.CreateDirectory(targetImagesPath);
            System.Diagnostics.Debug.WriteLine($"[ImageHelper] Папка 'Images' создана по пути: {targetImagesPath}");
        }

        return targetImagesPath;
    }
}