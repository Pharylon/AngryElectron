﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    interface IParser
    {
        ChemicalEquation Parse(string reaction);
    }
}
