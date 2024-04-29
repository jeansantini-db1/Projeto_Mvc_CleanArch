using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchMvc.Application.Mediator.CQRS.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Mediator.CQRS.Products.Handlers;

public class ProductRemoveCommandHandler: IRequestHandler<ProductRemoveCommand, Product>
{
    private readonly IProductRepository _productRepository;

    public ProductRemoveCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> Handle(ProductRemoveCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);

        if (product is null)
            throw new ArgumentNullException(nameof(request), "Produto não encontrado");

        return await _productRepository.RemoveAsync(product);
    }
}