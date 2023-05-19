using Newtonsoft.Json;

namespace w3dniDoSetki.Models;

    using Newtonsoft.Json;

    public class CarJson
    {
        public class Car
        {
            [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
            public string Value { get; set; }

            [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
            public string Title { get; set; }

            [JsonProperty("models", NullValueHandling = NullValueHandling.Ignore)]
            public Model[] Models { get; set; }
        }

        public class Model
        {
            [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
            public string Value { get; set; }

            [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
            public string Title { get; set; }
        }
    }