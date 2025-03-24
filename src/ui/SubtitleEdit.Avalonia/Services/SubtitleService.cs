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
    public class SubtitleService : ISubtitleService
    {
        private Subtitle _subtitle;
        private SubtitleFormat _format;
        private bool _hasUnsavedChanges;

        public SubtitleService()
        {
            _subtitle = new Subtitle();
            _format = new SubRip();
        }

        public bool HasUnsavedChanges => _hasUnsavedChanges;

        public async Task LoadSubtitleAsync(string filePath)
        {
            try
            {
                var lines = await File.ReadAllLinesAsync(filePath);
                _format.LoadSubtitle(_subtitle, new List<string>(lines), filePath);
                _hasUnsavedChanges = false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SaveSubtitleAsync(string filePath)
        {
            try
            {
                var text = _format.ToText(_subtitle, string.Empty);
                await File.WriteAllTextAsync(filePath, text);
                _hasUnsavedChanges = false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ImportSubtitleAsync(string filePath)
        {
            try
            {
                await LoadSubtitleAsync(filePath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ExportSubtitleAsync(string filePath, string format)
        {
            try
            {
                await SaveSubtitleAsync(filePath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ValidateSubtitleAsync()
        {
            try
            {
                // TODO: Implement subtitle validation
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> FixSubtitleTimingAsync()
        {
            try
            {
                // TODO: Implement timing fix
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> TranslateSubtitleAsync(string targetLanguage)
        {
            try
            {
                // TODO: Implement translation
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> OCRSubtitleAsync(string videoPath)
        {
            try
            {
                // TODO: Implement OCR
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
                _hasUnsavedChanges = true;
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
                    _hasUnsavedChanges = true;
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
                        _hasUnsavedChanges = true;
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
                    _hasUnsavedChanges = true;
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
                    _hasUnsavedChanges = true;
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