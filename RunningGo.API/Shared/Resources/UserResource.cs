namespace RunningGo.API.Shared.Resources;

public class UserResource
{
    public int Id { get; set; }
    public string Name { set; get; }
    public string LastName { set; get; }
    public short Age { set; get; }
    public float Height { set; get; }
    public float Weight { set; get; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
}