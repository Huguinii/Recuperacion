using Examen_Galaxy.Constants;
using Examen_Galaxy.Interface;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Examen_Galaxy.Service
{
    internal class HttpJsonService<T> : IHttpJsonProvider<T> where T : class
    {
        public async Task<IEnumerable<T>> GetAsync(string api_url)
        {
            try  
            {
                using HttpClient httpClient = new HttpClient();
                {
                    HttpResponseMessage datos = await httpClient.GetAsync($"{Constants.Constants.BASE_URL}{api_url}");
                    string dataget = await datos.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<IEnumerable<T>>(dataget);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return default;
        }

        public async Task<T?> PostAsync(T data, string path)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    string jsonContent = JsonSerializer.Serialize(data);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await httpClient.PostAsync($"{Constants.Constants.BASE_URL}{path}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        return JsonSerializer.Deserialize<T>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    }
                    else
                    {
                        Console.WriteLine("Error en la respuesta: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la solicitud POST: {ex.Message}");
            }

            return default;
        }

        public async Task<bool> RemoveAsync(T data, string api_url,int id)
        {
            try
            {
                using HttpClient httpClient = new HttpClient();
                {
                    HttpResponseMessage datos = await httpClient.GetAsync($"{Constants.Constants.BASE_URL}{api_url}/{id}");
                    string dataget = await datos.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<bool>(dataget);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public async Task<T?> DeleteAsync(string path)
        {
            try
            {
                using HttpClient httpClient = new HttpClient();

                HttpResponseMessage request = await httpClient.DeleteAsync($"{Constants.Constants.BASE_URL}{path}");

                string dataRequest = await request.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(dataRequest);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return default;
        }



    }
}
