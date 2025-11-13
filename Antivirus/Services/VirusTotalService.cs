using Antivirus.Model;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;

namespace Antivirus.Services
{
    internal class VirusTotalService
    {
        string ApiKey = "b6490a52053b60fb7cb93651b35d9f5acfe0cad52d3592cc1d843c024860cc7f";
        public async Task<(Dictionary<string, AnalysisResult> Analysis, FileData FileDetails, string FileHash)> GetAnalysisResults(string filePath)
        {
            var fileDataTuple = GetFileData(filePath);
            var fileDetails = new FileData
            {
                NameFile = fileDataTuple.fileName,
                SizeFile = fileDataTuple.sizeFile
            };

            string fileHash;
            using (var sha256 = SHA256.Create())
            using (var fileStream = File.OpenRead(filePath))
            {
                byte[] hashBytes = sha256.ComputeHash(fileStream);
                fileHash = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("x-apikey", ApiKey);
                HttpResponseMessage response = await httpClient.GetAsync($"https://www.virustotal.com/api/v3/files/{fileHash}");

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var fullResponse = JsonConvert.DeserializeObject<VirusTotalResponse>(jsonResponse);
                    if (fullResponse?.data?.attributes?.last_analysis_results != null)
                    {
                        return (fullResponse.data.attributes.last_analysis_results, fileDetails, fileHash);
                    }
                }

                throw new HttpRequestException($"Ошибка API VirusTotal: {response.StatusCode}");
            }
        }

        private (string fileName, string sizeFile) GetFileData(string filePath)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);
            string fileName = Path.GetFileName(filePath);
            double sizeFileMB = fileInfo.Length / (1024.0 * 1024.0);
            string sizeFileFormatted = $"{sizeFileMB:F2} MB";
            return (fileName, sizeFileFormatted);
        }
    }
}