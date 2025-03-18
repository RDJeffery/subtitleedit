#pragma once

#include <string>
#include <vector>
#include <memory>
#include <optional>

namespace SubtitleEdit::Services {

class IOcrService {
public:
    virtual ~IOcrService() = default;

    // Initialize OCR with specified language and settings
    virtual bool Initialize(const std::string& language, const std::string& engineMode = "3") = 0;

    // Perform OCR on a single image
    virtual std::optional<std::string> PerformOcr(const std::string& imagePath, const std::string& psmMode = "6") = 0;

    // Perform OCR on multiple images
    virtual std::vector<std::string> PerformBatchOcr(const std::vector<std::string>& imagePaths, const std::string& psmMode = "6") = 0;

    // Get available OCR languages
    virtual std::vector<std::string> GetAvailableLanguages() = 0;

    // Get last error message
    virtual std::string GetLastError() const = 0;

    // Get all errors from last operation
    virtual std::vector<std::string> GetErrors() const = 0;

    // Check if OCR is properly initialized
    virtual bool IsInitialized() const = 0;
};

} // namespace SubtitleEdit::Services 