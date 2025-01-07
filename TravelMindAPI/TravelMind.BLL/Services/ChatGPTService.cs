﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TravelMind.BLL.Interfaces;

namespace TravelMind.BLL.Services
{
    public class ChatGPTService : IChatGPTService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "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"; // Replace with your actual ChatGPT API key

        public ChatGPTService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetTripPlanAsync(string prompt)
        {
            var requestContent = new
            {
                model = "text-davinci-003",
                prompt = prompt,
                max_tokens = 1000
            };

            var httpContent = new StringContent(JsonSerializer.Serialize(requestContent), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/completions", httpContent);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonSerializer.Deserialize<JsonElement>(responseContent);

            return jsonResponse.GetProperty("choices")[0].GetProperty("text").GetString();
        }
    }

}
