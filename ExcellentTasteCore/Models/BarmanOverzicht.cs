using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExcellentTasteCore.Models
{
    public class BarmanOverzicht
    {
        public int BestellingId { get; set; }
        public int ReserveringId { get; set; }
        public string ConsumptieItemCode { get; set; }
        public int Aantal { get; set; }
        public DateTime? DateTimeBereidingConsumptie { get; set; }

        public virtual ConsumptieItem ConsumptieItem { get; set; }
        public virtual Reservering Reservering { get; set; }
    }
}
