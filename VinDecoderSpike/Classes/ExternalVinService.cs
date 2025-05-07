using System.Net.Http;
using Newtonsoft.Json;

namespace VinDecoderSpike
{
    public class ExternalVinService
    {
        private readonly HttpClient _httpClient;

        public ExternalVinService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Vin> GetVinDetails(string vin)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://vpic.nhtsa.dot.gov/api/vehicles/DecodeVinExtended");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Vin vinDetails = JsonConvert.DeserializeObject<Vin>(content);
                return vinDetails;
            }
            else
            {
                throw new Exception("Failed to decode VIN");
            }
        }
        
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddHttpClient<ExternalVinService>();
            var app = builder.Build();
            var vinService = new ExternalVinService(app.Services.GetRequiredService<HttpClient>());
            string vin = "YourVinNumber";
            Vin vinDetails = await vinService.GetVinDetails(vin);

            Console.WriteLine($"Make: {vinDetails.Make}");
            Console.WriteLine($"Model: {vinDetails.Model}");
            Console.WriteLine($"Year: {vinDetails.Year}");
        }
    }
}