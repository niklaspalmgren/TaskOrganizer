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
        public TaskCategory Category { get; set; }

        [Required]
        public int Order { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public int TaskBoardId { get; set; }

        public int? UserId { get; set; }

    }
}