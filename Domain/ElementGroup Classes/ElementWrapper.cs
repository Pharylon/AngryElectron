using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    class ElementWrapper : ElementGroup
    {
        public ElementWrapper(Element element, int coefficient = 1) : base(coefficient)
        {
            this.Add(element);
        }
    }
}
