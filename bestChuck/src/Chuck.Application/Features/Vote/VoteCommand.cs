using MediatR;

namespace Chuck.Application.Features.Vote;

public record VoteCommand(string Id) : IRequest<Unit>;