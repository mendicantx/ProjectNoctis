using System.Data;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;
using ProjectNoctis.Domain.Models;
using ProjectNoctis.Domain.SheetDatabase;
using ProjectNoctis.Factories.Interfaces;
using ProjectNoctis.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace ProjectNoctis.BotModules
{
	public class FfrkModule : ModuleBase<SocketCommandContext>
	{
		private readonly IEmbedBuilderFactory embedBuilder;
		private readonly IFfrkSheetContext ffrkSheetContext;
		private readonly Aliases aliases;
		private readonly Settings settings;

		private readonly List<ulong> bannedUsers;

		public FfrkModule(IEmbedBuilderFactory embedBuilder, IFfrkSheetContext ffrkSheetContext, Aliases aliases, Settings settings)
		{
			this.embedBuilder = embedBuilder;
			this.ffrkSheetContext = ffrkSheetContext;
			this.aliases = aliases;
			this.settings = settings;
			bannedUsers = new List<ulong>();
			// bannedUsers.Add(676608867588767789);

		}

		[Command("ue", RunMode = RunMode.Async)]
		[Alias("he")]
		public async Task UniqueEquipmentInfo(string name)
		{
			LogMessageInfo();
			var equipments = embedBuilder.BuildsEmbedsForUniqueEquipment(name);

			foreach (var equip in equipments)
			{
				await Context.Channel.SendMessageAsync(embed: equip);
			}
		}

		[Command("bsb", RunMode = RunMode.Async)]
		[Alias("burst")]
		public async Task BsbSoulbreakInfo(string name, int? index = null)
		{
			LogMessageInfo();

			var soulbreaks = embedBuilder.BuildSoulbreakEmbeds("BSB", name, index);

			foreach (var soulbreak in soulbreaks)
			{
				foreach (var soulbreakGroup in soulbreak)
				{
					await Context.Channel.SendMessageAsync(embed: soulbreakGroup);
				}
			}
		}

		[Command("unique", RunMode = RunMode.Async)]
		[Alias("uni")]
		public async Task UniqueSoulbreakInfo(string name, int? index = null)
		{
			LogMessageInfo();

			var soulbreaks = embedBuilder.BuildSoulbreakEmbeds("SB", name, index);

			foreach (var soulbreak in soulbreaks)
			{
				foreach (var soulbreakGroup in soulbreak)
				{
					await Context.Channel.SendMessageAsync(embed: soulbreakGroup);
				}
			}
		}

		[Command("aasb", RunMode = RunMode.Async)]
		[Alias(new string[2] { "woke", "awake"})]
		public async Task AasbSoulbreakInfo(string name, int? index = null)
		{
			LogMessageInfo();

			var soulbreaks = embedBuilder.BuildSoulbreakEmbeds("AASB", name, index);

			foreach (var soulbreak in soulbreaks)
			{
				foreach (var soulbreakGroup in soulbreak)
				{
					await Context.Channel.SendMessageAsync(embed: soulbreakGroup);
				}
			}
		}

		[Command("default", RunMode = RunMode.Async)]
		public async Task DefaultSoulbreakInfo(string name, int? index = null)
		{
			LogMessageInfo();

			var soulbreaks = embedBuilder.BuildSoulbreakEmbeds("Default", name, index);

			foreach (var soulbreak in soulbreaks)
			{
				foreach (var soulbreakGroup in soulbreak)
				{
					await Context.Channel.SendMessageAsync(embed: soulbreakGroup);
				}
			}
		}

		[Command("osb", RunMode = RunMode.Async)]
		public async Task OsbSoulbreakInfo(string name, int? index = null)
		{
			LogMessageInfo();

			var soulbreaks = embedBuilder.BuildSoulbreakEmbeds("OSB", name, index);

			foreach (var soulbreak in soulbreaks)
			{
				foreach (var soulbreakGroup in soulbreak)
				{
					await Context.Channel.SendMessageAsync(embed: soulbreakGroup);
				}
			}
		}

		[Command("aosb", RunMode = RunMode.Async)]
		[Alias(new string[2] { "arcane", "uosb"})]
		public async Task AosbSoulbreakInfo(string name, int? index = null)
		{
			LogMessageInfo();

			var soulbreaks = embedBuilder.BuildSoulbreakEmbeds("AOSB", name, index);

			foreach (var soulbreak in soulbreaks)
			{
				foreach (var soulbreakGroup in soulbreak)
				{
					await Context.Channel.SendMessageAsync(embed: soulbreakGroup);
				}
			}
		}

		[Command("sasb", RunMode = RunMode.Async)]
		[Alias("sync")]
		public async Task SasbSoulbreakInfo(string name, int? index = null)
		{
			LogMessageInfo();

			var soulbreaks = embedBuilder.BuildSoulbreakEmbeds("SASB", name, index);

			foreach (var soulbreak in soulbreaks)
			{
				foreach (var soulbreakGroup in soulbreak)
				{
					await Context.Channel.SendMessageAsync(embed: soulbreakGroup);
				}
			}
		}

		[Command("csb", RunMode = RunMode.Async)]
		[Alias("chain")]
		public async Task CsbSoulbreakInfo(string name, int? index = null)
		{
			LogMessageInfo();

			var soulbreaks = embedBuilder.BuildSoulbreakEmbeds("CSB", name, index);

			foreach (var soulbreak in soulbreaks)
			{
				foreach (var soulbreakGroup in soulbreak)
				{
					await Context.Channel.SendMessageAsync(embed: soulbreakGroup);
				}
			}
		}

		[Command("g", RunMode = RunMode.Async)]
		[Alias(new string[] { "glint", "glint+", "glint++", "g+", "g++", "fsb", "fsb+", "fsb++" })]
		public async Task GlintSoulbreakInfo(string name, int? index = null)
		{
			LogMessageInfo();

			var soulbreaks = embedBuilder.BuildSoulbreakEmbeds("g", name, index);

			foreach (var soulbreak in soulbreaks)
			{
				foreach (var soulbreakGroup in soulbreak)
				{
					await Context.Channel.SendMessageAsync(embed: soulbreakGroup);
				}
			}
		}

		[Command("brave", RunMode = RunMode.Async)]
		[Alias("brsb")]
		public async Task BraveSoulbreakInfo(string name, int? index = null)
		{
			LogMessageInfo();

			var soulbreaks = embedBuilder.BuildSoulbreakEmbeds("brave", name, index);

			foreach (var soulbreak in soulbreaks)
			{
				foreach (var soulbreakGroup in soulbreak)
				{
					await Context.Channel.SendMessageAsync(embed: soulbreakGroup);
				}
			}
		}

		[Command("usb", RunMode = RunMode.Async)]
		[Alias("ultra")]
		public async Task UsbSoulbreakInfo(string name, int? index = null)
		{
			LogMessageInfo();

			var soulbreaks = embedBuilder.BuildSoulbreakEmbeds("USB", name, index);

			foreach (var soulbreak in soulbreaks)
			{
				foreach (var soulbreakGroup in soulbreak)
				{
					await Context.Channel.SendMessageAsync(embed: soulbreakGroup);
				}
			}
		}

		[Command("ssb", RunMode = RunMode.Async)]
		[Alias("super")]
		public async Task SsbSoulbreakInfo(string name, int? index = null)
		{
			LogMessageInfo();

			var soulbreaks = embedBuilder.BuildSoulbreakEmbeds("SSB", name, index);

			foreach (var soulbreak in soulbreaks)
			{
				foreach (var soulbreakGroup in soulbreak)
				{
					await Context.Channel.SendMessageAsync(embed: soulbreakGroup);
				}
			}
		}

		[Command("lbo", RunMode = RunMode.Async)]
		[Alias("olb")]
		public async Task LboLimitBreakInfo(string name, int? index = null)
		{
			LogMessageInfo();

			var limitbreaks = embedBuilder.BuildLimitBreakEmbeds("LBO", name, index);

			foreach (var limitbreak in limitbreaks)
			{
				await Context.Channel.SendMessageAsync(embed: limitbreak);
			}
		}

		[Command("lbg", RunMode = RunMode.Async)]
		[Alias("flb")]
		public async Task LbgLimitBreakInfo(string name, int? index = null)
		{
			LogMessageInfo();

			var limitbreaks = embedBuilder.BuildLimitBreakEmbeds("LBG", name, index);

			foreach (var limitbreak in limitbreaks)
			{
				await Context.Channel.SendMessageAsync(embed: limitbreak);
			}
		}

		[Command("lbc", RunMode = RunMode.Async)]
		[Alias("lcsb")]

		public async Task LbcLimitBreakInfo(string name, int? index = null)
		{
			LogMessageInfo();

			var limitbreaks = embedBuilder.BuildLimitBreakEmbeds("LBC", name, index);

			foreach (var limitbreak in limitbreaks)
			{
				await Context.Channel.SendMessageAsync(embed: limitbreak);
			}
		}


		[Command("lbgs", RunMode = RunMode.Async)]
		[Alias("glb")]

		public async Task LbgsLimitBreakInfo(string name, int? index = null)
		{
			LogMessageInfo();

			var limitbreaks = embedBuilder.BuildLimitBreakEmbeds("LBGS", name, index);

			foreach (var limitbreak in limitbreaks)
			{
				await Context.Channel.SendMessageAsync(embed: limitbreak);
			}
		}


		[Command("lb", RunMode = RunMode.Async)]
		public async Task LbLimitBreakInfo(string name, int? index = null)
		{
			LogMessageInfo();

			var limitbreaks = embedBuilder.BuildLimitBreakEmbeds(null, name, index);

			foreach (var limitbreak in limitbreaks)
			{
				await Context.Channel.SendMessageAsync(embed: limitbreak);
			}
		}

		[Command("tasb", RunMode = RunMode.Async)]
		[Alias(new string[3] { "adsb", "dryad", "dyad" })]
		public async Task TasbSoulbreakInfo(string name)
		{
			LogMessageInfo();

			var soulbreaks = embedBuilder.BuildSoulbreakEmbeds("ADSB", name, null);

			foreach (var soulbreak in soulbreaks)
			{
				foreach (var soulbreakGroup in soulbreak)
				{
					await Context.Channel.SendMessageAsync(embed: soulbreakGroup);
				}
			}
		}

		[Command("masb", RunMode = RunMode.Async)]
		public async Task MasbSoulbreakInfo(string name, int? index = null)
		{
			LogMessageInfo();

			var soulbreaks = embedBuilder.BuildSoulbreakEmbeds("MASB", name, index);

			foreach (var soulbreak in soulbreaks)
			{
				foreach (var soulbreakGroup in soulbreak)
				{
					await Context.Channel.SendMessageAsync(embed: soulbreakGroup);
				}
			}
		}

		[Command("tactical", RunMode = RunMode.Async)]
		[Alias(new string[1] { "tact" })]
		public async Task TacticalSoulbreakInfo(string name, int? index = null)
		{
			LogMessageInfo();

			var soulbreaks = embedBuilder.BuildSoulbreakEmbeds("TASB", name, index);

			foreach (var soulbreak in soulbreaks)
			{
				foreach (var soulbreakGroup in soulbreak)
				{
					await Context.Channel.SendMessageAsync(embed: soulbreakGroup);
				}
			}
		}

		[Command("zsb", RunMode = RunMode.Async)]
		[Alias(new string[1] { "uasb" })]
		public async Task ZsbSoulbreakInfo(string name, int? index = null)
		{
			LogMessageInfo();

			var soulbreaks = embedBuilder.BuildSoulbreakEmbeds("ZSB", name, index);

			foreach (var soulbreak in soulbreaks)
			{
				foreach (var soulbreakGroup in soulbreak)
				{
					await Context.Channel.SendMessageAsync(embed: soulbreakGroup);
				}
			}
		}
		[Command("lbsd", RunMode = RunMode.Async)]
		
		public async Task LbsdSoulbreakInfo(string name, int? index = null)
		{
			LogMessageInfo();

			var soulbreaks = embedBuilder.BuildSoulbreakEmbeds("LBSD", name, index);

			foreach (var soulbreak in soulbreaks)
			{
				foreach (var soulbreakGroup in soulbreak)
				{
					await Context.Channel.SendMessageAsync(embed: soulbreakGroup);
				}
			}
		}

		[Command("casb", RunMode = RunMode.Async)]
		public async Task CasbSoulbreakInfo(string name, int? index = null)
		{
			LogMessageInfo();

			var soulbreaks = embedBuilder.BuildSoulbreakEmbeds("CASB", name, index);

			foreach (var soulbreak in soulbreaks)
			{
				foreach (var soulbreakGroup in soulbreak)
				{
					await Context.Channel.SendMessageAsync(embed: soulbreakGroup);
				}
			}
		}


		[Command("dasb", RunMode = RunMode.Async)]
		[Alias(new string[2] { "daasb", "dual" })]
		public async Task DasbSoulbreakInfo(string name, int? index = null)
		{
			LogMessageInfo();

			var soulbreaks = embedBuilder.BuildSoulbreakEmbeds("DASB", name, index);

			foreach (var soulbreak in soulbreaks)
			{
				foreach (var soulbreakGroup in soulbreak)
				{
					await Context.Channel.SendMessageAsync(embed: soulbreakGroup);
				}
			}
		}

		[Command("ozsb", RunMode = RunMode.Async)]
		[Alias(new string[1] { "oz" })]
		public async Task OzsbSoulbreakInfo(string name, int? index = null)
		{
			LogMessageInfo();

			var soulbreaks = embedBuilder.BuildSoulbreakEmbeds("OZSB", name, index);

			foreach (var soulbreak in soulbreaks)
			{
				foreach (var soulbreakGroup in soulbreak)
				{
					await Context.Channel.SendMessageAsync(embed: soulbreakGroup);
				}
			}
		}

		[Command("asb", RunMode = RunMode.Async)]
		[Alias(new string[1] { "accel" })]
		public async Task AccelSoulbreakInfo(string name, int? index = null)
		{
			LogMessageInfo();

			var soulbreaks = embedBuilder.BuildSoulbreakEmbeds("ASB", name, index);

			foreach (var soulbreak in soulbreaks)
			{
				foreach (var soulbreakGroup in soulbreak)
				{
					await Context.Channel.SendMessageAsync(embed: soulbreakGroup);
				}
			}
		}

		[Command("sb", RunMode = RunMode.Async)]
		[Alias("sbs")]
		public async Task SsbSoulbreakInfo([Remainder] string name)
		{
			LogMessageInfo();

			var soulbreaks = embedBuilder.BuildEmbedsForCharacterSoulbreaks(name);

			foreach (var soulbreak in soulbreaks)
			{
				await Context.Channel.SendMessageAsync(embed: soulbreak);
			}
		}

		[Command("char", RunMode = RunMode.Async)]
		[Summary("Return Character Info.")]
		public async Task CharacterInfoAsync([Remainder] string name)
		{
			LogMessageInfo();

			var character = embedBuilder.BuildEmbedForBasicCharacterInfo(name);
			await Context.Channel.SendMessageAsync(embed: character);
		}

		[Command("ld", RunMode = RunMode.Async)]
		[Alias("ls")]
		[Summary("Return Legend Dive Info.")]
		public async Task LegendDiveInfoAsync([Remainder] string name)
		{
			LogMessageInfo();

			var legendDive = embedBuilder.BuildEmbedForLegendDive(name);
			await Context.Channel.SendMessageAsync(embed: legendDive);
		}

		[Command("rd", RunMode = RunMode.Async)]
		[Alias("rs")]
		[Summary("Return Record Dive Info.")]
		public async Task RecordDiveInfoAsync([Remainder] string name)
		{
			LogMessageInfo();

			var recordDive = embedBuilder.BuildEmbedForRecordDive(name);
			await Context.Channel.SendMessageAsync(embed: recordDive);
		}

		[Command("dive", RunMode = RunMode.Async)]
		[Alias(new string[2] { "full", "fd" })]
		[Summary("Return Full Dive Info.")]
		public async Task FullDiveInfoAsync([Remainder] string name)
		{
			LogMessageInfo();

			var fullDive = embedBuilder.BuildEmbedForFullDive(name);
			await Context.Channel.SendMessageAsync(embed: fullDive);
		}

		[Command("board", RunMode = RunMode.Async)]
		[Alias(new string[2] { "rb", "bd" })]
		[Summary("Return Character Record Board Info.")]
		public async Task RecordBoardInfoAsync([Remainder] string name)
		{
			LogMessageInfo();

			var recordBoard = embedBuilder.BuildEmbedForRecordBoard(name);
			await Context.Channel.SendMessageAsync(embed: recordBoard);
		}

		[Command("abil", RunMode = RunMode.Async)]
		[Alias(new string[2] { "a", "ability" })]
		[Summary("Return ability Info.")]
		public async Task AbilityInfoAsync([Remainder] string name)
		{
			LogMessageInfo();

			var ability = embedBuilder.BuildEmbedForAbilityInformation(name);

			foreach(var abilEmbed in ability)
			{
				await Context.Channel.SendMessageAsync(embed: abilEmbed);
			}
		}

		[Command("abs", RunMode = RunMode.Async)]
		[Summary("Return ability Info.")]
		public async Task AbilityBySchoolInfoAsync(string school, string rank = "6", string element = null)
		{
			LogMessageInfo();

			var ability = embedBuilder.BuildEmbedForAbilityBySchoolInformation(school, rank, element);
			await Context.Channel.SendMessageAsync(embed: ability);
		}

		[Command("ha", RunMode = RunMode.Async)]
		[Alias("ua")]
		[Summary("Return hero ability Info.")]
		public async Task HeroAbilityInfoAsync([Remainder] string name)
		{
			LogMessageInfo();

			var ability = embedBuilder.BuildEmbedForHeroAbilityAbilityInformation(name);

			foreach (var abilEmbed in ability)
			{
				await Context.Channel.SendMessageAsync(embed: abilEmbed);
			}
		}

		[Command("uas", RunMode = RunMode.Async)]
		[Alias("has")]
		[Summary("Return hero ability Info by school.")]
		public async Task HeroAbilityBySchoolInfoAsync(string school)
		{
			LogMessageInfo();

			var ability = embedBuilder.BuildEmbedForHeroAbilityBySchoolInformation(school);

			foreach (var abilEmbed in ability)
			{
				await Context.Channel.SendMessageAsync(embed: abilEmbed);
			}
		}

		[Command("status", RunMode = RunMode.Async)]
		[Alias(new string[2] { "s", "stat" })]
		[Summary("Return Status Info.")]
		public async Task StatusInfoAsync([Remainder] string name)
		{
			LogMessageInfo();

			var status = embedBuilder.BuildEmbedForStatus(name);
			await Context.Channel.SendMessageAsync(embed: status);
		}

		[Command("chase", RunMode = RunMode.Async)]
		[Alias(new string[2] { "0", "other" })]
		[Summary("Return Other Info.")]
		public async Task OtherInfoAsync([Remainder] string name)
		{
			LogMessageInfo();

			var other = embedBuilder.BuildEmbedForOther(name);
			await Context.Channel.SendMessageAsync(embed: other);
		}

		[Command("magi", RunMode = RunMode.Async)]
		public async Task MagiciteInfoAsync([Remainder] string name)
		{
			LogMessageInfo();

			var magicite = embedBuilder.BuildEmbedForMagicite(name);

			await Context.Channel.SendMessageAsync(embed: magicite);
		}

		[Command("lm", RunMode = RunMode.Async)]
		[Alias("lms")]
		public async Task LegendMateriaInfoAsync([Remainder] string name)
		{
			LogMessageInfo();

			var materias = embedBuilder.BuildEmbedsForLegendMaterias(name);

			foreach(var materia in materias)
			{
				await Context.Channel.SendMessageAsync(embed: materia);
			}
		}

		[Command("rm", RunMode = RunMode.Async)]
		[Alias("rms")]
		public async Task RecordMateriaInfoAsync([Remainder] string name)
		{
			LogMessageInfo();

			var materias = embedBuilder.BuildEmbedsForRecordMaterias(name);

			foreach (var materia in materias)
			{
				await Context.Channel.SendMessageAsync(embed: materia);
			}
		}

		[Command("anima", RunMode = RunMode.Async)]
		public async Task AnimaWaveSoulbreakInfoAsync(string wave, string tier)
		{
			LogMessageInfo();

			tier = tier.ToLower();
			if (new [] {"unique", "uni"}.Contains(tier))
				tier = "unique";
			if (new [] {"ssb", "super"}.Contains(tier))
				tier = "ssb";
			if (new [] {"bsb", "burst"}.Contains(tier))
				tier = "bsb";
			if (new [] {"usb", "ultra"}.Contains(tier))
				tier = "usb";
			if (new [] {"glint", "glint+", "g+", "glint++", "g++", "fsb", "fsb+", "fsb++"}.Contains(tier))
				tier = "glint";
			if (new [] {"aasb", "woke", "awake"}.Contains(tier))
				tier = "aasb";
			if (new [] {"csb", "chain"}.Contains(tier))
				tier = "csb";				
			if (new [] {"lm", "lms", "lmr" }.Contains(tier))
				tier = "lmr";				
			if (new [] {"aosb", "uosb" }.Contains(tier))
				tier = "aosb";				


			var validTiers = new [] {"unique", "ssb", "bsb", "usb", "glint", "aasb", "csb", "lmr", "aosb"};

			if ( !validTiers.Contains(tier) ) {
				await Context.Channel.SendMessageAsync($"Usage: ?anima <wave> <tier>. Valid tiers are: {string.Join(", ", validTiers)}"); 
				return;
			}

			var animaWaveEmbeds = embedBuilder.BuildEmbedsForAnimaWave(wave, tier);

			foreach (var animaWave in animaWaveEmbeds)
			{
				await Context.Channel.SendMessageAsync(embed: animaWave);
			}
		}

		[Command("LastUpdate", RunMode = RunMode.Async)]
		public async Task LastUpdate()
		{
			await Context.Channel.SendMessageAsync($"Last update was {ffrkSheetContext.LastUpdateTime} and Result was: Success {ffrkSheetContext.LastUpdateSuccessful}");
		}

		[Command("help", RunMode = RunMode.Async)]
		public async Task Help()
		{
			LogMessageInfo();

			var embed = new EmbedBuilder();
			embed.ImageUrl = settings.HelpLink;

			await Context.Channel.SendMessageAsync(embed:embed.Build());
		}

		// Restricted Commands

		[Command("update")]
		[RequireUserPermission(ChannelPermission.ManageMessages)]
		[Summary("Updates Sheet Info.")]
		public async Task UpdateDbAsync()
		{
			await Context.Channel.SendMessageAsync("Commencing Sheet Update. Commands will be unavailable until completion.");
			var update = await ffrkSheetContext.SetupProperties();

			if (update)
			{
				await Context.Channel.SendMessageAsync("Update Successful, commands may resume.");
			}
			else
			{
				await Context.Channel.SendMessageAsync("Update Failed. Feel free to try again later, if it doesn't work ping Purgedmoon");
			}
		}

		[Command("addalias")]
		[RequireUserPermission(ChannelPermission.ManageMessages)]
		public async Task AddAliasAsync(string alias, [Remainder] string realName)
		{
			LogMessageInfo();

			var result = aliases.AddAlias(alias, realName);
			if (result)
			{
				await Context.Channel.SendMessageAsync("Alias added successfully.");
			}
			else
			{
				await Context.Channel.SendMessageAsync("Alias was not added, alias may exist already.");
			}
		}

		[Command("ralias")]
		[RequireUserPermission(ChannelPermission.ManageMessages)]
		public async Task RemoveAliasAsync([Remainder] string alias)
		{
			LogMessageInfo();

			var result = aliases.RemoveAlias(alias);
			if (result)
			{
				await Context.Channel.SendMessageAsync("Alias Removed successfully.");
			}
			else
			{
				await Context.Channel.SendMessageAsync("Alias was not removed, alias may not exist already.");
			}
		}

		[Command("calias")]
		[RequireUserPermission(ChannelPermission.ManageMessages)]
		public async Task CheckAliasAsync([Remainder] string alias)
		{
			LogMessageInfo();

			var result = aliases.GetAlias(alias);
			if (result != null)
			{
				await Context.Channel.SendMessageAsync($"Alias is: {result}");
			}
			else
			{
				await Context.Channel.SendMessageAsync("Alias does not exist");
			}
		}

		[Command("updatesetting")]
		[RequireUserPermission(ChannelPermission.ManageMessages)]
		public async Task CheckAliasAsync(string setting, [Remainder] string value)
		{
			LogMessageInfo();

			var result = settings.UpdateSettings(setting, value);
			if (result)
			{
				await Context.Channel.SendMessageAsync($"Setting Updated Successfully.");
			}
			else
			{
				await Context.Channel.SendMessageAsync("Failed to update setting.");
			}
		}

		[Command("elnino", RunMode = RunMode.Async)]
		public async Task ElNino()
		{
			LogMessageInfo();

			await Context.Channel.SendMessageAsync($"https://media.discordapp.net/attachments/653313996241371137/883198628473217075/1630641351884995866381789306802.gif");
		}

		private void LogMessageInfo() {
			if (Context.Guild != null) 
				Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - Command in {Context.Guild.Name} ({Context.Guild.Id}) #{Context.Channel.Name} from {Context.User.ToString()} ({Context.User.Id}): {Context.Message.Content} ");
			else
				Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - Command in DMs from {Context.User.ToString()} ({Context.User.Id}): {Context.Message.Content} ");
			
			if (bannedUsers.Contains(Context.User.Id)) 
				throw new Exception("Banned user detected. " + Context.User.ToString());
		}
	}
}
