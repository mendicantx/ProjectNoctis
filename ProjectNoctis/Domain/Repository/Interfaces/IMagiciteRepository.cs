using ProjectNoctis.Domain.SheetDatabase.Models;

namespace ProjectNoctis.Domain.Repository.Interfaces
{
    public interface IMagiciteRepository
    {
        SheetMagicites GetMagiciteByName(string name);
    }
}