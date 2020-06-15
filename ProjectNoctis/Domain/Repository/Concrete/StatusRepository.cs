using FuzzySharp;
using ProjectNoctis.Domain.Models;
using ProjectNoctis.Domain.Repository.Interfaces;
using ProjectNoctis.Domain.SheetDatabase;
using ProjectNoctis.Domain.SheetDatabase.Models;
using ProjectNoctis.Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProjectNoctis.Domain.Repository.Concrete
{
    public class StatusRepository : IStatusRepository
    {
        private readonly IFfrkSheetContext dbContext;
        private readonly Aliases aliases;

        public StatusRepository(IFfrkSheetContext context, Aliases aliases)
        {
            dbContext = context;
            this.aliases = aliases;
        }

        public SheetStatus GetStatusByName(string name)
        {
            name = aliases.ResolveAlias(name);

            var status = dbContext.Statuses.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());

            if (status == null)
            {
                status = dbContext.Statuses.OrderByDescending(x => Fuzz.PartialRatio(x.Name.ToLower(), name.ToLower())).FirstOrDefault();
            }

            return status;
        }

        public SheetOthers GetOthersByName(string name)
        {
            name = aliases.ResolveAlias(name);

            var other = dbContext.Others.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());

            if (other == null)
            {
                other = dbContext.Others.OrderByDescending(x => Fuzz.PartialRatio(x.Name.ToLower(), name.ToLower())).FirstOrDefault();
            }

            return other;
        }


        public Dictionary<string,List<SheetStatus>> GetStatusesByEffectText(string source, string effect)
        {
            Regex statusRegex = new Regex(Constants.Constants.statusRegex);
            var statuses = statusRegex.Matches(effect).Select(x => x?.Groups[1]?.Value).ToList();

            return GetStatusByNamesAndSource(source, statuses, 0);
        }

        public Dictionary<string,List<SheetStatus>> GetStatusByNamesAndSource(string source, List<string> names, int counter, Dictionary<string, List<SheetStatus>> currentMatches = null)
        {
            Regex rx = new Regex(Constants.Constants.statusRegex);
            
            var statusResults = currentMatches ?? new Dictionary<string, List<SheetStatus>>();

            if (counter == 7 || source == null) 
            {
                return statusResults;
            }

            var splitMatches = new List<string>();
            foreach(var name in names)
            {
                if (name.Contains(@"/"))
                {
                    var nameSplit = name.Split('/');

                    var primaryName = nameSplit[0];
                    if (primaryName.Contains("("))
                    {
                        splitMatches.Add(primaryName + ")");
                    }
                    else
                    {
                        splitMatches.Add(primaryName);
                    }
                    
                    var breakingIndex = primaryName.LastIndexOf(" ") + 1;

                    for (int i = 1; i < nameSplit.Count(); i++)
                    {
                        var replacementSubstring = primaryName.Substring(breakingIndex, primaryName.Length - breakingIndex);

                        if (primaryName.Contains("(") && !nameSplit[i].Contains(")"))
                        {
                            splitMatches.Add(primaryName.Replace(replacementSubstring,"("+nameSplit[i])+")");
                        }
                        else
                        {
                            splitMatches.Add(primaryName.Replace(replacementSubstring, nameSplit[i]));
                        }

                    }
                }
            }

            names.AddRange(splitMatches);

            var statuses = dbContext.Statuses.Where(x => names.Contains(x.Name)).ToList();

            if (statuses.Count() != 0)
            {
                if (!statusResults.ContainsKey(source))
                {
                    statusResults.Add(source, statuses);

                    foreach (var status in statuses)
                    {
                        var statusMatches = rx.Matches(status.Effects).Select(x => x?.Groups[1]?.Value).ToList();

                        if (statusMatches.Count != 0)
                        {
                            GetStatusByNamesAndSource(status.Name, statusMatches, counter + 1, statusResults);
                        }
                    }
                }
            }

            if (statusResults.ContainsKey(source))
            {
                var uniqueList = new List<SheetStatus>();
                foreach (var status in statusResults[source])
                {
                    if (!uniqueList.Any(x => x.Name == status.Name))
                    {
                        uniqueList.Add(status);
                    }
                }

                statusResults[source] = uniqueList;
            }
            return statusResults;
        }

        public Dictionary<string, List<SheetOthers>> GetOthersByNamesAndSource(string source, Dictionary<string, List<SheetOthers>> currentMatches = null)
        {
            Regex rx = new Regex(Constants.Constants.statusRegex);

            var otherResults = currentMatches ?? new Dictionary<string, List<SheetOthers>>();

            var others = dbContext.Others.Where(x => x.Source == source && x.SourceType != "Attach Status").ToList();

            if (others.Count() != 0)
            {
                if (!otherResults.ContainsKey(source))
                {
                    foreach (var other in others)
                    {
                        var otherMatches = rx.Matches(other.Effects).Select(x => x?.Groups[1]?.Value).ToList();
                        other.OtherStatuses = GetStatusByNamesAndSource(other.Name, otherMatches, 0);
                    }

                    otherResults.Add(source, others);
                }
            }

            return otherResults;
        }
    }
}
