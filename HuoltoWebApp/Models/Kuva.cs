using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuoltoWebApp.Models
{
    public class Kuva
    {
        [Key]
        public int KuvaID { get; set; }
        [Required]
        [StringLength(255)]
        public string? KuvaNimi { get; set; }
        [Required]
        public byte[]? KuvaData { get; set; }
        public int? AutoInfoId { get; set; }
        public int? SäiliöInfoId { get; set; }
        public int? PvInfoId { get; set; }
        public string? KuvaType { get; set; }
        public int EntityId { get; set; }

        [ForeignKey("AutoInfoId")]
        public virtual AutoInfo? AutoInfo { get; set; }
        [ForeignKey("SäiliöInfoId")]
        public  virtual SäiliöInfo? SäiliöInfo { get; set; }
        [ForeignKey("PvInfoId")]
        public virtual PvInfo? PvInfo { get; set; }

        //public int KuvaId { get; set; }
        //public string? KuvaNimi { get; set; }  // Tallennettavan kuvan nimi
        //public byte[]? KuvaData { get; set; }  // Itse kuvatiedosto
        //public int AutoInfoId { get; set; }  // Viite AutoInfoon
        //public virtual AutoInfo? AutoInfo { get; set; }  // Navigaatio-ominaisuus
    }
}
