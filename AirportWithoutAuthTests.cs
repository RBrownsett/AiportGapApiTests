using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;
using AiportGapApiTests.Data;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions;

namespace AiportGapApiTests
{
    public class TestAirportWithoutAuthTests
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

            var responseContent = await _httpResponseMessage.Content.ReadAsStringAsync();

            //Assert

            Assert.That(_httpResponseMessage.IsSuccessStatusCode, Is.True);
            Assert.That(responseContent.Contains("\"miles\":304.76001022047103"), Is.True);

            var getResults = JObject.Parse(responseContent);
            var productCount = getResults["data"].ToString();

            Console.WriteLine(responseContent);
        }

        [Test]

        public async Task GetAllAirportsReturns200()
        {
            _httpResponseMessage = await _httpClient.GetAsync("api/airports");

            //var responseContent = await _httpResponseMessage.Content.ReadFromJsonAsync<AiportGapApiTests.Data.InstructionClass>();

            Assert.That(_httpResponseMessage, Is.Not.Null); 
            Assert.That(_httpResponseMessage.IsSuccessStatusCode, Is.True);
        }

        [Test]

        public async Task GetAirportByIdReturns200()
        {
            _httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "api/airports/");

            //Build the parameters same as before
            var parameter = new Dictionary<string, string> { { "id", "KIX" } };
            var encodedContent = new FormUrlEncodedContent(parameter);

            //Set the content of the request message object to your paramaters
            _httpRequestMessage.Content = encodedContent;

            //send the response

            _httpResponseMessage = await _httpClient.SendAsync(_httpRequestMessage);

            var responseContent = await _httpResponseMessage.Content.ReadAsStringAsync();   

            //var responseContent = await _httpResponseMessage.Content.ReadAsAsync<InstructionClass>();
            //responseContent.Data.Attributes.Altitude = 0;
            //var test = JsonConvert.DeserializeObject<InstructionClass>(_httpResponseMessage);

            //Assert
            Assert.That(_httpResponseMessage, Is.Not.Null);
            Assert.That(_httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(responseContent.Contains("Goroka Airport"));
        }

        [Test]

        public async Task PostGetTokenEndpointReturns200()
        {
            _httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "api/tokens");

            //Build the parameters same as before
            var parameters = new Dictionary<string, string> { { "email", "richard.brownsett@yahoo.co.uk" }, { "password", "Hoppus182" } };
            var encodedContent = new FormUrlEncodedContent(parameters);

            _httpRequestMessage.Content = encodedContent;

            _httpResponseMessage = await _httpClient.SendAsync(_httpRequestMessage);

            var responseBody = await _httpResponseMessage.Content.ReadAsStringAsync();

            var getResults = JObject.Parse(responseBody);
            var token = getResults["token"].ToString();

            Assert.That(_httpResponseMessage, Is.Not.Null);
            Assert.That(_httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(token.Length, Is.GreaterThan(0));

        }



    }
}