using Antivirus.Model;
using Antivirus.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.IO;

namespace Antivirus.ViewModel
{
    internal partial class FileAllInfoViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<VirusInfoItem> _virusDetections = new ObservableCollection<VirusInfoItem>();
        [ObservableProperty]
        private string _nameFile = "N/A";

        [ObservableProperty]
        private string _sizeFile = "N/A";

        private readonly VirusTotalService _virusTotalService = new VirusTotalService();


        public async Task LoadFileAnalysisAsync(string filePath)
        {
            VirusDetections.Clear();
            NameFile = Path.GetFileName(filePath);
            SizeFile = "Загрузка...";

            try
            {
                (Dictionary<string, AnalysisResult> results, FileData fileDetails, string fileHash) =
                    await _virusTotalService.GetAnalysisResults(filePath);

                NameFile = fileDetails.NameFile;
                SizeFile = fileDetails.SizeFile;

                foreach (var kvp in results)
                {
                    string scannerName = kvp.Key;
                    AnalysisResult analysis = kvp.Value;

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
                // Обработка ошибок
            }
        }
    }
}
