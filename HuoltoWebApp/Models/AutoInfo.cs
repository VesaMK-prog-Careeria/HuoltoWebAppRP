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
        //public byte[]? Kuva { get; set; }

        public virtual ICollection<Kuva> Kuvat { get; set; } = new List<Kuva>();
        public virtual Auto Auto { get; set; } = null!;
    }
    public class Kuva
    {
        public int KuvaId { get; set; }
        public string KuvaNimi { get; set; }  // Tallennettavan kuvan nimi
        public byte[]? KuvaData { get; set; }  // Itse kuvatiedosto
        public int AutoInfoId { get; set; }  // Viite AutoInfoon
        public virtual AutoInfo AutoInfo { get; set; }  // Navigaatio-ominaisuus

    }
}
