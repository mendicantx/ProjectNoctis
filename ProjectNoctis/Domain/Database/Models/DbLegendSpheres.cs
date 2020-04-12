using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNoctis.Domain.Database.Models
{
    public class DbLegendSpheres
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Character { get; set; }
        public string Stats { get; set; }
        public string Misc { get; set; }
        public string MoteOne { get; set; }
        public string MoteTwo { get; set; }
    }
}
