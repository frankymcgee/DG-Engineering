using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        private async void ProjectAddress_TextChanged(object sender, EventArgs e)
        {
            string input = ProjectAddress.Text;

            if (string.IsNullOrWhiteSpace(input))
            {
                JobClearSuggestions();
                return;
            }
            using (HttpClient httpClient = new HttpClient())
            {
                string apiUrl = $"https://maps.googleapis.com/maps/api/place/autocomplete/json" +
                    $"?input={Uri.EscapeDataString(input)}" +
                    $"&key={Static.MapsApiKey}";

                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    JObject result = JObject.Parse(json);

                    // Extract suggestions from the API response and display them
                    JobDisplaySuggestions(result);
                    Console.WriteLine(result.ToString());
                }
                else
                {
                    // Handle API error or network issues
                }
            }
        }

        private void JobClearSuggestions()
        {
            // Clear the suggestions or hide the dropdown as needed
            Predictions.Items.Clear();
            Predictions.Visible = false;
        }

        private void JobDisplaySuggestions(JObject result)
        {
            Predictions.Items.Clear();
            Predictions.Visible = true;
            JArray predictions = (JArray)result["predictions"];
            if (predictions != null)
            {
                // Get the current cursor position in the TextBox
                int cursorPosition = Predictions.SelectionStart;
                foreach (JToken prediction in predictions)
                {
                    string description = prediction["description"].ToString();
                    Predictions.Items.Add(description);
                }

                if (Predictions.Items.Count > 0)
                {
                    // Display the ComboBox with suggestions below the TextBox
                    Predictions.DroppedDown = true;
                    // Restore the cursor position to the end of the TextBox text
                    Predictions.SelectionStart = cursorPosition;
                    Predictions.SelectionLength = 0;
                }
            }
        }

        private void Predictions_TextChanged(object sender, EventArgs e)
        {
            ProjectAddress.Text = Predictions.Text;
            Predictions.Items.Clear();
            Predictions.Visible = false;
        }
    }
}