using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiportGapApiTests.Utilities
{
    public class ApiTestClient
    {
        private HttpClient? _httpClient;


        public ApiTestClient()
        {
            _httpClient = CreateApiClient();
        }


        public HttpClient CreateApiClient()
        {
            var client = new HttpClient { BaseAddress = new Uri("https://airportgap.com/"), Timeout = TimeSpan.FromMilliseconds(1750) };
            return client;

        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }

    }
}
