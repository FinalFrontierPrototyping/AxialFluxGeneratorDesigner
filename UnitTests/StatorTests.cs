using AxialFluxGeneratorDesigner.Calculations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class StatorTests
    {
        private const double Delta = 0.000000000001;

        [TestMethod]
        public void TestCoilWindings()
        {
            Assert.AreEqual(339, Stator.CalculateCoilWindings(82.22, 20, 85, 5, 0.00081, 0.95));
        }

        [TestMethod]
        public void TestCoilLegWidth()
        {
            Assert.AreEqual(27.6787345012158, Stator.CalculateCoilLegWidth(4.27, 337, 13.76, 3000, 0.55), Delta);
        }

        [TestMethod]
        public void TestCoilCrossSectionalArea()
        {
            Assert.AreEqual(0.70829293768546000000, Stator.CalculateCoilCrossSectionalArea(31.54, 13.76, 337, 0.55),
                Delta);
        }

        [TestMethod]
        public void TestMaximumCurrentDensity()
        {
            Assert.AreEqual(6.02857909885899, Stator.CalculateMaximumCurrentDensity(4.27, 0.70829293768546000000), Delta);
        }

        [TestMethod]
        public void TestCoilWireDiameter()
        {
            Assert.AreEqual(0.94964550097274, Stator.CalculateCoilWireDiameter(0.70829293768546000000), Delta);
        }

        [TestMethod]
        public void TestStatorThickness()
        {
            Assert.AreEqual(14, Stator.CalculateStatorThickness(10, 3), Delta);
            Assert.AreEqual(17.8, Stator.CalculateStatorThickness(12, 3.1), Delta);
            Assert.AreEqual(29.6, Stator.CalculateStatorThickness(20, 5.2), Delta);
        }

        [TestMethod]
        public void TestCalculateWireResistance()
        {
            var wireResistance = Stator.CalculateWireResistance(10, 1);
            Assert.AreEqual(0.213904243515507, wireResistance, Delta);

            var wireResistance1 = Stator.CalculateWireResistance(100, 0.8);
            Assert.AreEqual(3.3422538049298, wireResistance1, Delta);
        }

        [TestMethod]
        public void TestCalculateCoilLegWidthMod()
        {
            var coilLegWidth = Stator.CalculateCoilLegWidthMod(4.27, 337, 13.76, 5, 0.55);
            Assert.AreEqual(38.0282769556025, coilLegWidth, Delta);
        }
    }
}