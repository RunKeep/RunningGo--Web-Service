using RunningGo.API.Checkeos.Domain.Models;
using RunningGo.API.Dietas.Domain.Models;
using RunningGo.API.Rutinas.Domain.Models;
using RunningGo.API.SistemaDeMetas.Domain.Models;

namespace RunningGo.API.Shared.Domain.Models;

public class User
{
    public long Id { set; get; }
    public string Name { set; get; }
    public string LastName { set; get; }
    public string Email { set; get; }
    public string Password { set; get; }
    public short Age { set; get; }
    public float Height { set; get; }
    public float Weight { set; get; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    
    //Relationships
    public IList<Diet> Diets { set; get; } = new List<Diet>();
    
    public IList<Routine> Routines { set; get; } = new List<Routine>();
    
    public IList<Process> Processes { set; get; } = new List<Process>();

    public IList<Checkup> Checkups { set; get; } = new List<Checkup>();
}