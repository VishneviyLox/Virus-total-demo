
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using Antivirus.Model;
using Newtonsoft.Json;

namespace Antivirus.Services
{
    internal class VirusTotalService
    {
        string ApiKey = "b6490a52053b60fb7cb93651b35d9f5acfe0cad52d3592cc1d843c024860cc7f";

        public async Task<Dictionary<string, AnalysisResult>> GetAnalysisResults(string filePath)
        {
            string fileHash;
            // 1. Расчет хеша файла
            using (var sha256 = SHA256.Create())
            using (var fileStream = File.OpenRead(filePath))
            {
                byte[] hashBytes = sha256.ComputeHash(fileStream);
                fileHash = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }

            // 2. Вызов API
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("x-apikey", ApiKey);
                HttpResponseMessage response = await httpClient.GetAsync($"https://www.virustotal.com/api/v3/files/{fileHash}");

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    // 3. Десериализация в полную модель VirusTotalResponse
                    var fullResponse = JsonConvert.DeserializeObject<VirusTotalResponse>(jsonResponse);

                    // 4. Проверка и возврат детальных результатов
                    if (fullResponse?.data?.attributes?.last_analysis_results != null)
                    {
                        return fullResponse.data.attributes.last_analysis_results;
                    }
                }

                throw new HttpRequestException($"Ошибка API VirusTotal: {response.StatusCode}");
            }
        }
    }
}