using System;
using MolarMassCalculator.Common;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Collections.Generic;
using FormulaParserUWPComponent;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        private const double allowedDifference = 0.1;


        [TestMethod]
        public void TestCase001()
        {
            bool equal;
            int retVal = Constants.Failure;
            string errorMsg = string.Empty;
            double expectedElementComposition;
            string Formula = "C8H10N2O", formulaSummary;
            double expectedMolarMass = 150.18, actualMolarMass;
            Dictionary<string, double> expectedComposition = new Dictionary<string, double>()
            {
                { "C", 63.98 },
                { "H", 6.71  },
                { "N", 18.66 },
                { "O", 10.65 },
            };

            FormulaParser parser = new FormulaParser();
            retVal = parser.SetFormula(Formula, out formulaSummary, out actualMolarMass);
            Assert.AreEqual(Constants.Success, retVal);
            equal = Math.Abs(expectedMolarMass - actualMolarMass) <= allowedDifference;
            Assert.IsTrue(equal);

            IDictionary<string, double> actualComposition = parser.ParseElements(out retVal);
            Assert.AreEqual(Constants.Success, retVal);
            Assert.AreEqual(expectedComposition.Count, actualComposition.Count);

            foreach (KeyValuePair<string, double> kvp in actualComposition)
            {
                expectedElementComposition = expectedComposition[kvp.Key];
                equal = Math.Abs(expectedElementComposition - kvp.Value) <= allowedDifference;
                Assert.IsTrue(equal);
            }
        }

        [TestMethod]
        public void TestCase002()
        {
            bool equal;
            int retVal = Constants.Failure;
            string errorMsg = string.Empty;
            double expectedElementComposition;
            string Formula = "C8H8ClNO3S", formulaSummary;
            double expectedMolarMass = 233.68, actualMolarMass;
            Dictionary<string, double> expectedComposition = new Dictionary<string, double>()
            {
                { "C", 41.12 },
                { "H", 3.45  },
                { "Cl", 15.17 },
                { "N", 5.99 },
                { "O", 20.45 },
                { "S", 13.72 },
            };

            FormulaParser parser = new FormulaParser();
            retVal = parser.SetFormula(Formula, out formulaSummary, out actualMolarMass);
            Assert.AreEqual(Constants.Success, retVal);
            equal = Math.Abs(expectedMolarMass - actualMolarMass) <= allowedDifference;
            Assert.IsTrue(equal);

            IDictionary<string, double> actualComposition = parser.ParseElements(out retVal);
            Assert.AreEqual(Constants.Success, retVal);
            Assert.AreEqual(expectedComposition.Count, actualComposition.Count);

            foreach (KeyValuePair<string, double> kvp in actualComposition)
            {
                expectedElementComposition = expectedComposition[kvp.Key];
                equal = Math.Abs(expectedElementComposition - kvp.Value) <= allowedDifference;
                Assert.IsTrue(equal);
            }
        }

        [TestMethod]
        public void TestCase003()
        {
            bool equal;
            int retVal = Constants.Failure;
            string errorMsg = string.Empty;
            double expectedElementComposition;
            string Formula = "C8H7HgNaO3", formulaSummary;
            double expectedMolarMass = 374.74, actualMolarMass;
            Dictionary<string, double> expectedComposition = new Dictionary<string, double>()
            {
                { "C", 25.64 },
                { "H", 1.88  },
                { "Hg", 53.53 },
                { "Na", 6.14 },
                { "O", 12.81 }
            };

            FormulaParser parser = new FormulaParser();
            retVal = parser.SetFormula(Formula, out formulaSummary, out actualMolarMass);
            Assert.AreEqual(Constants.Success, retVal);
            equal = Math.Abs(expectedMolarMass - actualMolarMass) <= allowedDifference;
            Assert.IsTrue(equal);

            IDictionary<string, double> actualComposition = parser.ParseElements(out retVal);
            Assert.AreEqual(Constants.Success, retVal);
            Assert.AreEqual(expectedComposition.Count, actualComposition.Count);

            foreach (KeyValuePair<string, double> kvp in actualComposition)
            {
                expectedElementComposition = expectedComposition[kvp.Key];
                equal = Math.Abs(expectedElementComposition - kvp.Value) <= allowedDifference;
                Assert.IsTrue(equal);
            }
        }

        [TestMethod]
        public void TestCase004()
        {
            bool equal;
            int retVal = Constants.Failure;
            string errorMsg = string.Empty;
            double expectedElementComposition;
            string Formula = "Al(ClO3)3", formulaSummary;
            double expectedMolarMass = 277.35, actualMolarMass;
            Dictionary<string, double> expectedComposition = new Dictionary<string, double>()
            {
                { "Al", 9.73 },
                { "Cl", 38.35  },
                { "O", 51.92 }
            };

            FormulaParser parser = new FormulaParser();
            retVal = parser.SetFormula(Formula, out formulaSummary, out actualMolarMass);
            Assert.AreEqual(Constants.Success, retVal);
            equal = Math.Abs(expectedMolarMass - actualMolarMass) <= allowedDifference;
            Assert.IsTrue(equal);

            IDictionary<string, double> actualComposition = parser.ParseElements(out retVal);
            Assert.AreEqual(Constants.Success, retVal);
            Assert.AreEqual(expectedComposition.Count, actualComposition.Count);

            foreach (KeyValuePair<string, double> kvp in actualComposition)
            {
                expectedElementComposition = expectedComposition[kvp.Key];
                equal = Math.Abs(expectedElementComposition - kvp.Value) <= allowedDifference;
                Assert.IsTrue(equal);
            }
        }

        [TestMethod]
        public void TestCase005()
        {
            bool equal;
            int retVal = Constants.Failure;
            string errorMsg = string.Empty;
            double expectedElementComposition;
            string Formula = "Al2(SiF6)3", formulaSummary;
            double expectedMolarMass = 480.23, actualMolarMass;
            Dictionary<string, double> expectedComposition = new Dictionary<string, double>()
            {
                { "Al", 11.24 },
                { "F", 71.22  },
                { "Si", 17.55 }
            };

            FormulaParser parser = new FormulaParser();
            retVal = parser.SetFormula(Formula, out formulaSummary, out actualMolarMass);
            Assert.AreEqual(Constants.Success, retVal);
            equal = Math.Abs(expectedMolarMass - actualMolarMass) <= allowedDifference;
            Assert.IsTrue(equal);

            IDictionary<string, double> actualComposition = parser.ParseElements(out retVal);
            Assert.AreEqual(Constants.Success, retVal);
            Assert.AreEqual(expectedComposition.Count, actualComposition.Count);

            foreach (KeyValuePair<string, double> kvp in actualComposition)
            {
                expectedElementComposition = expectedComposition[kvp.Key];
                equal = Math.Abs(expectedElementComposition - kvp.Value) <= allowedDifference;
                Assert.IsTrue(equal);
            }
        }

        [TestMethod]
        public void TestCase006()
        {
            bool equal;
            int retVal = Constants.Failure;
            string errorMsg = string.Empty;
            double expectedElementComposition;
            string Formula = "AlO8RbS2", formulaSummary;
            double expectedMolarMass = 304.58, actualMolarMass;
            Dictionary<string, double> expectedComposition = new Dictionary<string, double>()
            {
                { "Al", 8.86 },
                { "O", 42.03},
                { "S", 21.05},
                { "Rb", 28.06 }
            };

            FormulaParser parser = new FormulaParser();
            retVal = parser.SetFormula(Formula, out formulaSummary, out actualMolarMass);
            Assert.AreEqual(Constants.Success, retVal);
            equal = Math.Abs(expectedMolarMass - actualMolarMass) <= allowedDifference;
            Assert.IsTrue(equal);

            IDictionary<string, double> actualComposition = parser.ParseElements(out retVal);
            Assert.AreEqual(Constants.Success, retVal);
            Assert.AreEqual(expectedComposition.Count, actualComposition.Count);

            foreach (KeyValuePair<string, double> kvp in actualComposition)
            {
                expectedElementComposition = expectedComposition[kvp.Key];
                equal = Math.Abs(expectedElementComposition - kvp.Value) <= allowedDifference;
                Assert.IsTrue(equal);
            }
        }

        [TestMethod]
        public void TestCase007()
        {
            bool equal;
            int retVal = Constants.Failure;
            string errorMsg = string.Empty;
            double expectedElementComposition;
            string Formula = "CH3(CH2)4(CHCHCH2)4CH2CH2COOH", formulaSummary;
            double expectedMolarMass = 304.46, actualMolarMass;
            Dictionary<string, double> expectedComposition = new Dictionary<string, double>()
            {
                { "C", 78.89 },
                { "H", 10.60},
                { "O", 10.51 }
            };

            FormulaParser parser = new FormulaParser();
            retVal = parser.SetFormula(Formula, out formulaSummary, out actualMolarMass);
            Assert.AreEqual(Constants.Success, retVal);
            equal = Math.Abs(expectedMolarMass - actualMolarMass) <= allowedDifference;
            Assert.IsTrue(equal);

            IDictionary<string, double> actualComposition = parser.ParseElements(out retVal);
            Assert.AreEqual(Constants.Success, retVal);
            Assert.AreEqual(expectedComposition.Count, actualComposition.Count);

            foreach (KeyValuePair<string, double> kvp in actualComposition)
            {
                expectedElementComposition = expectedComposition[kvp.Key];
                equal = Math.Abs(expectedElementComposition - kvp.Value) <= allowedDifference;
                Assert.IsTrue(equal);
            }
        }

        [TestMethod]
        public void TestCase008()
        {
            bool equal;
            int retVal = Constants.Failure;
            string errorMsg = string.Empty;
            double expectedElementComposition;
            string Formula = "C20H14CaO8S2", formulaSummary;
            double expectedMolarMass = 486.54, actualMolarMass;
            Dictionary<string, double> expectedComposition = new Dictionary<string, double>()
            {
                { "C", 49.37 },
                { "H", 2.90},
                { "Ca", 8.24},
                { "S", 13.18},
                { "O", 26.31 }
            };

            FormulaParser parser = new FormulaParser();
            retVal = parser.SetFormula(Formula, out formulaSummary, out actualMolarMass);
            Assert.AreEqual(Constants.Success, retVal);
            equal = Math.Abs(expectedMolarMass - actualMolarMass) <= allowedDifference;
            Assert.IsTrue(equal);

            IDictionary<string, double> actualComposition = parser.ParseElements(out retVal);
            Assert.AreEqual(Constants.Success, retVal);
            Assert.AreEqual(expectedComposition.Count, actualComposition.Count);

            foreach (KeyValuePair<string, double> kvp in actualComposition)
            {
                expectedElementComposition = expectedComposition[kvp.Key];
                equal = Math.Abs(expectedElementComposition - kvp.Value) <= allowedDifference;
                Assert.IsTrue(equal);
            }
        }

        [TestMethod]
        public void TestCase009()
        {
            bool equal;
            int retVal = Constants.Failure;
            string errorMsg = string.Empty;
            double expectedElementComposition;
            string Formula = "(C6H5CNN)2Hg", formulaSummary;
            double expectedMolarMass = 434.87, actualMolarMass;
            Dictionary<string, double> expectedComposition = new Dictionary<string, double>()
            {
                { "C", 38.67 },
                { "H", 2.32},
                { "Hg", 46.13},
                { "N", 12.88}
            };

            FormulaParser parser = new FormulaParser();
            retVal = parser.SetFormula(Formula, out formulaSummary, out actualMolarMass);
            Assert.AreEqual(Constants.Success, retVal);
            equal = Math.Abs(expectedMolarMass - actualMolarMass) <= allowedDifference;
            Assert.IsTrue(equal);

            IDictionary<string, double> actualComposition = parser.ParseElements(out retVal);
            Assert.AreEqual(Constants.Success, retVal);
            Assert.AreEqual(expectedComposition.Count, actualComposition.Count);

            foreach (KeyValuePair<string, double> kvp in actualComposition)
            {
                expectedElementComposition = expectedComposition[kvp.Key];
                equal = Math.Abs(expectedElementComposition - kvp.Value) <= allowedDifference;
                Assert.IsTrue(equal);
            }
        }

        [TestMethod]
        public void TestCase010()
        {
            bool equal;
            int retVal = Constants.Failure;
            string errorMsg = string.Empty;
            double expectedElementComposition;
            string Formula = "Ba(OH)2", formulaSummary;
            double expectedMolarMass = 171.38, actualMolarMass;
            Dictionary<string, double> expectedComposition = new Dictionary<string, double>()
            {
                { "H", 1.18},
                { "O", 18.67},
                { "Ba", 80.15}
            };

            FormulaParser parser = new FormulaParser();
            retVal = parser.SetFormula(Formula, out formulaSummary, out actualMolarMass);
            Assert.AreEqual(Constants.Success, retVal);
            equal = Math.Abs(expectedMolarMass - actualMolarMass) <= allowedDifference;
            Assert.IsTrue(equal);

            IDictionary<string, double> actualComposition = parser.ParseElements(out retVal);
            Assert.AreEqual(Constants.Success, retVal);
            Assert.AreEqual(expectedComposition.Count, actualComposition.Count);

            foreach (KeyValuePair<string, double> kvp in actualComposition)
            {
                expectedElementComposition = expectedComposition[kvp.Key];
                equal = Math.Abs(expectedElementComposition - kvp.Value) <= allowedDifference;
                Assert.IsTrue(equal);
            }
        }
    }
}
