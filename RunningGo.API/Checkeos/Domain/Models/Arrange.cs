namespace RunningGo.API.Checkeos.Domain.Models;

public class Arrange
{
    public int Id { set; get; }
    public string State { set; get; }
    
    //Relationships
    public Checkup Checkup { set; get; }
}