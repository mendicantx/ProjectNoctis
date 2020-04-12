using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using ProjectNoctis.Domain.Database;
using ProjectNoctis.Domain.Repository.Concrete;
using ProjectNoctis.Domain.Repository.Interfaces;
using ProjectNoctis.Domain.SheetDatabase;
using ProjectNoctis.Services.Interfaces;

namespace ProjectNoctis.Controllers
{
    [ApiController]
    //[Route("[controller]/[action]")]
    public class BotController : ControllerBase
    {    
        private readonly ILogger<BotController> _logger;
        private readonly ISheetUpdateService sheetUpdateService;
        private readonly ICharacterRepository characterRepository;
        private readonly ISoulbreakRepository soulbreakRepository;
        private readonly ISoulbreakManager soulbreakManager;
        

       //TODO: Make a service layer
        public BotController(ILogger<BotController> logger, ISheetUpdateService sheetUpdateService, ICharacterRepository characterRepository, ISoulbreakRepository soulbreakRepository,
            ISoulbreakManager soulbreakManager)
        {
            _logger = logger;
            this.sheetUpdateService = sheetUpdateService;
            this.characterRepository = characterRepository;
            this.soulbreakRepository = soulbreakRepository;
            this.soulbreakManager = soulbreakManager;
        }

        [Route("Bot/Index")]
        [HttpGet]
        public ActionResult<string> Index()
        {

            return "Hello";
        }

        [Route("Bot/UpdateDatabase")]
        [HttpGet]
        public ActionResult<string> UpdateDatabase()
        {
            var updated = sheetUpdateService.UpdateDatabase();
            return updated.ToString();

        }

        [Route("Bot/GetCharacter")]
        [HttpGet]
        public ActionResult<string> GetCharacter(string charName)
        {
            var character = characterRepository.GetCharacterByName(charName);
            return System.Text.Json.JsonSerializer.Serialize(character);
        }

        [HttpGet]
        [Route("Bot/UpdateSoulbreakStatuses")]
        public ActionResult<string> UpdateSoulbreakStatuses()
        {
            soulbreakRepository.UpdateSoulbreakStatuses();
            return "true";
        }

        [HttpGet]
        [Route("Bot/GetSoulbreakByCharacterName")]
        public ActionResult<string> GetSoulbreakByCharacterName(string charName)
        {
            var soulbreaks = soulbreakManager.GetSoulbreaksByCharacter(charName);
            return JsonSerializer.Serialize(soulbreaks);
        }

    }
}
