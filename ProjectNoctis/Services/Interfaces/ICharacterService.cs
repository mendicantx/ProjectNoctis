using Discord;
using ProjectNoctis.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Services.Interfaces
{
    public interface ICharacterService
    {
        Character BuildBasicCharacterInfoByName(string name);
    }
}
