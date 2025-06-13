using LoginRegister.Interface;
using LoginRegister.Models;
using LoginRegister.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LoginRegister.Services
{
    public class HttpJsonService<T> : IHttpJsonProvider<T> where T : class
    {
        private readonly HttpClient _httpClient;
        private readonly LoginDTO _loginDto;

        public HttpJsonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _loginDto = App.Current.Services.GetRequiredService<LoginDTO>();
        }

        private void Authorize()
        {
            if (!string.IsNullOrEmpty(_loginDto.Token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _loginDto.Token);
            }
        }

        public async Task<IEnumerable<T>> GetAsync(string path)
        {
            Authorize();
            var resp = await _httpClient.GetAsync(path);
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<T>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        }

        public async Task<T> PostAsync(string path, T data)
        {
            Authorize();
            var content = new StringContent(
                JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json");
            var resp = await _httpClient.PostAsync(path, content);
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        }

        public async Task<T> LoginPostAsync(string path, LoginDTO data)
        {
            // No necesita autorización previa
            var content = new StringContent(
                JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json");
            var resp = await _httpClient.PostAsync(path, content);
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadAsStringAsync();
            var userDto = JsonSerializer.Deserialize<UserDTO>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
            if (userDto.Result?.Token != null)
            {
                _loginDto.Token = userDto.Result.Token;
            }
            return userDto as T;
        }

        public Task<T> RegisterPostAsync(string path, UserRegistroDTO data)
            => PostAsync(path, data as T!);

        public Task<T> PutAsync(string path, T data)
            => PostAsync(path, data);

        public async Task<T> DeleteAsync(string path, int id)
        {
            Authorize();
            var resp = await _httpClient.DeleteAsync($"{path}/{id}");
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        }
    }
}
