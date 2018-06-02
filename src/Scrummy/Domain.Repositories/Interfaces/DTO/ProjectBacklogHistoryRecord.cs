using System;
using Scrummy.Domain.Core.Entities.Common;

namespace Scrummy.Domain.Repositories.Interfaces.DTO
{
    public class ProjectBacklogHistoryRecord
    {
        public Identity Id { get; set; }

        public DateTime Date { get; set; }

        public int ToDoTasks { get; set; }

        public int ReadyTasks { get; set; }

        public int InSprintTasks { get; set; }

        public int DoneTasks { get; set; }
    }
}
