using HuoltoWebApp.Pages.Säiliöt;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HuoltoWebApp.Models   // Tässä Modelissa on Auto-luokka, joka on osa tietokantamallia
{
    public partial class Auto
    {
        public Auto()   // Tällä varmistetaan, että kaikki navigointiominaisuudet on alustettu
        {               // eli Auto-olioon liittyvät AutoHuollot, AutoHuoltopyynnöt ja AutoMuistutukset
            AutoHuollots = new HashSet<AutoHuollot>();
            AutoHuoltopyyntös = new HashSet<AutoHuoltopyyntö>();
            AutoMuistutus = new HashSet<AutoMuistutu>();
        }

        //Nämä ovat Auto-luokan ominaisuuksia ja niiden tyyppejä ja nimiä ei saa muuttaa
        public int AutoId { get; set; }
        public string RekNro { get; set; } = null!;
        public int? SäiliöId { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Katsastus { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Adr { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Piirturi { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Alkolukko { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Nopeudenrajoitin { get; set; }
        public int? AutoInfoId { get; set; }

        // Tämä ominaisuus ei ole tietokantataulussa, joten se on merkitty NotMapped-ominaisuudella
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string? InfoTxt { get; set; }

        // Nämä ominaisuudet ovat navigointiominaisuuksia, jotka liittyvät Auto-luokkaan
        public virtual Säiliö? Säiliö { get; set; }
        public virtual AutoInfo? AutoInfo { get; set; }

        // Nämä ominaisuudet ovat myös navigointiominaisuuksia, jotka liittyvät Auto-luokkaan
        // ICollection-tyyppi mahdollistaa useiden AutoHuollot-, AutoHuoltopyynnöt- ja AutoMuistutukset-olioiden tallentamisen
        public virtual ICollection<AutoHuollot> AutoHuollots { get; set; }
        public virtual ICollection<AutoHuoltopyyntö> AutoHuoltopyyntös { get; set; }
        public virtual ICollection<AutoMuistutu> AutoMuistutus { get; set; }
    }
}
