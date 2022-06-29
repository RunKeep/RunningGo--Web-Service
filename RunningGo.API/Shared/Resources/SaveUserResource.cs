using System.ComponentModel.DataAnnotations;

namespace RunningGo.API.Shared.Resources;

public class SaveUserResource
{
    [Required]
    [MaxLength(30)]
    public string Name { set; get; }
    
    [Required]
    [MaxLength(30)]
    public string LastName { set; get; }
    
    [Required]
    [MaxLength(100)]
    public string Email { set; get; }
    
    [Required]
    public string Password { set; get; }

    [Required]
    public short Age { set; get; }
    
    [Required]
    public float Height { set; get; }
    
    [Required]
    public float Weight { set; get; }
    
    public string Address { get; set; }

    public string PhoneNumber { get; set; }
}