using Ecosia.Api.Domain.Repositories;
using MediatR;

namespace Ecosia.Api.Domain.Features.Projects.Handlers;

public abstract class BaseRequestHandler<TReq, TResp> : IRequestHandler<TReq, TResp> where TReq : IRequest<TResp>
{
    protected readonly IUnitOfWork UnitOfWork;

    protected BaseRequestHandler(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }
    
    public abstract Task<TResp> Handle(TReq request, CancellationToken cancellationToken);
}