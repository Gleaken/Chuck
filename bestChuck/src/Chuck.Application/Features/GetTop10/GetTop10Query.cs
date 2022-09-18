using Chuck.Application.Models;
using MediatR;

namespace Chuck.Application.Features.GetTop10;

public record GetTop10Query() : IRequest<IEnumerable<QuoteDto>>;