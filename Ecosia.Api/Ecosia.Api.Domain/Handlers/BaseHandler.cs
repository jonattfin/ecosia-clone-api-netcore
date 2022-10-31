using Ecosia.Api.Domain.Repositories;
using MediatR;

namespace Ecosia.Api.Domain.Handlers;

public abstract class BaseHandler<Req, Resp> : IRequestHandler<Req, Resp> where Req : IRequest<Resp>
{
    protected readonly IUnitOfWork _unitOfWork;

    protected BaseHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public abstract Task<Resp> Handle(Req request, CancellationToken cancellationToken);
}