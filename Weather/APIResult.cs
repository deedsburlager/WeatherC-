using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    public class APITemp
    {
        [JsonProperty("temp")]
        public decimal CurrentTemp { get; set; }
        [JsonProperty("temp_max")]
        public decimal HighTemp { get; set; }
        [JsonProperty("temp_min")]
        public decimal LowTemp { get; set; }
        [JsonProperty("humidity")]
        public decimal Humidity { get; set; }

    }

    public class WeatherObj
    {
        [JsonProperty("main")]
        public APITemp Main { get; set; }
        [JsonProperty("name")]
        public string City { get; set; }
    }
}
