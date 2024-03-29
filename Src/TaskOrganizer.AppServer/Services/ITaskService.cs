﻿using TaskOrganizer.Shared;

namespace TaskOrganizer.AppServer.Services
{
    public interface ITaskService
    {
        Task<List<TaskDto>> GetTasksAsync();
        Task<List<TaskDto>> GetTasksForBoardAsync(int taskBoardId, string filter = "");
        Task<TaskDto> GetTask(int id);
        Task<TaskDto> CreateTaskAsync(TaskDto taskDto);
        Task UpdateTaskAsync(TaskDto taskDto);
        Task UpdateTasksAsync(List<TaskDto> taskDtos);
        Task DeleteTaskAsync(TaskDto taskDto);
        
    }
}