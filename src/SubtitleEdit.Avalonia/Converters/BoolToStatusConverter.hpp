#pragma once

#include <Avalonia/Data/Converters/IValueConverter.hpp>
#include <string>

namespace SubtitleEdit::Converters {

class BoolToStatusConverter : public Avalonia::Data::Converters::IValueConverter {
public:
    Avalonia::Data::BindingValue Convert(
        const Avalonia::Data::BindingValue& value,
        const Avalonia::Data::BindingValue& parameter) override {
        
        if (value.TryGet<bool>(out bool isInitialized)) {
            return isInitialized ? "Initialized" : "Not Initialized";
        }
        return "Unknown";
    }
};

} // namespace SubtitleEdit::Converters 