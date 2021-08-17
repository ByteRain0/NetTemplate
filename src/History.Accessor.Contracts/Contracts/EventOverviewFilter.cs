using System;
using System.Collections.Generic;

namespace History.Accessor.Contracts.Contracts
{
    public class EventOverviewFilter
    {
        public int Take { get; set; } = Int32.MaxValue;

        public int Skip { get; set; }
        
        public List<string> Events { get; set; } = new List<string>();
        
        public List<string> EntityTypes { get; set; } = new List<string>();
        
        public Guid? UserId { get; set; }

        public List<string> EntityPrimaryKeys { get; set; }

        /// <summary>
        /// Can be used to search inside the messages will be operation OR ELSE based on entity primary keys.
        /// Global search.
        /// </summary>
        public string OrElseSearchValue { get; set; }

        /// <summary>
        /// Can be used to search inside the messages will be operation AND ELSE.
        /// Filtering.
        /// </summary>
        public string AndContainsSearchValue { get; set; }
    }
}