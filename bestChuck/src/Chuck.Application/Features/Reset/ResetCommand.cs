using MediatR;

namespace Chuck.Application.Features.Reset;

public record ResetCommand() : IRequest<Unit>;