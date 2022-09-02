using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskOrganizer.Api.Entities
{
    public class TaskBoard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [ForeignKey("TaskBoardId")]
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
