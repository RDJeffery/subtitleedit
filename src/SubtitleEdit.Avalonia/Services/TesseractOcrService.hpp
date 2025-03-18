#pragma once

#include "IOcrService.hpp"
#include <string>
#include <vector>

namespace SubtitleEdit::Services {

class TesseractOcrService : public IOcrService {
public:
    TesseractOcrService();
    ~TesseractOcrService() override = default;

    bool Initialize(const std::string& language, const std::string& engineMode = "3") override;
    std::optional<std::string> PerformOcr(const std::string& imagePath, const std::string& psmMode = "6") override;
    std::vector<std::string> PerformBatchOcr(const std::vector<std::string>& imagePaths, const std::string& psmMode = "6") override;
    std::vector<std::string> GetAvailableLanguages() override;
    std::string GetLastError() const override;
    std::vector<std::string> GetErrors() const override;
    bool IsInitialized() const override;

private:
    bool IsTesseractInstalled();
    bool IsLanguageDataAvailable(const std::string& language);

    std::string _language;
    std::string _engineMode;
    std::string _lastError;
    std::vector<std::string> _errors;
    bool _initialized;
};

} // namespace SubtitleEdit::Services 