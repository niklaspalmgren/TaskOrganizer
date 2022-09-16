using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskOrganizer.Api.Entities;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] 
    public string FirstName { get; set; } = string.Empty;

    [Required] 
    public string LastName { get; set; } = string.Empty;

    public ICollection<Task> Tasks { get; set; }

}