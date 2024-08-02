using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace IntegrationTest
{
    public class UnitTestInstruction : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _httpClient;
        public UnitTestInstruction(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _httpClient = factory.CreateClient();
        }

        /// <summary>
        /// Test the GetAll Endpoint of Instructions. 
        /// </summary>
        [Fact]
        public async void GetAllInstructions()
        {
            // Send an HTTP GET request to the "/api/instructions/getAll" endpoint using the _httpClient.
            var response = await _httpClient.GetAsync($"/api/instructions");

            // Assert that the response is not null.
            Assert.NotNull(response);

            // Assert that the request message within the response is not null.
            Assert.NotNull(response.RequestMessage);

            // Assert that the HTTP status code in the response is HttpStatusCode.OK (200).
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        /// <summary>
        /// Test the GetById Endpoint of Instructions.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetById()
        {
            // Define a variable 'id' and set it. This will be used in the API request.
            var id = 3;

            // Send an HTTP GET request to the "/api/instructions/getById/{id}" endpoint with the specified 'id'.
            var response = await _httpClient.GetAsync($"/api/instructions/{id}");

            // Assert that the response is not null.
            Assert.NotNull(response);

            // Assert that the reason phrase (a textual description of the response status) in the response is not null.
            Assert.NotNull(response.ReasonPhrase);

            // Assert that the HTTP status code in the response is HttpStatusCode.OK (200), indicating a successful request.
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        /// <summary>
        /// Test the Post Endpoint of Instructions By Post Static Data.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PostInstructions()
        {
            // Create a new HTTP POST request using the _httpClient to the "/api/instructions/post" endpoint.
            var response = await _httpClient.PostAsJsonAsync($"/api/instructions", new Instruction()
            {
                // Set properties of the new instruction here.
                InstructionDate = DateTime.Today.ToUniversalTime(), // Set the instruction date to the current UTC time.
                ClientName = "Akshay Sharma", 
                PickupAddress = "Chd mohali", 
                DeliveryAddress = "Pb", 
                ClientRef = "Abc", 
                BillingRef = "Card", 
                Products = new List<Product> { new Product
                {
                     ProductCode="5", 
                     ProductDescription="Nice Toys can u Buy", 
                     Qty=2 
                } }
            });

            // Assert that the response is not null.
            Assert.NotNull(response);

            // Assert that the HTTP status code in the response is HttpStatusCode.OK (200),
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        /// <summary>
        /// Test the Update Endpoint of Instructions By Update record using Id.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PutInstructions()
        {
            // Create a new HTTP PUT request using the _httpClient to the "/api/instructions/put" endpoint.
            var response = await _httpClient.PutAsJsonAsync($"/api/instructions", new Instruction()
            {
                // Set properties of the new instruction here.
                Id = 3,
                InstructionDate = DateTime.Today.ToUniversalTime(),
                ClientName = "Akshay ssss",
                PickupAddress = "Chd mmm",
                DeliveryAddress = "PbbB",
                ClientRef = "Xyzzz",
                BillingRef = "Cash",
                Status = "Pending",
                Products = new List<Product> { new Product
             {
                 ProductId = 14,
                 ProductCode = "5",
                 ProductDescription = "Nice",
                 Qty=2
             } }
            });

            // Assert that the response is not null.
            Assert.NotNull(response);

            // Assert that the HTTP status code in the response is HttpStatusCode.OK (200),
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        /// <summary>
        /// Test the Delete Endpoint of Instruction By Id.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteInstructions()
        {
            // Define a variable 'id' and set it. This will be used in the API request.
            var id = 4;

            // Create a new HTTP Delete request using the _httpClient to the "/api/instructions/delete" endpoint.
            var response = await _httpClient.DeleteAsync($"/api/instructions/{id}");

            // Assert that the response is not null.
            Assert.NotNull(response);

            // Assert that the HTTP status code in the response is HttpStatusCode.OK (200),
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}