using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskOrganizer.Shared
{
    public class TaskBoardDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;

        public List<TaskDto> Tasks { get; set; } = new List<TaskDto>();
    }
}
