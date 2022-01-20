using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ExcellentTasteCore.Models
{
    public partial class Consumptie
    {
        public Consumptie()
        {
            ConsumptieGroep = new HashSet<ConsumptieGroep>();
        }


        public string ConsumptieCode { get; set; }
        
    
        public string ConsumptieNaam { get; set; }

        public virtual ICollection<ConsumptieGroep> ConsumptieGroep { get; set; }
    }
}
