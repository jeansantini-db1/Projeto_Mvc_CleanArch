using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Mediator.CQRS.Products.Commands;
using CleanArchMvc.Application.Mediator.CQRS.Products.Queries;
using MediatR;

namespace CleanArchMvc.Application.Services;

public class ProductService : IProductService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    
    public ProductService(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<ProductDto>> GetProductsAsync()
    {
        var products = await _mediator.Send(new GetProductsQuery());
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto> GetByIdAsync(int? id)
    {
        if (!id.HasValue)
            throw new ArgumentNullException(nameof(id));
        
        var product = await _mediator.Send(new GetProductByIdQuery(id.Value));
        return _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> GetByCategoryAsync(int? id)
    {
        if (!id.HasValue)
            throw new ArgumentNullException(nameof(id));
        
        var product = await _mediator.Send(new GetByCategoryQuery(id.Value));
        return _mapper.Map<ProductDto>(product);
    }

    public async Task CreateAsync(ProductDto productDto)
    {
        var productCreate = _mapper.Map<ProductCreateCommand>(productDto);
        await _mediator.Send(productCreate);
    }

    public async Task UpdateAsync(ProductDto productDto)
    {
        var productUpdate = _mapper.Map<ProductUpdateCommand>(productDto);
        await _mediator.Send(productUpdate);
    }

    public async Task RemoveAsync(int? id)
    {
        if (!id.HasValue)
            throw new ArgumentNullException(nameof(id));
        
        var productRemove = new ProductRemoveCommand(id.Value);
        await _mediator.Send(productRemove);
    }
}