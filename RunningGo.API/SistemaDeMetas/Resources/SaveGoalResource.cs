using System.ComponentModel.DataAnnotations;

namespace RunningGo.API.SistemaDeMetas.Resources;

public class SaveGoalResource
{
    [Required]
    [MaxLength(100)]
    public string Description { set; get; }
    
    [Required]
    public int Quantity { set; get; }
}