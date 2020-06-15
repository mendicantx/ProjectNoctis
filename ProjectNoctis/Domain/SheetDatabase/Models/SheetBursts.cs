using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.SheetDatabase.Models
{
    public class SheetBursts
    {
        public string Character { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public string Type { get; set; }
        public string Target { get; set; }
        public string Formula { get; set; }
        public string Multiplier { get; set; }
        public string Element { get; set; }
        public string Effects { get; set; }
        public string SB { get; set; }
        public string School { get; set; }
        public string BurstId { get; set; }
        public string Time { get; set; }
        public string JPName { get; set; }
        public string BurstImage
        {
            get
            {
                return $"{Constants.Constants.ffrkImageBaseUrl}ability/{BurstId}/{BurstId}_128.png";
            }
        }
    }
}
