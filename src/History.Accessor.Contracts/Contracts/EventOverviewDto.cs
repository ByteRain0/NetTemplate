using System.Collections.Generic;

namespace History.Accessor.Contracts.Contracts
{
    public class EventOverviewDto
    {
        public List<EventDto> Events { get; set; }

        public int Count { get; set; }
    }
}