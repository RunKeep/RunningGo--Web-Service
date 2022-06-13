using System.ComponentModel.DataAnnotations;

namespace RunningGo.API.Dietas.Resources;

public class SaveDietResource
{
    [Required]
    [MaxLength(200)]
    public string Description { set; get; }
    
    [Required]
    [MaxLength(200)]
    public string Specs { set; get; }
    
    [Required]
    public string Duration { set; get; }
    
    [Required]
    public float Quantity { set; get; }
    
    //Relationships
    [Required]
    public int FoodId { set; get; }
    
    [Required]
    public long UserId { set; get; }
}