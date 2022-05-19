using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DG_Engineering.Framework.Global.Assignar
{
    internal class ProjectDocument
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Root
        {
            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("path")]
            public string Path { get; set; }
        }
    }
}
