using System;
using System.Collections.Generic;

namespace HuoltoWebApp.Models
{
    public partial class PvInfo
    {
        public PvInfo()
        {
            Kuvat = new List<Kuva>();  // Varmistaa, että lista on alustettu
        }
        public int PvInfoId { get; set; }
        public int PvId { get; set; }
        public string? InfoTxt { get; set; }

        public virtual ICollection<Kuva> Kuvat { get; set; } = new List<Kuva>();
        public virtual Pv Pv { get; set; } = null!;
    }
}
