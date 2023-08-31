using System.Collections.Generic;
using Newtonsoft.Json;

namespace DG_Engineering.Framework.Global.SimPro
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class SPDocumentList
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
        public class Root
        {
            [JsonProperty("ID")]
            public string ID { get; set; }

            [JsonProperty("Filename")]
            public string Filename { get; set; }
        }
    }
}
