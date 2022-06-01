using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace BackEndTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RapidAPIController : ControllerBase
    {

        [HttpGet("GetGoldPrice")]
        public async Task<IActionResult> GetGoldPrice()
        {
            HttpClient client;
            HttpRequestMessage request;
            CallRapidAPIEndpoint(out client, out request);
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(body);
                return Ok(body);
            }
        }

        private static void CallRapidAPIEndpoint(out HttpClient client, out HttpRequestMessage request)
        {
            client = new HttpClient();
            request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://gold-price-live.p.rapidapi.com/get_metal_prices"),
                Headers =
                {
                    { "X-RapidAPI-Host", "gold-price-live.p.rapidapi.com" },
                    { "X-RapidAPI-Key", "95930d6135msh9dbf8d88b4cd7dcp1f2663jsne63aa321554b"},
                },
            };
        }
    }
}
