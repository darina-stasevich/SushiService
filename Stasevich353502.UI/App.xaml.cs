namespace Stasevich353502.UI;

public partial class App : Microsoft.Maui.Controls.Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
    protected override Window CreateWindow(IActivationState activationState)
    {
        var window = base.CreateWindow(activationState);
        
        // Устанавливаем минимальные размеры
        SetMinimumWindowSize(window);
        window.Width = 400;
        window.Height = 700;
        return window;
    }

    private void SetMinimumWindowSize(Window window)
    {
#if WINDOWS || MACCATALYST
        window.MinimumWidth = 400;
        window.MinimumHeight = 700;
#endif
    }
}