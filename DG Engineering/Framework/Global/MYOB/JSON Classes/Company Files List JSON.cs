using Newtonsoft.Json;

// ReSharper disable once CheckNamespace
namespace DG_Engineering.Framework.Global.MYOB
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class CompanyFiles
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
        public class ProductLevel
        {
            [JsonProperty("Code")] public int Code { get; set; }

            [JsonProperty("Name")] public string Name { get; set; }
        }

        public class Root
        {
            [JsonProperty("Id")] public string Id { get; set; }

            [JsonProperty("Name")] public string Name { get; set; }

            [JsonProperty("LibraryPath")] public string LibraryPath { get; set; }

            [JsonProperty("ProductVersion")] public string ProductVersion { get; set; }

            [JsonProperty("ProductLevel")] public ProductLevel ProductLevel { get; set; }

            [JsonProperty("Subscription")] public object Subscription { get; set; }

            [JsonProperty("CheckedOutDate")] public object CheckedOutDate { get; set; }

            [JsonProperty("CheckedOutBy")] public object CheckedOutBy { get; set; }

            [JsonProperty("Uri")] public string Uri { get; set; }

            [JsonProperty("Country")] public string Country { get; set; }

            [JsonProperty("LauncherId")] public string LauncherId { get; set; }

            [JsonProperty("SerialNumber")] public string SerialNumber { get; set; }

            [JsonProperty("UIAccessFlags")] public int UiAccessFlags { get; set; }
        }

    }

}
