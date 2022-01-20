using System;
using System.Collections.Generic;

#nullable disable

namespace ExcellentTasteCore.Models
{
    public partial class Bestelling
    {
        public int BestellingId { get; set; }
        public int ReserveringId { get; set; }
        public string ConsumptieItemCode { get; set; }
        
        public int Aantal { get; set; }

        // Adresgegevens gast.
        #region Adresgegevens gast
        public string Straat { get; set; }
        public int Huisnummer { get; set; }
        public string Toevoeging { get; set; }
        public string Postcode { get; set; }
        public string Woonplaats { get; set; }
        public string Land { get; set; }
        #endregion

        public DateTime? DateTimeBereidingConsumptie { get; set; }
        public decimal? Prijs { get; set; }
        public decimal? Totaal { get; set; }

        public virtual ConsumptieItem ConsumptieItem { get; set; }
        public virtual Reservering Reservering { get; set; }
    }
}
