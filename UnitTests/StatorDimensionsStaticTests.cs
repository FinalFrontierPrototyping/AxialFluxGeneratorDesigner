//using AxialFluxGeneratorDesigner.Calculations;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace UnitTests
//{
//    [TestClass]
//    public class StatorDimensionsStaticTests
//    {
//        private const double Delta = 0.00000000001;

//        [TestMethod]
//        public void TestCentralCoilAngle()
//        {
//            Assert.AreEqual(60, StatorDimensionsStatic.CalculateCentralCoilAngle(6), Delta);
//            Assert.AreEqual(40, StatorDimensionsStatic.CalculateCentralCoilAngle(9), Delta);
//            Assert.AreEqual(30, StatorDimensionsStatic.CalculateCentralCoilAngle(12), Delta);
//            Assert.AreEqual(24, StatorDimensionsStatic.CalculateCentralCoilAngle(15), Delta);
//            Assert.AreEqual(20, StatorDimensionsStatic.CalculateCentralCoilAngle(18), Delta);
//            Assert.AreEqual(17.1428571428571, StatorDimensionsStatic.CalculateCentralCoilAngle(21), Delta);
//        }

//        [TestMethod]
//        public void TestTopCoilAngle()
//        {
//            Assert.AreEqual(60, StatorDimensionsStatic.CalculateTopCoilAngle(60), Delta);
//            Assert.AreEqual(70, StatorDimensionsStatic.CalculateTopCoilAngle(40), Delta);
//            Assert.AreEqual(75, StatorDimensionsStatic.CalculateTopCoilAngle(30), Delta);
//            Assert.AreEqual(78, StatorDimensionsStatic.CalculateTopCoilAngle(24), Delta);
//            Assert.AreEqual(80, StatorDimensionsStatic.CalculateTopCoilAngle(20), Delta);
//            Assert.AreEqual(81.4285714285714, StatorDimensionsStatic.CalculateTopCoilAngle(17.1428571428571), Delta);
//        }

//        [TestMethod]
//        public void TestBottomCoilAngle()
//        {
//            Assert.AreEqual(120, StatorDimensionsStatic.CalculateBottomCoilAngle(60), Delta);
//            Assert.AreEqual(110, StatorDimensionsStatic.CalculateBottomCoilAngle(40), Delta);
//            Assert.AreEqual(105, StatorDimensionsStatic.CalculateBottomCoilAngle(30), Delta);
//            Assert.AreEqual(102, StatorDimensionsStatic.CalculateBottomCoilAngle(24), Delta);
//            Assert.AreEqual(100, StatorDimensionsStatic.CalculateBottomCoilAngle(20), Delta);
//            Assert.AreEqual(98.5714285714286, StatorDimensionsStatic.CalculateBottomCoilAngle(17.1428571428571), Delta);
//        }

//        [TestMethod]
//        public void TestCoilLegWidth()
//        {
//            var centralCoilAngle = StatorDimensionsStatic.CalculateCentralCoilAngle(9);
//            var topCoilAngle = StatorDimensionsStatic.CalculateTopCoilAngle(centralCoilAngle);
//            Assert.AreEqual(31.9253331742774, StatorDimensionsStatic.CalculateCoilLegWidth(30, topCoilAngle), Delta);
//        }

//        [TestMethod]
//        public void TestBetweenCoilWidth()
//        {
//            var centralCoilAngle = StatorDimensionsStatic.CalculateCentralCoilAngle(9);
//            var topCoilAngle = StatorDimensionsStatic.CalculateTopCoilAngle(centralCoilAngle);
//            Assert.AreEqual(10.6417777247591, StatorDimensionsStatic.CalculateBetweenCoilWidth(10, topCoilAngle), Delta);
//        }

//        [TestMethod]
//        public void TestOuterCoilSegmentBottomWidth()
//        {
//            Assert.AreEqual(74.49244407331396,
//                StatorDimensionsStatic.CalculateOuterCoilSegmentBottom(31.9253331742774, 10.6417777247591), Delta);
//        }

//        [TestMethod]
//        public void TestOuterCoilBottomWidth()
//        {
//            Assert.AreEqual(63.8506663485548, StatorDimensionsStatic.CalculateOuterCoilBottomWidth(31.9253331742774), Delta);
//        }

//        [TestMethod]
//        public void TestStatorInnerRadius()
//        {
//            var centralCoilAngle = StatorDimensionsStatic.CalculateCentralCoilAngle(9);
//            var topCoilAngle = StatorDimensionsStatic.CalculateTopCoilAngle(centralCoilAngle);
//            Assert.AreEqual(102.333154005708, StatorDimensionsStatic.CalculateStatorInnerRadius(74.49244407331396, topCoilAngle), Delta);
//        }

//        [TestMethod]
//        public void TestStatorOuterRadius()
//        {
//            Assert.AreEqual(192.333154005708, StatorDimensionsStatic.CalculateStatorOuterRadius(102.333154005708, 30, 30, 100),
//                Delta);
//        }

//        [TestMethod]
//        public void TestOuterCoilTop()
//        {
//            var centralCoilAngle = StatorDimensionsStatic.CalculateCentralCoilAngle(9);
//            var topCoilAngle = StatorDimensionsStatic.CalculateTopCoilAngle(centralCoilAngle);
//            Assert.AreEqual(129.365308516471, StatorDimensionsStatic.CalculateOuterCoilTop(topCoilAngle, 90, 63.8506663485548), Delta);
//        }

//        [TestMethod]
//        public void TestOuterCoilside()
//        {
//            var centralCoilAngle = StatorDimensionsStatic.CalculateCentralCoilAngle(9);
//            var topCoilAngle = StatorDimensionsStatic.CalculateTopCoilAngle(centralCoilAngle);
//            Assert.AreEqual(95.7759995228318,
//                StatorDimensionsStatic.CalculateOuterCoilside(90, topCoilAngle), Delta);
//        }

//        [TestMethod]
//        public void TestOuterCoilCicumferenceAngular()
//        {
//            Assert.AreEqual(384.767973910689,
//                StatorDimensionsStatic.CalculateOuterCoilCicumferenceAngular(129.365308516471, 63.8506663485548,
//                    95.7759995228318), Delta);
//        }

//        [TestMethod]
//        public void TestInnerCoilBottom()
//        {
//            var centralCoilAngle = StatorDimensionsStatic.CalculateCentralCoilAngle(9);
//            var topCoilAngle = StatorDimensionsStatic.CalculateTopCoilAngle(centralCoilAngle);
//            Assert.AreEqual(21.8382140559721, StatorDimensionsStatic.CalculateInnerCoilBottom(30, topCoilAngle), Delta);
//        }

//        [TestMethod]
//        public void TestInnerCoilTop()
//        {
//            var centralCoilAngle = StatorDimensionsStatic.CalculateCentralCoilAngle(9);
//            var topCoilAngle = StatorDimensionsStatic.CalculateTopCoilAngle(centralCoilAngle);
//            Assert.AreEqual(43.6764281119443, StatorDimensionsStatic.CalculateInnerCoilTop(30, topCoilAngle, 30, 100),
//                Delta);
//        }

//        [TestMethod]
//        public void TestInnerCoilside()
//        {
//            var centralCoilAngle = StatorDimensionsStatic.CalculateCentralCoilAngle(9);
//            var topCoilAngle = StatorDimensionsStatic.CalculateTopCoilAngle(centralCoilAngle);
//            Assert.AreEqual(31.9253331742774,
//                StatorDimensionsStatic.CalculateInnerCoilside(30, topCoilAngle), Delta);
//        }

//        [TestMethod]
//        public void TestInnerCoilCicumferenceAngular()
//        {
//            Assert.AreEqual(129.365308516471,
//                StatorDimensionsStatic.CalculateInnerCoilCicumferenceAngular(43.6764281119443, 21.83821405597218,
//                    31.9253331742774), Delta);
//        }

//        [TestMethod]
//        public void TestInnerCoilTopAngularError()
//        {
//            var centralCoilAngle = StatorDimensionsStatic.CalculateCentralCoilAngle(9);
//            var topCoilAngle = StatorDimensionsStatic.CalculateTopCoilAngle(centralCoilAngle);
//            Assert.AreEqual(5.71259202696846, StatorDimensionsStatic.CalculateInnerCoilTopCornerAngularError(2, topCoilAngle),
//                Delta);
//        }

//        [TestMethod]
//        public void TestInnerCoilTopRoundedCorrection()
//        {
//            var centralCoilAngle = StatorDimensionsStatic.CalculateCentralCoilAngle(9);
//            var bottomCoilAngle = StatorDimensionsStatic.CalculateBottomCoilAngle(centralCoilAngle);
//            Assert.AreEqual(3.83972435438753, StatorDimensionsStatic.CalculateInnerCoilTopCornerRoundedCorrection(2, bottomCoilAngle),
//                Delta);
//        }

//        [TestMethod]
//        public void TestInnerCoilBottomAngularError()
//        {
//            var centralCoilAngle = StatorDimensionsStatic.CalculateCentralCoilAngle(9);
//            var bottomCoilAngle = StatorDimensionsStatic.CalculateBottomCoilAngle(centralCoilAngle);
//            Assert.AreEqual(2.80083015283884, StatorDimensionsStatic.CalculateInnerCoilBottomCornerAngularError(2, bottomCoilAngle),
//                Delta);
//        }

//        [TestMethod]
//        public void TestInnerCoilBottomRoundedCorrection()
//        {
//            var centralCoilAngle = StatorDimensionsStatic.CalculateCentralCoilAngle(9);
//            var topCoilAngle = StatorDimensionsStatic.CalculateTopCoilAngle(centralCoilAngle);
//            Assert.AreEqual(2.44346095279206, StatorDimensionsStatic.CalculateInnerCoilBottomCornerRoundedCorrection(2, topCoilAngle),
//                Delta);
//        }

//        [TestMethod]
//        public void TestInnerCoilCicumferenceRounded()
//        {
//            Assert.AreEqual(124.904834771216, StatorDimensionsStatic.CalculateInnerCoilCicumferenceRounded(129.365308516471, 5.71259202696846, 3.83972435438753, 2.80083015283884, 2.44346095279206),
//                Delta);
//        }

//        [TestMethod]
//        public void TestCalculateOuterCoilRadius()
//        {
//            Assert.AreEqual(32, StatorDimensionsStatic.CalculateOuterCoilRadius(2, 30), Delta);
//        }

//        [TestMethod]
//        public void TestCalculateOuterCoilTopCornerAngularError()
//        {
//            var centralCoilAngle = StatorDimensionsStatic.CalculateCentralCoilAngle(9);
//            var topCoilAngle = StatorDimensionsStatic.CalculateTopCoilAngle(centralCoilAngle);
//            Assert.AreEqual(91.4014724314953, StatorDimensionsStatic.CalculateOuterCoilTopCornerAngularError(32, topCoilAngle), Delta);
//        }

//        [TestMethod]
//        public void TestCalculateOuterCoilBottomCornerAngularError()
//        {
//            var centralCoilAngle = StatorDimensionsStatic.CalculateCentralCoilAngle(9);
//            var bottomCoilAngle = StatorDimensionsStatic.CalculateBottomCoilAngle(centralCoilAngle);
//            Assert.AreEqual(44.8132824454214, StatorDimensionsStatic.CalculateOuterCoilBottomCornerAngularError(32, bottomCoilAngle), Delta);
//        }

//        [TestMethod]
//        public void TestCalculateOuterCoilTopCornerRoundedCorrection()
//        {
//            var centralCoilAngle = StatorDimensionsStatic.CalculateCentralCoilAngle(9);
//            var bottomCoilAngle = StatorDimensionsStatic.CalculateBottomCoilAngle(centralCoilAngle);
//            Assert.AreEqual(61.4355896702004, StatorDimensionsStatic.CalculateOuterCoilTopCornerRoundedCorrection(32, bottomCoilAngle), Delta);
//        }

//        [TestMethod]
//        public void TestCalculateOuterCoilBottomCornerRoundedCorrection()
//        {
//            var centralCoilAngle = StatorDimensionsStatic.CalculateCentralCoilAngle(9);
//            var topCoilAngle = StatorDimensionsStatic.CalculateTopCoilAngle(centralCoilAngle);
//            Assert.AreEqual(39.095375244673, StatorDimensionsStatic.CalculateOuterCoilBottomCornerRoundedCorrection(32, topCoilAngle), Delta);
//        }

//        [TestMethod]
//        public void TestCalculateOuterCoilCicumferenceRounded()
//        {
//            var centralCoilAngle = StatorDimensionsStatic.CalculateCentralCoilAngle(9);
//            var topCoilAngle = StatorDimensionsStatic.CalculateTopCoilAngle(centralCoilAngle);
//            Assert.AreEqual(313.400393986602, StatorDimensionsStatic.CalculateOuterCoilCicumferenceRounded(384.767973910689, 91.4014724314953, 61.4355896702004, 44.8132824454214, 39.095375244673), Delta);
//        }

//        [TestMethod]
//        public void TestCalculateAverageCoilCircumference()
//        {
//            Assert.AreEqual(219.152614378909, StatorDimensionsStatic.CalculateAverageCoilCircumferenceRounded(124.904834771216, 313.400393986602), Delta);
//        }


//        [TestMethod]
//        public void TestCalculateCoilSurfaceRounded()
//        {
//            Assert.AreEqual(6574.57843136727, StatorDimensionsStatic.CalculateCoilSurfaceRounded(30, 219.152614378909), Delta);
//        }

//        [TestMethod]
//        public void TestCalculateInnerOuterTopRounded()
//        {
//            Assert.AreEqual(37.9638360849757, StatorDimensionsStatic.CalculateInnerOuterTopRounded(129.365308516471, 91.4014724314953), Delta);
//        }

//        [TestMethod]
//        public void TestCalculateInnerOuterBottomRounded()
//        {
//            Assert.AreEqual(19.0373839031334, StatorDimensionsStatic.CalculateInnerOuterBottomRounded(63.8506663485548, 44.8132824454214), Delta);
//        }

//        [TestMethod]
//        public void TestCalculateInnerOuterSideRounded()
//        {
//            Assert.AreEqual(27.6686220843734, StatorDimensionsStatic.CalculateInnerOuterSideRounded(95.7759995228318, 91.4014724314953, 44.8132824454214), Delta);
//        }

//        [TestMethod]
//        public void TestCalculateStatorDimensions()
//        {
//            var statorDimensions = StatorDimensionsStatic.CalculateStatorDimensions(9, 30, 30, 10, 2);
//            Assert.AreEqual(124.904834771216, statorDimensions.Item1, Delta);
//            Assert.AreEqual(313.400393986603, statorDimensions.Item2, Delta);
//            Assert.AreEqual(219.152614378909, statorDimensions.Item3, Delta);
//            Assert.AreEqual(6574.57843136729, statorDimensions.Item4, Delta);
//            Assert.AreEqual(102.333154005708, statorDimensions.Item5, Delta);
//            Assert.AreEqual(192.333154005708, statorDimensions.Item6, Delta);
//        }
//    }
//}