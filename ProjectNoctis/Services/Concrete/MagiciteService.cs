using Google.Apis.Util;
using ProjectNoctis.Domain.Repository.Interfaces;
using ProjectNoctis.Services.Interfaces;
using ProjectNoctis.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectNoctis.Services.Concrete
{
    public class MagiciteService : IMagiciteService
    {

        private readonly IMagiciteRepository magiciteRepository;
        private readonly IStatusRepository statusRepository;

        public MagiciteService(IMagiciteRepository magiciteRepository, IStatusRepository statusRepository)
        {
            this.magiciteRepository = magiciteRepository;
            this.statusRepository = statusRepository;
        }

        public Magicite BuildMagiciteInfoFromName(string name)
        {
            var magicite = magiciteRepository.GetMagiciteByName(name);
            var newMagicite = new Magicite();
            newMagicite.Info = magicite;

            var statusRegex = new Regex(Constants.Constants.statusRegex);
            var statuses = statusRegex.Matches(magicite.Effects).Select(x => x?.Groups[1]?.Value).ToList();

            newMagicite.MagiciteStatuses = statusRepository.GetStatusByNamesAndSource(magicite.MagiciteUltra,statuses,0);

            return newMagicite;

        }
    }
}
