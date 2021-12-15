using AutoMapper;
using History.Accessor.Contracts;
using History.Accessor.Contracts.Commands;

namespace Manager.Service.Services.History.Commands.RecordEvent;

public class RecordEventToEventDtoProfile : Profile
{
    public RecordEventToEventDtoProfile()
    {
        CreateMap<RecordEvent, RecordEventCommand>()
            .ReverseMap();
    }
}