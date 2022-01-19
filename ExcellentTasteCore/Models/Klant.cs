using System;
using System.Collections.Generic;

#nullable disable

namespace ExcellentTasteCore.Models
{
    public partial class Klant
    {
        public Klant()
        {
            Reserveringen = new HashSet<Reservering>();
        }

        public int KlantId { get; set; }
        public string KlantNaam { get; set; }
        public string Telefoon { get; set; }
        public int Status { get; set; }

        public virtual ICollection<Reservering> Reserveringen { get; set; }
    }
}
