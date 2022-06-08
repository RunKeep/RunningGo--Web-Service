using RunningGo.API.Dietas.Domain.Models;

namespace RunningGo.API.Shared.Domain.Models;

public class User
{
    public long Id { set; get; }
    public string Name { set; get; }
    public string LastName { set; get; }
    public short Age { set; get; }
    public float Height { set; get; }
    public float Weight { set; get; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    
    //Relationships
    public IList<Diet> Diets { set; get; } = new List<Diet>();
}