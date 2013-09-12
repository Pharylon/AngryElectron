using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngryElectron.Domain;

namespace AngryElectron.Tests.Domain
{
    [TestClass]
    public class ParserTests
    {
        ChemicalEquation SideEquation;

        [TestMethod]
        public void InitializeEquationUsingStringConstructor()
        {
            ChemicalEquation myEquation = new ChemicalEquation("FeS2 + O2 -> Fe2O3 + SO2");
            Assert.IsTrue("FeS2 + O2 -> Fe2O3 + SO2" == myEquation.ToString());
        }

        //[TestMethod]
        //public void EquationCanParseOneSideTest()
        //{
        //    SideEquation = SideParser.Parse("H2O");
        //    Assert.IsTrue(SideEquation.ToHTML() == "H<sub>2</sub>O");
        //}
    }
}
