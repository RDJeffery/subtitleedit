using System;
using System.Threading.Tasks;

namespace SubtitleEdit.Avalonia.Services.Interfaces
{
    public interface IVideoService
    {
        bool IsPlaying { get; }
        Task LoadVideoAsync(string filePath);
        Task PlayAsync();
        Task PauseAsync();
        Task StopAsync();
        Task SeekAsync(TimeSpan position);
        Task SetVolumeAsync(double volume);
        event EventHandler<TimeSpan> PositionChanged;
        event EventHandler<TimeSpan> DurationChanged;
    }
} 