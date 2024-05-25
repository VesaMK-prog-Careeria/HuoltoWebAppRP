using System;
using System.Collections.Generic;

namespace HuoltoWebApp.Models
{
    public partial class AutoInfo
    {
        public AutoInfo()
        {
            Kuvat = new List<Kuva>();  // Varmistaa, että lista on alustettu
        }
        public int AutoInfoId { get; set; }
        public int AutoId { get; set; }
        public string? InfoTxt { get; set; }

        public virtual ICollection<Kuva> Kuvat { get; set; }
        public virtual Auto Auto { get; set; } = null!;
    }
}
