using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;
using FakeItEasy;
using BlobToEventHub;
using Microsoft.Azure.WebJobs;


namespace Tests
{
    public class FunctionsTests
    {
        private readonly ILogger logger = TestFactory.CreateLogger();
        AsyncCollector<string> a = new AsyncCollector<string>();



        [Fact]
        public async void Http_trigger_should_return_known_string()
        {

            var request = TestFactory.CreateHttpRequest("name", "Bill");
            var response = (OkObjectResult)await Function1.Run(request, a, logger);
            Assert.Equal("Hello, Bill. Name sent to Event HUB.", response.Value);
        }

        [Theory]
        [MemberData(nameof(TestFactory.Data), MemberType = typeof(TestFactory))]
        public async void Http_trigger_should_return_known_string_from_member_data(string queryStringKey, string queryStringValue)
        {
            var request = TestFactory.CreateHttpRequest(queryStringKey, queryStringValue);
            var response = (OkObjectResult)await Function1.Run(request, a, logger);
            Assert.Equal($"Hello, {queryStringValue}. Name sent to Event HUB.", response.Value);
        }

    }
}