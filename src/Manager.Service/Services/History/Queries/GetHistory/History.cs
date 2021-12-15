using System.Collections.Generic;

namespace Manager.Service.Services.History.Queries.GetHistory;

public class History
{
    public int Taken { get; set; }

    public int Skipped { get; set; }
    
    public List<HistoryEventDTO> Events { get; set; }

    public int TotalEventsCount { get; set; }
}