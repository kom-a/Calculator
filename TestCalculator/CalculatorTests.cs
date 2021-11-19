using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorNS;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TestCalculator
{
    [TestFixture]
    public class CalculatorTests
    {
        [TestCase("2 + 2 * 2", 6)]
        [TestCase("2 + 3 * 4 + 5", 19)]
        public void TestCase2(string expression, double expected)
        {
            Calculator calc = new Calculator();
            double result = calc.Calculate(expression);
            Assert.AreEqual(expected, result);
        }
    }
}
