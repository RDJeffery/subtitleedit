# SubtitleEdit Porting Guidelines

## Overview
This document provides detailed guidelines and best practices for porting SubtitleEdit from Windows Forms to Avalonia UI. These guidelines ensure consistency, maintainability, and high-quality code throughout the porting process.

## Core Development Principles

### 1. Architecture Guidelines
- **MVVM Pattern**
  - Strictly follow MVVM pattern for all new UI components
  - ViewModels should be platform-agnostic
  - Views should only contain UI-specific code
  - Models should remain unchanged from original implementation

- **Dependency Injection**
  - Use dependency injection for all services and components
  - Register platform-specific implementations in DI container
  - Keep platform-specific code isolated in separate assemblies

- **Code Organization**
  - Maintain clear separation between UI and business logic
  - Keep platform-specific code in dedicated folders
  - Use interfaces for platform-dependent functionality
  - Follow SOLID principles

### 2. UI Guidelines

#### Avalonia-Specific Rules
- Use Avalonia's built-in controls when possible
- Implement custom controls only when necessary
- Follow Avalonia's styling system
- Use XAML for all UI definitions
- Implement proper data binding

#### Cross-Platform Considerations
- Use platform-agnostic APIs
- Handle platform-specific UI patterns
- Implement responsive layouts
- Support different screen resolutions
- Follow platform-specific design guidelines

### 3. Code Migration Rules

#### General Rules
- One component at a time
- Maintain feature parity
- Write tests before migration
- Document all changes
- Keep original code as reference

#### Specific Guidelines
1. **Windows Forms to Avalonia Mapping**
   - Form → Window
   - Panel → Grid/StackPanel
   - Button → Button
   - TextBox → TextBox
   - DataGridView → DataGrid
   - MenuStrip → Menu
   - ToolStrip → ToolBar

2. **Event Handling**
   - Convert Windows Forms events to Avalonia events
   - Use command binding where appropriate
   - Implement proper event cleanup

3. **Resource Management**
   - Use Avalonia's resource system
   - Convert Windows Forms resources
   - Handle platform-specific resources

### 4. Testing Requirements

#### Unit Testing
- Test all ViewModels
- Mock platform-specific dependencies
- Maintain existing test coverage
- Add new tests for platform-specific features

#### UI Testing
- Test on all target platforms
- Verify responsive behavior
- Test accessibility features
- Validate keyboard navigation

#### Integration Testing
- Test file operations
- Verify video playback
- Test subtitle synchronization
- Validate OCR functionality

### 5. Performance Guidelines

#### UI Performance
- Implement virtual scrolling for large lists
- Use lazy loading where appropriate
- Optimize resource usage
- Minimize UI updates

#### Memory Management
- Properly dispose of resources
- Implement cleanup in ViewModels
- Handle large file operations efficiently
- Monitor memory usage

### 6. Platform-Specific Guidelines

#### macOS Specific
- Follow macOS Human Interface Guidelines
- Implement proper menu structure
- Handle macOS-specific file operations
- Support macOS keyboard shortcuts

#### Cross-Platform Features
- Use platform-agnostic file paths
- Handle platform-specific dialogs
- Implement proper clipboard operations
- Support platform-specific drag and drop

### 7. Documentation Requirements

#### Code Documentation
- Document all public APIs
- Include usage examples
- Document platform-specific considerations
- Maintain changelog

#### User Documentation
- Update user guides
- Document platform-specific features
- Include troubleshooting guides
- Maintain FAQ section

### 8. Version Control Guidelines

#### Branching Strategy
- Main branch for stable releases
- Develop branch for ongoing work
- Feature branches for new components
- Release branches for version management

#### Commit Guidelines
- Clear commit messages
- Reference issue numbers
- Document breaking changes
- Keep commits focused and atomic

### 9. Build and Deployment

#### Build Process
- Maintain separate build configurations
- Implement proper versioning
- Handle platform-specific dependencies
- Support CI/CD pipeline

#### Deployment
- Create platform-specific installers
- Handle updates properly
- Manage dependencies
- Support auto-updates

## Quality Assurance Checklist

### Before Committing
- [ ] Code follows MVVM pattern
- [ ] All tests pass
- [ ] No platform-specific code in shared components
- [ ] Documentation is updated
- [ ] Performance is acceptable
- [ ] Memory usage is optimized
- [ ] UI follows platform guidelines
- [ ] Accessibility requirements met
- [ ] Error handling is implemented
- [ ] Logging is in place

### Before Release
- [ ] All features tested on all platforms
- [ ] Performance benchmarks met
- [ ] Security requirements satisfied
- [ ] Documentation complete
- [ ] Installer tested
- [ ] Update process verified
- [ ] Known issues documented
- [ ] Release notes prepared

## Resources and References

### Documentation
- [Avalonia UI Documentation](https://docs.avaloniaui.net/)
- [macOS Human Interface Guidelines](https://developer.apple.com/design/human-interface-guidelines/)
- [Cross-Platform Development Best Practices](https://docs.microsoft.com/en-us/dotnet/core/tutorials/cross-platform-tools)
- [MVVM Pattern Documentation](https://docs.microsoft.com/en-us/dotnet/architecture/maui/mvvm)

### Tools
- Visual Studio for Windows
- Visual Studio for Mac
- Avalonia UI Designer
- Platform-specific debugging tools
- Performance profiling tools

### Testing Tools
- Unit testing frameworks
- UI testing tools
- Performance monitoring tools
- Memory profiling tools

## Support and Maintenance

### Issue Tracking
- Use GitHub Issues
- Categorize issues properly
- Track platform-specific bugs
- Maintain known issues list

### Release Management
- Regular release schedule
- Version numbering scheme
- Changelog maintenance
- Release notes preparation

### User Support
- Platform-specific documentation
- Troubleshooting guides
- FAQ maintenance
- User feedback collection 