using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectronDomain
{
    interface IParser
    {
        IEnumerable<String> ParsableSymbols;
        IEquation parse(string reaction);

    }
}
