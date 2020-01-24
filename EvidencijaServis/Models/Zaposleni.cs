using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EvidencijaServis.Models
{
    public class Zaposleni
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string ImeIPrezime { get; set; }
        [Required]
        [Range(1952, 1990)]
        public int GodinaRodjenja { get; set; }
        [Range(2001, int.MaxValue)]
        public int? GodinaZaposlenja { get; set; }
        [Required]
        [Range(2001, 9999)]
        public double Plata { get; set; }

        public int KompanijaId { get; set; }
        public Kompanija Kompanija { get; set; }
    }
}