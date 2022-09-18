using Chuck.Application.Validators;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Chuck.Application.Features.Quotes;

public class AddQuotesCommandHandler : IRequestHandler<AddQuotesCommand, Unit>
{
    private readonly IQuotesRepository _repository;
    private readonly ILogger<AddQuotesCommandHandler> _logger;
    private readonly IValidator _validator;

    public AddQuotesCommandHandler(IQuotesRepository repository, ILogger<AddQuotesCommandHandler> logger, IValidator validator)
    {
        _repository = repository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Unit> Handle(AddQuotesCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var filteredQuotes = await request.Quotes.ToAsyncEnumerable()
                .WhereAwait(async x => await _validator.Validate(x))
                .Select(x => x)
                .ToListAsync();

            if (!filteredQuotes.Any())
            {
                _logger.LogInformation("None of the quotes didn't pass validation");
                return Unit.Value;
            }
            
            await _repository.AddQuotesAsync(filteredQuotes, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("{Exception}", ex);
            throw;
        }
        return Unit.Value;
    }
}