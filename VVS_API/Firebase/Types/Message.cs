using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firebase.Types
{
    public class Message
    {
        #region Targets
        [JsonProperty(PropertyName ="to")]
        public string To { get; set; }

        [JsonProperty(PropertyName = "registration_ids")]
        public string[] Registration_ids { get; set; }

        [JsonProperty(PropertyName = "condition")]
        public string Condition { get; set; }
        #endregion Targets

        #region Options
        [JsonProperty(PropertyName = "collapse_key")]
        public string[] Collapse_key { get; set; }

        [JsonProperty(PropertyName = "priority")]
        public string[] Priority { get; set; }

        [JsonProperty(PropertyName = "content_available")]
        public bool Content_available { get; set; }

        [JsonProperty(PropertyName = "time_to_live")]
        public int Time_to_live { get; set; }

        [JsonProperty(PropertyName = "restricted_package_name")]
        public string Restricted_package_name { get; set; }

        [JsonProperty(PropertyName = "dry_run")]
        public string Dry_run { get; set; }
        #endregion Options

        #region Payload
        [JsonProperty(PropertyName = "data")]
        public object Data { get; set; }

        [JsonProperty(PropertyName = "notification")]
        public INotification Notification { get; set; }
        #endregion Payload
    }
}
