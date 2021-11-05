using System;
using System.Collections.Generic;

namespace CounrtriesRateServer.Boundries
{
    [Serializable]
    public class Country
    {
        public string country { get; set; }
        public List<string> cities { get; set; }
    }
}