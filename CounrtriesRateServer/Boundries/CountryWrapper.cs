using System;
using System.Collections.Generic;

namespace CounrtriesRateServer.Boundries
{
    [Serializable]
    public class CountryWrapper
    {
        public string error { get; set; }
        public string msg { get; set; }
        public List<Country> data { get; set; }
    }
}