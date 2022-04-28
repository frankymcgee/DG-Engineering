using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DG_Engineering.Framework.Global.SimPro
{
    internal class DocumentInformation
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Folder
        {
            [JsonProperty("ID")]
            public int ID { get; set; }

            [JsonProperty("Name")]
            public string Name { get; set; }
        }

        public class Root
        {
            [JsonProperty("ID")]
            public string ID { get; set; }

            [JsonProperty("Filename")]
            public string Filename { get; set; }

            [JsonProperty("Folder")]
            public Folder Folder { get; set; }

            [JsonProperty("Public")]
            public bool Public { get; set; }

            [JsonProperty("Email")]
            public bool Email { get; set; }

            [JsonProperty("MimeType")]
            public string MimeType { get; set; }

            [JsonProperty("FileSizeBytes")]
            public int FileSizeBytes { get; set; }

            [JsonProperty("DateAdded")]
            public string DateAdded { get; set; }
        }


    }
}
