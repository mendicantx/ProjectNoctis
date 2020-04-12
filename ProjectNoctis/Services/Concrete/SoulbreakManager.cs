using AutoMapper;
using ProjectNoctis.Domain.Database.Models;
using ProjectNoctis.Domain.Repository.Interfaces;
using ProjectNoctis.Services.Interfaces;
using ProjectNoctis.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Services.Concrete
{
    public class SoulbreakManager : ISoulbreakManager
    {
        private readonly ISoulbreakRepository soulbreakRepository;
        private readonly IMapper mapper;
        public SoulbreakManager(IMapper mapper, ISoulbreakRepository soulbreakRepository)
        { 
            this.mapper = mapper;
            this.soulbreakRepository = soulbreakRepository;
        }
        public IList<Soulbreak> GetSoulbreaksByCharacter(string characterName)
        {
            var soulbreaks = soulbreakRepository.GetSoulbreaksByCharacters(characterName);

            var mappedSoulbreaks = mapper.Map<IList<DbSoulbreaks>, IList<Soulbreak>>(soulbreaks);

            return mappedSoulbreaks; 
        }
    }
}
