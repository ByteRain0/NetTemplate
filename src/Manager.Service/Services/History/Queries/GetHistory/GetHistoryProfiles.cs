using AutoMapper;
using History.Accessor.Contracts.DTO_s;
using History.Accessor.Contracts.Queries;
using History.Accessor.Contracts.Queries.GetEventsQuery;

namespace Manager.Service.Services.History.Queries.GetHistory;

public class GetHistoryProfiles : Profile
{
    public GetHistoryProfiles()
    {
        CreateMap<GetHistory, GetEventsQuery>()
            .ReverseMap();

        CreateMap<EventDTO, HistoryEventDTO>()
            .ReverseMap();
    }
}