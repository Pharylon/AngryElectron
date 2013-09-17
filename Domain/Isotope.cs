using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    public class Isotope : Element
    {

        public override double AtomicMass { get; set; }
        public Element IsoElement { get; set; }
        public Isotope(Element IsoElement, double AtomicMass)
        {
            this.IsoElement = IsoElement;
            this.AtomicMass = AtomicMass;
        }
    }
}
