using Chuck.Application.Models;
using MediatR;

namespace Chuck.Application.Features.Quotes;

public record AddQuotesCommand(List<QuoteDto> Quotes) : IRequest<Unit>;