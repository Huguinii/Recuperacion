using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WpfAulaAtecA.Utils
{
    public static class HttpJsonClient<T>
    {
        public static string AuthToken { get; set; }

        private static readonly HttpClient httpClient = new();

        private static void AddAuthHeader()
        {
            httpClient.DefaultRequestHeaders.Authorization = null;

            if (!string.IsNullOrEmpty(AuthSession.Token))
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AuthSession.Token);
            }
        }


        public static async Task<T> Get(string url)
        {
            try
            {
                AddAuthHeader();

                var response = await httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"GET error: {response.StatusCode}");
                    return default;
                }

                string content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GET Exception: {ex.Message}");
                return default;
            }
        }

        public static async Task<T> Post(string url, object data)
        {
            try
            {
                AddAuthHeader();

                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(url, content);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"POST error: {response.StatusCode}");
                    return default;
                }

                string result = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"POST Exception: {ex.Message}");
                return default;
            }
        }

        public static async Task<T> Patch(string url, object data)
        {
            try
            {
                AddAuthHeader();

                var request = new HttpRequestMessage(new HttpMethod("PATCH"), url)
                {
                    Content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json")
                };

                var response = await httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"PATCH error: {response.StatusCode}");
                    return default;
                }

                string result = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(result))
                {
                    return default; // Por ejemplo false para bool
                }
                return JsonSerializer.Deserialize<T>(result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PATCH Exception: {ex.Message}");
                return default;
            }
        }

        public static async Task<T> Put(string url, object data)
        {
            try
            {
                AddAuthHeader();

                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsync(url, content);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"PUT error: {response.StatusCode}");
                    return default;
                }

                string result = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PUT Exception: {ex.Message}");
                return default;
            }
        }


        public static async Task DeleteAll(string url)
        {
            try
            {
                AddAuthHeader();

                var response = await httpClient.DeleteAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    string errorDetails = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"DELETE Error: {response.StatusCode} - {errorDetails}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DELETE Exception: {ex.Message}");
            }
        }
    }
}
