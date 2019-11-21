using System.Collections.Generic;
using ProjectNoctis.Domain.Database.Models;
using ProjectNoctis.Domain.SheetDatabase.Models;

namespace ProjectNoctis.Domain.Repository.Interfaces
{
    public interface IMagiciteRepository
    {
        void AddOrUpdateMagicitesFromSheet(IList<SheetMagicites> magicites);
        DbMagicite GetMagiciteByMagiciteId(string id);
    }
}