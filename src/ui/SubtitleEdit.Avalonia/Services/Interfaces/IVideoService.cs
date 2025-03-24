using System;
using System.Threading.Tasks;

namespace SubtitleEdit.Avalonia.Services.Interfaces
{
    public interface IVideoService : IDisposable
    {
        bool IsPlaying { get; }
        double Volume { get; }
        TimeSpan Position { get; }
        TimeSpan Duration { get; }
        Task LoadVideoAsync(string filePath);
        Task PlayAsync();
        Task PauseAsync();
        Task StopAsync();
        Task SeekAsync(TimeSpan position);
        Task SetVolumeAsync(double volume);
        Task<bool> SetPlaybackRateAsync(double rate);
        Task<bool> TakeScreenshotAsync(string outputPath);
        Task<bool> ExtractAudioAsync(string outputPath);
        event EventHandler<TimeSpan> PositionChanged;
        event EventHandler<TimeSpan> DurationChanged;
    }
} 