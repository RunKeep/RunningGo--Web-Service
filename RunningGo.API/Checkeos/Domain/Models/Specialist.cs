namespace RunningGo.API.Checkeos.Domain.Models;

public class Specialist
{
    public long Id { set; get; }
    public string Name { set; get; }
    public string Degree { set; get; }

    public IList<Checkup> Checkups { set; get; } = new List<Checkup>();

}