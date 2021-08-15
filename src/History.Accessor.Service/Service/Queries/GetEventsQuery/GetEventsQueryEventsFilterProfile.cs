using AutoMapper;
using HistoryAccessor.Contracts;

namespace HistoryAccessorService.Service.Queries.GetEventsQuery
{
    public class GetEventsQueryEventsFilterProfile : Profile
    {
        public GetEventsQueryEventsFilterProfile()
        {
            CreateMap<GetEventsQuery, EventOverviewFilter>()
                .ReverseMap();
        }
    }
}