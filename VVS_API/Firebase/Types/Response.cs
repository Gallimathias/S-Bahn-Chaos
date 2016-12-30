using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firebase.Types
{
    public class Response
    {
        [JsonProperty(PropertyName = "multicast_id")]
        public int Multicast_id { get; set; }

        [JsonProperty(PropertyName = "success")]
        public int Success { get; set; }

        [JsonProperty(PropertyName = "failure")]
        public int Failure { get; set; }

        [JsonProperty(PropertyName = "canonical_ids")]
        public int Canonical_ids { get; set; }

        [JsonProperty(PropertyName = "results")]
        public object[] Results { get; set; }
    }
}
