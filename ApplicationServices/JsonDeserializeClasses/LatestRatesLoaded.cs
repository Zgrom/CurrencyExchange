using System;
using System.Collections.Generic;

namespace ApplicationServices.JsonDeserializeClasses
{
    public class LatestRatesLoaded
    {
        public bool success { get; set; }
        public long timestamp { get; set; }
        public string baseCurrency { get; set; }
        public DateTime date { get; set; }
        public Dictionary<string,double> rates { get; set; }
    }
}