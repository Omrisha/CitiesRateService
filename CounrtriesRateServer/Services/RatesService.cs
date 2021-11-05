using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CounrtriesRateServer.Boundries;
using CounrtriesRateServer.Interfaces;
using Newtonsoft.Json;

namespace CounrtriesRateServer.Services
{
    public class RatesService : IRateService
    {
        private readonly HttpClient _client;

        public RatesService(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> GetRateByCity(string cityName)
        {
            try
            {
                var response = await _client.GetAsync($"countries");
                var ratesResponse = await _client.GetAsync("countries/info?returns=currency");

                if (response.IsSuccessStatusCode)
                {
                    var serializedString = await response.Content.ReadAsStringAsync();
                    var items = JsonConvert.DeserializeObject<CountryWrapper>(serializedString);

                    if (ratesResponse.IsSuccessStatusCode)
                    {
                        var serializedRates = await ratesResponse.Content.ReadAsStringAsync();
                        var rates = JsonConvert.DeserializeObject<CountryToRateWrapper>(serializedRates);

                        var country = items?.data.FirstOrDefault(c => c.cities.Select(s => s.ToLower()).Contains(cityName));
                        if (country != null)
                        {
                            var rate = rates?.data.FirstOrDefault(r => r.name == country.country);
                            return rate != null ? rate.currency : "";
                        }
                        // foreach (var countryToRate in rates.data)
                        // {
                        //     var country = items.data.FirstOrDefault(c => c.country == countryToRate.name);
                        //     country.cities.ForEach(c => citiesToRate.Add(new CityToRate()
                        //     {
                        //         cityName = c, 
                        //         currency = countryToRate.currency
                        //     }));
                        // }
                    }
                    
                    return "";
                }
                else
                {
                    throw new Exception($"problem with queries from server.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<string>> GetRates()
        {
            try
            {
                var ratesResponse = await _client.GetAsync("countries/info?returns=currency");

                if (ratesResponse.IsSuccessStatusCode)
                {
                    var serializedRates = await ratesResponse.Content.ReadAsStringAsync();
                    var rates = JsonConvert.DeserializeObject<CountryToRateWrapper>(serializedRates);
                    
                    return rates?.data.Select(r => r.currency);
                }
                else
                {
                    throw new Exception($"problem with queries from server.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> GetCountryByCity(string cityName)
        {
            try
            {
                var response = await _client.GetAsync($"countries");

                if (response.IsSuccessStatusCode)
                {
                    var serializedString = await response.Content.ReadAsStringAsync();
                    var items = JsonConvert.DeserializeObject<CountryWrapper>(serializedString);

                    var country = items?.data.FirstOrDefault(c => c.cities.Select(s => s.ToLower()).Contains(cityName));
                    
                    return country?.country;
                }
                else
                {
                    throw new Exception($"problem with queries from server.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}