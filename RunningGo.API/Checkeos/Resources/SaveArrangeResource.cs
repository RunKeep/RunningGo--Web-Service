using System.ComponentModel.DataAnnotations;

namespace RunningGo.API.Checkeos.Resources;

public class SaveArrangeResource
{
    [Required]
    [MaxLength(10)]
    public string State { set; get; }
}