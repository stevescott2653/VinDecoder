using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using VinDecoderSpike.Classes;

namespace VinDecoderSpike.Service
{
    public class VinService
    {
        private readonly HttpClient _httpClient;
        private readonly VinValidator _vinValidator;
        private readonly ILogger<VinService> _logger;

        public VinService(HttpClient httpClient, VinValidator vinValidator, ILogger<VinService> logger)
        {
            _httpClient = httpClient;
            _vinValidator = vinValidator;
            _logger = logger;
        }

        public async Task<Root> GetVinDetails(string vin)
        {
            if (!_vinValidator.ValidateVin(vin))
            {
                _logger.LogWarning("Invalid VIN: {Vin}", vin);
                throw new ArgumentException("Invalid VIN provided.");
            }

            try
            {
                var response = await _httpClient.GetAsync($"https://vpic.nhtsa.dot.gov/api/vehicles/DecodeVinExtended/{vin}").ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    _logger.LogInformation("Response received: {Response}", jsonString);

                    var vinDetails = JsonConvert.DeserializeObject<Root>(jsonString);
                    return vinDetails;
                }

                _logger.LogError("Failed to fetch VIN details. Status Code: {StatusCode}, Reason: {ReasonPhrase}", response.StatusCode, response.ReasonPhrase);
                return null;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP request failed for VIN: {Vin}", vin);
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON deserialization failed for VIN: {Vin}", vin);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while processing VIN: {Vin}", vin);
                throw;
            }
        }
    }
}
