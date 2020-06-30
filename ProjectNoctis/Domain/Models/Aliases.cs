using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

        public bool AddAlias(string alias, string realName)
        {
            if (AliasList.ContainsKey(alias))
            {
                return false;    
            }
            else
            {
                try
                {
                    AliasList.Add(alias, realName);

                    var aliasListJson = JsonConvert.SerializeObject(AliasList);
                    File.WriteAllText(@"UtilFiles\alias.txt", aliasListJson);

                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
        }

        public bool RemoveAlias(string alias)
        {
            if (!AliasList.ContainsKey(alias))
            {
                return false;
            }
            else
            {
                try
                {
                    AliasList.Remove(alias);

                    var aliasListJson = JsonConvert.SerializeObject(AliasList);
                    File.WriteAllText(@"UtilFiles\alias.txt", aliasListJson);

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public string GetAlias(string alias)
        {
            if (AliasList.ContainsKey(alias))
            {
                return $"{alias} : {AliasList[alias]}";
            }
            else
            {
                return null;
            }
        }

        public string ResolveAlias(string name)
        {
            if (AliasList.ContainsKey(name))
            {
                return AliasList[name];
            }

            if (AliasList.ContainsKey(name.ToLower()))
            {
                return AliasList[name.ToLower()];
            }

            return name;
        }
    }
}
