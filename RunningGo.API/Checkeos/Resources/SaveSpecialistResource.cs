using System.ComponentModel.DataAnnotations;

namespace RunningGo.API.Checkeos.Resources;

public class SaveSpecialistResource
{
    [Required]
    [MaxLength(50)]
    public string Name { set; get; }
    
    [Required]
    [MaxLength(50)]
    public string Degree { set; get; }
}