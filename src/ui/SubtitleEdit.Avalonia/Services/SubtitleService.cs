using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Nikse.SubtitleEdit.Core.Common;
using Nikse.SubtitleEdit.Core.SubtitleFormats;
using SubtitleEdit.Avalonia.Models;
using SubtitleEdit.Avalonia.Services.Interfaces;

namespace SubtitleEdit.Avalonia.Services
{
    public interface ISubtitleService
    {
        Task<bool> LoadSubtitleAsync(string filePath);
        Task<bool> SaveSubtitleAsync(string filePath);
        Task<SubtitleItem?> AddSubtitleAsync(TimeSpan startTime, TimeSpan endTime, string text);
        Task<bool> DeleteSubtitleAsync(int index);
        Task<bool> SplitSubtitleAsync(int index, TimeSpan splitTime);
        Task<bool> MergeSubtitlesAsync(int index);
        Task<List<SubtitleItem>> GetSubtitlesAsync();
        Task<SubtitleItem?> GetSubtitleAsync(int index);
        Task<bool> UpdateSubtitleAsync(int index, SubtitleItem subtitle);
    }

    public class SubtitleService : ISubtitleService
    {
        private Subtitle _subtitle;
        private SubtitleFormat _format;

        public SubtitleService()
        {
            _subtitle = new Subtitle();
            _format = new SubRip();
        }

        public async Task<bool> LoadSubtitleAsync(string filePath)
        {
            try
            {
                var lines = await File.ReadAllLinesAsync(filePath);
                _format.LoadSubtitle(_subtitle, new List<string>(lines), filePath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> SaveSubtitleAsync(string filePath)
        {
            try
            {
                var text = _format.ToText(_subtitle, string.Empty);
                await File.WriteAllTextAsync(filePath, text);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<SubtitleItem?> AddSubtitleAsync(TimeSpan startTime, TimeSpan endTime, string text)
        {
            try
            {
                var paragraph = new Paragraph
                {
                    Number = _subtitle.Paragraphs.Count + 1,
                    StartTime = new TimeCode(startTime),
                    EndTime = new TimeCode(endTime),
                    Text = text
                };
                _subtitle.Paragraphs.Add(paragraph);
                return ConvertToSubtitleItem(paragraph);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteSubtitleAsync(int index)
        {
            try
            {
                if (index >= 0 && index < _subtitle.Paragraphs.Count)
                {
                    _subtitle.Paragraphs.RemoveAt(index);
                    _subtitle.Renumber();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> SplitSubtitleAsync(int index, TimeSpan splitTime)
        {
            try
            {
                if (index >= 0 && index < _subtitle.Paragraphs.Count)
                {
                    var paragraph = _subtitle.Paragraphs[index];
                    if (splitTime > paragraph.StartTime.TimeSpan && splitTime < paragraph.EndTime.TimeSpan)
                    {
                        var newParagraph = new Paragraph
                        {
                            Number = paragraph.Number + 1,
                            StartTime = new TimeCode(splitTime),
                            EndTime = paragraph.EndTime,
                            Text = paragraph.Text
                        };
                        paragraph.EndTime = new TimeCode(splitTime);
                        _subtitle.Paragraphs.Insert(index + 1, newParagraph);
                        _subtitle.Renumber();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> MergeSubtitlesAsync(int index)
        {
            try
            {
                if (index >= 0 && index < _subtitle.Paragraphs.Count - 1)
                {
                    var paragraph1 = _subtitle.Paragraphs[index];
                    var paragraph2 = _subtitle.Paragraphs[index + 1];
                    paragraph1.EndTime = paragraph2.EndTime;
                    paragraph1.Text += Environment.NewLine + paragraph2.Text;
                    _subtitle.Paragraphs.RemoveAt(index + 1);
                    _subtitle.Renumber();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<SubtitleItem>> GetSubtitlesAsync()
        {
            var items = new List<SubtitleItem>();
            foreach (var paragraph in _subtitle.Paragraphs)
            {
                items.Add(ConvertToSubtitleItem(paragraph));
            }
            return items;
        }

        public async Task<SubtitleItem?> GetSubtitleAsync(int index)
        {
            if (index >= 0 && index < _subtitle.Paragraphs.Count)
            {
                return ConvertToSubtitleItem(_subtitle.Paragraphs[index]);
            }
            return null;
        }

        public async Task<bool> UpdateSubtitleAsync(int index, SubtitleItem subtitle)
        {
            try
            {
                if (index >= 0 && index < _subtitle.Paragraphs.Count)
                {
                    var paragraph = _subtitle.Paragraphs[index];
                    paragraph.Number = subtitle.Number;
                    paragraph.StartTime = new TimeCode(subtitle.StartTime);
                    paragraph.EndTime = new TimeCode(subtitle.EndTime);
                    paragraph.Text = subtitle.Text;
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static SubtitleItem ConvertToSubtitleItem(Paragraph paragraph)
        {
            return new SubtitleItem
            {
                Number = paragraph.Number,
                StartTime = paragraph.StartTime.TimeSpan,
                EndTime = paragraph.EndTime.TimeSpan,
                Text = paragraph.Text
            };
        }
    }
} 