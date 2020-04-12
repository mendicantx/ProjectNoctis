using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.Database.Models
{
    public class DbBraveCommands
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Character { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public string BraveLevel { get; set; }
        public string Type { get; set; }
        public string Target { get; set; }
        public string Formula { get; set; }
        public string Multiplier { get; set; }
        public string Element { get; set; }
        public string Effects { get; set; }
        public string SB { get; set; }
        public string School { get; set; }
        public string BraveCondition { get; set; }
        public string BraveId { get; set; }
        public string Time { get; set; }
        public string JPName { get; set; }
    }
}
