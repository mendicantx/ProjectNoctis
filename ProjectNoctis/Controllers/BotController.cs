using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProjectNoctis.Domain.Database;
using ProjectNoctis.Domain.Repository.Concrete;
using ProjectNoctis.Domain.Repository.Interfaces;
using ProjectNoctis.Domain.SheetDatabase;

namespace ProjectNoctis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BotController : ControllerBase
    {    
        private readonly ILogger<BotController> _logger;
        private readonly FFRecordContext dbContext;
        private readonly ICharacterRepository characterRepository;
        private readonly ISoulbreakRepository soulbreakRepository;

      
        public BotController(ILogger<BotController> logger, FFRecordContext dbContext, ICharacterRepository characterRepository, ISoulbreakRepository soulbreakRepository)
        {
            _logger = logger;
            this.dbContext = dbContext;
            this.characterRepository = characterRepository;
            this.soulbreakRepository = soulbreakRepository;
        }

        [HttpGet]
        public ActionResult<string> Index()
        {

            
            var sheetContext = new FfrkSheetContext();
            
   

            characterRepository.UpdateCharactersFromSheet(sheetContext.Characters);
            var charNames = characterRepository.GetAllCharacterNames();
            soulbreakRepository.UpdateSoulbreaksFromSheet(sheetContext.Soulbreaks, charNames);
            var character = characterRepository.GetCharacterByName("Lunafreya");
            return JsonConvert.SerializeObject(character);
        }
    }
}
