using System.ComponentModel.DataAnnotations;

namespace RunningGo.API.Dietas.Resources;

public class SaveFoodResource
{
    [Required]
    [MaxLength(50)]
    public string Name { set; get; }
    
    [Required]
    public float Calories { set; get; }
    
    [Required]
    public float Vitamins { set; get; }
    
    [Required]
    public float Quantity { set; get; }
}