﻿using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebStep.Api.Data;
using WebStep.Api.Entities;

namespace WebStep.Api.Services
{
    public class TaskBoardRepo : ITaskBoardRepo
    {
        private readonly TasksDataContext _context;

        public TaskBoardRepo(TasksDataContext context)
        {
            _context = context;
        }

        public IEnumerable<TaskBoard> GetAllTaskBoards()
        {
            var tasks = _context.TaskBoards.Include(x => x.Tasks).ToList();
            return tasks;
        }

        public TaskBoard GetTaskBoardById(int id)
        {
            var task = _context.TaskBoards.Find(id);
            return task;
        }

        public void CreateTaskBoard(TaskBoard taskBoard)
        {
            if (taskBoard == null)
                throw new ArgumentNullException(nameof(taskBoard));

            _context.TaskBoards.Add(taskBoard);
        }

        public void UpdateTaskBoard(TaskBoard dto)
        {
        }

        public void DeleteTaskBoardAndRelatedTasks(TaskBoard taskBoard)
        {
            if (taskBoard == null)
                throw new ArgumentNullException(nameof(taskBoard));

            var taskBoardWithTasks = _context.TaskBoards.Include(x => x.Tasks).First(x => x.Id == taskBoard.Id);

            // Will cascade delete the related tasks when context is saved.
            _context.Remove(taskBoardWithTasks);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        
    }
}