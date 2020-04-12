using AutoMapper;
using ProjectNoctis.Domain.Database.Models;
using ProjectNoctis.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.UtilFiles.AutoMapper
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<DbSoulbreaks, Soulbreak>().PreserveReferences();
            CreateMap<DbBraveCommands, BraveCommand>();
            CreateMap<DbBurstCommands, BurstCommand>();
            CreateMap<DbSynchroCommands, SynchroCommand>();
            CreateMap<DbStatuses, Status>().PreserveReferences();
        }
    }
}
