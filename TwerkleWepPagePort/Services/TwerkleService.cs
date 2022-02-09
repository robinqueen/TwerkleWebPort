using RestSharp;
using System.Net.Http;
using System.Text.Json;
using TwerkleWepPagePort.Models;

namespace TwerkleWepPagePort.Services
{
    public class TwerkleService : ITwerkleService
    {

        public TwerkleService()
        {
        }

        public async Task<string> GetWordOfDay()
        {
            string word = "";
            try
            {
                var client = new RestClient("http://api.rqfreelance.com/GetTodaysWordOfDay");
                var request = new RestRequest();
                var response = await client.ExecuteGetAsync(request);
                word = JsonSerializer.Deserialize<string>(response.Content);

            }
            catch
            {
                throw;
            }

            return word;

        }
    }
}
