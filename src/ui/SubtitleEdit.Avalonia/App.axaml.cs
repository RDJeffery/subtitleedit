using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Themes.Fluent;
using SubtitleEdit.Avalonia.Services;
using SubtitleEdit.Avalonia.Services.Interfaces;

namespace SubtitleEdit.Avalonia
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            InitializeServices();
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void InitializeServices()
        {
            var serviceLocator = ServiceLocator.Current;
            serviceLocator.RegisterService<ISubtitleService>(new SubtitleService());
            serviceLocator.RegisterService<IVideoService>(new VideoService());
            serviceLocator.RegisterService<ILanguageService>(new LanguageService());
        }
    }
} 