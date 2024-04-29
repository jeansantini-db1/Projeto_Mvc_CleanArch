using System.Threading;
using System.Threading.Tasks;
using CleanArchMvc.Application.Mediator.CQRS.Products.Queries;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Mediator.CQRS.Products.Handlers;

public class GetByCategoryHandler: IRequestHandler<GetByCategoryQuery, Product>
{
    private readonly IProductRepository _productRepository;

    public GetByCategoryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<Product> Handle(GetByCategoryQuery request, CancellationToken cancellationToken)
    {
        return await _productRepository.GetByIdAsync(request.Id);
    }
    
}