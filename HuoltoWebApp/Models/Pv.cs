using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuoltoWebApp.Models
{
    public partial class Pv
    {
        public Pv()
        {
            PvHuollots = new HashSet<PvHuollot>();
            PvHuoltopyyntös = new HashSet<PvHuoltopyyntö>();
            PvMuistutus = new HashSet<PvMuistutu>();
        }

        public int PvId { get; set; }
        public string RekNro { get; set; } = null!;
        [DataType(DataType.Date)]
        public DateTime? Katsastus { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Adr { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Välitarkastus { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Määräaikaistarkastus { get; set; }
        public int? PvInfoId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string? InfoTxt { get; set; }

        public virtual PvInfo? PvInfo { get; set; }
        public virtual ICollection<PvHuollot> PvHuollots { get; set; }
        public virtual ICollection<PvHuoltopyyntö> PvHuoltopyyntös { get; set; }
        public virtual ICollection<PvMuistutu> PvMuistutus { get; set; }
    }
}
