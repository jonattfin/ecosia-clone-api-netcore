using Ecosia.Api.Domain.Repositories;
using MediatR;

namespace Ecosia.Api.Domain.Features.Shared.Handlers;

public abstract class BaseRequestHandler<TReq, TResp> : IRequestHandler<TReq, TResp> where TReq : IRequest<TResp>
{
    protected readonly IUnitOfWork UnitOfWork;

    protected BaseRequestHandler(IUnitOfWork unitOfWork) : this()
    {
        UnitOfWork = unitOfWork;
    }

    protected BaseRequestHandler()
    {
    }

    public abstract Task<TResp> Handle(TReq query, CancellationToken cancellationToken);
}