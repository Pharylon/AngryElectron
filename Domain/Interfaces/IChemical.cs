using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    public interface IChemical
    {
        List<Element> ListOfElements { get; }
        String ToHTML();
        double Mass { get; }
    }
}
