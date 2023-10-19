using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System.Net.Http;
using System.Net;

namespace AiportGapApiTests
{
    public class Tests
    {
        private HttpClient _httpClient;
        private HttpResponseMessage? _httpResponseMessage;
        private HttpRequestMessage? _httpRequestMessage;

        [OneTimeSetUp]
        public void Setup()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://airportgap.com/"), Timeout = TimeSpan.FromMilliseconds(1750)};
        }

        [Test]
        public async Task PostAirportsDistanceReturns200()
        {

            _httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "api/airports/distance");

            //Build the parameters same as before
            var parameters = new Dictionary<string, string> { { "from", "KIX" }, { "to", "NRT" } };
            var encodedContent = new FormUrlEncodedContent(parameters);

            //Set the content of the request message object to your paramaters
            _httpRequestMessage.Content = encodedContent;

            //send the response

            _httpResponseMessage = await _httpClient.SendAsync(_httpRequestMessage);

            //Assert

            Assert.That(_httpResponseMessage.IsSuccessStatusCode, Is.True);


        }

        [Test]

        public async Task Test2()
        {
            using (var message = new HttpRequestMessage(HttpMethod.Post, "api/airports/distance"))
            {
                //Build the parameters same as before
                var parameters = new Dictionary<string, string> { { "from", "KIX" }, { "to", "NRT" } };
                var encodedContent = new FormUrlEncodedContent(parameters);

                //Set the content of the request message object to your paramaters
                message.Content = encodedContent;

                //Send the request and await the response.
                var response = await _httpClient.SendAsync(message).ConfigureAwait(false);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // Do something with response. Example get content:
                    // var responseContent = await response.Content.ReadAsStringAsync ().ConfigureAwait (false);
                }
            }
        }
    }
}