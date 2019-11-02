using System.Collections.Generic;
using ProjectNoctis.Domain.SheetDatabase.Models;

namespace ProjectNoctis.Domain.Repository.Interfaces
{
    public interface ISoulbreakRepository
    {
        void UpdateSoulbreaksFromSheet(IList<SheetSoulbreaks> soulbreaks, IList<string> charNames);
    }
}