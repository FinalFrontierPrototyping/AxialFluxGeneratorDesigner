using System;
using AxialFluxGeneratorDesigner.Calculations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class StatorDimensionsDynamicTests
    {
        private const double Delta = 0.00000000001;


        [TestMethod]
        public void TestCoilCount()
        {
            Assert.AreEqual(9, Stator.CalculateCoilCount(3, 3), Delta);
            Assert.AreEqual(15, Stator.CalculateCoilCount(3, 5), Delta);
        }

        [TestMethod]
        public void TestMagnetCount()
        {
            Assert.AreEqual(12, Rotor.CalculateMagnetCount(9), Delta);
            Assert.AreEqual(20, Rotor.CalculateMagnetCount(15), Delta);
        }

        [TestMethod]
        public void TestPoleToPolePitch()
        {
            Assert.AreEqual(72.2566310325652, Rotor.CalculateMagnetPoleToPolePitch(2/Math.PI, 46), Delta);
        }

        [TestMethod]
        public void TestBetweenPoleDistance()
        {
            Assert.AreEqual(26.2566310325652, Rotor.CalculateBetweenPoleDistance(72.2566310325652, 46), Delta);
        }

        [TestMethod]
        public void TestMagnetCentralAngle()
        {
            Assert.AreEqual(18, Rotor.CalculateMagnetCentralAngle(20), Delta);
        }


        [TestMethod]
        public void TestMagnetSegmentAngle()
        {
            Assert.AreEqual(11.4591559026165, Rotor.CalculateMagnetSegmentAngle(18, 2/Math.PI), Delta);
        }

        [TestMethod]
        public void TestBetweenMagnetSegmentAngle()
        {
            Assert.AreEqual(6.5408, Rotor.CalculateBetweenMagnetSegmentAngle(18, 11.4592), Delta);
        }

        [TestMethod]
        public void TestRotorInnerRadius()
        {
            Assert.AreEqual(229.23193369094, Rotor.CalculateRotorInnerRadius(46, 11.4592), Delta);
        }

        [TestMethod]
        public void TestRotorOuterRadius()
        {
            Assert.AreEqual(259.23193369094, Rotor.CalculateRotorOuterRadius(229.23193369094, 30), Delta);
        }

        [TestMethod]
        public void TestStatorInnerRadius()
        {
            Assert.AreEqual(197.69193369094, Stator.CalculateStatorInnerRadius(229.23193369094, 31.54), Delta);
        }

        [TestMethod]
        public void TestStatorOuterRadius()
        {
            Assert.AreEqual(290.77193369094, Stator.CalculateStatorOuterRadius(259.23193369094, 31.54), Delta);
        }

        [TestMethod]
        public void TestCentralCoilAngle()
        {
            Assert.AreEqual(60, StatorDimensionsDynamic.CalculateCentralCoilAngle(6), Delta);
            Assert.AreEqual(40, StatorDimensionsDynamic.CalculateCentralCoilAngle(9), Delta);
            Assert.AreEqual(30, StatorDimensionsDynamic.CalculateCentralCoilAngle(12), Delta);
            Assert.AreEqual(24, StatorDimensionsDynamic.CalculateCentralCoilAngle(15), Delta);
            Assert.AreEqual(20, StatorDimensionsDynamic.CalculateCentralCoilAngle(18), Delta);
            Assert.AreEqual(17.1428571428571, StatorDimensionsDynamic.CalculateCentralCoilAngle(21), Delta);
        }

        [TestMethod]
        public void TestOuterCoilSegmentTopWidth()
        {
            Assert.AreEqual(123.61096491098,
                StatorDimensionsDynamic.CalculateOuterCoilSegmentTopWidth(24, 290.77193369094), Delta);
        }

        [TestMethod]
        public void TestOuterCoilSegmentBottomWidth()
        {
            Assert.AreEqual(84.0414353904884,
                StatorDimensionsDynamic.CalculateOuterCoilSegmentBottomWidth(24, 197.69193369094), Delta);
        }

        [TestMethod]
        public void TestBetweenCoilWidth()
        {
            var centralCoilAngle = StatorDimensionsDynamic.CalculateCentralCoilAngle(15);
            Assert.AreEqual(5.11170297432515, StatorDimensionsDynamic.CalculateBetweenCoilWidth(5, centralCoilAngle), Delta);
        }

        [TestMethod]
        public void TestOuterCoilTopWidth()
        {
            Assert.AreEqual(118.499261936655,
                StatorDimensionsDynamic.CalculateOuterCoilTopWidth(123.61096491098, 5.11170297432515), Delta);
        }

        [TestMethod]
        public void TestOuterCoilBottomWidth()
        {
            Assert.AreEqual(78.9297324161632,
                StatorDimensionsDynamic.CalculateOuterCoilBottomWidth(84.0414353904884, 5.11170297432515), Delta);
        }

        [TestMethod]
        public void TestOuterCoilSide()
        {
            Assert.AreEqual(95.1594625700381,
                StatorDimensionsDynamic.CalculateOuterCoilSideLength(118.499261936655, 78.9297324161632, 24), Delta);
        }

        [TestMethod]
        public void TestOuterCoilCircumference()
        {
            Assert.AreEqual(387.747919492894,
                StatorDimensionsDynamic.CalculateOuterCoilCircumference(118.499261936655, 78.9297324161632,
                    95.1594625700381), Delta);
        }

        [TestMethod]
        public void TestInnerCoilSegmentTopWidth()
        {
            Assert.AreEqual(110.202897000835,
                StatorDimensionsDynamic.CalculateInnerCoilSegmentTopWidth(259.23193369094, 24), Delta);
        }

        [TestMethod]
        public void TestInnerCoilSegmentBottomWidth()
        {
            Assert.AreEqual(97.4495033006334,
                StatorDimensionsDynamic.CalculateInnerCoilSegmentBottomWidth(229.23193369094, 24), Delta);
        }

        [TestMethod]
        public void TestCoilLegWidth()
        {
            var centralCoilAngle = StatorDimensionsDynamic.CalculateCentralCoilAngle(15);
            Assert.AreEqual(32.244622362043, StatorDimensionsDynamic.CalculateCoilLegWidth(31.54, centralCoilAngle), Delta);
        }

        [TestMethod]
        public void TestInnerCoilTopWidth()
        {
            Assert.AreEqual(40.6019493024238,
                StatorDimensionsDynamic.CalculateInnerCoilTopWidth(110.202897000835, 32.244622362043, 5.11170297432515), Delta);
        }

        [TestMethod]
        public void TestInnerCoilBottomWidth()
        {
            Assert.AreEqual(27.8485556022222,
                StatorDimensionsDynamic.CalculateInnerCoilBottomWidth(97.4495033006334, 32.244622362043, 5.11170297432515), Delta);
        }

        [TestMethod]
        public void TestInnerCoilSide()
        {
            Assert.AreEqual(30.6702178459515,
                StatorDimensionsDynamic.CalculateInnerCoilSideLength(40.6019493024238, 27.8485556022222, 24), Delta);
        }

        [TestMethod]
        public void TestInnerCoilCircumference()
        {
            Assert.AreEqual(129.790940596549,
                StatorDimensionsDynamic.CalculateInnerCoilCircumference(40.6019493024238, 27.8485556022222,
                    30.6702178459515), Delta);
        }

        [TestMethod]
        public void TestTopCoilAngle()
        {
            Assert.AreEqual(60, StatorDimensionsDynamic.CalculateTopCoilAngle(60), Delta);
            Assert.AreEqual(70, StatorDimensionsDynamic.CalculateTopCoilAngle(40), Delta);
            Assert.AreEqual(75, StatorDimensionsDynamic.CalculateTopCoilAngle(30), Delta);
            Assert.AreEqual(78, StatorDimensionsDynamic.CalculateTopCoilAngle(24), Delta);
            Assert.AreEqual(80, StatorDimensionsDynamic.CalculateTopCoilAngle(20), Delta);
            Assert.AreEqual(81.4285714285714, StatorDimensionsDynamic.CalculateTopCoilAngle(17.1428571428571), Delta);
        }

        [TestMethod]
        public void TestBottomCoilAngle()
        {
            Assert.AreEqual(120, StatorDimensionsDynamic.CalculateBottomCoilAngle(60), Delta);
            Assert.AreEqual(110, StatorDimensionsDynamic.CalculateBottomCoilAngle(40), Delta);
            Assert.AreEqual(105, StatorDimensionsDynamic.CalculateBottomCoilAngle(30), Delta);
            Assert.AreEqual(102, StatorDimensionsDynamic.CalculateBottomCoilAngle(24), Delta);
            Assert.AreEqual(100, StatorDimensionsDynamic.CalculateBottomCoilAngle(20), Delta);
            Assert.AreEqual(98.5714285714286, StatorDimensionsDynamic.CalculateBottomCoilAngle(17.1428571428571), Delta);
        }

        [TestMethod]
        public void TestInnerCoilTopError()
        {
            Assert.AreEqual(6.17448578267526,
                StatorDimensionsDynamic.CalculateInnerCoilTopCornerAngularError(5, 78), Delta);
        }

        [TestMethod]
        public void TestInnerCoilBottomError()
        {
            Assert.AreEqual(4.04892016597504,
                StatorDimensionsDynamic.CalculateInnerCoilBottomCornerAngularError(5, 102), Delta);
        }

        [TestMethod]
        public void TestInnerCoilTopRoundedCorrection()
        {
            var centralCoilAngle = StatorDimensionsDynamic.CalculateCentralCoilAngle(15);
            var bottomCoilAngle = StatorDimensionsDynamic.CalculateBottomCoilAngle(centralCoilAngle);
            Assert.AreEqual(8.90117918517108, StatorDimensionsDynamic.CalculateInnerCoilTopCornerRoundedCorrection(5, bottomCoilAngle),
                Delta);
        }

        [TestMethod]
        public void TestInnerCoilBottomRoundedCorrection()
        {
            var centralCoilAngle = StatorDimensionsDynamic.CalculateCentralCoilAngle(15);
            var topCoilAngle = StatorDimensionsDynamic.CalculateTopCoilAngle(centralCoilAngle);
            Assert.AreEqual(6.80678408277789, StatorDimensionsDynamic.CalculateInnerCoilBottomCornerRoundedCorrection(5, topCoilAngle),
                Delta);
        }

        [TestMethod]
        public void TestInnerCoilCicumferenceRounded()
        {
            Assert.AreEqual(120.313243337846, StatorDimensionsDynamic.CalculateInnerCoilCicumferenceRounded(129.790940596549, 6.17448578267526, 8.90117918517108, 4.04892016597504, 6.80678408277789),
                Delta);
        }

        [TestMethod]
        public void TestCalculateOuterCoilRadius()
        {
            Assert.AreEqual(37.244622362043, StatorDimensionsDynamic.CalculateOuterCoilRadius(5, 32.244622362043), Delta);
        }

        [TestMethod]
        public void TestCalculateOuterCoilTopCornerAngularError()
        {
            var centralCoilAngle = StatorDimensionsDynamic.CalculateCentralCoilAngle(15);
            var topCoilAngle = StatorDimensionsDynamic.CalculateTopCoilAngle(centralCoilAngle);
            Assert.AreEqual(45.9932782511087, StatorDimensionsDynamic.CalculateOuterCoilTopCornerAngularError(37.244622362043, topCoilAngle), Delta);
        }

        [TestMethod]
        public void TestCalculateOuterCoilBottomCornerAngularError()
        {
            var centralCoilAngle = StatorDimensionsDynamic.CalculateCentralCoilAngle(15);
            var bottomCoilAngle = StatorDimensionsDynamic.CalculateBottomCoilAngle(centralCoilAngle);
            Assert.AreEqual(30.1601005111601, StatorDimensionsDynamic.CalculateOuterCoilBottomCornerAngularError(37.244622362043, bottomCoilAngle), Delta);
        }

        [TestMethod]
        public void TestCalculateOuterCoilTopCornerRoundedCorrection()
        {
            var centralCoilAngle = StatorDimensionsDynamic.CalculateCentralCoilAngle(15);
            var bottomCoilAngle = StatorDimensionsDynamic.CalculateBottomCoilAngle(centralCoilAngle);
            Assert.AreEqual(66.3042114657149, StatorDimensionsDynamic.CalculateOuterCoilTopCornerRoundedCorrection(37.244622362043, bottomCoilAngle), Delta);
        }

        [TestMethod]
        public void TestCalculateOuterCoilBottomCornerRoundedCorrection()
        {
            var centralCoilAngle = StatorDimensionsDynamic.CalculateCentralCoilAngle(15);
            var topCoilAngle = StatorDimensionsDynamic.CalculateTopCoilAngle(centralCoilAngle);
            Assert.AreEqual(50.7032205326055, StatorDimensionsDynamic.CalculateOuterCoilBottomCornerRoundedCorrection(37.244622362043, topCoilAngle), Delta);
        }

        [TestMethod]
        public void TestCalculateOuterCoilCicumferenceRounded()
        {
            var centralCoilAngle = StatorDimensionsDynamic.CalculateCentralCoilAngle(9);
            var topCoilAngle = StatorDimensionsDynamic.CalculateTopCoilAngle(centralCoilAngle);
            Assert.AreEqual(317.14926844046, StatorDimensionsDynamic.CalculateOuterCoilCicumferenceRounded(387.747919492894, 45.9932782511087, 66.3042114657149, 30.1601005111601, 50.7032205326055), Delta);
        }

        [TestMethod]
        public void TestCalculateAverageCoilCircumference()
        {
            Assert.AreEqual(218.731255889153, StatorDimensionsDynamic.CalculateAverageCoilCircumferenceRounded(120.313243337846, 317.14926844046), Delta);
        }


        [TestMethod]
        public void TestCalculateCoilSurfaceRounded()
        {
            Assert.AreEqual(6898.78381074389, StatorDimensionsDynamic.CalculateCoilSurfaceRounded(31.54, 218.731255889153), Delta);
        }

        [TestMethod]
        public void TestCalculateCoilOuterDimensionsAngular()
        {
            var coilOuterDimensionsAngular = StatorDimensionsDynamic.CalculateCoilOuterDimensionsAngular(15, 197.69193369094, 290.77193369094, 5);

            Assert.AreEqual(123.610964910983, coilOuterDimensionsAngular.Item1, Delta);
            Assert.AreEqual(84.0414353904884, coilOuterDimensionsAngular.Item2, Delta);
            Assert.AreEqual(118.499261936655, coilOuterDimensionsAngular.Item3, Delta);
            Assert.AreEqual(78.9297324161632, coilOuterDimensionsAngular.Item4, Delta);
            Assert.AreEqual(95.1594625700381, coilOuterDimensionsAngular.Item5, Delta);
            Assert.AreEqual(387.747919492894, coilOuterDimensionsAngular.Item6, Delta);
        }

        [TestMethod]
        public void TestCalculateCoilInnerDimensionsAngular()
        {
            var coilOuterDimensionsAngular = StatorDimensionsDynamic.CalculateCoilInnerDimensionsAngular(15, 229.231933690942, 259.23193369094, 31.54, 5);

            Assert.AreEqual(110.202897000835, coilOuterDimensionsAngular.Item1, Delta);
            Assert.AreEqual(97.4495033006334, coilOuterDimensionsAngular.Item2, Delta);
            Assert.AreEqual(40.6019493024238, coilOuterDimensionsAngular.Item3, Delta);
            Assert.AreEqual(27.8485556022222, coilOuterDimensionsAngular.Item4, Delta);
            Assert.AreEqual(30.6702178459515, coilOuterDimensionsAngular.Item5, Delta);
            Assert.AreEqual(129.790940596549, coilOuterDimensionsAngular.Item6, Delta);
        }

        [TestMethod]
        public void TestCalculateCoilRoundedVariables()
        {
            var coilOuterDimensionsAngular = StatorDimensionsDynamic.CalculateCoilOuterDimensionsAngular(15, 197.69193369094, 290.77193369094, 5);
            var coilInnerDimensionsAngular = StatorDimensionsDynamic.CalculateCoilInnerDimensionsAngular(15, 229.231933690942, 259.23193369094, 31.54, 5);
            var coilRoundedVariables = StatorDimensionsDynamic.CalculateCoilRoundedVariables(5, 31.54, 24, coilInnerDimensionsAngular, coilOuterDimensionsAngular);

            Assert.AreEqual(120.313243337841, coilRoundedVariables.Item1, Delta);
            Assert.AreEqual(318.484907926288, coilRoundedVariables.Item2, Delta);
            Assert.AreEqual(219.399075632065, coilRoundedVariables.Item3, Delta);
            Assert.AreEqual(6919.84684543532, coilRoundedVariables.Item4, Delta);
        }

        [TestMethod]
        public void TestCalculateCoilStraightDimensions()
        {
            var coilOuterDimensionsAngular = StatorDimensionsDynamic.CalculateCoilOuterDimensionsAngular(15, 197.69193369094, 290.77193369094, 5);
            var coilInnerDimensionsAngular = StatorDimensionsDynamic.CalculateCoilInnerDimensionsAngular(15, 229.231933690942, 259.23193369094, 31.54, 5);
            var coilStraightDimensions = StatorDimensionsDynamic.CalculateCoilStraightDimensions(5, 31.54, 24, coilInnerDimensionsAngular, coilOuterDimensionsAngular);

            Assert.AreEqual(28.252977737073, coilStraightDimensions.Item1, Delta);
            Assert.AreEqual(19.750715270273, coilStraightDimensions.Item2, Delta);
            Assert.AreEqual(20.4468118972986, coilStraightDimensions.Item3, Delta);

            Assert.AreEqual(28.252977737073, coilStraightDimensions.Item4, Delta);
            Assert.AreEqual(19.750715270273, coilStraightDimensions.Item5, Delta);
            Assert.AreEqual(20.4468118972986, coilStraightDimensions.Item6, Delta);
        }
    }
}
