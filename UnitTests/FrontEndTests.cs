using AxialFluxGeneratorDesigner.Calculations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class FrontEndTests
    {
        private const double Delta = 0.000000000001;
        private readonly Generator _test = new Generator();

        [TestMethod]
        public void TestTurbineRotorRadius()
        {
            Assert.AreEqual(2.38413613504448, FrontEndCalculations.CalculateTurbineRotorRadius(3000, 1.2, 0.35, 10, 0.8),
                Delta);
            Assert.AreEqual(2.24778510448245, FrontEndCalculations.CalculateTurbineRotorRadius(3000, 1.2, 0.35, 10, 0.9),
                Delta);
            Assert.AreEqual(2.13243618622923, FrontEndCalculations.CalculateTurbineRotorRadius(3000, 1.2, 0.35, 10, 1.0),
                Delta);
        }

        [TestMethod]
        public void TestTurbineOptimalSpeed()
        {
            Assert.AreEqual(111, FrontEndCalculations.CalculateTurbineOptimalRotationSpeed(3, 8.75, 2.25), Delta);
            Assert.AreEqual(297, FrontEndCalculations.CalculateTurbineOptimalRotationSpeed(10, 7, 2.25), Delta);
        }

        [TestMethod]
        public void TestBatteryTurbineCase()
        {
            _test.GeneratorEnergyStorageConnection.Value = 0;
            _test.GeneratorFrontEnd.Value = 0;
            _test.DcInverterVoltageMin.Value = 48;

            _test.UpdateCalculations(true);

            Assert.AreEqual(20.1674655489148, _test.PhaseVoltageMin, Delta);
            Assert.AreEqual(2.24778510448245, _test.TurbineRotorRadius, Delta);
            Assert.AreEqual(111, _test.FrontEndRpmMin.Value, Delta);
            Assert.AreEqual(297, _test.FrontEndRpmMax.Value, Delta);
            Assert.AreEqual(53.9615970092586, _test.PhaseVoltageMax, Delta);
        }

        [TestMethod]
        public void TestGridTurbineCase()
        {
            _test.GeneratorEnergyStorageConnection.Value = 1;
            _test.GeneratorFrontEnd.Value = 0;
            _test.DcInverterVoltageMin.Value = 200;
            _test.DcInverterVoltageMax.Value = 500;

            _test.UpdateCalculations(true);

            Assert.AreEqual(82.221205699422, _test.PhaseVoltageMin, Delta);
            Assert.AreEqual(204.695692838581, _test.PhaseVoltageMax, Delta);

            Assert.AreEqual(119, _test.FrontEndRpmMin.Value, Delta);
            Assert.AreEqual(297, _test.FrontEndRpmMax.Value, Delta);
        }

        [TestMethod]
        public void TestBatteryOtherCase()
        {
            _test.GeneratorEnergyStorageConnection.Value = 0;
            _test.GeneratorFrontEnd.Value = 1;
            _test.FrontEndRpmMin.Value = 300;
            _test.FrontEndRpmMax.Value = 500;
            _test.DcInverterVoltageMin.Value = 48;

            _test.UpdateCalculations(true);

            Assert.AreEqual(1.4, _test.RectifierDiodeVoltageDrop, Delta);
            Assert.AreEqual(22.4082950543498, _test.PhaseVoltageMin, Delta);
            Assert.AreEqual(41.4968426932404, _test.PhaseVoltageMax, Delta);
        }

        /// <summary>
        /// </summary>
        [TestMethod]
        public void TestGridyOtherCase()
        {
            _test.GeneratorEnergyStorageConnection.Value = 1;
            _test.GeneratorFrontEnd.Value = 1;
            _test.DcInverterVoltageMin.Value = 200;
            _test.DcInverterVoltageMax.Value = 500;

            _test.UpdateCalculations(true);

            Assert.AreEqual(91.35689522158, _test.PhaseVoltageMin, Delta);
            Assert.AreEqual(227.439658709534, _test.PhaseVoltageMax, Delta);
        }

        [TestMethod]
        public void VoltageDropThreePhase()
        {
            Assert.AreEqual(3.70493017723444, Stator.VoltageDrop(100, 1, 1, 3), Delta);
            Assert.AreEqual(4.27808487031015, Stator.VoltageDrop(100, 1, 1, 1), Delta);
        }

        [TestMethod]
        public void TestPhaseVoltage()
        {
            Assert.AreEqual(19.5959179422654, Stator.CalculatePhaseVoltage(48, 0), Delta);
            Assert.AreEqual(20.1674655489148, Stator.CalculatePhaseVoltage(48, 1.4), Delta);
            Assert.AreEqual(9.79795897113271, Stator.CalculatePhaseVoltage(24, 0), Delta);
            Assert.AreEqual(10.3695065777821, Stator.CalculatePhaseVoltage(24, 1.4), Delta);
            Assert.AreEqual(4.89897948556636, Stator.CalculatePhaseVoltage(12, 0), Delta);
            Assert.AreEqual(5.47052709221576, Stator.CalculatePhaseVoltage(12, 1.4), Delta);
        }
    }
}