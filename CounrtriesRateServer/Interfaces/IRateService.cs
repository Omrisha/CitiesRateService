using System.Collections.Generic;
using System.Threading.Tasks;
using CounrtriesRateServer.Boundries;

namespace CounrtriesRateServer.Interfaces
{
    public interface IRateService
    {
        Task<IEnumerable<string>> GetRates();
        Task<CityData> GetRateAndCountryByCity(string cityName);
    }
}