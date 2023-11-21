using AiportGapApiTests.Utilities;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AiportGapApiTests
{
    public class AirportWithAuthTests
    {
        private ApiTestClient _apiClient;


        [OneTimeSetUp]
        public void Setup()
        {
            _apiClient = new ApiTestClient();
        }

        [OneTimeTearDown]

        public void Teardown() { _apiClient.Dispose(); }


        [Test]

        public async Task GetAllFavouritesReturns200()
        {

            var client = _apiClient.CreateApiClient();
            var token = GetToken().Result;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", token?.ToString());
            var response = await client.GetAsync("api/favorites");

            var responseBody = await response.Content.ReadAsStringAsync();

            var getResults = JObject.Parse(responseBody);

            var test = getResults["data"].ToString();

            Assert.That(response.IsSuccessStatusCode, Is.True);
            //check the airpot id
            Assert.That(test.Contains("12646"));

            Console.WriteLine(test);

        }

        [Test]

        public async Task GetFavouriteByIdRetunrs200()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "api/favorites/?airport_id=2983");

            var client = _apiClient.CreateApiClient();
            var token = GetToken().Result;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", token?.ToString());
            var parameter = new Dictionary<string, string> { { "airport_id", "2983" } };
            var encodedContent = new FormUrlEncodedContent(parameter);

            //Set the content of the request message object to your paramaters
            requestMessage.Content = encodedContent;

            var response = await client.SendAsync(requestMessage);

            Assert.That(response.IsSuccessStatusCode);



        }



        public async Task<string?> GetToken()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "api/tokens");

            //Build the parameters same as before
            var parameters = new Dictionary<string, string> { { "email", "richard.brownsett@yahoo.co.uk" }, { "password", "Hoppus182" } };
            var encodedContent = new FormUrlEncodedContent(parameters);

            requestMessage.Content = encodedContent;

            var responseMessage = await _apiClient.CreateApiClient().SendAsync(requestMessage);

            var responseBody = await responseMessage.Content.ReadAsStringAsync();

            var getResults = JObject.Parse(responseBody);
            var token = getResults["token"].ToString();

            return token.ToString();

        }


    }
}
