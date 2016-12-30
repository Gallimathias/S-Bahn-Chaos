using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firebase.Types
{
    public class AndroidNotification : INotification
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }

        [JsonProperty(PropertyName = "icon")]
        public string Icon { get; set; }

        [JsonProperty(PropertyName = "sound")]
        public string Sound { get; set; }

        [JsonProperty(PropertyName = "tag")]
        public string Tag { get; set; }

        [JsonProperty(PropertyName = "color")]
        public string Color { get; set; }

        [JsonProperty(PropertyName = "click_action")]
        public string Click_action { get; set; }

        [JsonProperty(PropertyName = "body_loc_key")]
        public string Body_loc_key { get; set; }

        [JsonProperty(PropertyName = "body_loc_args")]
        public string[] Body_loc_args { get; set; }

        [JsonProperty(PropertyName = "title_loc_key")]
        public string Title_loc_key { get; set; }

        [JsonProperty(PropertyName = "title_loc_args")]
        public string[] Title_loc_args { get; set; }
    }
}
