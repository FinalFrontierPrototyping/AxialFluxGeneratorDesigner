using AxialFluxGeneratorDesigner.Calculations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class GeneratorTests
    {
        private const double Delta = 0.000000000001;

        [TestMethod]
        public void TestCaseStudy()
        {
            const double dcVoltage = 200;
            const double singleGap = 3;
            const double magnetThickness = 10;
            const double magnetWidth = 46;
            const double magnetLength = 30;
            const int magnets = 20;
            //double coil_count = 15;
            const int rpm = 85;
            const int coilsPhase = 5;

            //Phase voltage
            var phaseVoltage = Stator.CalculatePhaseVoltage(dcVoltage, 1.4);
            Assert.AreEqual(82.221205699422, phaseVoltage, Delta);

            //Stator thickness
            var statorThickness = Stator.CalculateStatorThickness(magnetThickness, singleGap);
            Assert.AreEqual(14, statorThickness, Delta);

            //Flux density
            var fluxDensity = Rotor.CalculateMagnetFluxDensity(1.275, 927.5, magnetThickness, singleGap);
            Assert.AreEqual(0.608905169159082, fluxDensity, Delta);

            //Pole flux
            var poleFlux = Rotor.CalculateMaximumPoleFlux(fluxDensity, magnetWidth, magnetLength);
            Assert.AreEqual(0.000840289133439534, poleFlux, Delta);

            //Coil windings
            var coilWindings = Stator.CalculateCoilWindings(phaseVoltage, magnets, rpm, coilsPhase, poleFlux, 0.95);
            Assert.AreEqual(327, coilWindings, Delta);

            //Coil leg width
            var coilLegWidth = Stator.CalculateCoilLegWidth(4.27, coilWindings, statorThickness, 3000, 0.55);
            Assert.AreEqual(26.6262055747546, coilLegWidth, Delta);
        }
    }
}