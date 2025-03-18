# OCR Integration

This document outlines the implementation of OCR (Optical Character Recognition) functionality in SubtitleEdit.Avalonia.

## Current State

The OCR integration is in an initial implementation phase with the following components:

1. **OCR Service Interface and Implementation**
   - `IOcrService.hpp`: Interface defining OCR operations
   - `TesseractOcrService.hpp/cpp`: Implementation using Tesseract OCR engine

2. **OCR Settings UI**
   - `OcrSettingsViewModel.hpp/cpp`: View model for OCR settings
   - `OcrSettingsView.axaml`: Avalonia UI view for OCR settings

3. **Infrastructure**
   - `BoolToStatusConverter.hpp`: Converter for displaying initialization status
   - Service registration in dependency injection container
   - Menu integration in the main window

## Pending Tasks

The following tasks remain to be completed:

1. **OCR Processing Pipeline**
   - Implement subtitle image extraction
   - Create OCR batch processing UI
   - Integrate with subtitle timing data

2. **Language Management**
   - Add language download functionality
   - Implement language detection

3. **OCR Results Management**
   - Create OCR results view
   - Implement manual correction UI
   - Add post-processing filters

4. **Integration with Video Player**
   - Synchronize OCR with video frames
   - Implement frame extraction for OCR

## Usage

To use the OCR functionality:

1. Open OCR Settings from Tools > OCR Settings
2. Select the desired language
3. Choose engine and page segmentation modes
4. Click "Initialize" to prepare the OCR engine
5. Use OCR functions on subtitle images (not yet implemented)

## Dependencies

- Tesseract OCR engine must be installed on the system
- Language data files for the desired languages must be available

## Implementation Notes

- The OCR service uses command-line calls to Tesseract
- Temporary files are created for OCR processing
- Error handling captures and displays Tesseract errors

## Future Improvements

- Direct Tesseract API integration instead of command-line calls
- Caching of OCR results for performance
- Custom OCR training for subtitle-specific fonts
- Integration with cloud OCR services as fallback 