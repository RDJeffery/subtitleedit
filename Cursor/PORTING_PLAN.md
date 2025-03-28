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

### Phase 2: Main UI Components ✅
1. **Main Window**
   - [x] Port main window layout
   - [x] Implement menu system
   - [x] Port toolbar functionality
   - [x] Handle window management

2. **Subtitle Editor**
   - [x] Port subtitle grid/list view
   - [x] Implement text editing functionality
   - [x] Port timing controls
   - [x] Handle keyboard shortcuts

3. **Video Player**
   - [x] Port video player component
   - [x] Implement playback controls
   - [x] Handle video synchronization
   - [x] Port frame navigation

4. **LibSE Integration**
   - [x] Set up LibSE project references
   - [x] Port core subtitle data structures
   - [x] Implement subtitle format handlers
   - [x] Link subtitle processing logic
   - [x] Set up subtitle synchronization

### Phase 3: Feature Components 🟨
1. **Tools and Utilities**
   - [ ] Port OCR tools
   - [ ] Implement translation features
   - [ ] Port spell checking
   - [ ] Handle file operations

2. **Format Support**
   - [x] Ensure all subtitle format support
   - [x] Port import/export functionality
   - [x] Handle encoding/decoding
   - [x] Implement format conversion

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

### Completed (Phase 1 & 2)
1. **Project Structure**
   - Created SubtitleEdit.Avalonia project
   - Set up basic Avalonia UI files
   - Configured project references
   - Implemented MVVM architecture
   - Set up LibSE integration

2. **MVVM Infrastructure**
   - Implemented ViewModelBase with property change notification
   - Created MainWindowViewModel with full functionality
   - Set up data binding in MainWindow
   - Implemented AsyncRelayCommand and RelayCommand
   - Integrated LibSE subtitle processing

3. **Core Services**
   - Created service interfaces (ISubtitleService, IVideoService, ILanguageService)
   - Implemented service classes with LibSE integration
   - Set up dependency injection with ServiceLocator
   - Added file operation handling
   - Integrated subtitle format handlers

4. **UI Components**
   - Created complete MainWindow layout
   - Implemented full menu system with commands
   - Added toolbar with icons and functionality
   - Implemented status bar with dynamic updates
   - Added video preview component
   - Created subtitle grid with columns
   - Added loading overlay
   - Implemented comprehensive keyboard shortcuts system
   - Created detailed keyboard shortcuts documentation
   - Integrated subtitle synchronization
   - Added platform-specific UI optimizations

### In Progress (Phase 3)
1. **Tools and Utilities**
   - [ ] Implementing OCR tools integration
     - [ ] Port Tesseract integration
     - [ ] Add OCR settings UI
     - [ ] Implement OCR batch processing
     - [ ] Add OCR language support
   - [ ] Setting up translation service
     - [ ] Implement translation API integration
     - [ ] Add translation settings UI
     - [ ] Support multiple translation providers
   - [ ] Adding spell checking functionality
     - [ ] Integrate spell checking engine
     - [ ] Add custom dictionary support
     - [ ] Implement auto-correction
   - [ ] Implementing file operations
     - [ ] Add drag-and-drop support
     - [ ] Implement file watching
     - [ ] Add recent files list

2. **Format Support**
   - [x] Porting subtitle format handlers
   - [x] Implementing import/export functionality
   - [x] Adding encoding/decoding support
   - [x] Creating format conversion utilities

### Next Steps
1. Begin OCR tools integration
   - Set up Tesseract integration
   - Create OCR settings UI
   - Implement OCR processing pipeline
2. Implement translation service
   - Add translation API integration
   - Create translation settings UI
   - Implement translation caching
3. Add spell checking functionality
   - Integrate spell checking engine
   - Add custom dictionary support
4. Implement file operations
   - Add drag-and-drop support
   - Implement file watching
5. Port audio processing features
   - Add audio waveform display
   - Implement audio synchronization
   - Add audio editing capabilities

## Todo List (Prioritized)

### High Priority
1. [x] Set up Avalonia project structure
2. [x] Port main window and basic UI
3. [x] Implement core subtitle editing features
4. [x] Port video player component
5. [x] Handle basic file operations
6. [x] Integrate LibSE core functionality
7. [x] Port subtitle format handlers
8. [x] Implement keyboard shortcuts system
9. [ ] Implement OCR tools
   - [x] Create OCR service interface
   - [x] Implement Tesseract integration
   - [x] Create OCR settings UI
   - [ ] Implement OCR processing pipeline
   - [ ] Add OCR language management
10. [ ] Add translation service
    - [ ] Integrate translation API
    - [ ] Create translation UI
    - [ ] Add language support

### Medium Priority
1. [ ] Port OCR functionality
   - [ ] Add batch processing
   - [ ] Implement language packs
   - [ ] Add OCR quality settings
2. [ ] Implement translation features
   - [ ] Add multiple provider support
   - [ ] Implement translation memory
   - [ ] Add batch translation
3. [ ] Port spell checking
   - [ ] Add custom dictionaries
   - [ ] Implement auto-correction
   - [ ] Add language-specific rules
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
- Phase 2: 4-6 weeks ✅ (Completed)
- Phase 3: 6-8 weeks 🟨 (In Progress)
- Phase 4: 4-5 weeks ⬜ (Not Started)
- Phase 5: 3-4 weeks ⬜ (Not Started)

Total estimated time: 19-26 weeks

## Next Steps
1. [x] Review and approve this plan
2. [x] Set up development environment
3. [x] Create initial project structure
4. [x] Begin Phase 1 implementation
5. [x] Complete Phase 2 implementation
6. [ ] Start Phase 3 implementation
7. [ ] Begin OCR tools integration
8. [ ] Implement subtitle format handlers 