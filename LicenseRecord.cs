using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContosoLicenseFunctions
{
    public class LicenseRecord
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "platenumber")]
        public string PlateNumber { get; set; }

        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

        [JsonProperty(PropertyName = "datecaptured")]
        public DateTime DateCaptured { get; set; }

        [JsonProperty(PropertyName = "photourl")]
        public string PhotoURL { get; set; }

        [JsonProperty(PropertyName = "isprocessed")]
        public bool IsProcessed { get; set; }

        [JsonProperty(PropertyName = "dateprocessed", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime DateProcessed { get; set; }
    }
}

