using System.ComponentModel.DataAnnotations;

namespace A01.CleanArchMvc.API.DTOs;

public class RegisterModelDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [DataType(DataType.Password)]
    [Display(Name = "Confirme o password")]
    [Compare("Password", ErrorMessage = "Password de confirmação deve ser igual ao password")]
    public string ConfirmPassword { get; set; }
}