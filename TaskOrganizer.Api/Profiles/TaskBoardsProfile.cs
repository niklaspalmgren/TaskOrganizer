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

            // Could create seperate dto's for read/create/update etc.. but for simplicity sake.
            CreateMap<TaskBoardDto, TaskBoard>().ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
