using RunningGo.API.Shared.Domain.Models;

namespace RunningGo.API.SistemaDeMetas.Domain.Models;

public class Process
{
    public int Id { set; get; }
    public string Description { set; get; }
    public string State { set; get; }
    public DateTime Date { set; get; }
    
    //Relationships
    public int UserId { set; get; }
    public User User { set; get; }

    public IList<Goal> Goals { set; get; } = new List<Goal>();
}