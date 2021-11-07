using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CounrtriesRateServer.Boundries;
using CounrtriesRateServer.Interfaces;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IEnumerable<string>> GetRates()
        {
            try
            {
                var ratesResponse = await _client.GetAsync("countries/info?returns=currency");

                if (ratesResponse.IsSuccessStatusCode)
                {
                    var serializedRates = await ratesResponse.Content.ReadAsStringAsync();
                    var rates = JsonConvert.DeserializeObject<CountryToRateWrapper>(serializedRates);

                    return rates?.data.GroupBy(rate => rate.currency).Select(r => r.FirstOrDefault().currency);
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
        
        public async Task<CityData> GetRateAndCountryByCity(string cityName)
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
                            return new CityData
                            {
                                name = cityName,
                                country = country.country,
                                rate = rate?.currency
                            };
                        }
                    }
                    
                    return new CityData();
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