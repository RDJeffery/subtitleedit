using System;
using System.Collections.Generic;
using Avalonia.Input;

namespace SubtitleEdit.Avalonia.Input
{
    public static class KeyboardShortcuts
    {
        public static readonly Dictionary<KeyGesture, string> Shortcuts = new()
        {
            // File operations
            { new KeyGesture(Key.O, KeyModifiers.Control), "Open subtitle file" },
            { new KeyGesture(Key.S, KeyModifiers.Control), "Save subtitle file" },
            { new KeyGesture(Key.S, KeyModifiers.Control | KeyModifiers.Shift), "Save subtitle file as..." },
            { new KeyGesture(Key.Q, KeyModifiers.Control), "Quit application" },

            // Video playback
            { new KeyGesture(Key.Space), "Play/Pause" },
            { new KeyGesture(Key.Escape), "Stop" },
            { new KeyGesture(Key.Left), "Previous subtitle" },
            { new KeyGesture(Key.Right), "Next subtitle" },
            { new KeyGesture(Key.Up), "Previous subtitle" },
            { new KeyGesture(Key.Down), "Next subtitle" },
            { new KeyGesture(Key.Home), "Go to first subtitle" },
            { new KeyGesture(Key.End), "Go to last subtitle" },
            { new KeyGesture(Key.PageUp), "Previous 10 subtitles" },
            { new KeyGesture(Key.PageDown), "Next 10 subtitles" },

            // Subtitle editing
            { new KeyGesture(Key.Insert), "Add new subtitle" },
            { new KeyGesture(Key.Delete), "Delete selected subtitle" },
            { new KeyGesture(Key.Z, KeyModifiers.Control), "Undo" },
            { new KeyGesture(Key.Y, KeyModifiers.Control), "Redo" },
            { new KeyGesture(Key.S, KeyModifiers.Control | KeyModifiers.Alt), "Split subtitle" },
            { new KeyGesture(Key.M, KeyModifiers.Control | KeyModifiers.Alt), "Merge subtitles" },

            // Timing adjustments
            { new KeyGesture(Key.Left, KeyModifiers.Control), "Move subtitle start time back" },
            { new KeyGesture(Key.Right, KeyModifiers.Control), "Move subtitle start time forward" },
            { new KeyGesture(Key.Left, KeyModifiers.Control | KeyModifiers.Shift), "Move subtitle end time back" },
            { new KeyGesture(Key.Right, KeyModifiers.Control | KeyModifiers.Shift), "Move subtitle end time forward" },
            { new KeyGesture(Key.Up, KeyModifiers.Control), "Adjust timing up" },
            { new KeyGesture(Key.Down, KeyModifiers.Control), "Adjust timing down" },

            // Text editing
            { new KeyGesture(Key.F7), "Spell check" },
            { new KeyGesture(Key.F8), "Translate" },
            { new KeyGesture(Key.F9), "Style" },
            { new KeyGesture(Key.F10), "Settings" },

            // Search and navigation
            { new KeyGesture(Key.F, KeyModifiers.Control), "Find" },
            { new KeyGesture(Key.H, KeyModifiers.Control), "Replace" },
            { new KeyGesture(Key.G, KeyModifiers.Control), "Go to line" },
            { new KeyGesture(Key.F3), "Find next" },
            { new KeyGesture(Key.F3, KeyModifiers.Shift), "Find previous" },
        };

        public static string GetShortcutDescription(KeyGesture gesture)
        {
            return Shortcuts.TryGetValue(gesture, out var description) ? description : string.Empty;
        }

        public static KeyGesture? GetShortcutForAction(string action)
        {
            foreach (var shortcut in Shortcuts)
            {
                if (shortcut.Value.Equals(action, StringComparison.OrdinalIgnoreCase))
                {
                    return shortcut.Key;
                }
            }
            return null;
        }
    }
} 