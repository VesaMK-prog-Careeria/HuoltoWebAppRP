using System;
using System.Collections.Generic;

namespace HuoltoWebApp.Models
{
    public partial class SäiliöInfo
    {
        public SäiliöInfo()
        {
            Kuvat = new List<Kuva>();  // Varmistaa, että lista on alustettu
        }
        public int SäiliöInfoId { get; set; }
        public int SäiliöId { get; set; }
        public string? InfoTxt { get; set; }

        public virtual ICollection<Kuva> Kuvat { get; set; }

        public virtual Säiliö Säiliö { get; set; } = null!;
    }
}
