using AxialFluxGeneratorDesigner.Calculations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UseCaseTest
    {
        private const double Delta = 0.000000000001;
        private readonly Generator _test = new Generator();

        [TestMethod]
        public void TestUseCase()
        {
            //Front end
            //_test.GeneratorEnergyStorageConnection = 1;
            //_test.GeneratorFrontEnd = 1;
            
            //_test.DcVoltageMin = 200;
            //_test.DcVoltageMax = 500;
            //_test.GeneratorPower = 1000;

            //_test.FrontEndRpmMax = 500;
            //_test.FrontEndRpmMin = 500;

            //_test.UpdateCalculations(true);

            //Assert.AreEqual(91.35689522158, _test.PhaseVoltageMin, Delta);
            //Assert.AreEqual(227.439658709534, _test.PhaseVoltageMax, Delta);
            //Assert.AreEqual(227.439658709534, _test.FrontEndTorque, Delta);

        }
    }
}