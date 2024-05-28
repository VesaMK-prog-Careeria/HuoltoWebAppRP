using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuoltoWebApp.Models
{
    public partial class SäiliöHuoltopyyntö
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HuoltoId { get; set; }
        public int SäiliöId { get; set; }
        public string? HuollonKuvaus { get; set; }
        public byte[]? Kuva { get; set; }

        public virtual Säiliö? Säiliö { get; set; }
    }
}
