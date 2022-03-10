﻿using System.Collections.Generic;

namespace WebStep.Dto
{
    public class TaskBoardDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<TaskDto> Tasks { get; set; } = new List<TaskDto>();
    }
}