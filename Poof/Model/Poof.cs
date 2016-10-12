using System;

namespace Poof.Model
{
    public class Poof
    {
        [Newtonsoft.Json.JsonProperty("userId")]
        public string UserId { get; set; }

        [Newtonsoft.Json.JsonProperty("Id")]
        public string Id { get; set; }

        [Microsoft.WindowsAzure.MobileServices.Version]
        public string AzureVersion { get; set; }
      
        public DateTime DateUtc { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public string DateDisplay => DateUtc.ToLocalTime().ToString("d");

        [Newtonsoft.Json.JsonIgnore]
        public string TimeDisplay => DateUtc.ToLocalTime().ToString("t");
    }
}
