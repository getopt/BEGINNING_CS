using Calculator;
using Xunit;

namespace XUnitTestsCalculatorOp
{
    public class Class1
    {
        private CalculatorOp calc = new CalculatorOp();

        [Fact]
        public void AdditionTest()
        {
            Assert.Equal(7, calc.Addition(2,5));
        }

        [Fact]
        public void MultiplicationTest()
        {
            Assert.Equal(10, calc.Multiplication(2, 5));
        }

    }
}