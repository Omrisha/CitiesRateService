using System;

namespace CounrtriesRateServer.Boundries
{
    [Serializable]
    public class CityToRate
    {
        public string cityName { get; set; }
        public string currency { get; set; }
    }
}