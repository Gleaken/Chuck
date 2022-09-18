using Chuck.Application;
using Chuck.Application.Features.Filters;
using Chuck.Application.Models;
using Microsoft.Extensions.Logging;

namespace Chuck.Tests.Filters;

public class AlreadyHarvestedFilterTests
{
    private readonly AlreadyHarvestedFilter _sut;
    private readonly ILogger<AlreadyHarvestedFilter> _logger = Substitute.For<ILogger<AlreadyHarvestedFilter>>();
    private readonly IQuotesRepository _repository = Substitute.For<IQuotesRepository>();

    public AlreadyHarvestedFilterTests()
    {
        _sut = new AlreadyHarvestedFilter(_repository, _logger);
    }

    [Fact]
    public async Task PassedShouldReturnTrue_WhenRepositoryReturnsFalse()
    {
        //Arrange
        var quote = new QuoteDto()
        {
            Id = "1",
            Quote = "aa"
        };

        _repository.ExistsAsync(quote.Id).Returns(false);
        //Act
        var result = await _sut.Passed(quote);
        //Assert
        Assert.True(result);
    }
}