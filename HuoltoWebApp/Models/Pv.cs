using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuoltoWebApp.Models   // Tässä Modelissa on Pv-luokka, joka on osa tietokantamallia
{
    public partial class Pv
    {
        public Pv()     // Tällä varmistetaan, että kaikki navigointiominaisuudet on alustettu
        {               // eli Pv-olioon liittyvät PvHuollot, PvHuoltopyynnöt ja PvMuistutukset
            PvHuollots = new HashSet<PvHuollot>();
            PvHuoltopyyntös = new HashSet<PvHuoltopyyntö>();
            PvMuistutus = new HashSet<PvMuistutu>();
        }

        //Nämä ovat Pv-luokan ominaisuuksia ja niiden tyyppejä ja nimiä ei saa muuttaa
        public int PvId { get; set; }
        public string RekNro { get; set; } = null!;
        [DataType(DataType.Date)]
        public DateTime? Katsastus { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Adr { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Välitarkastus { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Määräaikaistarkastus { get; set; }
        public int? PvInfoId { get; set; }

        // Tämä ominaisuus ei ole tietokantataulussa, joten se on merkitty NotMapped-ominaisuudella
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string? InfoTxt { get; set; }

        // Nämä ominaisuudet ovat navigointiominaisuuksia, jotka liittyvät Pv-luokkaan
        public virtual PvInfo? PvInfo { get; set; }

        // Nämä ominaisuudet ovat myös navigointiominaisuuksia, jotka liittyvät Pv-luokkaan
        // ICollection-tyyppi mahdollistaa useiden PvHuollot-, PvHuoltopyynnöt- ja PvMuistutukset-olioiden tallentamisen
        public virtual ICollection<PvHuollot> PvHuollots { get; set; }
        public virtual ICollection<PvHuoltopyyntö> PvHuoltopyyntös { get; set; }
        public virtual ICollection<PvMuistutu> PvMuistutus { get; set; }
    }
}
