using System.ComponentModel.DataAnnotations;

namespace RunningGo.API.Rutinas.Resources;

public class SaveHabitResource
{
    [Required]
    [MaxLength(250)]
    public string Description { set; get; }
}