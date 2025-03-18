#pragma once

#include <Avalonia/ReactiveUI/ReactiveObject.hpp>
#include <Avalonia/ReactiveUI/ReactiveCommand.hpp>
#include <string>
#include <vector>
#include <memory>
#include <optional>
#include <functional>
#include "../Services/IOcrService.hpp"

namespace SubtitleEdit::ViewModels {

class OcrSettingsViewModel : public ReactiveObject {
public:
    OcrSettingsViewModel();
    ~OcrSettingsViewModel() = default;

    // Properties
    std::string SelectedLanguage() const { return _selectedLanguage; }
    void SelectedLanguage(const std::string& value) {
        if (_selectedLanguage != value) {
            _selectedLanguage = value;
            this->RaisePropertyChanged();
        }
    }

    std::string EngineMode() const { return _engineMode; }
    void EngineMode(const std::string& value) {
        if (_engineMode != value) {
            _engineMode = value;
            this->RaisePropertyChanged();
        }
    }

    std::string PsmMode() const { return _psmMode; }
    void PsmMode(const std::string& value) {
        if (_psmMode != value) {
            _psmMode = value;
            this->RaisePropertyChanged();
        }
    }

    bool IsInitialized() const { return _isInitialized; }
    void IsInitialized(bool value) {
        if (_isInitialized != value) {
            _isInitialized = value;
            this->RaisePropertyChanged();
        }
    }

    std::string LastError() const { return _lastError; }
    void LastError(const std::string& value) {
        if (_lastError != value) {
            _lastError = value;
            this->RaisePropertyChanged();
        }
    }

    // Commands
    std::shared_ptr<ReactiveCommand> InitializeCommand() const { return _initializeCommand; }
    std::shared_ptr<ReactiveCommand> RefreshLanguagesCommand() const { return _refreshLanguagesCommand; }

    // Available languages
    const std::vector<std::string>& AvailableLanguages() const { return _availableLanguages; }

private:
    void Initialize();
    void RefreshLanguages();

    std::unique_ptr<Services::IOcrService> _ocrService;
    std::string _selectedLanguage;
    std::string _engineMode;
    std::string _psmMode;
    bool _isInitialized;
    std::string _lastError;
    std::vector<std::string> _availableLanguages;

    std::shared_ptr<ReactiveCommand> _initializeCommand;
    std::shared_ptr<ReactiveCommand> _refreshLanguagesCommand;
};

} // namespace SubtitleEdit::ViewModels 