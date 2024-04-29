using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchMvc.Application.Mediator.CQRS.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Mediator.CQRS.Products.Handlers;

public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand, Product>
{
    private readonly IProductRepository _productRepository;

    public ProductUpdateCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<Product> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);

        if (product is null)
            throw new ArgumentNullException(nameof(request), "Produto não encontrado");
        
        product.Update(request.Name, request.Description, request.Price, request.Stock, request.Image,
            request.CategoryId);

        return await _productRepository.UpdateAsync(product);
    }
}