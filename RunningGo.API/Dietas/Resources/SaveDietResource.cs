namespace RunningGo.API.Dietas.Resources;

public class SaveDietResource
{
    public string Description { set; get; }
    public string Specs { set; get; }
    public string Duration { set; get; }
    
    //Relationships
    public int FoodId { set; get; }
    public int UserId { set; get; }
}