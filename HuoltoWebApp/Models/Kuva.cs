namespace HuoltoWebApp.Models
{
    public class Kuva
    {
        public int KuvaId { get; set; }
        public string KuvaNimi { get; set; }  // Tallennettavan kuvan nimi
        public byte[]? KuvaData { get; set; }  // Itse kuvatiedosto
        public int AutoInfoId { get; set; }  // Viite AutoInfoon
        public virtual AutoInfo AutoInfo { get; set; }  // Navigaatio-ominaisuus
    }
}
