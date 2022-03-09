using Api.Dtos;
using AutoMapper;

namespace Api.Profiles
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
