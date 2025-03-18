#include "../Views/OcrSettingsView.axaml.h"
#include "OcrSettingsViewModel.hpp"

void MainWindowViewModel::ShowOcrSettings() {
    auto ocrSettingsViewModel = std::make_shared<OcrSettingsViewModel>();
    auto ocrSettingsView = new Views::OcrSettingsView();
    ocrSettingsView->DataContext(ocrSettingsViewModel);
    ocrSettingsView->Show();
} 