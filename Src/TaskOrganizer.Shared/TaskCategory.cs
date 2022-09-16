using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
namespace TaskOrganizer.Shared;

public enum TaskCategory
{
    [Display(Name = "Bug")]
    Bug = 0,

    [Display(Name = "Feature")]
    Feature = 1,

    [Display(Name = "Change")]
    Change = 2
}

