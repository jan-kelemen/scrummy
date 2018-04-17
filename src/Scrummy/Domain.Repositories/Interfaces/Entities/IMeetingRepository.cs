using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;

namespace Scrummy.Domain.Repositories.Interfaces.Entities
{
    public interface IMeetingRepository : IRepository
    {
        Identity CreateMeeting(Meeting meeting);

        Meeting ReadMeeting(Identity id);

        void UpdateMeeting(Meeting meeting);

        void DeleteMeeting(Identity id);
    }
}
