using System.Net;
using System.Net.Http.Json;
using Chuck.Worker;
using Chuck.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RichardSzalay.MockHttp;

namespace Chuck.Tests.HttpClient;

public class ChuckHttpClientTests
{
    private IChuckHttpClient _sut;
    private readonly ILogger<ChuckHttpClient> _logger = Substitute.For<ILogger<ChuckHttpClient>>();
    private readonly MockHttpMessageHandler _handlerMock = new MockHttpMessageHandler();
    private const string Url = "https://api.chucknorris.io/jokes/random";

    [Theory]
    [InlineData(HttpStatusCode.Conflict)]
    [InlineData(HttpStatusCode.Forbidden)]
    [InlineData(HttpStatusCode.Unauthorized)]
    [InlineData(HttpStatusCode.BadRequest)]
    [InlineData(HttpStatusCode.InternalServerError)]
    [InlineData(HttpStatusCode.NotFound)]
    [InlineData(HttpStatusCode.ServiceUnavailable)]
    public async Task GetQuoteShouldReturnNull_WhenRequestFails(HttpStatusCode httpStatusCode)
    {
        //Arrange
        _handlerMock.When(Url).Respond(httpStatusCode);
        var options = Options.Create(new WorkerConfig()
        {
            QuoteUrl = Url
        });
        _sut = new ChuckHttpClient(new System.Net.Http.HttpClient(_handlerMock), options, _logger);

        //Act
        var result = await _sut.GetQuote();

        //Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task GetQuoteShouldReturnNull_WhenCantParseResponse()
    {
        //Arrange
        _handlerMock.When(Url).Respond(HttpStatusCode.OK, "application/json", @"{{""test"": ""should fail"" }}");
        var options = Options.Create(new WorkerConfig()
        {
            QuoteUrl = Url
        });
        _sut = new ChuckHttpClient(new System.Net.Http.HttpClient(_handlerMock), options, _logger);

        //Act
        var result = await _sut.GetQuote();

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetQuoteShouldReturnQuote_WhenResponseIsValid()
    {
        //Arrange
        var quote = new
        {
            categories = new string[] { },
            created_at = DateTime.UtcNow,
            icon_url = "some string",
            id = "L5177IAFR0CPvrr89N6kag",
            updated_at = DateTime.UtcNow,
            url = "https://api.chucknorris.io/jokes/L5177IAFR0CPvrr89N6kag",
            value = "Chuck Norris's beard is so powerful that it has a beard itself"
        };
        _handlerMock.When(Url).Respond(HttpStatusCode.OK, JsonContent.Create(quote));
        var options = Options.Create(new WorkerConfig()
        {
            QuoteUrl = Url
        });
        _sut = new ChuckHttpClient(new System.Net.Http.HttpClient(_handlerMock), options, _logger);

        //Act
        var result = await _sut.GetQuote();

        //Assert
        Assert.Equal(quote.value, result.Value);
        Assert.Equal(quote.id, result.Id);
    }
}