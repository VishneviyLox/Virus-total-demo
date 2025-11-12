using Antivirus.Model; // Добавьте
using Antivirus.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Antivirus.ViewModel
{
    internal partial class FileAllInfoViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<VirusInfoItem> _virusDetections = new ObservableCollection<VirusInfoItem>();

        private readonly VirusTotalService _virusTotalService = new VirusTotalService();

        // Обновленный метод загрузки
        public async Task LoadFileAnalysisAsync(string filePath)
        {
            VirusDetections.Clear();

            try
            {
                // Вызов обновленного сервиса
                Dictionary<string, AnalysisResult> results = await _virusTotalService.GetAnalysisResults(filePath);

                // Итерация по всем сканерам и создание VirusInfoItem
                foreach (var kvp in results)
                {
                    string scannerName = kvp.Key; // Имя антивируса (например, "Kaspersky")
                    AnalysisResult analysis = kvp.Value; // Детали сканирования

                    _virusDetections.Add(new VirusInfoItem
                    {
                        AntivirusName = scannerName,
                        VirusName = string.IsNullOrEmpty(analysis.result) ? "Non detected" : analysis.result, 
                        VirusType = analysis.category 
                    });
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок, например, вывод сообщения пользователю
                System.Diagnostics.Debug.WriteLine($"Ошибка загрузки данных VirusTotal: {ex.Message}");
            }
        }
    }
}