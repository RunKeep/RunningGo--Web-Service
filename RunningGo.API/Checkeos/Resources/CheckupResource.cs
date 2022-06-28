namespace RunningGo.API.Checkeos.Resources;

public class CheckupResource
{
    public int Id { set; get; }
    public DateTime Date { set; get; }
    public string UserData { set; get; }
    public string Results { set; get; }
    
    //Relationships
    public long UserId { set; get; }
    public long SpecialistId { set; get; }
}