using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EvidencijaServis.Models
{
    public class Kompanija
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(60)]
        public string Naziv { get; set; }
        [Required]
        [Range(1901, 2000)]
        public int Godina { get; set; }

        //public List<Zaposleni> Zaposleni { get; set; }
    }
}