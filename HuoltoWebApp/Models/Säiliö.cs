using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HuoltoWebApp.Models   // Tässä Modelissa on Säiliö-luokka, joka on osa tietokantamallia
{
    public partial class Säiliö
    {
        public Säiliö()     // Tällä varmistetaan, että kaikki navigointiominaisuudet on alustettu
        {                   // eli Säiliö-olioon liittyvät Autos, SäiliöHuollots, SäiliöHuoltopyynnöt ja SäiliöMuistutukset
            Autos = new HashSet<Auto>();
            SäiliöHuollots = new HashSet<SäiliöHuollot>();
            SäiliöHuoltopyyntös = new HashSet<SäiliöHuoltopyyntö>();
            SäiliöMuistutus = new HashSet<SäiliöMuistutu>();
        }

        //Nämä ovat Säiliö-luokan ominaisuuksia ja niiden tyyppejä ja nimiä ei saa muuttaa
        public int SäiliöId { get; set; }
        public int SäiliöNro { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Vakaus { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Välitarkastus { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Määräaikaistarkastus { get; set; }
        public int? SäiliöInfoId { get; set; }

        // Tämä ominaisuus ei ole tietokantataulussa, joten se on merkitty NotMapped-ominaisuudella
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string? InfoTxt { get; set; }

        public virtual SäiliöInfo? SäiliöInfo { get; set; }

        // Nämä ominaisuudet ovat navigointiominaisuuksia, jotka liittyvät Säiliö-luokkaan
        // ICollection-tyyppi mahdollistaa useiden Auto-, SäiliöHuollot-, SäiliöHuoltopyynnöt- ja SäiliöMuistutukset-olioiden tallentamisen
        public virtual ICollection<Auto> Autos { get; set; }
        public virtual ICollection<SäiliöHuollot> SäiliöHuollots { get; set; }
        public virtual ICollection<SäiliöHuoltopyyntö> SäiliöHuoltopyyntös { get; set; }
        public virtual ICollection<SäiliöMuistutu> SäiliöMuistutus { get; set; }
    }
}
