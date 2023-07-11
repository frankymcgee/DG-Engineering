using System.Collections.Generic;
using Newtonsoft.Json;

namespace DG_Engineering.Framework.Global.SimPro
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class SPDocuments
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Root
        {
            [JsonProperty("ID")]
            public string ID { get; set; }

            [JsonProperty("Filename")]
            public string Filename { get; set; }

            [JsonProperty("Folder")]
            public object Folder { get; set; }

            [JsonProperty("Public")]
            public bool Public { get; set; }

            [JsonProperty("Email")]
            public bool Email { get; set; }

            [JsonProperty("Base64Data")]
            public string Base64Data { get; set; }

            [JsonProperty("MimeType")]
            public string MimeType { get; set; }

            [JsonProperty("FileSizeBytes")]
            public int FileSizeBytes { get; set; }

            [JsonProperty("DateAdded")]
            public string DateAdded { get; set; }
        }
    }
}
