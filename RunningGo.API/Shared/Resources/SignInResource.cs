using System.ComponentModel.DataAnnotations;

namespace RunningGo.API.Shared.Resources;

public class SignInResource
{
    [Required]
    public string Email { set; get; }
    
    [Required]
    public string Password { set; get; }
}