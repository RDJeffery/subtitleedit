using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SubtitleEdit.Avalonia.Models;

namespace SubtitleEdit.Avalonia.Services.Interfaces
{
    public interface ISubtitleService
    {
        Task LoadSubtitleAsync(string filePath);
        Task SaveSubtitleAsync(string filePath);
        bool HasUnsavedChanges { get; }
        Task<bool> ImportSubtitleAsync(string filePath);
        Task<bool> ExportSubtitleAsync(string filePath, string format);
        Task<bool> ValidateSubtitleAsync();
        Task<bool> FixSubtitleTimingAsync();
        Task<bool> TranslateSubtitleAsync(string targetLanguage);
        Task<bool> OCRSubtitleAsync(string videoPath);
        Task<SubtitleItem?> AddSubtitleAsync(TimeSpan startTime, TimeSpan endTime, string text);
        Task<bool> DeleteSubtitleAsync(int index);
        Task<bool> SplitSubtitleAsync(int index, TimeSpan splitTime);
        Task<bool> MergeSubtitlesAsync(int index);
        Task<List<SubtitleItem>> GetSubtitlesAsync();
        Task<SubtitleItem?> GetSubtitleAsync(int index);
        Task<bool> UpdateSubtitleAsync(int index, SubtitleItem subtitle);
    }
} 