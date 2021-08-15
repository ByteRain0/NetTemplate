using AutoMapper;
using HistoryAccessor.Contracts;

namespace Orchestra.Manager.Service.History.Commands.RecordEvent
{
    public class RecordEventToEventDtoProfile : Profile
    {
        public RecordEventToEventDtoProfile()
        {
            CreateMap<RecordEvent, EventDto>()
                .ForMember(x => x.UserId, src => src.Ignore())
                .ForMember(x => x.UserName, src => src.Ignore())
                .ReverseMap();
        }
    }
}