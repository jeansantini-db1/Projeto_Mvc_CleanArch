using CleanArchMvc.Domain.Entities;
using MediatR;

namespace CleanArchMvc.Application.Mediator.CQRS.Products.Queries;

public class GetProductByIdQuery : IRequest<Product>
{
    public int Id { get; set; }

    public GetProductByIdQuery(int id)
    {
        Id = id;
    }
}