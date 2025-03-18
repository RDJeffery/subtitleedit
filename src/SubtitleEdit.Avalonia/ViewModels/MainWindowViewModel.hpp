#pragma once

#include <memory>
#include <string>
#include <vector>

#include "ReactiveCommand.hpp"

namespace SubtitleEdit.Avalonia.ViewModels
{
    class MainWindowViewModel
    {
    public:
        std::shared_ptr<ReactiveCommand> ShowOcrSettingsCommand() const { return _showOcrSettingsCommand; }

    private:
        void ShowOcrSettings();
        std::shared_ptr<ReactiveCommand> _showOcrSettingsCommand;
    };
} 