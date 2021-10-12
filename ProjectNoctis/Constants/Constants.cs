using Microsoft.Extensions.Localization.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ProjectNoctis.Domain.SheetDatabase.Models;
using System.Net.NetworkInformation;

namespace ProjectNoctis.Constants
{
    public static class Constants
    {
        public static List<string> skillList = new List<string>()
            {   "Black Magic", "White Magic", "Combat", "Support", "Celerity", "Summoning",
                "Spellblade", "Dragoon", "Monk", "Thief", "Knight", "Samurai","Ninja","Bard",
                "Dancer","Machinist", "Darkness", "Sharpshooter", "Witch", "Heavy"
            };

        public static List<string> weaponList = new List<string>()
        {
            "Dagger", "Sword", "Katana", "Axe", "Hammer", "Spear",
            "Fist", "Rod", "Staff", "Bow", "Instrument", "Whip",
            "Thrown", "Gun", "Book", "Blitzball", "Hairpin",
            "Gun-arm", "Gambling Gear", "Doll", "Keyblade"
        };

        public static List<string> armorList = new List<string>()
        {
            "Shield", "Hat", "Helm", "Light Armor", "Heavy Armor", "Robe", "Bracer", "Accessory"
        };

        public static List<string> miscFilter = new List<string>()
        {
            "Dexterity (5★)", "Strength (5★)", "Spirit (5★)", "Vitality (5★)", "Wisdom (5★)"
        };

        public static List<string> nightmareEnhancedSkills = new List<string>()
            {
                "White Magic", "Black Magic", "Summoning", "Combat", "Celerity", "Support"
            };

        public static List<string> elementList = new List<string>()
        {
            "Holy","Dark","Water","Ice","Wind","Earth","Fire","Lightning","Poison"
        };

        public static Dictionary<string, string> elementWeakness = new Dictionary<string, string>()
        {
            {"Holy","Dark"},
            {"Dark","Holy"},
            {"Wind","Ice" },
            {"Fire","Water"},
            {"Lightning","Earth"},
            {"Earth","Wind"},
            {"Water","Lightning"},
            {"Ice","Fire"},
            {"Poison","Poison"}
        };

        public static string[] abilityAliases = new string[2] { "a", "ability" };
        public static string[] glintAliases = new string[2] { "glint", "glint+" };

        public static string ffrkImageBaseUrl = "https://dff.sp.mbga.jp/dff/static/lang/image/";
        public static string ffrkImageABBaseUrl = "https://dff.sp.mbga.jp/dff/static/lang/ab_image/";

        public static List<string> ffrkImagePoseList = new List<string>() { "base_hands_up.png", "base_fatal.png" };

        public static string statusRegex = @"\[(.*?)\]";

        public static SheetStatus attachElement = new SheetStatus() { DefaultDuration = "25", Effects = "Replaces Attack command, increases {Element} damage dealt by 50/80/120% (abilities) or 80/100/120% (Soul Breaks), {Element} resistance +20%"};
        public static SheetStatus attachElementStacking = new SheetStatus() { DefaultDuration = "25", Effects = "Allow to stack Attach {Element}, up to Attach {Element} 3" };
        public static SheetStatus buffElement = new SheetStatus() { DefaultDuration = "15", Effects = "Increases {Element} damage dealt by 10%, cumulable" };
        public static SheetStatus imperilElement = new SheetStatus() { DefaultDuration = "15", Effects = "{Element} Resistance -10%, cumulable" };
    }
}
