namespace RunningGo.API.Rutinas.Resources;

public class RoutineResource
{
    public long Id { set; get; }
    public string Name { set; get; }
    public DateTime Date { set; get; }
    public string State { set; get; }
    
    public long UserId { set; get; }
    public int HabitId { set; get; }
}