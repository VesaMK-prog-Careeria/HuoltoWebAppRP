using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuoltoWebApp.Models
{
    public partial class PvHuollot
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HuoltoId { get; set; }
        public int PvId { get; set; }
        [DataType(DataType.Date)]
        public DateTime? HuoltoPvm { get; set; }
        public int? HuoltoPaikkaId { get; set; }
        public string? HuollonKuvaus { get; set; }
        public byte[]? Kuva { get; set; }

        public virtual Pv? Pv { get; set; } = null!;
    }
}
