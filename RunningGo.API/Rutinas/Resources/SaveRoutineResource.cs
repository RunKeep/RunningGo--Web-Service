using System.ComponentModel.DataAnnotations;

namespace RunningGo.API.Rutinas.Resources;

public class SaveRoutineResource
{
    [Required]
    [MaxLength(50)]
    public string Name { set; get; }
    
    [Required]
    public DateTime Date { set; get; }
    
    [Required]
    public string State { set; get; }

    [Required]
    public long UserId { set; get; }
    
    [Required]
    public int HabitId { set; get; }
}