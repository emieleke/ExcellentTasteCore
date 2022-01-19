using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ExcellentTasteCore.Models
{
    public partial class ConsumptieItem
    {
        public ConsumptieItem()
        {
            Bestelling = new HashSet<Bestelling>();
        }

   
        public string ConsumptieItemCode { get; set; }


        public string ConsumptieGroepCode { get; set; }

        public string ConsumptieItemNaam { get; set; }

        public decimal Prijs { get; set; }

        public virtual ConsumptieGroep ConsumptieGroep { get; set; }
        public virtual ICollection<Bestelling> Bestelling { get; set; }
    }
}
