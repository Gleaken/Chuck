using Chuck.Application.Features.Filters;
using Chuck.Application.Features.Quotes;

namespace Chuck.Application.Validators;

public interface IValidator
{
    Task<bool> Validate(QuoteDto quote);
}

public class QuoteValidator : IValidator
{
    private readonly List<IQuoteFilter> _filters;

    public QuoteValidator(IEnumerable<IQuoteFilter> filters)
    {
        _filters = filters.ToList();
    }

    public async Task<bool> Validate(QuoteDto item)
    {
        var taskList = _filters.Select(filter => filter.Passed(item)).ToList();
        await Task.WhenAll(taskList);
        return taskList.All(x => x.Result);
    }
}