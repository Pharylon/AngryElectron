﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngryElectron.Domain;
using System.Collections.Generic;

namespace AngryElectron.Tests.Domain
{
    [TestClass]
    public class BalancerTest
    {
        Balancer myBalancer;
        ChemicalEquation myEquation;
        Parser myParser;

        [TestInitialize]
        public void Inititalize()
        {
            myBalancer = new Balancer();
            myParser = new Parser();
        }

        [TestMethod]
        public void BasicBalancerTest()
        {
            myEquation = myParser.Parse("FeS2 + O2 -> Fe2O3 + SO2");
            myEquation = myBalancer.Balance(myEquation);
            Assert.IsTrue(myEquation.ToString() == "4FeS2 + 11O2 -> 2Fe2O3 + 8SO2");
        }

        [TestMethod]
        public void BalancerWithComplexTest()
        {
            myEquation = myParser.Parse("CaCl2 + Ag(NO3) -> Ca(NO3)2 + AgCl");
            myEquation = myBalancer.Balance(myEquation);
            Assert.IsTrue(myEquation.ToString() == "CaCl2 + 2Ag(NO3) -> Ca(NO3)2 + 2AgCl");
        }

        [TestMethod]
        public void EquationWithLoneElementTest()
        {
            myEquation = myParser.Parse("HCl + Na = NaCl + H2");
            myEquation = myBalancer.Balance(myEquation);
            Assert.IsTrue(myEquation.ToString() == "2HCl + 2Na -> 2NaCl + H2");
        }
    }
}
