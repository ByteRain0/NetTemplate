using System.Collections.Generic;

namespace HistoryAccessor.Contracts
{
    public class EventOverviewDto
    {
        public List<EventDto> Events { get; set; }

        public int Count { get; set; }
    }
}