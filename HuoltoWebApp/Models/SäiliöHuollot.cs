using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuoltoWebApp.Models
{
    public partial class SäiliöHuollot
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HuoltoId { get; set; }
        public int SäiliöId { get; set; }
        public DateTime? HuoltoPvm { get; set; }
        public int? HuoltoPaikkaId { get; set; }
        public string? HuollonKuvaus { get; set; }
        public byte[]? Kuva { get; set; }

        //navigointiominaisuus Säiliö-luokkaan (yksi säiliö voi olla useassa huollossa)
        //? = voi olla null, tarvitaan koska SäiliöHuollot-luokkaa käytetään myös ilman Säiliö-oliota
        public virtual Säiliö? Säiliö { get; set; } = null!; 
    }
}
