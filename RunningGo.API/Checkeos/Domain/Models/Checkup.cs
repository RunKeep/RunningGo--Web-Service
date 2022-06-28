using RunningGo.API.Shared.Domain.Models;

namespace RunningGo.API.Checkeos.Domain.Models;

public class Checkup
{
    public int Id { set; get; }
    public DateTime Date { set; get; }
    public string UserData { set; get; }
    public string Results { set; get; }
    
    //Relationships
    public long UserId { set; get; }
    public User User { set; get; }
    
    public long SpecialistId { set; get; }
    public Specialist Specialist { set; get; }
    
    public int ArrangeId { set; get; }
    public Arrange Arrange { set; get; }
}