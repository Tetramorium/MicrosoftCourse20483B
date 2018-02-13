using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Model
{
    public class Country
    {
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("capital")]
        public string Capital { get; set; }

        [JsonProperty("population")]
        public int Population { get; set; }


    }
}
