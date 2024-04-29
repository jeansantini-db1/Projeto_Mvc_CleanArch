namespace CleanArchMvc.Application.Mediator.CQRS.Products.Commands;

public class ProductUpdateCommand : ProductCommand
{
    public int Id { get; set; }
}