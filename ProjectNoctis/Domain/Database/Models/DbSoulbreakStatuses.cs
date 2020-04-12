using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.Database.Models
{
    public class DbSoulbreakStatuses
    {
        public int SoulbreakId { get; set; }
        public int StatusId { get; set; }

        public virtual DbStatuses Status { get; set; }
        public virtual DbSoulbreaks Soulbreak { get; set; }
    }
}
