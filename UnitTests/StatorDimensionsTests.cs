using AxialFluxGeneratorDesigner.Calculations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class StatorDimensionsTests
    {
        private const double Delta = 0.00000000001;

        [TestMethod]
        public void TestCentralCoilAngle()
        {
            Assert.AreEqual(60, StatorDimensions.CalculateCentralCoilAngle(6), Delta);
            Assert.AreEqual(40, StatorDimensions.CalculateCentralCoilAngle(9), Delta);
            Assert.AreEqual(30, StatorDimensions.CalculateCentralCoilAngle(12), Delta);
            Assert.AreEqual(24, StatorDimensions.CalculateCentralCoilAngle(15), Delta);
            Assert.AreEqual(20, StatorDimensions.CalculateCentralCoilAngle(18), Delta);
            Assert.AreEqual(17.1428571428571, StatorDimensions.CalculateCentralCoilAngle(21), Delta);
        }

        [TestMethod]
        public void TestTopCoilAngle()
        {
            Assert.AreEqual(60, StatorDimensions.CalculateTopCoilAngle(60), Delta);
            Assert.AreEqual(70, StatorDimensions.CalculateTopCoilAngle(40), Delta);
            Assert.AreEqual(75, StatorDimensions.CalculateTopCoilAngle(30), Delta);
            Assert.AreEqual(78, StatorDimensions.CalculateTopCoilAngle(24), Delta);
            Assert.AreEqual(80, StatorDimensions.CalculateTopCoilAngle(20), Delta);
            Assert.AreEqual(81.4285714285714, StatorDimensions.CalculateTopCoilAngle(17.1428571428571), Delta);
        }

        [TestMethod]
        public void TestBottomCoilAngle()
        {
            Assert.AreEqual(120, StatorDimensions.CalculateBottomCoilAngle(60), Delta);
            Assert.AreEqual(110, StatorDimensions.CalculateBottomCoilAngle(40), Delta);
            Assert.AreEqual(105, StatorDimensions.CalculateBottomCoilAngle(30), Delta);
            Assert.AreEqual(102, StatorDimensions.CalculateBottomCoilAngle(24), Delta);
            Assert.AreEqual(100, StatorDimensions.CalculateBottomCoilAngle(20), Delta);
            Assert.AreEqual(98.5714285714286, StatorDimensions.CalculateBottomCoilAngle(17.1428571428571), Delta);
        }

        [TestMethod]
        public void TestCoilLegWidth()
        {
            var centralCoilAngle = StatorDimensions.CalculateCentralCoilAngle(9);
            var topCoilAngle = StatorDimensions.CalculateTopCoilAngle(centralCoilAngle);
            Assert.AreEqual(31.9253331742774, StatorDimensions.CalculateCoilLegWidth(30, topCoilAngle), Delta);
        }

        [TestMethod]
        public void TestBetweenCoilWidth()
        {
            var centralCoilAngle = StatorDimensions.CalculateCentralCoilAngle(9);
            var topCoilAngle = StatorDimensions.CalculateTopCoilAngle(centralCoilAngle);
            Assert.AreEqual(10.6417777247591, StatorDimensions.CalculateBetweenCoilWidth(10, topCoilAngle), Delta);
        }

        [TestMethod]
        public void TestOuterCoilSegmentBottomWidth()
        {
            Assert.AreEqual(74.49244407331396,
                StatorDimensions.CalculateOuterCoilSegmentBottom(31.9253331742774, 10.6417777247591), Delta);
        }

        [TestMethod]
        public void TestOuterCoilBottomWidth()
        {
            Assert.AreEqual(63.8506663485548, StatorDimensions.CalculateOuterCoilBottomWidth(31.9253331742774), Delta);
        }

        [TestMethod]
        public void TestStatorInnerRadius()
        {
            var centralCoilAngle = StatorDimensions.CalculateCentralCoilAngle(9);
            var topCoilAngle = StatorDimensions.CalculateTopCoilAngle(centralCoilAngle);
            Assert.AreEqual(102.333154005708, StatorDimensions.CalculateStatorInnerRadius(74.49244407331396, topCoilAngle), Delta);
        }

        [TestMethod]
        public void TestStatorOuterRadius()
        {
            Assert.AreEqual(192.333154005708, StatorDimensions.CalculateStatorOuterRadius(102.333154005708, 30, 30, 100),
                Delta);
        }

        [TestMethod]
        public void TestOuterCoilTop()
        {
            var centralCoilAngle = StatorDimensions.CalculateCentralCoilAngle(9);
            var topCoilAngle = StatorDimensions.CalculateTopCoilAngle(centralCoilAngle);
            Assert.AreEqual(129.365308516471, StatorDimensions.CalculateOuterCoilTop(topCoilAngle, 90, 63.8506663485548), Delta);
        }

        [TestMethod]
        public void TestOuterCoilside()
        {
            var centralCoilAngle = StatorDimensions.CalculateCentralCoilAngle(9);
            var topCoilAngle = StatorDimensions.CalculateTopCoilAngle(centralCoilAngle);
            Assert.AreEqual(95.7759995228318,
                StatorDimensions.CalculateOuterCoilside(90, topCoilAngle), Delta);
        }

        [TestMethod]
        public void TestOuterCoilCicumferenceAngular()
        {
            Assert.AreEqual(384.767973910689,
                StatorDimensions.CalculateOuterCoilCicumferenceAngular(129.365308516471, 63.8506663485548,
                    95.7759995228318), Delta);
        }

        [TestMethod]
        public void TestInnerCoilBottom()
        {
            var centralCoilAngle = StatorDimensions.CalculateCentralCoilAngle(9);
            var topCoilAngle = StatorDimensions.CalculateTopCoilAngle(centralCoilAngle);
            Assert.AreEqual(21.8382140559721, StatorDimensions.CalculateInnerCoilBottom(30, topCoilAngle), Delta);
        }

        [TestMethod]
        public void TestInnerCoilTop()
        {
            var centralCoilAngle = StatorDimensions.CalculateCentralCoilAngle(9);
            var topCoilAngle = StatorDimensions.CalculateTopCoilAngle(centralCoilAngle);
            Assert.AreEqual(43.6764281119443, StatorDimensions.CalculateInnerCoilTop(30, topCoilAngle, 30, 100),
                Delta);
        }

        [TestMethod]
        public void TestInnerCoilside()
        {
            var centralCoilAngle = StatorDimensions.CalculateCentralCoilAngle(9);
            var topCoilAngle = StatorDimensions.CalculateTopCoilAngle(centralCoilAngle);
            Assert.AreEqual(31.9253331742774,
                StatorDimensions.CalculateInnerCoilside(30, topCoilAngle), Delta);
        }

        [TestMethod]
        public void TestInnerCoilCicumferenceAngular()
        {
            Assert.AreEqual(129.365308516471,
                StatorDimensions.CalculateInnerCoilCicumferenceAngular(43.6764281119443, 21.83821405597218,
                    31.9253331742774), Delta);
        }

        [TestMethod]
        public void TestInnerCoilTopAngularError()
        {
            var centralCoilAngle = StatorDimensions.CalculateCentralCoilAngle(9);
            var topCoilAngle = StatorDimensions.CalculateTopCoilAngle(centralCoilAngle);
            Assert.AreEqual(5.71259202696846, StatorDimensions.CalculateInnerCoilTopCornerAngularError(2, topCoilAngle),
                Delta);
        }

        [TestMethod]
        public void TestInnerCoilTopRoundedCorrection()
        {
            var centralCoilAngle = StatorDimensions.CalculateCentralCoilAngle(9);
            var bottomCoilAngle = StatorDimensions.CalculateBottomCoilAngle(centralCoilAngle);
            Assert.AreEqual(3.83972435438753, StatorDimensions.CalculateInnerCoilTopCornerRoundedCorrection(2, bottomCoilAngle),
                Delta);
        }

        [TestMethod]
        public void TestInnerCoilBottomAngularError()
        {
            var centralCoilAngle = StatorDimensions.CalculateCentralCoilAngle(9);
            var bottomCoilAngle = StatorDimensions.CalculateBottomCoilAngle(centralCoilAngle);
            Assert.AreEqual(2.80083015283884, StatorDimensions.CalculateInnerCoilBottomCornerAngularError(2, bottomCoilAngle),
                Delta);
        }

        [TestMethod]
        public void TestInnerCoilBottomRoundedCorrection()
        {
            var centralCoilAngle = StatorDimensions.CalculateCentralCoilAngle(9);
            var topCoilAngle = StatorDimensions.CalculateTopCoilAngle(centralCoilAngle);
            Assert.AreEqual(2.44346095279206, StatorDimensions.CalculateInnerCoilBottomCornerRoundedCorrection(2, topCoilAngle),
                Delta);
        }

        [TestMethod]
        public void TestInnerCoilCicumferenceRounded()
        {
            Assert.AreEqual(124.904834771216, StatorDimensions.CalculateInnerCoilCicumferenceRounded(129.365308516471, 5.71259202696846, 3.83972435438753, 2.80083015283884, 2.44346095279206),
                Delta);
        }

        [TestMethod]
        public void TestCalculateOuterCoilRadius()
        {
            Assert.AreEqual(32, StatorDimensions.CalculateOuterCoilRadius(2, 30), Delta);
        }

        [TestMethod]
        public void TestCalculateOuterCoilTopCornerAngularError()
        {
            var centralCoilAngle = StatorDimensions.CalculateCentralCoilAngle(9);
            var topCoilAngle = StatorDimensions.CalculateTopCoilAngle(centralCoilAngle);
            Assert.AreEqual(91.4014724314953, StatorDimensions.CalculateOuterCoilTopCornerAngularError(32, topCoilAngle), Delta);
        }

        [TestMethod]
        public void TestCalculateOuterCoilBottomCornerAngularError()
        {
            var centralCoilAngle = StatorDimensions.CalculateCentralCoilAngle(9);
            var bottomCoilAngle = StatorDimensions.CalculateBottomCoilAngle(centralCoilAngle);
            Assert.AreEqual(44.8132824454214, StatorDimensions.CalculateOuterCoilBottomCornerAngularError(32, bottomCoilAngle), Delta);
        }

        [TestMethod]
        public void TestCalculateOuterCoilTopCornerRoundedCorrection()
        {
            var centralCoilAngle = StatorDimensions.CalculateCentralCoilAngle(9);
            var bottomCoilAngle = StatorDimensions.CalculateBottomCoilAngle(centralCoilAngle);
            Assert.AreEqual(61.4355896702004, StatorDimensions.CalculateOuterCoilTopCornerRoundedCorrection(32, bottomCoilAngle), Delta);
        }

        [TestMethod]
        public void TestCalculateOuterCoilBottomCornerRoundedCorrection()
        {
            var centralCoilAngle = StatorDimensions.CalculateCentralCoilAngle(9);
            var topCoilAngle = StatorDimensions.CalculateTopCoilAngle(centralCoilAngle);
            Assert.AreEqual(39.095375244673, StatorDimensions.CalculateOuterCoilBottomCornerRoundedCorrection(32, topCoilAngle), Delta);
        }

        [TestMethod]
        public void TestCalculateOuterCoilCicumferenceRounded()
        {
            var centralCoilAngle = StatorDimensions.CalculateCentralCoilAngle(9);
            var topCoilAngle = StatorDimensions.CalculateTopCoilAngle(centralCoilAngle);
            Assert.AreEqual(313.400393986602, StatorDimensions.CalculateOuterCoilCicumferenceRounded(384.767973910689, 91.4014724314953, 61.4355896702004, 44.8132824454214, 39.095375244673), Delta);
        }

        [TestMethod]
        public void TestCalculateAverageCoilCircumference()
        {
            Assert.AreEqual(219.152614378909, StatorDimensions.CalculateAverageCoilCircumferenceRounded(124.904834771216, 313.400393986602), Delta);
        }


        [TestMethod]
        public void TestCalculateCoilSurfaceRounded()
        {
            Assert.AreEqual(6574.57843136727, StatorDimensions.CalculateCoilSurfaceRounded(30, 219.152614378909), Delta);
        }

        [TestMethod]
        public void TestCalculateInnerOuterTopRounded()
        {
            Assert.AreEqual(37.9638360849757, StatorDimensions.CalculateInnerOuterTopRounded(129.365308516471, 91.4014724314953), Delta);
        }

        [TestMethod]
        public void TestCalculateInnerOuterBottomRounded()
        {
            Assert.AreEqual(19.0373839031334, StatorDimensions.CalculateInnerOuterBottomRounded(63.8506663485548, 44.8132824454214), Delta);
        }

        [TestMethod]
        public void TestCalculateInnerOuterSideRounded()
        {
            Assert.AreEqual(27.6686220843734, StatorDimensions.CalculateInnerOuterSideRounded(95.7759995228318, 91.4014724314953, 44.8132824454214), Delta);
        }

        [TestMethod]
        public void TestCalculateStatorDimensions()
        {
            var statorDimensions = StatorDimensions.CalculateStatorDimensions(9, 30, 30, 10, 2);
            Assert.AreEqual(124.904834771216, statorDimensions.Item1, Delta);
            Assert.AreEqual(313.400393986603, statorDimensions.Item2, Delta);
            Assert.AreEqual(219.152614378909, statorDimensions.Item3, Delta);
            Assert.AreEqual(6574.57843136729, statorDimensions.Item4, Delta);
            Assert.AreEqual(102.333154005708, statorDimensions.Item5, Delta);
            Assert.AreEqual(192.333154005708, statorDimensions.Item6, Delta);
        }

        [TestMethod]
        public void TestCalculateStatorDimensions1()
        {
            var statorDimensions = StatorDimensions.CalculateStatorDimensions(9, 30, 37.37, 10, 5);
            Assert.AreEqual(128.9439666595, statorDimensions.Item1, Delta);
            Assert.AreEqual(363.746601588801, statorDimensions.Item2, Delta);
            Assert.AreEqual(246.345284124151, statorDimensions.Item3, Delta);
            Assert.AreEqual(6574.57843136729, statorDimensions.Item4, Delta);
            Assert.AreEqual(102.333154005708, statorDimensions.Item5, Delta);
            Assert.AreEqual(192.333154005708, statorDimensions.Item6, Delta);
        }

    }
}