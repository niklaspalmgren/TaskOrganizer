using Api.Dtos;
using Api.Entities;
using AutoMapper;

namespace Api.Profiles
{
    public class TaskBoardsProfile : Profile
    {
        public TaskBoardsProfile()
        {
            CreateMap<TaskBoard, TaskBoardDto>();
            CreateMap<TaskBoardDto, TaskBoard>();
        }
    }
}
