using Chuck.Application;
using Chuck.Application.Features.Quotes;
using Chuck.Application.Models;
using Chuck.Infrastructure.Extensions;
using Chuck.Infrastructure.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Chuck.Infrastructure;

public class QuotesRepository : IQuotesRepository
{
    private readonly IMongoCollection<QuoteDao> _collection;

    public QuotesRepository(IOptions<QuotesDbSettings> options)
    {
        var client = new MongoClient(options.Value.ConnectionString);
        var database = client.GetDatabase(options.Value.DatabaseName);
        _collection = database.GetCollection<QuoteDao>(options.Value.CollectionName);
    }

    public Task AddQuotesAsync(IEnumerable<QuoteDto> quotes, CancellationToken cancellationToken) =>
        _collection.InsertManyAsync(quotes.Select(x => x.MapTo()).ToList(), cancellationToken: cancellationToken);
    
    public async Task<bool> ExistsAsync(string id) => await _collection.Find(x => x.Id == id).FirstOrDefaultAsync() is not null;
}