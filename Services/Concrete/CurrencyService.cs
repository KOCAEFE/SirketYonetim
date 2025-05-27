using System.Text.Json;

namespace SirketYonetim.Services.Concrete
{
    public class CurrencyService
    {
        private readonly HttpClient _httpClient = new();

        public async Task<Dictionary<string, decimal>> GetExchangeRatesAsync(string baseCurrency, string[] targetCurrencies)
        {
            var url = $"https://open.er-api.com/v6/latest/{baseCurrency}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // JSON'daki küçük harfli alanlara uyum sağlar
            };

            var data = JsonSerializer.Deserialize<ExchangeRateResponse>(content, options);

            if (data == null || data.Rates == null || data.Result != "success")
                return null;

            var filteredRates = new Dictionary<string, decimal>();

            foreach (var currency in targetCurrencies)
            {
                if (data.Rates.TryGetValue(currency, out var rate))
                {
                    filteredRates[currency] = rate;
                }
            }

            return filteredRates;
        }

        private class ExchangeRateResponse
        {
            public string Result { get; set; }
            public string Provider { get; set; }
            public string Base_Code { get; set; }
            public Dictionary<string, decimal> Rates { get; set; }
        }
    }
}
