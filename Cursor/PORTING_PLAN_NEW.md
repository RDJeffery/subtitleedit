# SubtitleEdit Porting Plan to Avalonia UI

## Project Overview
This document outlines the plan for porting SubtitleEdit from Windows Forms to Avalonia UI, making it cross-platform compatible with macOS and other platforms.

## Core Rules
1. **Code Organization**
   - Keep the existing core business logic intact
   - Only modify UI-related code
   - Maintain separation of concerns between UI and business logic
   - Follow MVVM pattern for new UI components

2. **Compatibility**
   - Ensure all existing features work on macOS
   - Maintain Windows compatibility
   - Handle platform-specific code appropriately
   - Use cross-platform APIs where possible

3. **Testing**
   - Test each ported component thoroughly
   - Verify functionality on both Windows and macOS
   - Maintain existing test coverage
   - Add new tests for platform-specific features

4. **Documentation**
   - Document all major changes
   - Update existing documentation
   - Keep track of known issues and workarounds
   - Document platform-specific considerations

## Project Breakdown

### Phase 1: Setup and Infrastructure ✅
1. **Initial Setup**
   - [x] Create new Avalonia project structure
   - [x] Set up development environment
   - [x] Configure build system
   - [x] Set up CI/CD pipeline

2. **Core Framework**
   - [x] Port core UI components
   - [x] Implement MVVM infrastructure
   - [x] Set up dependency injection
   - [x] Create base styles and themes

### Phase 2: Main UI Components 🟨
1. **Main Window**
   - [x] Port main window layout
   - [x] Implement menu system
   - [ ] Port toolbar functionality
   - [ ] Handle window management

2. **Subtitle Editor**
   - [ ] Port subtitle grid/list view
   - [ ] Implement text editing
   - [ ] Port timing controls
   - [ ] Handle keyboard shortcuts

3. **Video Player**
   - [ ] Port video player component
   - [ ] Implement playback controls
   - [ ] Handle video synchronization
   - [ ] Port frame navigation

### Phase 3: Feature Components ⬜
1. **Tools and Utilities**
   - [ ] Port OCR tools
   - [ ] Implement translation features
   - [ ] Port spell checking
   - [ ] Handle file operations

2. **Format Support**
   - [ ] Ensure all subtitle format support
   - [ ] Port import/export functionality
   - [ ] Handle encoding/decoding
   - [ ] Implement format conversion

3. **Advanced Features**
   - [ ] Port audio processing
   - [ ] Implement batch processing
   - [ ] Port networking features
   - [ ] Handle settings management

### Phase 4: Platform-Specific Features ⬜
1. **macOS Specific**
   - [ ] Implement macOS-specific UI patterns
   - [ ] Handle macOS file system
   - [ ] Port macOS-specific features
   - [ ] Optimize for macOS performance

2. **Cross-Platform Considerations**
   - [ ] Handle platform-specific paths
   - [ ] Implement platform-specific dialogs
   - [ ] Handle platform-specific shortcuts
   - [ ] Optimize for different screen resolutions

### Phase 5: Testing and Optimization ⬜
1. **Testing**
   - [ ] Implement unit tests
   - [ ] Create integration tests
   - [ ] Perform UI testing
   - [ ] Conduct performance testing

2. **Optimization**
   - [ ] Optimize UI performance
   - [ ] Reduce memory usage
   - [ ] Improve startup time
   - [ ] Optimize file operations

## Progress Details

### Completed (Phase 1)
1. **Project Structure**
   - Created SubtitleEdit.Avalonia project
   - Set up basic Avalonia UI files
   - Configured project references

2. **MVVM Infrastructure**
   - Implemented ViewModelBase with property change notification
   - Created MainWindowViewModel
   - Set up data binding in MainWindow

3. **Core Services**
   - Created service interfaces (ISubtitleService, IVideoService, ILanguageService)
   - Implemented service classes with placeholder functionality
   - Set up dependency injection with ServiceLocator

4. **UI Components**
   - Created basic MainWindow layout
   - Implemented menu structure
   - Added status bar and loading overlay

### In Progress
1. **Main Window Functionality**
   - Implementing toolbar functionality
   - Setting up window management
   - Adding platform-specific window behaviors

### Next Steps
1. Implement actual LibSE integration in services
2. Create subtitle grid/list view
3. Set up video player component
4. Add file operations handling

## Todo List (Prioritized)

### High Priority
1. [x] Set up Avalonia project structure
2. [x] Port main window and basic UI
3. [ ] Implement core subtitle editing features
4. [ ] Port video player component
5. [ ] Handle basic file operations

### Medium Priority
1. [ ] Port OCR functionality
2. [ ] Implement translation features
3. [ ] Port spell checking
4. [ ] Handle advanced subtitle formats
5. [ ] Implement batch processing

### Low Priority
1. [ ] Add advanced video features
2. [ ] Implement networking features
3. [ ] Add platform-specific optimizations
4. [ ] Create additional UI themes
5. [ ] Add advanced export options

## Progress Tracking
- Use GitHub issues for tracking tasks
- Regular progress updates in this document
- Weekly status reports
- Milestone tracking

## Known Challenges
1. Video playback implementation
2. Platform-specific file system operations
3. UI performance optimization
4. Cross-platform testing
5. Maintaining feature parity

## Resources
- Avalonia UI documentation
- Existing SubtitleEdit documentation
- Cross-platform development guides
- macOS development guidelines

## Timeline
- Phase 1: 2-3 weeks ✅ (Completed)
- Phase 2: 4-6 weeks 🟨 (In Progress)
- Phase 3: 6-8 weeks ⬜ (Not Started)
- Phase 4: 4-5 weeks ⬜ (Not Started)
- Phase 5: 3-4 weeks ⬜ (Not Started)

Total estimated time: 19-26 weeks

## Next Steps
1. [x] Review and approve this plan
2. [x] Set up development environment
3. [x] Create initial project structure
4. [x] Begin Phase 1 implementation
5. [ ] Start Phase 2 implementation
6. [ ] Begin LibSE integration
7. [ ] Implement core subtitle editing features 