#include "Services/TesseractOcrService.hpp"

void App::RegisterServices() {
    // ... existing service registrations ...
    
    // Register OCR service
    auto ocrService = std::make_shared<Services::TesseractOcrService>();
    Locator::Current->Register<Services::IOcrService>(ocrService);
    
    // ... existing code ...
} 