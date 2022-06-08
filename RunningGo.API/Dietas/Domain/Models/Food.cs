namespace RunningGo.API.Dietas.Domain.Models;

public class Food
{
    public int Id { set; get; }
    public string Name { set; get; }
    public float Calories { set; get; }
    public float Vitamins { set; get; }
    public float Quantity { set; get; }
    
    //Relationships
    
    public IList<Diet> Diets { set; get; } = new List<Diet>();
}