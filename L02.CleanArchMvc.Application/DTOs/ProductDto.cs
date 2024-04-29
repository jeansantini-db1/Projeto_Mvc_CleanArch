using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CleanArchMvc.Application.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Nome inválido! Informação é obrigatória.")]
    [RegularExpression(@"^.{3,}$", ErrorMessage = "Nome inválido! Mínimo 3 caracteres são exigidos.")]
    [StringLength(100, ErrorMessage = "Nome inválido! Valor máximo de 100 caracteres ultrapassado.")]
    [DisplayName("Nome")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Descrição inválida! Informação é obrigatória.")]
    [RegularExpression(@"^.{5,}$", ErrorMessage = "Descrição inválida! Mínimo 5 caracteres são exigidos.")]
    [StringLength(100, ErrorMessage = "Descrição inválida! Valor máximo de 100 caracteres ultrapassado.")]
    [DisplayName("Descrição")]
    public string Description { get; set; }
    
    [Required(ErrorMessage = "Preço inválido! Informação é obrigatória.")]
    [Column(TypeName = "decimal(18,2)")]
    [DisplayFormat(DataFormatString = "{0:C2}")]
    [DataType(DataType.Currency)]
    [DisplayName("Preço")]
    public decimal Price { get; set; }
    
    [Required(ErrorMessage = "Estoque inválido! Informação é obrigatória.")]
    [Range(1, 9999)]
    [DisplayName("Estoque")]
    public int Stock { get; set; }
    
    [StringLength(250, ErrorMessage = "Imagem inválida! Valor máximo de 250 caracteres ultrapassado.")]
    [DisplayName("Imagem")]
    public string Image { get; set; }
    
    [DisplayName("Categoria")]
    public int CategoryId { get; set; }
    
    [JsonIgnore]
    public CategoryDto Category { get; set; }
}