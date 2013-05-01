using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngryElectron.Domain;
using System.Linq;

namespace AngryElectron.Tests.Domain
{
    [TestClass]
    public class IsotopeTests
    {
        TableOfElements MyElements;

        [TestInitialize]
        public void initialize()
        {
            MyElements = new TableOfElements();
        }
        [TestMethod]
        public void IsotopeReturnsAtomicMassItWasCreatedWith()
        {
            Isotope MyIsotope = new Isotope(MyElements.Where(E => E.Name == "Hydrogen").FirstOrDefault(), 12);
            Assert.IsTrue(MyIsotope.AtomicMass == 12);
            Assert.AreEqual(12, MyIsotope.AtomicMass);
        }

  
    }
}
