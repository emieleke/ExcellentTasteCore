using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ExcellentTasteCore.Models
{
    public partial class Reservering
    {
        public Reservering()
        {
            Bestelling = new HashSet<Bestelling>();
        }

        public int ReserveringId { get; set; }
        public int KlantId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Datum { get; set; }

        [Display(Name = "Tijd")]
        [DataType(DataType.Time)]
        public TimeSpan Tijd { get; set; }
        public int Tafel { get; set; }

        /// <summary>
        /// Hier staan de adresgegevens van de klant.
        /// Er is een nieuwe migration toegevoegd zodat dit in de database opgenomen kan worden.
        /// Ik heb de controller opnieuw gegenereerd met CRUD, waardoor de gegevens nu echt ingevoerd kunnen worden.
        /// </summary>
        #region Gegevens adres gast
        public string Straat { get; set; }
        public int Huisnummer { get; set; }
        public string Toevoeging { get; set; }
        public string Postcode { get; set; }
        public string Woonplaats { get; set; }
        public string Land { get; set; }
        #endregion

        public int AantalPersonen { get; set; }
        public int Status { get; set; }
        public DateTime DatumToegevoegd { get; set; }
        public DateTime BonDatum { get; set; }
        public string Betalingswijze { get; set; }
        public decimal? BonTotaal { get; set; }

        public virtual Klant Klant { get; set; }
        public virtual ICollection<Bestelling> Bestelling { get; set; }
    }
}
