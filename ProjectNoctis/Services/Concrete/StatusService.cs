using ProjectNoctis.Domain.Repository.Interfaces;
using ProjectNoctis.Domain.SheetDatabase.Models;
using ProjectNoctis.Services.Interfaces;
using ProjectNoctis.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Services.Concrete
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository statusRepository;

        public StatusService(IStatusRepository statusRepository)
        {
            this.statusRepository = statusRepository;
        }

        public Status BuildStatusInfoFromName(string name)
        {
            var status = statusRepository.GetStatusByName(name);

            var newStatus = new Status();

            newStatus.StatusOthers = new Dictionary<string, List<SheetOthers>>();

            newStatus.Info = status;
            newStatus.Statuses = statusRepository.GetStatusesByEffectText(status.Name, status.Effects);
            statusRepository.GetOthersByNamesAndSource(status.Name, newStatus.StatusOthers);

            return newStatus;
        }

        public Other BuildOtherInfoFromName(string name)
        {
            var other = statusRepository.GetOthersByName(name);

            var newOther = new Other();

            newOther.Others = new Dictionary<string, List<SheetOthers>>();

            newOther.Info = other;
            newOther.Statuses = statusRepository.GetStatusesByEffectText(other.Name, other.Effects);
            statusRepository.GetOthersByNamesAndSource(other.Name, newOther.Others);

            return newOther;
        }

    }
}
