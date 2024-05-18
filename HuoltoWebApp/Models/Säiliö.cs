using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HuoltoWebApp.Models
{
    public partial class Säiliö
    {
        public Säiliö()
        {
            Autos = new HashSet<Auto>();
            SäiliöHuollots = new HashSet<SäiliöHuollot>();
            SäiliöHuoltopyyntös = new HashSet<SäiliöHuoltopyyntö>();
            SäiliöMuistutus = new HashSet<SäiliöMuistutu>();
        }

        public int SäiliöId { get; set; }
        public int SäiliöNro { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Vakaus { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Välitarkastus { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Määräaikaistarkastus { get; set; }
        public int? SäiliöInfoId { get; set; }


        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string InfoTxt { get; set; }

        public virtual SäiliöInfo? SäiliöInfo { get; set; }
        public virtual ICollection<Auto> Autos { get; set; }
        public virtual ICollection<SäiliöHuollot> SäiliöHuollots { get; set; }
        public virtual ICollection<SäiliöHuoltopyyntö> SäiliöHuoltopyyntös { get; set; }
        public virtual ICollection<SäiliöMuistutu> SäiliöMuistutus { get; set; }
    }
}
