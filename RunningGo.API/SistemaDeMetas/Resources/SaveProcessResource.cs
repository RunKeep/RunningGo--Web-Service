using System.ComponentModel.DataAnnotations;

namespace RunningGo.API.SistemaDeMetas.Resources;

public class SaveProcessResource
{
    [Required]
    [MaxLength(50)]
    public string Description { set; get; }
    
    [Required]
    [MaxLength(50)]
    public string State { set; get; }
    
    [Required]
    public DateTime Date { set; get; }
    
    //Relationships
    [Required]
    public long UserId { set; get; }
}