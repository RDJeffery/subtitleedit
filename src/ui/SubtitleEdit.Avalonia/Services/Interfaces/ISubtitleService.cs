using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
    }
} 