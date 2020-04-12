using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.Database.Models
{
    public class DbRecordMaterias
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Character { get; set; }
        public string Name { get; set; }
        public string Realm { get; set; }
        public string Effect { get; set; }
        public string UnlockCriteria { get; set; }
        public string JPName { get; set; }
        public string RMId { get; set; }
    }
}
