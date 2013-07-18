using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngryElectron.Domain;

namespace AngryElectron.Tests.Domain
{
    [TestClass]
    public class EquationSideTests
    {

        [TestMethod]
        public void CanInitializeWithString()
        {
            EquationSide reactants = new EquationSide("H2SO4");
        }
    }
}
