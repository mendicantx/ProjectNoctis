using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProjectNoctis.Domain.Repository.Interfaces;
using ProjectNoctis.Domain.SheetDatabase;
using ProjectNoctis.Services.Concrete;

namespace ProjectNoctis.Controllers
{
    [ApiController]
    //[Route("[controller]/[action]")]
    public class BotController : ControllerBase
    {    
        private readonly ILogger<BotController> _logger;
        private readonly ICharacterRepository characterRepository;
        private readonly ISoulbreakRepository soulbreakRepository;
        private readonly IFfrkSheetContext ffrkSheetContext;
        

       //TODO: Make a service layer
        public BotController(ILogger<BotController> logger, ICharacterRepository characterRepository, ISoulbreakRepository soulbreakRepository, IFfrkSheetContext ffrkSheetContext)
        {
            _logger = logger;
            this.characterRepository = characterRepository;
            this.soulbreakRepository = soulbreakRepository;
            this.ffrkSheetContext = ffrkSheetContext;
        }

        [Route("Bot/Index")]
        [HttpGet]
        public void Index()
        {

        }

        [Route("Bot/UpdateDatabase")]
        [HttpGet]
        public ActionResult<string> UpdateDatabase()
        {
            ffrkSheetContext.SetupProperties();
            var updated = ffrkSheetContext.LastUpdateSuccessful;
            return updated.ToString();

        }

        [Route("Bot/GetCharacter")]
        [HttpGet]
        public ActionResult<string> GetCharacter(string charName)
        {
            var character = ffrkSheetContext.RecordSpheres.FirstOrDefault(x => x.Character == charName);
            return System.Text.Json.JsonSerializer.Serialize(character);
        }

    }
}
