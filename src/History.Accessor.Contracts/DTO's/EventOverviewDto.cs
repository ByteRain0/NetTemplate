using System.Collections.Generic;

namespace History.Accessor.Contracts.DTO_s
{
    public class EventOverviewDto
    {
        public List<EventDto> Events { get; set; }

        public int Count { get; set; }
    }
}