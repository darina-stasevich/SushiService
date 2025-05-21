using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Formats.Asn1;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Stasevich353502.Application.SushiSetUseCases.Queries;
using Stasevich353502.Application.SushiUseCases.Commands;
using Stasevich353502.UI.Helpers;

namespace Stasevich353502.UI.ViewModels;

[QueryProperty(nameof(CurrentSushi), "SelectedSushiObject")]
public partial class UpdateSushiViewModel : ObservableObject
{
    private readonly IMediator _mediator;

    [ObservableProperty]
    private Sushi _currentSushi;

    [ObservableProperty]
    string? _sushiType;

    [ObservableProperty]
    string? _name;

    [ObservableProperty] 
    string? _amount;

    [ObservableProperty]
    SushiSet _selectedSushiSet;
    public ObservableCollection<SushiSet> SushiSets { get; set; } = new();

    [ObservableProperty] private ImageSource _previewImageSource;

    private FileResult? _pickedPhotoResult;

    public UpdateSushiViewModel(IMediator mediator)
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
    
    public async Task GetSushiSets()
    {
        var sushiSets = await _mediator.Send(new GetAllSushiSetsQuery());
        if (sushiSets != null)
        {
            SushiSets.Clear();
            foreach (var sushiSet in sushiSets)
            {
                SushiSets.Add(sushiSet);
            }
        }

        SelectedSushiSet = SushiSets.First(x => x.Id == _currentSushi.SushiSetId);
        
    }
    
    async partial void OnCurrentSushiChanged(Sushi value)
    {
        if (value == null) return;

        SushiType = value.SushiData.SushiType;
        Name = value.SushiData.SushiName;
        Amount = value.Amount.ToString();

        string imagesFolderPath = ImageHelper.GetImagesFolderPath();
        string expectedFileName = $"{value.Id}.png";
        string fullPathToImage = Path.Combine(imagesFolderPath, expectedFileName);

        if (File.Exists(fullPathToImage))
        {
            PreviewImageSource = ImageSource.FromFile(fullPathToImage);
            Debug.WriteLine($"[OnCurrentSushiChanged] Загружено существующее изображение: {fullPathToImage}");
        }
        else
        {
            PreviewImageSource = ImageSource.FromFile("placeholder.png");
            Debug.WriteLine($"[OnCurrentSushiChanged] Существующее изображение не найдено, используется placeholder.");
        }

        _pickedPhotoResult = null;
       
        await GetSushiSets();
    }

    [RelayCommand]
    async Task UpdateSushi() => await UpdateCurrentSushi();

    private async Task UpdateCurrentSushi()
    {
        bool nameValidationResult = await ValidateName(Name);
        bool typeValidationResult = await ValidateType(SushiType);
        bool amountValidationResult = await ValidateAmount(Amount);
        if(nameValidationResult && typeValidationResult && amountValidationResult == false)
        {
            return;
        }
        try
        {
            if (_selectedSushiSet.Id != CurrentSushi.SushiSetId)
            {
                await _mediator.Send(new ChangeSushiSetCommand(CurrentSushi, _selectedSushiSet.Id));
            }

            await _mediator.Send(new UpdateSushiCommand(_currentSushi.Id, SushiType, Name, Int32.Parse(Amount)));
            
            if (_pickedPhotoResult != null)
            {
                string imagesFolderPath = ImageHelper.GetImagesFolderPath();
                var guidString = CurrentSushi.Id.ToString();
                string targetFileName = $"{guidString}.png";
                        
                string destinationPath = Path.Combine(imagesFolderPath, targetFileName);
                using var sourceStream = await _pickedPhotoResult.OpenReadAsync();
                using var destinationStream = File.Create(destinationPath);
                await sourceStream.CopyToAsync(destinationStream);
            }

            _pickedPhotoResult = null;

            await Shell.Current.DisplayAlert("Успех", "Изменено успешно", "Ок");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Ошибка", $"{ex.Message}", "Ок");
        }
        finally
        {
            await Shell.Current.GoToAsync("../");
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
        var parseResult = Int32.TryParse(amount, out var currentAmount);
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