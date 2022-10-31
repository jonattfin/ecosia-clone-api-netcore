using Ecosia.Api.Domain.Features.Projects.Models;
using MediatR;

namespace Ecosia.Api.Domain.Features.Projects.Handlers;

public class EmailNotificationHandler : BaseNotificationHandler<ProjectAddedNotification>
{
    public override async Task Handle(ProjectAddedNotification notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Email sent for {notification.Project}");
        await Task.CompletedTask;
    }
}

public class CacheInvalidationNotificationHandler : BaseNotificationHandler<ProjectAddedNotification>
{
    public override async Task Handle(ProjectAddedNotification notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Cache invalidated for {notification.Project}");
        await Task.CompletedTask;
    }
}

public record ProjectAddedNotification(Project Project) : INotification;


