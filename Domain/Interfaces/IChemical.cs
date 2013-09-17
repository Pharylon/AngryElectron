using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    public interface IChemical
    {
        String ToHTML();
        double Mass { get; }
        Element[] Elements { get; }
    }
}
