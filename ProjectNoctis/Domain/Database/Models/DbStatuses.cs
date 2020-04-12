using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.Database.Models
{
    public class DbStatuses
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Effects { get; set; }
        public string StatusId { get; set; }
        public string DefaultDuration { get; set; }
        public string MindModifier { get; set; }
        public string ExclusiveStatus { get; set; }
        public virtual ICollection<DbSoulbreakStatuses> SoulbreakStatuses { get; set; }
        
        [NotMapped]
        public ICollection<DbSoulbreaks> Soulbreaks { get 
            {
                if(SoulbreakStatuses != null)
                {
                    return SoulbreakStatuses.Select(x => x.Soulbreak).ToList();
                }
                return null;
            }

            set
            {
                Soulbreaks = value;
            }
        }
        
    }
}
