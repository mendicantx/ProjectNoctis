using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.Models
{
    public class Settings
    {
        public string JpAnimaWave { get; set; }

        public Settings()
        {
            SetupSettings();
        }
        public void SetupSettings()
        {
            var settings = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(@"UtilFiles\alias.txt"));

            if (settings.ContainsKey("jpAnimaWave"))
            {
                JpAnimaWave = settings["jpAnimaWave"];
            }
        }

        public bool UpdateSettings(string setting, string value)
        {
            var settings = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(@"UtilFiles\settings.txt"));

            if (settings.ContainsKey(setting))
            {
                settings[setting] = value;
                try
                {
                    File.WriteAllText(@"UtilFiles\alias.txt", JsonConvert.SerializeObject(settings));
                    SetupSettings();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            return false;
        }
    }
}
