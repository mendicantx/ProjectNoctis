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

		public FfrkModule(IEmbedBuilderFactory embedBuilder, IFfrkSheetContext ffrkSheetContext, Aliases aliases, Settings settings)
		{
			this.embedBuilder = embedBuilder;
			this.ffrkSheetContext = ffrkSheetContext;
			this.aliases = aliases;
			this.settings = settings;
		}

		[Command("bsb")]
		[Alias("burst")]
		public async Task BsbSoulbreakInfo(string name, int? index = null)
		{
			var soulbreaks = embedBuilder.BuildSoulbreakEmbeds("BSB", name, index);

			foreach (var soulbreak in soulbreaks)
			{
				foreach (var soulbreakGroup in soulbreak)
				{
					await Context.Channel.SendMessageAsync(embed: soulbreakGroup);
				}
			}
		}

		[Command("unique")]
		[Alias("uni")]
		public async Task UniqueSoulbreakInfo(string name, int? index = null)
		{
			var soulbreaks = embedBuilder.BuildSoulbreakEmbeds("SB", name, index);

			foreach (var soulbreak in soulbreaks)
			{
				foreach (var soulbreakGroup in soulbreak)
				{
					await Context.Channel.SendMessageAsync(embed: soulbreakGroup);
				}
			}
		}

		[Command("aasb")]
		[Alias("woke")]
		public async Task AasbSoulbreakInfo(string name, int? index = null)
		{
			var soulbreaks = embedBuilder.BuildSoulbreakEmbeds("AASB", name, index);

			foreach (var soulbreak in soulbreaks)
			{
				foreach (var soulbreakGroup in soulbreak)
				{
					await Context.Channel.SendMessageAsync(embed: soulbreakGroup);
				}
			}
		}

		[Command("default")]
		public async Task DefaultSoulbreakInfo(string name, int? index = null)
		{
			var soulbreaks = embedBuilder.BuildSoulbreakEmbeds("Default", name, index);

			foreach (var soulbreak in soulbreaks)
			{
				foreach (var soulbreakGroup in soulbreak)
				{
					await Context.Channel.SendMessageAsync(embed: soulbreakGroup);
				}
			}
		}

		[Command("osb")]
		public async Task OsbSoulbreakInfo(string name, int? index = null)
		{
			var soulbreaks = embedBuilder.BuildSoulbreakEmbeds("OSB", name, index);

			foreach (var soulbreak in soulbreaks)
			{
				foreach (var soulbreakGroup in soulbreak)
				{
					await Context.Channel.SendMessageAsync(embed: soulbreakGroup);
				}
			}
		}

		[Command("aosb")]
		[Alias("arcane")]
		public async Task AosbSoulbreakInfo(string name, int? index = null)
		{
			var soulbreaks = embedBuilder.BuildSoulbreakEmbeds("AOSB", name, index);

			foreach (var soulbreak in soulbreaks)
			{
				foreach (var soulbreakGroup in soulbreak)
				{
					await Context.Channel.SendMessageAsync(embed: soulbreakGroup);
				}
			}
		}

		[Command("sasb")]
		[Alias("sync")]
		public async Task SasbSoulbreakInfo(string name, int? index = null)
		{
			var soulbreaks = embedBuilder.BuildSoulbreakEmbeds("SASB", name, index);

			foreach (var soulbreak in soulbreaks)
			{
				foreach (var soulbreakGroup in soulbreak)
				{
					await Context.Channel.SendMessageAsync(embed: soulbreakGroup);
				}
			}
		}

		[Command("csb")]
		[Alias("chain")]
		public async Task CsbSoulbreakInfo(string name, int? index = null)
		{
			var soulbreaks = embedBuilder.BuildSoulbreakEmbeds("CSB", name, index);

			foreach (var soulbreak in soulbreaks)
			{
				foreach (var soulbreakGroup in soulbreak)
				{
					await Context.Channel.SendMessageAsync(embed: soulbreakGroup);
				}
			}
		}

		[Command("g")]
		[Alias(new string[5] { "glint", "glint+", "g+", "fsb", "fsb+" })]
		public async Task GlintSoulbreakInfo(string name, int? index = null)
		{
			var soulbreaks = embedBuilder.BuildSoulbreakEmbeds("g", name, index);

			foreach (var soulbreak in soulbreaks)
			{
				foreach (var soulbreakGroup in soulbreak)
				{
					await Context.Channel.SendMessageAsync(embed: soulbreakGroup);
				}
			}
		}

		[Command("brave")]
		[Alias("brsb")]
		public async Task BraveSoulbreakInfo(string name, int? index = null)
		{
			var soulbreaks = embedBuilder.BuildSoulbreakEmbeds("brave", name, index);

			foreach (var soulbreak in soulbreaks)
			{
				foreach (var soulbreakGroup in soulbreak)
				{
					await Context.Channel.SendMessageAsync(embed: soulbreakGroup);
				}
			}
		}

		[Command("usb")]
		[Alias("ultra")]
		public async Task UsbSoulbreakInfo(string name, int? index = null)
		{
			var soulbreaks = embedBuilder.BuildSoulbreakEmbeds("USB", name, index);

			foreach (var soulbreak in soulbreaks)
			{
				foreach (var soulbreakGroup in soulbreak)
				{
					await Context.Channel.SendMessageAsync(embed: soulbreakGroup);
				}
			}
		}

		[Command("ssb")]
		[Alias("super")]
		public async Task SsbSoulbreakInfo(string name, int? index = null)
		{
			var soulbreaks = embedBuilder.BuildSoulbreakEmbeds("SSB", name, index);

			foreach (var soulbreak in soulbreaks)
			{
				foreach (var soulbreakGroup in soulbreak)
				{
					await Context.Channel.SendMessageAsync(embed: soulbreakGroup);
				}
			}
		}

		[Command("lbo")]
		public async Task LboLimitBreakInfo(string name, int? index = null)
		{
			var limitbreaks = embedBuilder.BuildLimitBreakEmbeds("LBO", name, index);

			foreach (var limitbreak in limitbreaks)
			{
				await Context.Channel.SendMessageAsync(embed: limitbreak);
			}
		}

		[Command("lbg")]
		public async Task LbgLimitBreakInfo(string name, int? index = null)
		{
			var limitbreaks = embedBuilder.BuildLimitBreakEmbeds("LBG", name, index);

			foreach (var limitbreak in limitbreaks)
			{
				await Context.Channel.SendMessageAsync(embed: limitbreak);
			}
		}

		[Command("lbgs")]
		public async Task LbgsLimitBreakInfo(string name, int? index = null)
		{
			var limitbreaks = embedBuilder.BuildLimitBreakEmbeds("LBGS", name, index);

			foreach (var limitbreak in limitbreaks)
			{
				await Context.Channel.SendMessageAsync(embed: limitbreak);
			}
		}


		[Command("lb")]
		public async Task LbLimitBreakInfo(string name, int? index = null)
		{
			var limitbreaks = embedBuilder.BuildLimitBreakEmbeds(null, name, index);

			foreach (var limitbreak in limitbreaks)
			{
				await Context.Channel.SendMessageAsync(embed: limitbreak);
			}
		}

		[Command("tasb")]
		[Alias(new string[2] { "adsb", "dryad" })]
		public async Task TasbSoulbreakInfo(string name)
		{
			var soulbreaks = embedBuilder.BuildSoulbreakEmbeds("ADSB", name, null);

			foreach (var soulbreak in soulbreaks)
			{
				foreach (var soulbreakGroup in soulbreak)
				{
					await Context.Channel.SendMessageAsync(embed: soulbreakGroup);
				}
			}
		}

		[Command("sb")]
		[Alias("sbs")]
		public async Task SsbSoulbreakInfo([Remainder] string name)
		{
			var soulbreaks = embedBuilder.BuildEmbedsForCharacterSoulbreaks(name);

			foreach (var soulbreak in soulbreaks)
			{
				await Context.Channel.SendMessageAsync(embed: soulbreak);
			}
		}

		[Command("char")]
		[Summary("Return Character Info.")]
		public async Task CharacterInfoAsync([Remainder] string name)
		{
			var character = embedBuilder.BuildEmbedForBasicCharacterInfo(name);
			await Context.Channel.SendMessageAsync(embed: character);
		}

		[Command("ld")]
		[Alias("ls")]
		[Summary("Return Legend Dive Info.")]
		public async Task LegendDiveInfoAsync([Remainder] string name)
		{
			var legendDive = embedBuilder.BuildEmbedForLegendDive(name);
			await Context.Channel.SendMessageAsync(embed: legendDive);
		}

		[Command("rd")]
		[Alias("rs")]
		[Summary("Return Record Dive Info.")]
		public async Task RecordDiveInfoAsync([Remainder] string name)
		{
			var recordDive = embedBuilder.BuildEmbedForRecordDive(name);
			await Context.Channel.SendMessageAsync(embed: recordDive);
		}

		[Command("dive")]
		[Alias(new string[2] { "full", "fd" })]
		[Summary("Return Full Dive Info.")]
		public async Task FullDiveInfoAsync([Remainder] string name)
		{
			var fullDive = embedBuilder.BuildEmbedForFullDive(name);
			await Context.Channel.SendMessageAsync(embed: fullDive);
		}

		[Command("board")]
		[Alias(new string[2] { "rb", "bd" })]
		[Summary("Return Character Record Board Info.")]
		public async Task RecordBoardInfoAsync([Remainder] string name)
		{
			var recordBoard = embedBuilder.BuildEmbedForRecordBoard(name);
			await Context.Channel.SendMessageAsync(embed: recordBoard);
		}

		[Command("abil")]
		[Alias(new string[2] { "a", "ability" })]
		[Summary("Return ability Info.")]
		public async Task AbilityInfoAsync([Remainder] string name)
		{
			var ability = embedBuilder.BuildEmbedForAbilityInformation(name, false);

			foreach(var abilEmbed in ability)
			{
				await Context.Channel.SendMessageAsync(embed: abilEmbed);
			}
		}

		[Command("abs")]
		[Summary("Return ability Info.")]
		public async Task AbilityBySchoolInfoAsync(string school, string rank = "6", string element = null)
		{
			var ability = embedBuilder.BuildEmbedForAbilityBySchoolInformation(school, rank, element);
			await Context.Channel.SendMessageAsync(embed: ability);
		}

		[Command("ha")]
		[Alias("ua")]
		[Summary("Return hero ability Info.")]
		public async Task HeroAbilityInfoAsync([Remainder] string name)
		{
			var ability = embedBuilder.BuildEmbedForAbilityInformation(name, true);

			foreach (var abilEmbed in ability)
			{
				await Context.Channel.SendMessageAsync(embed: abilEmbed);
			}
		}

		[Command("status")]
		[Alias(new string[2] { "s", "stat" })]
		[Summary("Return Status Info.")]
		public async Task StatusInfoAsync([Remainder] string name)
		{
			var status = embedBuilder.BuildEmbedForStatus(name);
			await Context.Channel.SendMessageAsync(embed: status);
		}

		[Command("chase")]
		[Alias(new string[2] { "0", "other" })]
		[Summary("Return Other Info.")]
		public async Task OtherInfoAsync([Remainder] string name)
		{
			var other = embedBuilder.BuildEmbedForOther(name);
			await Context.Channel.SendMessageAsync(embed: other);
		}

		[Command("magi")]
		public async Task MagiciteInfoAsync([Remainder] string name)
		{
			var magicite = embedBuilder.BuildEmbedForMagicite(name);

			await Context.Channel.SendMessageAsync(embed: magicite);
		}

		[Command("lm")]
		[Alias("lms")]
		public async Task LegendMateriaInfoAsync([Remainder] string name)
		{
			var materias = embedBuilder.BuildEmbedsForLegendMaterias(name);

			foreach(var materia in materias)
			{
				await Context.Channel.SendMessageAsync(embed: materia);
			}
		}

		[Command("rm")]
		[Alias("rms")]
		public async Task RecordMateriaInfoAsync([Remainder] string name)
		{
			var materias = embedBuilder.BuildEmbedsForRecordMaterias(name);

			foreach (var materia in materias)
			{
				await Context.Channel.SendMessageAsync(embed: materia);
			}
		}

		[Command("LastUpdate")]
		public async Task LastUpdate()
		{
			await Context.Channel.SendMessageAsync($"Last update was {ffrkSheetContext.LastUpdateTime} and Result was: Success {ffrkSheetContext.LastUpdateSuccessful}");
		}

		[Command("help")]
		public async Task Help()
		{
			var embed = new EmbedBuilder();
			embed.ImageUrl = "https://i.imgur.com/zL4RNEQ.png";

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
	}
}
