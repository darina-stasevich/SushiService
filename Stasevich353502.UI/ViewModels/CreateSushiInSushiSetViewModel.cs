using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Stasevich353502.Application.SushiUseCases.Commands;
using Stasevich353502.UI.Helpers;

namespace Stasevich353502.UI.ViewModels;

[QueryProperty(nameof(CurrentSet), "SelectedSushiSetObject")]
public partial class CreateSushiInSushiSetViewModel : ObservableObject
{
    private readonly IMediator _mediator;

    [ObservableProperty] private SushiSet currentSet;

    [ObservableProperty] private string? sushiType;

    [ObservableProperty] private string? name;

    [ObservableProperty] private string? amount;

    [ObservableProperty] private ImageSource _previewImageSource;

    private FileResult? _pickedPhotoResult;

    public CreateSushiInSushiSetViewModel(IMediator mediator)
    {
        _mediator = mediator;
        PreviewImageSource = ImageSource.FromFile("placeholder.png");
    }

    [RelayCommand]
    private async Task PickImage()
    {
        try
        {
            var pngFileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.Android, new[] { "image/png" } },
                    { DevicePlatform.WinUI, new[] { ".png" } },
                });

            var pickOptions = new PickOptions
            {
                PickerTitle = "Выберите PNG изображение",
                FileTypes = pngFileType
            };

            var photo = await FilePicker.Default.PickAsync(pickOptions);

            if (photo != null)
            {
                if (!photo.FileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase) &&
                    !string.Equals(photo.ContentType, "image/png", StringComparison.OrdinalIgnoreCase))
                {
                    Debug.WriteLine(
                        $"[PickImageCommand] Выбран файл не PNG формата: {photo.FileName}, ContentType: {photo.ContentType}.");
                    await Shell.Current.DisplayAlert("Ошибка формата", "Пожалуйста, выберите файл в формате PNG.",
                        "OK");
                    _pickedPhotoResult = null; 
                    return;
                }

                _pickedPhotoResult = photo;

                byte[] imageBytes;
                try
                {
                    using (var fileStream = await _pickedPhotoResult.OpenReadAsync())
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await fileStream.CopyToAsync(memoryStream);
                            imageBytes = memoryStream.ToArray();
                        }
                    }
                    PreviewImageSource = ImageSource.FromStream(() => new MemoryStream(imageBytes));
                    Debug.WriteLine($"[PickImageCommand] PreviewImageSource обновлен новым выбором. Размер: {imageBytes.Length} байт.");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[PickImageCommand] Ошибка чтения файла в байты: {ex.Message}");
                    await Shell.Current.DisplayAlert("Ошибка", "Не удалось прочитать выбранный файл.", "OK");
                    _pickedPhotoResult = null;
                    // Здесь можно решить, сбрасывать ли PreviewImageSource на placeholder или оставить предыдущее
                    // PreviewImageSource = ImageSource.FromFile("placeholder.png"); 
                    return;
                }
            }
            else
            {
                Debug.WriteLine("[PickImageCommand] Выбор изображения отменен или произошла ошибка.");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[PickImageCommand] Исключение: {ex.Message}");
            await Shell.Current.DisplayAlert("Ошибка", "Не удалось выбрать изображение.", "OK");
        }
    }

    [RelayCommand]
    private async Task AddSushiToSushiSet()
    {
        await AddSushi();
    }

    private async Task AddSushi()
    {
        var nameValidationResult = await ValidateType(SushiType);
        var priceValidationResult = await ValidateName(Name);
        var weightValidationResult = await ValidateAmount(Amount);

        if (nameValidationResult && priceValidationResult && weightValidationResult)
        {
            try
            {
                var sushi = await _mediator.Send(new AddSushiCommand(new SushiData(name, sushiType), int.Parse(amount),
                    currentSet.Id));
                if (sushi != null)
                {
                    if (_pickedPhotoResult != null)
                    {
                        string imagesFolderPath = ImageHelper.GetImagesFolderPath();
                        var guidString = sushi.Id.ToString();
                        string targetFileName = $"{guidString}.png";
                        
                        string destinationPath = Path.Combine(imagesFolderPath, targetFileName);
                        using var sourceStream = await _pickedPhotoResult.OpenReadAsync();
                        using var destinationStream = File.Create(destinationPath);
                        await sourceStream.CopyToAsync(destinationStream);
                    }

                    _pickedPhotoResult = null;
                    PreviewImageSource = ImageSource.FromFile("placeholder.png");
                    
                    SushiType = string.Empty;
                    Name = string.Empty;
                    Amount = string.Empty;


                    await Shell.Current.DisplayAlert("Успех",
                        $"Суши {sushi.SushiData.SushiType} {sushi.SushiData.SushiName} успешно добавлен в сет {currentSet.Name}",
                        "Ок");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Ошибка",
                    $"Не удалось добавить суши в сет: {ex.Message}", "Ок");
            }
            finally
            {
                await Shell.Current.GoToAsync("..");
            }

        }
    }

    private async Task<bool> ValidateName(string? name)
    {
        if (string.IsNullOrEmpty(name))
        {
            await Shell.Current.DisplayAlert("Ошибка", "Название не должно быть пустым", "Ок");
            return false;
        }

        return true;
    }

    private async Task<bool> ValidateType(string? type)
    {
        if (string.IsNullOrEmpty(type))
        {
            await Shell.Current.DisplayAlert("Ошибка", "Тип не должен быть пустым", "Ок");
            return false;
        }

        return true;
    }

    private async Task<bool> ValidateAmount(string? amount)
    {
        if (string.IsNullOrEmpty(amount))
        {
            await Shell.Current.DisplayAlert("Ошибка", "Количество должно быть указано", "Ок");
            return false;
        }

        var parseResult = int.TryParse(amount, out var currentAmount);
        if (!parseResult)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Введите количество в корректном формате", "Ок");
            return false;
        }

        if (currentAmount <= 0)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Введите корректное количество", "Ок");
            return false;
        }

        return true;
    }
}