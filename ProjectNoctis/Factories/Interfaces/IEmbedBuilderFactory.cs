using Discord;
using ProjectNoctis.Services.Models;
using System.Collections.Generic;

namespace ProjectNoctis.Factories.Interfaces
{
    public interface IEmbedBuilderFactory
    {
        Embed BuildEmbedForBasicCharacterInfo(string name);

        List<Embed> BuildEmbedForAbilityInformation(string name);
        List<Embed> BuildEmbedForHeroAbilityAbilityInformation(string name);
        List<Embed> BuildEmbedForHeroAbilityBySchoolInformation(string school);

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

        List<Embed> BuildEmbedsForRecordMaterias(string name);

        List<Embed> BuildEmbedsForLegendMaterias(string name);
        List<Embed> BuildsEmbedsForUniqueEquipment(string name);

        List<Embed> BuildEmbedsForAnimaWave(string wave, string tier);
    }
}