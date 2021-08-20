using AutoMapper;
using History.Accessor.Contracts.Contracts;

namespace History.Accessor.Service.Service.Queries.GetEventsQuery
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