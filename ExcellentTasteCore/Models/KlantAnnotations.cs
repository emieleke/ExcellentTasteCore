using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExcellentTasteCore.Models
{
    public partial class Klant
    {
        public bool Actief
        {
            get { return Status == 1 ? true : false; }
        }
    }
}
