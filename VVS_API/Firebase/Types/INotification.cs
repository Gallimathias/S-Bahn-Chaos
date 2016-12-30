using Newtonsoft.Json;

namespace Firebase.Types
{
    public interface INotification
    {
        [JsonProperty(PropertyName = "title")]
        string Title { get; set; }

        [JsonProperty(PropertyName = "body")]
        string Body { get; set; }

        [JsonProperty(PropertyName = "sound")]
        string Sound { get; set; }

        [JsonProperty(PropertyName = "click_action")]
        string Click_action { get; set; }

        [JsonProperty(PropertyName = "body_loc_key")]
        string Body_loc_key { get; set; }

        [JsonProperty(PropertyName = "body_loc_args")]
        string[] Body_loc_args { get; set; }

        [JsonProperty(PropertyName = "title_loc_key")]
        string Title_loc_key { get; set; }

        [JsonProperty(PropertyName = "title_loc_args")]
        string[] Title_loc_args { get; set; }
    }
}