using System.ComponentModel.DataAnnotations;

namespace CleanArchMvc.WebUI.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Formato inválido")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Password é obrigatório")]
    [StringLength(20, ErrorMessage = "O {0} precisa ter no mínimo {2} e no máximo {1} caracteres", MinimumLength = 10)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    public string ReturnUrl { get; set; }
}