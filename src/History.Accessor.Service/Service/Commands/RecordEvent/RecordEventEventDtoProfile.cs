using AutoMapper;
using HistoryAccessor.Contracts;

namespace HistoryAccessorService.Service.Commands.RecordEvent
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