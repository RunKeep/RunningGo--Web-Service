using Org.BouncyCastle.Asn1.Cms;
using RunningGo.API.Shared.Domain.Models;

namespace RunningGo.API.Dietas.Domain.Models;

public class Diet
{
    public int Id { set; get; }
    public string Description { set; get; }
    public string Specs { set; get; }
    public string Duration { set; get; }
    
    //Relationships
    
    public int FoodId { set; get; }
    public Food Food;
    
    public int UserId { set; get; }
    public User User;
}