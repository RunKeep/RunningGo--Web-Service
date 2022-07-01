namespace RunningGo.API.SistemaDeMetas.Resources;

public class GoalResource
{
    public int Id { set; get; }
    public string Description { set; get; }
    public int Quantity { set; get; }
    public bool End { set; get; }
    
    //Relationships
    public int ProcessId { set; get; }
}