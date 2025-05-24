using System.Text.Json;

namespace SirketYonetim.Services.Concrete
{
    public class NominatimService
    {
        private readonly HttpClient _httpClient;

        public NominatimService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("SirketYonetim/1.0");
        }

        public async Task<(double? lat, double? lon)> GetCoordinatesAsync(string address)
        {
            var url = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(address)}&format=json&limit=1";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return (null, null);

            var content = await response.Content.ReadAsStringAsync();

            var results = JsonSerializer.Deserialize<List<NominatimResult>>(content);
            var first = results?.FirstOrDefault();

            if (first != null && double.TryParse(first.lat, out double latitude) && double.TryParse(first.lon, out double longitude))
                return (latitude, longitude);

            return (null, null);
        }

        private class NominatimResult
        {
            public string lat { get; set; }
            public string lon { get; set; }
        }
    }
}
