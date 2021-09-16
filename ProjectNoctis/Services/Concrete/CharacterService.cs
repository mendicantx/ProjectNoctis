using ProjectNoctis.Domain.Repository.Interfaces;
using ProjectNoctis.Services.Interfaces;
using ProjectNoctis.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectNoctis.Constants;
using Discord;
using ProjectNoctis.Factories.Interfaces;

namespace ProjectNoctis.Services.Concrete
{
    public class CharacterService : ICharacterService
    {
        private readonly ICharacterRepository characterRepository;
        private readonly IStatusRepository statusRepository;

        public CharacterService(ICharacterRepository characterRepository, IStatusRepository statusRepository)
        {
            this.characterRepository = characterRepository;
            this.statusRepository = statusRepository;
        }

        public UniqueEquipment BuildUniqueEquipmentForCharacterByName(string name)
        {
            var uniqueEquipment = characterRepository.GetUniqueEquipmentByCharacterName(name);

            if(uniqueEquipment.UniqueEquipmentSets.Info != null)
            {
                uniqueEquipment.UniqueEquipmentSets.Statuses = statusRepository.GetStatusesByEffectText(uniqueEquipment.UniqueEquipmentSets.Info.Character, uniqueEquipment.UniqueEquipmentSets.Info.TwoSetBonus + " " + uniqueEquipment.UniqueEquipmentSets.Info.ThreeSetBonus);
            }

            foreach (var equip in uniqueEquipment.UniqueEquipments)
            {
                equip.Statuses = statusRepository.GetStatusesByEffectText(equip.Info.Name, equip.Info.RandomPassives);
            }

            return uniqueEquipment;
        }

        public Character BuildBasicCharacterInfoByName(string name)
        {
            var character = characterRepository.GetCharacterByName(name);
            var recordSphere = characterRepository.GetCharacterRecordSphereByName(character.Name, true);
            var legendSphere = characterRepository.GetCharacterLegendSpereByName(character.Name, true);

            var characterInfo = new Character();
            characterInfo.Info = character;

            var abilitiesFromDive = new List<string>();

            if( recordSphere != null)
            {
                foreach (var sphere in recordSphere.Misc)
                {

                    if (!sphere.Contains("Enable") && sphere.Contains("★") && Constants.Constants.skillList.Any(x => sphere.ToLower().Contains(x.ToLower())))
                    {
                        abilitiesFromDive.Add(sphere);
                    }

                    if (sphere.ToLower().Contains("enable") && !sphere.Contains("★"))
                    {
                        character.Equipment.Add(sphere.Replace("Enable ", string.Empty));
                    }

                    if (sphere.Contains("★") && sphere.Contains("Enable"))
                    {
                        abilitiesFromDive.Add(string.Join(" 0★ -> ", sphere.Replace("Enable ", string.Empty).Split(" ")));
                    }
                }
            }
            
            if(legendSphere != null)
            {
                foreach (var sphere in legendSphere?.Misc)
                {
                    if (sphere.Contains("★") && Constants.Constants.skillList.Any(x => sphere.ToLower().Contains(x.ToLower())))
                    {
                        abilitiesFromDive.Add(sphere);
                    }
                }
            }

            characterInfo.DiveAbilities = abilitiesFromDive;

            return characterInfo;
        }
    }
}
