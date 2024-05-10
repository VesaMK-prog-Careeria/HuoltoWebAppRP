using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuoltoWebApp.Models
{
    public partial class AutoHuollot
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HuollonId { get; set; }
        public int AutoId { get; set; }
        public DateTime? HuoltoPvm { get; set; }
        public int? HuoltopaikkaId { get; set; }
        public string? HuollonKuvaus { get; set; }
        public byte[]? Kuva { get; set; }

        public virtual Auto Auto { get; set; } = null!;
    }
}
