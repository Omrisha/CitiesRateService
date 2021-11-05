using System.Collections.Generic;
using System.Threading.Tasks;

namespace CounrtriesRateServer.Interfaces
{
    public interface IRateService
    {
        Task<string> GetRateByCity(string cityName);
        Task<IEnumerable<string>> GetRates();
        Task<string> GetCountryByCity(string cityName);
    }
}