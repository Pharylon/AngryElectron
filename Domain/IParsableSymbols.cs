using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    interface IParsableSymbols
    {
        IEnumerable<String> ParsableSymbols { get; }
    }
}
