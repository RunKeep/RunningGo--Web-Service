using System.ComponentModel.DataAnnotations;

namespace RunningGo.API.SistemaDeMetas.Resources;

public class SaveProcessResource
{
    [Required]
    [MaxLength(50)]
    public string State { set; get; }
    
    [Required]
    public DateTime Date { set; get; }
    
    //Relationships
    [Required]
    public int UserId { set; get; }
}