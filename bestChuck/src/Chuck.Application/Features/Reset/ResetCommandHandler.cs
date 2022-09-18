using MediatR;

namespace Chuck.Application.Features.Reset;

public class ResetCommandHandler : IRequestHandler<ResetCommand, Unit>
{
    public Task<Unit> Handle(ResetCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}