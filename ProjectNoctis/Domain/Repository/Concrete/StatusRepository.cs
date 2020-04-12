using ProjectNoctis.Domain.Database;
using ProjectNoctis.Domain.Database.Models;
using ProjectNoctis.Domain.Repository.Interfaces;
using ProjectNoctis.Domain.SheetDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.Repository.Concrete
{
    public class StatusRepository : IStatusRepository
    {
        private readonly FFRecordContext dbContext;
        public StatusRepository(FFRecordContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public DbStatuses GetStatusByStatusId(string statusId)
        {
            return dbContext.Statuses.FirstOrDefault(x => x.StatusId == statusId);
        }
        public void UpdateOrAddStatusesFromSheet(IList<SheetStatus> statuses)
        {
            var updatedStatus = new List<DbStatuses>();

            foreach (var status in statuses)
            {

                var matchedStatus = GetStatusByStatusId(status.StatusId);

                if (matchedStatus != null)
                {
               
                    matchedStatus.DefaultDuration = status.DefaultDuration;
                    matchedStatus.Effects = status.Effects;
                    matchedStatus.ExclusiveStatus = status.ExclusiveStatus;
                    matchedStatus.MindModifier = status.MindModifier;
                    matchedStatus.StatusId = status.StatusId;
                    matchedStatus.Name = status.Name;

                    updatedStatus.Add(matchedStatus);
                }
                else
                {
                    var newStatus = new DbStatuses
                    {
                        DefaultDuration = status.DefaultDuration,
                        Effects = status.Effects,
                        ExclusiveStatus = status.ExclusiveStatus,
                        MindModifier = status.MindModifier,
                        StatusId = status.StatusId,
                        Name = status.Name
                    };

                    updatedStatus.Add(newStatus);
                }
            }

            dbContext.UpdateRange(updatedStatus);
            dbContext.SaveChanges();
        }
    }
}
