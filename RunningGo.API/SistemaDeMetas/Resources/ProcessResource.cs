namespace RunningGo.API.SistemaDeMetas.Resources;

public class ProcessResource
{
    public int Id { set; get; }
    public string State { set; get; }
    public DateTime Date { set; get; }
    
    //Relationships
    public int UserId { set; get; }
    public IList<GoalResource> Goals { set; get; } = new List<GoalResource>();
}