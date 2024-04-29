using CleanArchMvc.Domain.Entities;
using MediatR;

namespace CleanArchMvc.Application.Mediator.CQRS.Products.Queries;

public class GetByCategoryQuery: IRequest<Product>
{
    public int Id { get; set; }

    public GetByCategoryQuery(int id)
    {
        Id = id;
    }
}