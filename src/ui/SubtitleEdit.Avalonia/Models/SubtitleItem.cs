using System;

namespace SubtitleEdit.Avalonia.Models
{
    public class SubtitleItem
    {
        public int Number { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public TimeSpan Duration => EndTime - StartTime;
        public string Text { get; set; } = string.Empty;
        public string Translation { get; set; } = string.Empty;
    }
} 