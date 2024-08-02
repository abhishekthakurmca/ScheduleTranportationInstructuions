using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace IntegrationTest
{
    public class UnitTestSchedule : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _httpClient;
        public UnitTestSchedule(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _httpClient = factory.CreateClient();
        }

        /// <summary>
        /// Test the GetAll Endpoint of Schedule.
        /// </summary>
        [Fact]
        public async void GetAllSchedule()
        {
            // Send an HTTP GET request to the "api/schedule/getAll" endpoint using the _httpClient.
            var response = await _httpClient.GetAsync($"/api/schedule");

            // Assert that the response is not null.
            Assert.NotNull(response);

            // Assert that the request message within the response is not null.
            Assert.NotNull(response.RequestMessage);

            // Assert that the HTTP status code in the response is HttpStatusCode.OK (200).
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        /// <summary>
        /// Test the GetById Endpoint of Schedule By Id.
        /// </summary>
        [Fact]
        public async void GetByIdSchedule()
        {
            // Define a variable 'id' and set it to 20. This will be used in the API request.
            var id = 1;

            // Send an HTTP GET request to the "/api/schedule/getById/{id}" endpoint with the specified 'id'.
            var response = await _httpClient.GetAsync($"/api/schedule/{id}");

            // Assert that the response is not null.
            Assert.NotNull(response);

            // Assert that the reason phrase (a textual description of the response status) in the response is not null.
            Assert.NotNull(response.ReasonPhrase);

            // Assert that the HTTP status code in the response is HttpStatusCode.OK (200), indicating a successful request.
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        /// <summary>
        /// Test the Post Endpoint of Schedule By Post Static Data.
        /// </summary>
        [Fact]
        public async void PostSchedule()
        {
            // Create a new HTTP POST request using the _httpClient to the "/api/schedule/post" endpoint.
            var response = await _httpClient.PostAsJsonAsync($"/api/schedule", new ScheduleTransport()
            {
                DateScheduled = DateTime.Today.ToUniversalTime(), // Set the scheduled date to the current UTC time.
                Transporter = "anyOne", 
                InstructionId = 1, 
                ProductId = 1 
            });

            // Assert that the response is not null.
            Assert.NotNull(response);

            // Assert that the HTTP status code in the response is HttpStatusCode.OK (200),
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        /// <summary>
        /// Test the Update Endpoint of Schedule By Update record By Id.
        /// </summary>
        [Fact]
        public async void PutSchedule()
        {
            // Create a new HTTP PUT request using the _httpClient to the "/api/schedule/put" endpoint.
            var response = await _httpClient.PutAsJsonAsync($"/api/schedule", new ScheduleTransport()
            {
                ScheduleTransportID = 1, 
                DateScheduled = DateTime.Today.ToUniversalTime(), // Set the scheduled date to the current UTC time.
                Transporter = "Its Ok", 
                InstructionId = 1, 
                ProductId = 1 
            });

            // Assert that the response is not null.
            Assert.NotNull(response);

            // Assert that the HTTP status code in the response is HttpStatusCode.OK (200),
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }
    }
}
