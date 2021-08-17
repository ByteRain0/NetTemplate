using AutoMapper;
using History.Accessor.Contracts.Contracts;

namespace History.Accessor.Service.Service.Commands.RecordEvent
{
    public class RecordEventEventDtoProfile : Profile
    {
        public RecordEventEventDtoProfile()
        {
            CreateMap<EventDto, RecordEvent>()
                .ReverseMap();
        }
    }
}