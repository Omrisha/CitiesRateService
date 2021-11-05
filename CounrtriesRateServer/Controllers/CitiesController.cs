using System.Collections.Generic;
using System.Threading.Tasks;
using CounrtriesRateServer.Boundries;
using CounrtriesRateServer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CounrtriesRateServer.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class CitiesController : ControllerBase
    {
        private IRateService _ratesService;

        public CitiesController(IRateService ratesService)
        {
            _ratesService = ratesService;
        }

        [HttpGet("{cityName}")]
        public async Task<CityData> GetRateAndCountryByCity(string cityName)
        {
            return  await _ratesService.GetRateAndCountryByCity(cityName);
        }

        [HttpGet("rates")]
        public async Task<IEnumerable<string>> GetRates()
        {
            return await _ratesService.GetRates();
        }
    }
}