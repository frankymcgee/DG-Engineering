using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DG_Engineering.Framework.Global.SimPro
{
    internal class Documents
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Root
        {
            [JsonProperty("ID")]
            public string ID { get; set; }

            [JsonProperty("Filename")]
            public string Filename { get; set; }
        }
    }
}
