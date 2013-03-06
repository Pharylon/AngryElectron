using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngryElectron.Domain;
using System.Collections.Generic;

namespace AngryElectron.Tests.Domain
{
    [TestClass]
    public class TableOfElementsTests
    {
        //Initialization
        TableOfElements myElements;

        [TestInitialize]
        public void Initialize()
        {
            myElements = new TableOfElements();
        }

        [TestMethod]
        public void TableOfElementsIsInitializable()
        {
            Assert.IsNotNull(myElements);
        }
        [TestMethod]
        public void TableOfElementsIsNotListOfElements()
        {
            Assert.IsFalse(myElements.GetType() == new List<Element>().GetType());
        }

        [TestMethod]
        public void TableOfElementsContainsElementHydrogen()
        {
            Element hydrogen = new Element(){AtomicMass=1.0, AtomicNumber = 4, Name = "Hydrogen", Symbol = "H"};
            Assert.IsTrue(myElements.Contains(hydrogen));
        }
        [TestMethod]
        public void TableOfElementsIsNotEmpty()
        {
            Assert.IsTrue(myElements.Count > 0);
        }
    }
}
