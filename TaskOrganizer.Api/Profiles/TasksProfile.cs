using AutoMapper;
using TaskOrganizer.Shared;

namespace TaskOrganizer.Api.Profiles
{
    public class TasksProfile : Profile
    {
        public TasksProfile()
        {
            CreateMap<Entities.Task, TaskDto>();

            // Could create seperate dto's for read/create/update etc.. but for simplicity sake.
            CreateMap<TaskDto, Entities.Task>().ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
