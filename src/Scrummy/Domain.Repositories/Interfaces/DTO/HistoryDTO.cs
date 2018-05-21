using System;
using System.Collections.Generic;
using Scrummy.Domain.Core.Entities.Common;

namespace Scrummy.Domain.Repositories.Interfaces.DTO
{
    public class HistoryDTO<T>
    {
        public class Record
        {
            public DateTime From { get; set; }

            public DateTime To { get; set; }

            public T RecordId { get; set; }
        }
        public Identity Id { get; set; }

        public IEnumerable<Record> Records { get; set; }
    }
}
