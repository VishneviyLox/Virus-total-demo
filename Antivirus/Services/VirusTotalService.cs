using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text.Json;
using Antivirus.Model;
using Newtonsoft.Json;

namespace Antivirus.Services
{
    internal class VirusTotalService
    {
        string ApiKey = "b6490a52053b60fb7cb93651b35d9f5acfe0cad52d3592cc1d843c024860cc7f";

        #region methods
      public async Task<string> ScanFile(string filePath)
        {
            string fileHash;
            string message;

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
                    dynamic result = JsonConvert.DeserializeObject(jsonResponse);

                    var stats = result.data.attributes.last_analysis_stats;
                    int malicious = stats.malicious;
                    int harmless = stats.harmless;

                     message = $"Результаты сканирования:\n" +
                                $"🟥 Вредоносных: {stats.malicious}\n" +
                                $"🟨 Подозрительных: {stats.suspicious}\n" +
                                $"🟩 Безопасных: {stats.harmless}\n" +
                                $"⚪ Не обнаружено: {stats.undetected}";

                }
                else
                {
                    message = $"Ошибка API {response.StatusCode}";
                }

            }
                return message;
        }
        #endregion
    }
}
