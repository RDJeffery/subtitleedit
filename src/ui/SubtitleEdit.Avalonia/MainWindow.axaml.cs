using Avalonia.Controls;
using Avalonia.Input;
using SubtitleEdit.Avalonia.ViewModels;

namespace SubtitleEdit.Avalonia
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(this);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (DataContext is MainWindowViewModel viewModel)
            {
                viewModel.HandleKeyPressAsync(e);
            }
        }
    }
} 