namespace RunningGo.API.SistemaDeMetas.Domain.Models;

public class Goal
{
    public int Id { set; get; }
    public string Description { set; get; }
    public int Quantity { set; get; }
    public bool End { set; get; }
    
    //Relationships
    
    public int ProcessId { set; get; }
    public Process Process { set; get; }
}