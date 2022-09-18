using Chuck.Application.Features.Filters;
using Chuck.Application.Features.Quotes;
using Chuck.Application.Models;
using Microsoft.Extensions.Logging;


namespace Chuck.Tests.Filters;

public class FiltersTests
{
    private readonly QuoteLengthFilter _sut;
    private readonly ILogger<QuoteLengthFilter> _logger = Substitute.For<ILogger<QuoteLengthFilter>>();
    
    public FiltersTests()
    {
        _sut = new QuoteLengthFilter(_logger);
    }
    
    [Theory]
    [InlineData(201)]
    [InlineData(300)]
    [InlineData(500)]
    public async Task Passed_ShouldReturnFalse_WhenQuoteIsOver200CharLength(int length)
    {
        // Arrange
        var quote = new QuoteDto()
        {
            Id = "1",
            Quote = new string('*', length)
        };
        
        //Act
        var result = await _sut.Passed(quote);
        
        //Assert
        Assert.False(result);
    }
    
    [Theory]
    [InlineData(10)]
    [InlineData(0)]
    [InlineData(50)]
    [InlineData(200)]
    public async Task PassedShouldReturnTrue_WhenQuoteIs200OrLessCharLength(int length)
    {
        // Arrange
        var quote = new QuoteDto()
        {
            Id = "1",
            Quote = new string('*', length)
        };
        
        //Act
        var result = await _sut.Passed(quote);
        
        //Assert
        Assert.True(result);
    }
}