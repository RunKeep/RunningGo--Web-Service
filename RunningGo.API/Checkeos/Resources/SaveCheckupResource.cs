using System.ComponentModel.DataAnnotations;

namespace RunningGo.API.Checkeos.Resources;

public class SaveCheckupResource
{
    [Required]
    public DateTime Date { set; get; }
    
    [Required]
    [MaxLength(250)]
    public string UserData { set; get; }
    
    [Required]
    [MaxLength(250)]
    public string Results { set; get; }
    
    [Required]
    public long UserId { set; get; }
    
    [Required]
    public long SpecialistId { set; get; }
    
    
}