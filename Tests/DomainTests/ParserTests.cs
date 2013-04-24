using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngryElectron.Domain;

namespace AngryElectron.Tests.Domain
{
    [TestClass]
    public class ParserTests
    {
        Parser SideParser = new Parser();
        ChemicalEquation SideEquation;



        //[TestMethod]
        //public void EquationCanParseOneSideTest()
        //{
        //    SideEquation = SideParser.Parse("H2O");
        //    Assert.IsTrue(SideEquation.ToHTML() == "H<sub>2</sub>O");
        //}
    }
}
