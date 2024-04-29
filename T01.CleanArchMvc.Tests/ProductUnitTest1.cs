using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Validation;
using FluentAssertions;

namespace T00.CleanArchMvc.Tests;

public class ProductUnitTest1
{
    [Fact(DisplayName = "Criar entidade Product com parâmetros válidos NÃO DEVE disparar exceção")]
    public void CreateProduct_WithValidParameters_ResultObjectValidState()
    {
        var action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "Product Image");
        
        action.Should().NotThrow<DomainExceptionValidation>();
    }
    
    [Fact(DisplayName = "Criar entidade Product com id negativo DEVE disparar exceção")]
    public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
    {
        var action = () => new Product(-1, "Product Name", "Product Description", 9.99m, 99, "Product Image");
        
        action.Should().Throw<DomainExceptionValidation>().WithMessage("Id inválido! Valor deve ser maior que zero.");
    }
    
    [Fact(DisplayName = "Criar entidade Product com nome nulo DEVE disparar exceção")]
    public void CreateProduct_WithNullNameValue_DomainExceptionRequiredName()
    {
        var action = () => new Product(1, null, "Product Description", 9.99m, 99, "Product Image");
        
        action.Should().Throw<DomainExceptionValidation>().WithMessage("Nome inválido! Informação é obrigatória.");
    }
    
    [Fact(DisplayName = "Criar entidade Product com nome vazio DEVE disparar exceção")]
    public void CreateProduct_MissingNameValue_DomainExceptionRequiredName()
    {
        var action = () => new Product(1, "", "Product Description", 9.99m, 99, "Product Image");
        
        action.Should().Throw<DomainExceptionValidation>().WithMessage("Nome inválido! Informação é obrigatória.");
    }
    
    [Fact(DisplayName = "Criar entidade Product com nome de menos de 3 caracteres DEVE disparar exceção")]
    public void CreateProduct_ShortNameValue_DomainExceptionShortName()
    {
        var action = () => new Product(1, "Pr", "Product Description", 9.99m, 99, "Product Image");
        
        action.Should().Throw<DomainExceptionValidation>().WithMessage("Nome inválido! Mínimo 3 caracteres são exigidos.");
    }
    
    [Fact(DisplayName = "Criar entidade Product com descrição nula DEVE disparar exceção")]
    public void CreateProduct_WithNullDescriptionValue_DomainExceptionRequiredDescription()
    {
        var action = () => new Product(1, "Product Name", null, 9.99m, 99, "Product Image");
        
        action.Should().Throw<DomainExceptionValidation>().WithMessage("Descrição inválida! Informação é obrigatória.");
    }
    
    [Fact(DisplayName = "Criar entidade Product com descrição vazia DEVE disparar exceção")]
    public void CreateProduct_MissingDescriptionValue_DomainExceptionRequiredDescription()
    {
        var action = () => new Product(1, "Product Name", "", 9.99m, 99, "Product Image");
        
        action.Should().Throw<DomainExceptionValidation>().WithMessage("Descrição inválida! Informação é obrigatória.");
    }
    
    [Fact(DisplayName = "Criar entidade Product com descrição de menos de 5 caracteres DEVE disparar exceção")]
    public void CreateProduct_ShortDescriptionValue_DomainExceptionShortDescription()
    {
        var action = () => new Product(1, "Product Name", "Prod", 9.99m, 99, "Product Image");
        
        action.Should().Throw<DomainExceptionValidation>()
            .WithMessage("Descrição inválida! Mínimo 5 caracteres são exigidos.");
    }
    
    [Fact(DisplayName = "Criar entidade Product com imagem de mais de 250 caracteres DEVE disparar exceção")]
    public void CreateProduct_LongImageNameValue_DomainExceptionLongImageName()
    {
        var action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, string.Concat(Enumerable.Repeat("I", 251)));
        
        action.Should().Throw<DomainExceptionValidation>()
            .WithMessage("Imagem inválida! Valor máximo de 250 caracteres ultrapassado.");
    }
    
    [Fact(DisplayName = "Criar entidade Product com imagem nula NÃO DEVE disparar exceção")]
    public void CreateProduct_WithNullImageName_NoDomainException()
    {
        var action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, null);
        
        action.Should().NotThrow<DomainExceptionValidation>();
    }
    
    [Fact(DisplayName = "Criar entidade Product com imagem nula NÃO DEVE disparar exceção de referência nula")]
    public void CreateProduct_WithNullImageName_NoNullReferenceException()
    {
        var action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, null);
        
        action.Should().NotThrow<NullReferenceException>();
    }
    
    [Fact(DisplayName = "Criar entidade Product com imagem vazia NÃO DEVE disparar exceção")]
    public void CreateProduct_WithEmptyImageName_NoDomainException()
    {
        var action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "");
        
        action.Should().NotThrow<DomainExceptionValidation>();
    }
    
    [Theory(DisplayName = "Criar entidade Product preço de valor negativo DEVE disparar exceção")]
    [InlineData(-5)]
    public void CreateProduct_INvalidPriceValue_ExceptionDomainNegativeValue(decimal value)
    {
        var action = () => new Product(1, "Product Name", "Product Description", value, 99, "Product Image");
        
        action.Should().Throw<DomainExceptionValidation>()
            .WithMessage("Preço inválido! Valor deve ser maior ou igual a zero.");
    }
    
    [Theory(DisplayName = "Criar entidade Product estoque de valor negativo DEVE disparar exceção")]
    [InlineData(-5)]
    public void CreateProduct_INvalidStockValue_ExceptionDomainNegativeValue(int value)
    {
        var action = () => new Product(1, "Product Name", "Product Description", 9.99m, value, "Product Image");
        
        action.Should().Throw<DomainExceptionValidation>()
            .WithMessage("Estoque inválido! Valor deve ser maior ou igual a zero.");
    }
}