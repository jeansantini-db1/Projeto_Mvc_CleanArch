using System.Collections.Generic;
using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities;

public sealed class Category : Entity
{
    public string Name { get; private set; }
    
    public ICollection<Product> Products { get; set; }

    public Category(string name)
    {
        ValidateDomain(name);
        
        Name = name;
    }

    public Category(int id, string name) : this(name)
    {
        Id = id;
    }

    public void Update(string name)
    {
        ValidateDomain(name);
        
        Name = name;
    }

    private static void ValidateDomain(string name)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Nome inválido! Informação é obrigatória.");
        DomainExceptionValidation.When(name?.Length < 3, "Nome inválido! Mínimo 3 caracteres são exigidos.");
    }
    
}

#region Entity Category no Modelo Anêmico
    // public class Category
    // {
    //     public int Id { get; set; }
    //     public string Name { get; set; }
    //     
    //     public ICollection<Product> Products { get; set; }
    // }
#endregion