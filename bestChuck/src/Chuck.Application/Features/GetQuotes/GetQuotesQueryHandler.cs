using Chuck.Application.Models;
using MediatR;

namespace Chuck.Application.Features.GetQuotes;

public class GetQuotesQueryHandler : IRequestHandler<GetQuotesQuery, IEnumerable<QuoteDto>>
{
    public Task<IEnumerable<QuoteDto>> Handle(GetQuotesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}