using System.Collections.Generic;
using System.Threading.Tasks;
using CounrtriesRateServer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CounrtriesRateServer.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class CountriesRate : ControllerBase
    {
        private IRateService _ratesService;

        public CountriesRate(IRateService ratesService)
        {
            _ratesService = ratesService;
        }

        [HttpGet("rate/{cityName}")]
        public async Task<string> GetRateByCity(string cityName)
        {
            return  await _ratesService.GetRateByCity(cityName);
        }
        
        [HttpGet("country/{cityName}")]
        public async Task<string> GetCountryByCity(string cityName)
        {
            return  await _ratesService.GetCountryByCity(cityName);
        }

        [HttpGet()]
        public async Task<IEnumerable<string>> GetRates()
        {
            return await _ratesService.GetRates();
        }
    }
}