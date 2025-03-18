#include "TesseractOcrService.hpp"
#include <filesystem>
#include <fstream>
#include <sstream>
#include <cstdlib>
#include <spdlog/spdlog.h>

namespace SubtitleEdit::Services {

TesseractOcrService::TesseractOcrService() : _initialized(false) {
    _errors.reserve(100); // Reserve space for error messages
}

bool TesseractOcrService::Initialize(const std::string& language, const std::string& engineMode) {
    _errors.clear();
    _lastError.clear();

    // Check if Tesseract is installed
    if (!IsTesseractInstalled()) {
        _lastError = "Tesseract is not installed or not found in PATH";
        _errors.push_back(_lastError);
        return false;
    }

    // Check if language data exists
    if (!IsLanguageDataAvailable(language)) {
        _lastError = "Language data for " + language + " is not available";
        _errors.push_back(_lastError);
        return false;
    }

    _language = language;
    _engineMode = engineMode;
    _initialized = true;
    return true;
}

std::optional<std::string> TesseractOcrService::PerformOcr(const std::string& imagePath, const std::string& psmMode) {
    if (!_initialized) {
        _lastError = "OCR service not initialized";
        _errors.push_back(_lastError);
        return std::nullopt;
    }

    if (!std::filesystem::exists(imagePath)) {
        _lastError = "Image file not found: " + imagePath;
        _errors.push_back(_lastError);
        return std::nullopt;
    }

    // Create temporary output file
    auto tempOutput = std::filesystem::temp_directory_path() / "tesseract_output.txt";

    // Build Tesseract command
    std::stringstream cmd;
    cmd << "tesseract \"" << imagePath << "\" \"" << tempOutput.string() 
        << "\" -l " << _language 
        << " --psm " << psmMode
        << " --oem " << _engineMode;

    // Execute Tesseract
    int result = std::system(cmd.str().c_str());
    if (result != 0) {
        _lastError = "Tesseract failed with exit code " + std::to_string(result);
        _errors.push_back(_lastError);
        return std::nullopt;
    }

    // Read output file
    std::ifstream outputFile(tempOutput.string() + ".txt");
    if (!outputFile.is_open()) {
        _lastError = "Failed to read OCR output file";
        _errors.push_back(_lastError);
        return std::nullopt;
    }

    std::stringstream buffer;
    buffer << outputFile.rdbuf();
    outputFile.close();

    // Clean up temporary file
    std::filesystem::remove(tempOutput.string() + ".txt");

    return buffer.str();
}

std::vector<std::string> TesseractOcrService::PerformBatchOcr(const std::vector<std::string>& imagePaths, const std::string& psmMode) {
    std::vector<std::string> results;
    results.reserve(imagePaths.size());

    for (const auto& imagePath : imagePaths) {
        if (auto result = PerformOcr(imagePath, psmMode)) {
            results.push_back(*result);
        } else {
            results.push_back("");
        }
    }

    return results;
}

std::vector<std::string> TesseractOcrService::GetAvailableLanguages() {
    std::vector<std::string> languages;
    _errors.clear();
    _lastError.clear();

    // Execute tesseract --list-langs
    FILE* pipe = popen("tesseract --list-langs", "r");
    if (!pipe) {
        _lastError = "Failed to execute tesseract --list-langs";
        _errors.push_back(_lastError);
        return languages;
    }

    char buffer[128];
    std::string result;
    while (fgets(buffer, sizeof(buffer), pipe) != nullptr) {
        result += buffer;
    }

    int status = pclose(pipe);
    if (status != 0) {
        _lastError = "Failed to get available languages";
        _errors.push_back(_lastError);
        return languages;
    }

    // Parse output
    std::istringstream iss(result);
    std::string line;
    while (std::getline(iss, line)) {
        if (line.length() >= 3) { // Language codes are at least 3 characters
            languages.push_back(line);
        }
    }

    return languages;
}

std::string TesseractOcrService::GetLastError() const {
    return _lastError;
}

std::vector<std::string> TesseractOcrService::GetErrors() const {
    return _errors;
}

bool TesseractOcrService::IsInitialized() const {
    return _initialized;
}

bool TesseractOcrService::IsTesseractInstalled() {
    FILE* pipe = popen("tesseract --version", "r");
    if (!pipe) {
        return false;
    }

    char buffer[128];
    std::string result;
    while (fgets(buffer, sizeof(buffer), pipe) != nullptr) {
        result += buffer;
    }

    int status = pclose(pipe);
    return status == 0;
}

bool TesseractOcrService::IsLanguageDataAvailable(const std::string& language) {
    // Check common Tesseract data directories
    const std::vector<std::string> dataDirs = {
        "/usr/share/tesseract-ocr/5/tessdata",
        "/usr/share/tesseract-ocr/4.00/tessdata",
        "/usr/share/tesseract-ocr/tessdata",
        "/usr/share/tessdata"
    };

    for (const auto& dir : dataDirs) {
        if (std::filesystem::exists(dir + "/" + language + ".traineddata")) {
            return true;
        }
    }

    return false;
}

} // namespace SubtitleEdit::Services 