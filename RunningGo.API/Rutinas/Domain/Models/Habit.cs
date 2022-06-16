namespace RunningGo.API.Rutinas.Domain.Models;

public class Habit
{
    public int Id { set; get; }
    public string Description { set; get; }
    
    //Relationships
    public IList<Routine> Routines { set; get; } = new List<Routine>();
}