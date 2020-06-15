using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.Models
{
    public class Aliases
    {
        public Dictionary<string,string> AliasList { get; set; }

        public Aliases()
        {
            SetupAliases();
        }

        public void SetupAliases()
        {
            AliasList = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(@"UtilFiles\alias.txt"));
        }

        public string ResolveAlias(string name)
        {
            if (AliasList.ContainsKey(name.ToLower()))
            {
                return AliasList[name.ToLower()];
            }

            return name;
        }
    }
}
