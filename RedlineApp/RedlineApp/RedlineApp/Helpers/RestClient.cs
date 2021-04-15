/*
    File name: RestClient.cs
    Purpose:   Return results of Http query as JSON data.
    Author:    Cody Sheridan
    Version:   1.0.0
*/

using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace RedlineApp.Helpers
{
    public class RestClient<T>
    {
        public async Task<T> GetAsync(string WebServiceUrl)
        {
            // Get results from Http query string and return json data.
            try
            {
                var httpClient = new HttpClient();
                var json = await httpClient.GetStringAsync(WebServiceUrl);
                var taskModels = JsonConvert.DeserializeObject<T>(json);

                return taskModels;
            }
            catch (Exception e)
            {
                Debug.WriteLine("GetAsync error: " + e.Message);
                return default;
            }
        }
    }
}
