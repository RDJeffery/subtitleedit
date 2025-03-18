#include "OcrSettingsViewModel.hpp"
#include "../Services/TesseractOcrService.hpp"
#include <spdlog/spdlog.h>

namespace SubtitleEdit::ViewModels {

OcrSettingsViewModel::OcrSettingsViewModel() 
    : _ocrService(std::make_unique<Services::TesseractOcrService>())
    , _selectedLanguage("eng")
    , _engineMode("3")
    , _psmMode("6")
    , _isInitialized(false)
{
    // Initialize commands
    _initializeCommand = ReactiveCommand::Create(
        [this]() { Initialize(); },
        [this]() { return !IsInitialized(); }
    );

    _refreshLanguagesCommand = ReactiveCommand::Create(
        [this]() { RefreshLanguages(); }
    );

    RefreshLanguages();
}

void OcrSettingsViewModel::Initialize() {
    if (_ocrService->Initialize(_selectedLanguage, _engineMode)) {
        IsInitialized(true);
        LastError("");
    } else {
        IsInitialized(false);
        LastError(_ocrService->GetLastError());
    }
}

void OcrSettingsViewModel::RefreshLanguages() {
    _availableLanguages = _ocrService->GetAvailableLanguages();
    if (_availableLanguages.empty()) {
        LastError("No OCR languages available. Please install Tesseract and language data.");
    }
}

} // namespace SubtitleEdit::ViewModels 