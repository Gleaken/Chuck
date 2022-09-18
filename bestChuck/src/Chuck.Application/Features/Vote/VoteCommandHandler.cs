using MediatR;

namespace Chuck.Application.Features.Vote;

public class VoteCommandHandler : IRequestHandler<VoteCommand, Unit>
{
    public Task<Unit> Handle(VoteCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}