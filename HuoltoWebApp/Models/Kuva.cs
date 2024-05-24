namespace HuoltoWebApp.Models
{
    public class Kuva
    {
        public int KuvaID { get; set; }
        public string? KuvaNimi { get; set; }
        public byte[]? KuvaData { get; set; }
        public int? AutoInfoId { get; set; }
        public int? SäiliöInfoId { get; set; }
        public int? PvInfoId { get; set; }
        public string? KuvaType { get; set; }
        public int EntityId { get; set; }

        public AutoInfo? AutoInfo { get; set; }
        public SäiliöInfo? SäiliöInfo { get; set; }
        public PvInfo? PvInfo { get; set; }

        //public int KuvaId { get; set; }
        //public string? KuvaNimi { get; set; }  // Tallennettavan kuvan nimi
        //public byte[]? KuvaData { get; set; }  // Itse kuvatiedosto
        //public int AutoInfoId { get; set; }  // Viite AutoInfoon
        //public virtual AutoInfo? AutoInfo { get; set; }  // Navigaatio-ominaisuus
    }
}
