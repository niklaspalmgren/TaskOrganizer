using System.ComponentModel.DataAnnotations;

namespace TaskOrganizer.Shared
{
    public class TaskDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;

        [StringLength(200)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int TaskBoardId { get; set; }
    }
}