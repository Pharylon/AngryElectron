using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    interface IParser
    {
        IEquation Parse(string reaction);

        //Many things besides the parser will need to implement ParsableSymbols so moved this to it's own interface.
        //IEnumerable<String> ParsableSymbols { get; set; }
    }
}
