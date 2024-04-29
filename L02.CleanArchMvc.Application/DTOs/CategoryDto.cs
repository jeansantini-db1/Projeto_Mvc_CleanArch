using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CleanArchMvc.Application.DTOs;

public class CategoryDto
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Nome inválido! Informação é obrigatória.")]
    [RegularExpression(@"^.{3,}$", ErrorMessage = "Nome inválido! Mínimo 3 caracteres são exigidos.")]
    [StringLength(100, ErrorMessage = "Nome inválido! Valor máximo de 100 caracteres ultrapassado.")]
    [DisplayName("Nome")]
    public string Name { get; set; }
}