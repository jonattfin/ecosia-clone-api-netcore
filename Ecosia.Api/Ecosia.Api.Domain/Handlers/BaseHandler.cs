using Ecosia.Api.Domain.Repositories;
using MediatR;

namespace Ecosia.Api.Domain.Handlers;

public abstract class BaseHandler<TReq, TResp> : IRequestHandler<TReq, TResp> where TReq : IRequest<TResp>
{
    protected readonly IUnitOfWork UnitOfWork;

    protected BaseHandler(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }
    
    public abstract Task<TResp> Handle(TReq request, CancellationToken cancellationToken);
}