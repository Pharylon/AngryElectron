using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    interface IParser
    {
        IEnumerable<String> ParsableSymbols { get; set; }
       
    }
}
