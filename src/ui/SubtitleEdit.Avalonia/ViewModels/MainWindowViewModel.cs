using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using SubtitleEdit.Avalonia.Commands;
using SubtitleEdit.Avalonia.Models;
using SubtitleEdit.Avalonia.Services;
using SubtitleEdit.Avalonia.Services.Interfaces;
using System.Diagnostics;

namespace SubtitleEdit.Avalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ISubtitleService _subtitleService;
        private readonly IVideoService _videoService;
        private readonly ILanguageService _languageService;
        private readonly Window? _window;

        private string _title = "SubtitleEdit";
        private bool _isLoading;
        private string _statusMessage = string.Empty;
        private double _videoPosition;
        private double _videoDuration;
        private double _volume = 1.0;
        private SubtitleItem? _selectedSubtitle;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        public double VideoPosition
        {
            get => _videoPosition;
            set
            {
                if (SetProperty(ref _videoPosition, value))
                {
                    _videoService.SeekAsync(TimeSpan.FromSeconds(value));
                    OnPropertyChanged(nameof(CurrentVideoTime));
                }
            }
        }

        public double VideoDuration
        {
            get => _videoDuration;
            set
            {
                if (SetProperty(ref _videoDuration, value))
                {
                    OnPropertyChanged(nameof(TotalVideoTime));
                }
            }
        }

        public double Volume
        {
            get => _volume;
            set
            {
                if (SetProperty(ref _volume, value))
                {
                    _videoService.SetVolumeAsync(value);
                }
            }
        }

        public string VideoTimeDisplay => $"{TimeSpan.FromSeconds(VideoPosition):hh\\:mm\\:ss} / {TimeSpan.FromSeconds(VideoDuration):hh\\:mm\\:ss}";

        public string CurrentVideoTime => TimeSpan.FromSeconds(VideoPosition).ToString(@"hh\:mm\:ss\.fff");
        public string TotalVideoTime => TimeSpan.FromSeconds(VideoDuration).ToString(@"hh\:mm\:ss\.fff");

        public ObservableCollection<SubtitleItem> Subtitles { get; } = new();

        public SubtitleItem? SelectedSubtitle
        {
            get => _selectedSubtitle;
            set => SetProperty(ref _selectedSubtitle, value);
        }

        public ICommand OpenCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand ExitCommand { get; }
        public ICommand UndoCommand { get; }
        public ICommand RedoCommand { get; }
        public ICommand PlayPauseCommand { get; }
        public ICommand StopCommand { get; }
        public ICommand PreviousSubtitleCommand { get; }
        public ICommand NextSubtitleCommand { get; }
        public ICommand SpellCheckCommand { get; }
        public ICommand TranslateCommand { get; }
        public ICommand SettingsCommand { get; }
        public ICommand AddSubtitleCommand { get; }
        public ICommand DeleteSubtitleCommand { get; }
        public ICommand SplitSubtitleCommand { get; }
        public ICommand MergeSubtitlesCommand { get; }
        public ICommand StyleCommand { get; }

        public bool IsPlaying => _videoService.IsPlaying;

        public MainWindowViewModel(Window? window = null)
        {
            _window = window;
            _subtitleService = ServiceLocator.Instance.GetService<ISubtitleService>();
            _videoService = ServiceLocator.Instance.GetService<IVideoService>();
            _languageService = ServiceLocator.Instance.GetService<ILanguageService>();

            OpenCommand = new AsyncRelayCommand(OpenFileAsync);
            SaveCommand = new AsyncRelayCommand(SaveFileAsync);
            ExitCommand = new RelayCommand(Exit);
            UndoCommand = new RelayCommand(Undo);
            RedoCommand = new RelayCommand(Redo);
            PlayPauseCommand = new AsyncRelayCommand(PlayPauseAsync);
            StopCommand = new AsyncRelayCommand(StopAsync);
            PreviousSubtitleCommand = new RelayCommand(PreviousSubtitle);
            NextSubtitleCommand = new RelayCommand(NextSubtitle);
            SpellCheckCommand = new RelayCommand(SpellCheck);
            TranslateCommand = new RelayCommand(Translate);
            SettingsCommand = new RelayCommand(OpenSettings);
            AddSubtitleCommand = new RelayCommand(AddSubtitle);
            DeleteSubtitleCommand = new RelayCommand(DeleteSubtitle);
            SplitSubtitleCommand = new RelayCommand(SplitSubtitle);
            MergeSubtitlesCommand = new RelayCommand(MergeSubtitles);
            StyleCommand = new RelayCommand(OpenStyleDialog);

            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            try
            {
                IsLoading = true;
                StatusMessage = "Initializing...";
                
                await _languageService.LoadLanguageAsync("en-US");
                
                StatusMessage = "Ready";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task OpenFileAsync()
        {
            try
            {
                if (_window is null) return;

                var options = new FilePickerOpenOptions
                {
                    Title = "Open Subtitle File",
                    AllowMultiple = false,
                    FileTypeFilter = new[]
                    {
                        new FilePickerFileType("Subtitle Files")
                        {
                            Patterns = new[] { "*.srt", "*.ass", "*.ssa", "*.sub" }
                        },
                        new FilePickerFileType("All Files")
                        {
                            Patterns = new[] { "*.*" }
                        }
                    }
                };

                var result = await _window.StorageProvider.OpenFilePickerAsync(options);
                if (result.Count > 0)
                {
                    await _subtitleService.LoadSubtitleAsync(result[0].Path.LocalPath);
                    StatusMessage = "File loaded successfully";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error opening file: {ex.Message}";
            }
        }

        private async Task SaveFileAsync()
        {
            try
            {
                if (_window is null) return;

                var options = new FilePickerSaveOptions
                {
                    Title = "Save Subtitle File",
                    FileTypeChoices = new[]
                    {
                        new FilePickerFileType("SubRip Subtitle")
                        {
                            Patterns = new[] { "*.srt" }
                        },
                        new FilePickerFileType("Advanced SubStation Alpha")
                        {
                            Patterns = new[] { "*.ass" }
                        },
                        new FilePickerFileType("SubStation Alpha")
                        {
                            Patterns = new[] { "*.ssa" }
                        }
                    }
                };

                var result = await _window.StorageProvider.SaveFilePickerAsync(options);
                if (result is not null)
                {
                    await _subtitleService.SaveSubtitleAsync(result.Path.LocalPath);
                    StatusMessage = "File saved successfully";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error saving file: {ex.Message}";
            }
        }

        private void Exit()
        {
            _window?.Close();
        }

        private void Undo()
        {
            // TODO: Implement undo functionality
            StatusMessage = "Undo";
        }

        private void Redo()
        {
            // TODO: Implement redo functionality
            StatusMessage = "Redo";
        }

        private async Task PlayPauseAsync()
        {
            try
            {
                if (_videoService.IsPlaying)
                {
                    await _videoService.PauseAsync();
                }
                else
                {
                    await _videoService.PlayAsync();
                }
                OnPropertyChanged(nameof(IsPlaying));
            }
            catch (Exception ex)
            {
                // TODO: Show error message
                Debug.WriteLine($"Error in PlayPauseAsync: {ex}");
            }
        }

        private async Task StopAsync()
        {
            try
            {
                await _videoService.StopAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
        }

        private void PreviousSubtitle()
        {
            // TODO: Implement previous subtitle functionality
            StatusMessage = "Previous subtitle";
        }

        private void NextSubtitle()
        {
            // TODO: Implement next subtitle functionality
            StatusMessage = "Next subtitle";
        }

        private void SpellCheck()
        {
            // TODO: Implement spell check functionality
            StatusMessage = "Spell check not implemented";
        }

        private void Translate()
        {
            // TODO: Implement translation functionality
            StatusMessage = "Translation not implemented";
        }

        private void OpenSettings()
        {
            // TODO: Implement settings dialog
            StatusMessage = "Settings not implemented";
        }

        private void AddSubtitle()
        {
            try
            {
                var currentTime = TimeSpan.FromSeconds(VideoPosition);
                var newSubtitle = new SubtitleItem
                {
                    Number = Subtitles.Count + 1,
                    StartTime = currentTime,
                    EndTime = currentTime.Add(TimeSpan.FromSeconds(3)),
                    Text = string.Empty
                };
                Subtitles.Add(newSubtitle);
                SelectedSubtitle = newSubtitle;
                StatusMessage = "New subtitle added";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error adding subtitle: {ex.Message}";
            }
        }

        private void DeleteSubtitle()
        {
            if (SelectedSubtitle == null) return;

            try
            {
                Subtitles.Remove(SelectedSubtitle);
                // Renumber remaining subtitles
                for (int i = 0; i < Subtitles.Count; i++)
                {
                    Subtitles[i].Number = i + 1;
                }
                SelectedSubtitle = null;
                StatusMessage = "Subtitle deleted";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error deleting subtitle: {ex.Message}";
            }
        }

        private void SplitSubtitle()
        {
            if (SelectedSubtitle == null) return;

            try
            {
                var currentTime = TimeSpan.FromSeconds(VideoPosition);
                if (currentTime <= SelectedSubtitle.StartTime || currentTime >= SelectedSubtitle.EndTime)
                {
                    StatusMessage = "Cannot split subtitle at current position";
                    return;
                }

                var newSubtitle = new SubtitleItem
                {
                    Number = SelectedSubtitle.Number + 1,
                    StartTime = currentTime,
                    EndTime = SelectedSubtitle.EndTime,
                    Text = SelectedSubtitle.Text,
                    Translation = SelectedSubtitle.Translation
                };

                SelectedSubtitle.EndTime = currentTime;
                Subtitles.Insert(SelectedSubtitle.Number, newSubtitle);

                // Renumber remaining subtitles
                for (int i = 0; i < Subtitles.Count; i++)
                {
                    Subtitles[i].Number = i + 1;
                }

                SelectedSubtitle = newSubtitle;
                StatusMessage = "Subtitle split";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error splitting subtitle: {ex.Message}";
            }
        }

        private void MergeSubtitles()
        {
            if (SelectedSubtitle == null || SelectedSubtitle.Number >= Subtitles.Count) return;

            try
            {
                var nextSubtitle = Subtitles[SelectedSubtitle.Number];
                SelectedSubtitle.EndTime = nextSubtitle.EndTime;
                SelectedSubtitle.Text += Environment.NewLine + nextSubtitle.Text;
                if (!string.IsNullOrEmpty(nextSubtitle.Translation))
                {
                    SelectedSubtitle.Translation += Environment.NewLine + nextSubtitle.Translation;
                }

                Subtitles.Remove(nextSubtitle);
                // Renumber remaining subtitles
                for (int i = 0; i < Subtitles.Count; i++)
                {
                    Subtitles[i].Number = i + 1;
                }

                StatusMessage = "Subtitles merged";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error merging subtitles: {ex.Message}";
            }
        }

        private void OpenStyleDialog()
        {
            // TODO: Implement style dialog
            StatusMessage = "Style dialog not implemented";
        }
    }
} 