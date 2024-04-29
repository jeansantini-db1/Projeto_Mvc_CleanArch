using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Validation;
using FluentAssertions;

namespace T00.CleanArchMvc.Tests;

public class CategoryUnitTest1
{
    [Fact(DisplayName = "Criar entidade Category com parâmetros válidos NÃO DEVE disparar exceção")]
    public void CreateCategory_WithValidParameters_ResultObjectValidState()
    {
        var action = () => new Category(1, "Category Name");
        
        action.Should().NotThrow<DomainExceptionValidation>();
    }
    
    [Fact(DisplayName = "Criar entidade Category com id negativo DEVE disparar exceção")]
    public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId()
    {
        var action = () => new Category(-1, "Category Name");
        
        action.Should().Throw<DomainExceptionValidation>().WithMessage("Id inválido! Valor deve ser maior que zero.");
    }
    
    [Fact(DisplayName = "Criar entidade Category com nome nulo DEVE disparar exceção")]
    public void CreateCategory_WithNullNameValue_DomainExceptionRequiredName()
    {
        var action = () => new Category(1, null);
        
        action.Should().Throw<DomainExceptionValidation>().WithMessage("Nome inválido! Informação é obrigatória.");
    }
    
    [Fact(DisplayName = "Criar entidade Category com nome vazio DEVE disparar exceção")]
    public void CreateCategory_MissingNameValue_DomainExceptionRequiredName()
    {
        var action = () => new Category(1, "");
        
        action.Should().Throw<DomainExceptionValidation>().WithMessage("Nome inválido! Informação é obrigatória.");
    }
    
    [Fact(DisplayName = "Criar entidade Category com nome de menos de 3 caracteres DEVE disparar exceção")]
    public void CreateCategory_ShortNameValue_DomainExceptionShortName()
    {
        var action = () => new Category(1, "Ca");
        
        action.Should().Throw<DomainExceptionValidation>().WithMessage("Nome inválido! Mínimo 3 caracteres são exigidos.");
    }
}