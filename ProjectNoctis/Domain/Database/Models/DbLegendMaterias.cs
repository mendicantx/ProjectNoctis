using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.Database.Models
{
    public class DbLegendMaterias
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Character { get; set; }
        public string Name { get; set; }
        public string Realm { get; set; }
        public string Effect { get; set; }
        public string JPName { get; set; }
        public string LMId { get; set; }
        public string Relic { get; set; }
        public string Master { get; set; }
        public string Anima { get; set; }
    }
}
