# SubtitleEdit.Avalonia

A cross-platform subtitle editor built with Avalonia UI.

## Keyboard Shortcuts

### File Operations
- `Ctrl+O` - Open subtitle file
- `Ctrl+S` - Save subtitle file
- `Ctrl+Shift+S` - Save subtitle file as...
- `Ctrl+Q` - Quit application

### Video Playback
- `Space` - Play/Pause
- `Esc` - Stop
- `←` or `↑` - Previous subtitle
- `→` or `↓` - Next subtitle
- `Home` - Go to first subtitle
- `End` - Go to last subtitle
- `Page Up` - Previous 10 subtitles
- `Page Down` - Next 10 subtitles

### Subtitle Editing
- `Insert` - Add new subtitle
- `Delete` - Delete selected subtitle
- `Ctrl+Z` - Undo
- `Ctrl+Y` - Redo
- `Ctrl+Alt+S` - Split subtitle
- `Ctrl+Alt+M` - Merge subtitles

### Timing Adjustments
- `Ctrl+←` - Move subtitle start time back
- `Ctrl+→` - Move subtitle start time forward
- `Ctrl+Shift+←` - Move subtitle end time back
- `Ctrl+Shift+→` - Move subtitle end time forward
- `Ctrl+↑` - Adjust timing up
- `Ctrl+↓` - Adjust timing down

### Text Editing
- `F7` - Spell check
- `F8` - Translate
- `F9` - Style
- `F10` - Settings

### Search and Navigation
- `Ctrl+F` - Find
- `Ctrl+H` - Replace
- `Ctrl+G` - Go to line
- `F3` - Find next
- `Shift+F3` - Find previous

## Usage Tips

1. **Timing Adjustments**
   - Use the timing adjustment shortcuts to fine-tune subtitle synchronization
   - The default adjustment step is 100ms, but you can change this in settings

2. **Navigation**
   - Use arrow keys to move between subtitles
   - Use `Page Up` and `Page Down` for quick navigation through long subtitle files
   - `Home` and `End` help you quickly jump to the beginning or end of the file

3. **Editing**
   - `Insert` adds a new subtitle at the current position
   - `Delete` removes the selected subtitle
   - Use `Ctrl+Z` and `Ctrl+Y` to undo/redo changes
   - `Ctrl+Alt+S` splits a subtitle at the current time
   - `Ctrl+Alt+M` merges the selected subtitle with the next one

4. **Video Playback**
   - `Space` toggles play/pause
   - `Esc` stops playback
   - Arrow keys navigate between subtitles while playing

5. **Search and Replace**
   - `Ctrl+F` opens the find dialog
   - `Ctrl+H` opens the replace dialog
   - `F3` finds the next occurrence
   - `Shift+F3` finds the previous occurrence

## Customization

You can customize keyboard shortcuts in the Settings dialog (`F10`). This allows you to:
- Change existing shortcuts
- Add new shortcuts
- Reset to default shortcuts
- Save your custom shortcut configuration

## Platform-Specific Notes

- On macOS, use `Command` instead of `Ctrl`
- On Linux, use `Control` instead of `Ctrl`
- Some shortcuts might be reserved by the operating system 