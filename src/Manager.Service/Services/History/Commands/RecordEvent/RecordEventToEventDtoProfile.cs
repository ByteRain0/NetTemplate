using AutoMapper;
using History.Accessor.Contracts;

namespace Manager.Service.Services.History.Commands.RecordEvent
{
    public class RecordEventToEventDtoProfile : Profile
    {
        public RecordEventToEventDtoProfile()
        {
            CreateMap<RecordEvent, global::History.Accessor.Contracts.Commands.RecordEvent>()
                .ForMember(x => x.UserId, src => src.Ignore())
                .ForMember(x => x.UserName, src => src.Ignore())
                .ReverseMap();
        }
    }
}