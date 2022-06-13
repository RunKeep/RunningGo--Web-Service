using RunningGo.API.Shared.Domain.Models;

namespace RunningGo.API.Rutinas.Domain.Models;

public class Routine
{
    public long Id { set; get; }
    public string Name { set; get; }
    public DateTime Date { set; get; }
    public string State { set; get; }
    
    public long UserId { set; get; }
    public User User { set; get; }
    
    public int HabitId { set; get; }
    public Habit Habit { set; get; }
}