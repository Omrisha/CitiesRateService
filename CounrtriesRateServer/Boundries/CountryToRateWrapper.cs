using System;
using System.Collections.Generic;

namespace CounrtriesRateServer.Boundries
{
    [Serializable]
    public class CountryToRateWrapper
    {
        public string error { get; set; }
        public string msg { get; set; }
        public List<CountryToRate> data { get; set; }
    }
}