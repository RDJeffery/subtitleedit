using Avalonia.Controls;
using Avalonia.Input;
using System;
using System.Text.RegularExpressions;

namespace SubtitleEdit.Avalonia.Controls
{
    public class TimeCodeTextBox : TextBox
    {
        private static readonly Regex FullTimeCodePattern = new Regex(@"^\d{2}:\d{2}:\d{2},\d{3}$");
        private static readonly Regex PartialTimeCodePattern = new Regex(@"^\d{0,2}:\d{0,2}:\d{0,2},\d{0,3}$");

        public TimeCodeTextBox()
        {
            TextInput += OnTextInput;
            KeyDown += OnKeyDown;
            Watermark = "00:00:00,000";
        }

        private void OnTextInput(object? sender, TextInputEventArgs e)
        {
            if (IsReadOnly || string.IsNullOrEmpty(e.Text)) return;

            var currentText = Text ?? string.Empty;
            var newText = currentText.Insert(CaretIndex, e.Text);

            // Allow only digits and separators
            if (!Regex.IsMatch(e.Text, @"[\d:,]"))
            {
                e.Handled = true;
                return;
            }

            // Auto-insert separators
            if (char.IsDigit(e.Text[0]))
            {
                if (CaretIndex == 2 || CaretIndex == 5)
                {
                    Text = currentText.Insert(CaretIndex, ":");
                    CaretIndex++;
                }
                else if (CaretIndex == 8)
                {
                    Text = currentText.Insert(CaretIndex, ",");
                    CaretIndex++;
                }
            }

            // Validate partial timecode
            if (!PartialTimeCodePattern.IsMatch(newText))
            {
                e.Handled = true;
            }
        }

        private void OnKeyDown(object? sender, KeyEventArgs e)
        {
            if (IsReadOnly) return;

            if (e.Key == Key.Back)
            {
                if (Text?.EndsWith(":") == true || Text?.EndsWith(",") == true)
                {
                    Text = Text.Substring(0, Text.Length - 1);
                    e.Handled = true;
                }
            }
        }

        public TimeSpan? GetTimeSpan()
        {
            if (string.IsNullOrEmpty(Text) || !FullTimeCodePattern.IsMatch(Text))
                return null;

            var parts = Text.Split(':', ',');
            if (parts.Length != 4)
                return null;

            if (int.TryParse(parts[0], out int hours) &&
                int.TryParse(parts[1], out int minutes) &&
                int.TryParse(parts[2], out int seconds) &&
                int.TryParse(parts[3], out int milliseconds))
            {
                return new TimeSpan(0, hours, minutes, seconds, milliseconds);
            }

            return null;
        }

        public void SetTimeSpan(TimeSpan timeSpan)
        {
            Text = $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2},{timeSpan.Milliseconds:D3}";
        }
    }
} 