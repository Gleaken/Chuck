using Chuck.Application.Models;
using MediatR;

namespace Chuck.Application.Features.GetTop10;

public class GetTop10QueryHandler : IRequestHandler<GetTop10Query, IEnumerable<QuoteDto>>
{
    public Task<IEnumerable<QuoteDto>> Handle(GetTop10Query request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}