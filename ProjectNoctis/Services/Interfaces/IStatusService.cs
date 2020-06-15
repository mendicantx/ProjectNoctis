using ProjectNoctis.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Services.Interfaces
{
    public interface IStatusService
    {
        Status BuildStatusInfoFromName(string name);
        Other BuildOtherInfoFromName(string name);
    }
}
