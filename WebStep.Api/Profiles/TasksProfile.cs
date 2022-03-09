using AutoMapper;
using WebStep.Dto;

namespace WebStep.Api.Profiles
{
    public class TasksProfile : Profile
    {
        public TasksProfile()
        {
            CreateMap<Entities.Task, TaskDto>();
            CreateMap<TaskDto, Entities.Task>();
        }
    }
}
