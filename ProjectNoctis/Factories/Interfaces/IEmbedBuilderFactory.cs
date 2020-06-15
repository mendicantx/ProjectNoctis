using Discord;
using ProjectNoctis.Services.Models;
using System.Collections.Generic;

namespace ProjectNoctis.Factories.Interfaces
{
    public interface IEmbedBuilderFactory
    {
        Embed BuildEmbedForBasicCharacterInfo(string name);

        Embed BuildEmbedForAbilityInformation(string name, bool heroAbility);

        Embed BuildEmbedForAbilityBySchoolInformation(string school, string rank, string element);

        List<List<Embed>> BuildSoulbreakEmbeds(string tier, string charName, int? index);

        Embed BuildEmbedForRecordDive(string name);

        Embed BuildEmbedForLegendDive(string name);

        Embed BuildEmbedForRecordBoard(string name);

        Embed BuildEmbedForFullDive(string name);

        List<Embed> BuildLimitBreakEmbeds(string tier, string charName, int? index);

        List<Embed> BuildEmbedsForCharacterSoulbreaks(string name);

        Embed BuildEmbedForMagicite(string name);

        Embed BuildEmbedForStatus(string name);

        Embed BuildEmbedForOther(string name);
    }
}