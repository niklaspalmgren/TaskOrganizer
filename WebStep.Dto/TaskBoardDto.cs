using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebStep.Dto
{
    public class TaskBoardDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Name { get; set; } = string.Empty;

        public List<TaskDto> Tasks { get; set; } = new List<TaskDto>();
    }
}
