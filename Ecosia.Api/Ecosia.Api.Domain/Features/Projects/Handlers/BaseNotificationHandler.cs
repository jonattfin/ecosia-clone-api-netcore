using MediatR;

namespace Ecosia.Api.Domain.Features.Projects.Handlers;

public abstract class BaseNotificationHandler<T>: INotificationHandler<T> where T : INotification
{
    public abstract Task Handle(T notification, CancellationToken cancellationToken);
}