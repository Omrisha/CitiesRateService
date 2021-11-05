using System;

namespace CounrtriesRateServer.Boundries
{
    [Serializable]
    public class CountryToRate
    {
        public string name { get; set; }
        public string currency { get; set; }
    }
}