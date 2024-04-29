using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities;

public sealed class Product : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public string Image { get; private set; }
    
    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public Product(string name, string description, decimal price, int stock, string image)
    {
        ValidateDomain(name, description, price, stock, image);
        
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        Image = image;
    }
    
    public Product(int id, string name, string description, decimal price, int stock, string image)
        : this(name, description, price, stock, image)
    {
        Id = id;
    }

    public void Update(string name, string description, decimal price, int stock, string image, int categoryId)
    {
        ValidateDomain(name, description, price, stock, image);
        
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        Image = image;
        CategoryId = categoryId;
    }

    private static void ValidateDomain(string name, string description, decimal price, int stock, string image)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Nome inválido! Informação é obrigatória.");
        DomainExceptionValidation.When(name?.Length < 3, "Nome inválido! Mínimo 3 caracteres são exigidos.");
        
        DomainExceptionValidation.When(string.IsNullOrEmpty(description), "Descrição inválida! Informação é obrigatória.");
        DomainExceptionValidation.When(description?.Length < 5, "Descrição inválida! Mínimo 5 caracteres são exigidos.");
        
        DomainExceptionValidation.When(price < 0, "Preço inválido! Valor deve ser maior ou igual a zero.");
        
        DomainExceptionValidation.When(stock < 0, "Estoque inválido! Valor deve ser maior ou igual a zero.");
        
        DomainExceptionValidation.When(image?.Length > 250, "Imagem inválida! Valor máximo de 250 caracteres ultrapassado.");
    }
    
}

#region Entity Product no Modelo Anêmico
    // public class Product
    // {
    //     public int Id { get; set; }
    //     public string Name { get; set; }
    //     public string Description { get; set; }
    //     public decimal Price { get; set; }
    //     public int Stock { get; set; }
    //     public string Image { get; set; }
    //     
    //     public int CategoryId { get; set; }
    //     public Category Category { get; set; }
    // }
#endregion