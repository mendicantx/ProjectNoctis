using ProjectNoctis.Domain.SheetDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Services.Models
{
    public class Character
    {
        public SheetCharacters Info { get; set; }
        public List<string> DiveAbilities { get; set; }
    }
}
