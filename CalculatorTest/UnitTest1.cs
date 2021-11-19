using NUnit.Framework;
using CalculatorNS;

namespace CalculatorTest
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("2 + 2 * 2", 6)]
        [TestCase("2 + 3 * 4 + 5", 19)]
        [TestCase("-2 * 2", -4)]
        [TestCase("(2 + 3) * 4 + 5", 25)]
        [TestCase("(((10 - 5) + 2) * 2) - 4", 10)]
        public void TestCase2(string expression, double expected)
        {
            Calculator calc = new Calculator();
            double result = calc.Calculate(expression);
            Assert.AreEqual(expected, result);
        }
    }
}