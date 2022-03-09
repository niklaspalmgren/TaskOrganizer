using AutoMapper;
using WebStep.Api.Entities;
using WebStep.Dto;

namespace WebStep.Api.Profiles
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
