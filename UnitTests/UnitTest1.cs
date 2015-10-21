using AxialFluxGeneratorDesigner;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly Afpm _test = new Afpm();
        private const double Delta = 0.000000000001;

        [TestMethod]
        public void TestRotorRadius()
        {
            Assert.AreEqual(2.38413613504448, _test.CalculateTurbineRotorRadius(3000, 1.2, 0.35, 10, 0.8), Delta);
            Assert.AreEqual(2.24778510448245, _test.CalculateTurbineRotorRadius(3000, 1.2, 0.35, 10, 0.9), Delta);
            Assert.AreEqual(2.13243618622923, _test.CalculateTurbineRotorRadius(3000, 1.2, 0.35, 10, 1.0), Delta);
        }

        [TestMethod]
        public void TestTurbineOptimalSpeed()
        {
            Assert.AreEqual(111, _test.CalculateTurbineOptimalRotationSpeed(3, 8.75, 2.25), Delta);
            Assert.AreEqual(297, _test.CalculateTurbineOptimalRotationSpeed(10, 7, 2.25), Delta);
        }

        [TestMethod]
        public void TestPhaseVoltage()
        {
            Assert.AreEqual(19.5959179422654, _test.CalculatePhaseVoltage(48, 0), Delta);
            Assert.AreEqual(20.1674655489148, _test.CalculatePhaseVoltage(48, 1.4), Delta);
            Assert.AreEqual(9.79795897113271, _test.CalculatePhaseVoltage(24, 0), Delta);
            Assert.AreEqual(10.3695065777821, _test.CalculatePhaseVoltage(24, 1.4), Delta);
            Assert.AreEqual(4.89897948556636, _test.CalculatePhaseVoltage(12, 0), Delta);
            Assert.AreEqual(5.47052709221576, _test.CalculatePhaseVoltage(12, 1.4), Delta);
        }

        [TestMethod]
        public void TestPolePairs()
        {
            Assert.AreEqual(8, _test.CalculatePolePairs(6));
            Assert.AreEqual(20, _test.CalculatePolePairs(15));
            Assert.AreEqual(28, _test.CalculatePolePairs(21));
        }

        [TestMethod]
        public void TestStatorThickness()
        {
            Assert.AreEqual(14, _test.CalculateStatorThickness(10, 3), Delta);
            Assert.AreEqual(17.8, _test.CalculateStatorThickness(12, 3.1), Delta);
            Assert.AreEqual(29.6, _test.CalculateStatorThickness(20, 5.2), Delta);
        }

        [TestMethod]
        public void TestMagnetFluxDensity()
        {
            Assert.AreEqual(0.527994086359978, _test.CalculateMagnetFluxDensity(1.1, 808, 10, 2), Delta);
            Assert.AreEqual(0.645153110460925, _test.CalculateMagnetFluxDensity(1.445, 927.5, 10, 2), Delta);
            Assert.AreEqual(0.645153110460925, _test.CalculateMagnetFluxDensity(1.445, 927.5, 15, 2), Delta);
        }

        [TestMethod]
        public void TestMaximumPoleFlux()
        {
            Assert.AreEqual(0.00006451531105, _test.CalculateMaximumPoleFlux(0.645153110460925, 10, 10), Delta);
            Assert.AreEqual(0.000096772966575, _test.CalculateMaximumPoleFlux(0.645153110460925, 10, 15), Delta);
            Assert.AreEqual(0.000096772966575, _test.CalculateMaximumPoleFlux(0.645153110460925, 15, 10), Delta);
        }

        [TestMethod]
        public void TestCoilWindings()
        {
            Assert.AreEqual(339, _test.CalculateCoilWindings(82.22, 20, 85, 5, 0.00081, 0.95));
        }

        [TestMethod]
        public void TestCoilLegWidth()
        {
            Assert.AreEqual(0.0276787345012158, _test.CalculateCoilLegWidth(4.27, 337, 13.76), Delta);
        }

        [TestMethod]
        public void TestCoilCrossSectionalArea()
        {
            Assert.AreEqual(0.70829293768546000000, _test.CalculateCoilCrossSectionalArea(31.54, 13.76, 337), Delta);
        }

        [TestMethod]
        public void TestMaximumCurrentDensity()
        {
            Assert.AreEqual(6.02857909885899, _test.CalculateMaximumCurrentDensity(4.27, 0.70829293768546000000), Delta);
        }

        [TestMethod]
        public void TestCoilWireDiameter()
        {
            Assert.AreEqual(0.94964550097274, _test.CalculateCoilWireDiameter(0.70829293768546000000), Delta);
        }

        [TestMethod]
        public void TestGeneratorInnerRadius()
        {
            Assert.AreEqual(297.014954798095, _test.CalculateGeneratorInnerRadius(15, 31.54, 20, 46), Delta);
        }

        [TestMethod]
        public void TestGeneratorOuterRadius()
        {
            Assert.AreEqual(327, _test.CalculateCalculateGeneratorOuterRadius(297, 30), Delta);
        }

        [TestMethod]
        public void TestGeneratorInnerOuterRadiusRatio()
        {
            Assert.AreEqual(0.898989898989899, _test.CalculateGeneratorInnerOuterRadiusRatio(267, 297), Delta);
        }

        [TestMethod]
        public void TestCaseStudy()
        {
            double dc_voltage = 200;
            double single_gap = 3;
            double magnet_thickness = 10;
            double magnet_width = 46;
            double magnet_length = 30;
            int magnets = 20;
            //double coil_count = 15;
            int rpm = 85;
            int coils_phase = 5;

            //Phase voltage
            double phaseVoltage = _test.CalculatePhaseVoltage(dc_voltage, 1.4);
            Assert.AreEqual(82.221205699422, phaseVoltage, Delta);

            //Stator thickness
            double statorThickness = _test.CalculateStatorThickness(magnet_thickness, single_gap);
            Assert.AreEqual(14, statorThickness, Delta);

            //Flux density
            double fluxDensity = _test.CalculateMagnetFluxDensity(1.275, 927.5, magnet_thickness, single_gap);
            Assert.AreEqual(0.608905169159082, fluxDensity, Delta);

            //Pole flux
            double poleFlux = _test.CalculateMaximumPoleFlux(fluxDensity, magnet_width, magnet_length);
            Assert.AreEqual(0.000840289133439534, poleFlux, Delta);

            //Coil windings
            int coilWindings = _test.CalculateCoilWindings(phaseVoltage, magnets, rpm, coils_phase, poleFlux, 0.95);
            Assert.AreEqual(327, coilWindings, Delta);

            //Coil leg width
            double coilLegWidth = _test.CalculateCoilLegWidth(4.27, coilWindings, statorThickness);
            Assert.AreEqual(0.0266262055747546, coilLegWidth, Delta);
        }

        [TestMethod]
        public void TestCoilDimensions()
        {
            //var coilDimensionsExpected = new Tuple<double, double, double, double, double, double>(1, 1, 2, 1, 1, 1);

            //var coilDimensions = _test.CoilInnerDimensions(10, 9, 10, 1, 30);
            //Assert.AreEqual(coil_dimensions_expected, coil_dimensions);
        }

        [TestMethod]
        public void TestCalculateWireResistance()
        {
            var wireResistance = _test.CalculateWireResistance(10, 1);
            Assert.AreEqual(0.213904243515507, wireResistance, Delta);

            var wireResistance1 = _test.CalculateWireResistance(100, 0.8);
            Assert.AreEqual(3.3422538049298, wireResistance1, Delta);
        }

        [TestMethod]
        public void VoltageDropThreePhase()
        {
            Assert.AreEqual(3.70493017723444, _test.VoltageDrop(100, 1, 1, 3), Delta);
            Assert.AreEqual(4.27808487031015, _test.VoltageDrop(100, 1, 1, 1), Delta);
        }

        [TestMethod]
        public void TestBatteryTurbineCase()
        {
            _test.GeneratorEnergyStorageConnection = 0;
            _test.GeneratorFrontEnd = 0;
            _test.DcVoltageMin = 48;

            _test.UpdateCalculations(true);

            Assert.AreEqual(20.1674655489148, _test.PhaseVoltageMin, Delta);
            Assert.AreEqual(2.24778510448245, _test.TurbineRotorRadius, Delta);
            Assert.AreEqual(111, _test.FrontEndRpmMin, Delta);
            Assert.AreEqual(297, _test.FrontEndRpmMax, Delta);
            Assert.AreEqual(53.9615970092586, _test.PhaseVoltageMax, Delta);
        }

        [TestMethod]
        public void TestGridTurbineCase()
        {
            _test.GeneratorEnergyStorageConnection = 1;
            _test.GeneratorFrontEnd = 0;
            _test.DcVoltageMin = 200;
            _test.DcVoltageMax = 500;

            _test.UpdateCalculations(true);

            Assert.AreEqual(82.221205699422, _test.PhaseVoltageMin, Delta);
            Assert.AreEqual(204.695692838581, _test.PhaseVoltageMax, Delta);

            Assert.AreEqual(119, _test.FrontEndRpmMin, Delta);
            Assert.AreEqual(297, _test.FrontEndRpmMax, Delta);
            //Assert.AreEqual(53.9615970092586, test.PhaseVoltageMax, delta);
            //Assert.AreEqual(53.9615970092586, test.TurbineWindspeedMin, delta);
        }

        [TestMethod]
        public void TestBatteryOtherCase()
        {
            _test.GeneratorEnergyStorageConnection = 0;
            _test.GeneratorFrontEnd = 1;
            _test.FrontEndRpmMin = 300;
            _test.FrontEndRpmMax = 500;
            _test.DcVoltageMin = 48;

            _test.UpdateCalculations(true);

            Assert.AreEqual(1.4, _test.RectifierDiodeVoltageDrop, Delta);
            Assert.AreEqual(22.4082950543498, _test.PhaseVoltageMin, Delta);
            Assert.AreEqual(41.4968426932404, _test.PhaseVoltageMax, Delta);
        }

        [TestMethod]
        public void TestGridyOtherCase()
        {
            _test.GeneratorEnergyStorageConnection = 1;
            _test.GeneratorFrontEnd = 1;
            _test.DcVoltageMin = 200;
            _test.DcVoltageMax = 500;

            _test.UpdateCalculations(true);

            Assert.AreEqual(91.35689522158, _test.PhaseVoltageMin, Delta);
            Assert.AreEqual(227.439658709534, _test.PhaseVoltageMax, Delta);
        }
    }
}