using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SubtitleEdit.Avalonia.Services.Interfaces
{
    public interface ILanguageService
    {
        Task LoadLanguageAsync(string languageCode);
        Task<string> TranslateTextAsync(string text, string targetLanguage);
        Task<bool> SpellCheckAsync(string text);
        Task<IEnumerable<string>> GetSuggestionsAsync(string text);
        Task<bool> IsSpellingCorrectAsync(string text);
        string GetString(string key);
        string GetString(string key, params object[] args);
        bool IsLanguageLoaded { get; }
        string CurrentLanguage { get; }
        Task<bool> SetLanguageAsync(string languageCode);
        Task<IEnumerable<string>> GetAvailableLanguagesAsync();
        Task<bool> AddCustomTranslationAsync(string key, string value);
        Task<bool> SaveCustomTranslationsAsync();
    }
} 