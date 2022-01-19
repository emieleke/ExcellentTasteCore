using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ExcellentTasteCore.Models
{
    public partial class ConsumptieGroep
    {
        public ConsumptieGroep()
        {
            ConsumptieItems = new HashSet<ConsumptieItem>();
        }

     
        public string ConsumptieGroepCode { get; set; }

     
        public string ConsumptieCode { get; set; }

       
        public string ConsumptieGroepNaam { get; set; }

        public virtual Consumptie Consumptie { get; set; }
        public virtual ICollection<ConsumptieItem> ConsumptieItems { get; set; }
    }
}
