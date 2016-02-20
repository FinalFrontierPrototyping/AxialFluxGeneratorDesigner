using System;

namespace AxialFluxGeneratorDesigner.Calculations
{

    /// <summary>
    /// 
    /// </summary>
    public static class StatorDimensionsDynamic
    {

        #region Trigonometric functions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oppositeSide"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        private static double CalculateHypotenuseSin(double oppositeSide, double angle)
        {
            return oppositeSide/Math.Sin(Common.DegToRad(angle));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adjacentSide"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        private static double CalculateHypotenuseCos(double adjacentSide, double angle)
        {
            return adjacentSide/Math.Cos(Common.DegToRad(angle));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oppositeSide"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        private static double CalculateAdjacentTan(double oppositeSide, double angle)
        {
            return oppositeSide/Math.Tan(Common.DegToRad(angle));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adjacentSide"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        private static double CalculateOppositeTan(double adjacentSide, double angle)
        {
            return adjacentSide*Math.Tan(Common.DegToRad(angle));
        }

        #endregion

        #region Calculation of various angles

        /// <summary>
        ///     This method calculated the coil angle (Deg).
        /// </summary>
        /// <param name="coilCount">The total amount of coils.</param>
        /// <returns>The angle for each coil (deg).</returns>
        public static double CalculateCentralCoilAngle(int coilCount)
        {
            if (coilCount == 0)
            {
                return 0;
            }
            // ReSharper disable once PossibleLossOfFraction
            return 360.0/coilCount;
        }

        /// <summary>
        /// </summary>
        /// <param name="centralCoilAngle"></param>
        /// <returns></returns>
        public static double CalculateTopCoilAngle(double centralCoilAngle)
        {
            // ReSharper disable once PossibleLossOfFraction
            return (180 - centralCoilAngle)/2;
        }

        /// <summary>
        /// </summary>
        /// <param name="centralCoilAngle"></param>
        /// <returns></returns>
        public static double CalculateBottomCoilAngle(double centralCoilAngle)
        {
            // ReSharper disable once PossibleLossOfFraction
            return 180 - (180 - centralCoilAngle)/2;
        }

        #endregion

        /// <summary>
        ///  This method calculates the between coil width parallel to the coil bottom.
        /// </summary>
        /// <param name="betweenCoilWidth"></param>
        /// <param name="centralCoilAngle"></param>
        /// <returns></returns>
        public static double CalculateBetweenCoilWidth(double betweenCoilWidth, double centralCoilAngle)
        {
            return CalculateHypotenuseCos(betweenCoilWidth, centralCoilAngle/2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coilCentralAngle"></param>
        /// <param name="statorOuterRadius"></param>
        /// <returns></returns>
        public static double CalculateOuterCoilSegmentTopWidth(double coilCentralAngle, double statorOuterRadius)
        {
            return CalculateOppositeTan(statorOuterRadius, coilCentralAngle/2)*2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coilCentralAngle"></param>
        /// <param name="statorInnerRadius"></param>
        /// <returns></returns>
        public static double CalculateOuterCoilSegmentBottomWidth(double coilCentralAngle, double statorInnerRadius)
        {
            return CalculateOppositeTan(statorInnerRadius, coilCentralAngle/2)*2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outerCoilSegmentTopWidth"></param>
        /// <param name="betweenCoilWidth"></param>
        /// <returns></returns>
        public static double CalculateOuterCoilTopWidth(double outerCoilSegmentTopWidth, double betweenCoilWidth)
        {
            return outerCoilSegmentTopWidth - betweenCoilWidth;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outerCoilSegmentBottomWidth"></param>
        /// <param name="betweenCoilWidth"></param>
        /// <returns></returns>
        public static double CalculateOuterCoilBottomWidth(double outerCoilSegmentBottomWidth, double betweenCoilWidth)
        {
            return outerCoilSegmentBottomWidth - betweenCoilWidth;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outerCoilTopWidth"></param>
        /// <param name="outerCoilBottomWidth"></param>
        /// <param name="coilCentralAngle"></param>
        /// <returns></returns>
        public static double CalculateOuterCoilSideLength(double outerCoilTopWidth, double outerCoilBottomWidth,
            double coilCentralAngle)
        {
            return CalculateHypotenuseSin((outerCoilTopWidth - outerCoilBottomWidth)/2, coilCentralAngle/2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outerCoilTopWidth"></param>
        /// <param name="outerCoilBottomWidth"></param>
        /// <param name="outerCoilSideLength"></param>
        /// <returns></returns>
        public static double CalculateOuterCoilCircumference(double outerCoilTopWidth, double outerCoilBottomWidth,
            double outerCoilSideLength)
        {
            return outerCoilTopWidth + outerCoilBottomWidth + (2*outerCoilSideLength);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rotorOuterRadius"></param>
        /// <param name="coilCenterAngle"></param>
        /// <returns></returns>
        public static double CalculateInnerCoilSegmentTopWidth(double rotorOuterRadius, double coilCenterAngle)
        {
            return CalculateOppositeTan(rotorOuterRadius, coilCenterAngle/2)*2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rotorInnerRadius"></param>
        /// <param name="coilCenterAngle"></param>
        /// <returns></returns>
        public static double CalculateInnerCoilSegmentBottomWidth(double rotorInnerRadius, double coilCenterAngle)
        {
            return CalculateOppositeTan(rotorInnerRadius, coilCenterAngle/2)*2;
        }

        /// <summary>
        /// This method calculates the coil leg width parallel to the coil bottom.
        /// </summary>
        /// <param name="coilLegWidth"></param>
        /// <param name="coilCenterAngle"></param>
        /// <returns></returns>
        public static double CalculateCoilLegWidth(double coilLegWidth, double coilCenterAngle)
        {
            return CalculateHypotenuseCos(coilLegWidth, coilCenterAngle/2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerCoilSegmentTopWidth"></param>
        /// <param name="coilLegWidth"></param>
        /// <param name="betweenCoilLegWidth"></param>
        /// <returns></returns>
        public static double CalculateInnerCoilTopWidth(double innerCoilSegmentTopWidth, double coilLegWidth,
            double betweenCoilLegWidth)
        {
            return innerCoilSegmentTopWidth - (2*coilLegWidth) - betweenCoilLegWidth;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerCoilSegmentBottomWidth"></param>
        /// <param name="coilLegWidth"></param>
        /// <param name="betweenCoilLegWidth"></param>
        /// <returns></returns>
        public static double CalculateInnerCoilBottomWidth(double innerCoilSegmentBottomWidth, double coilLegWidth,
            double betweenCoilLegWidth)
        {
            return innerCoilSegmentBottomWidth - (2*coilLegWidth) - betweenCoilLegWidth;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerCoilTopWidth"></param>
        /// <param name="innerCoilBottomWidth"></param>
        /// <param name="coilCentralAngle"></param>
        /// <returns></returns>
        public static double CalculateInnerCoilSideLength(double innerCoilTopWidth, double innerCoilBottomWidth,
            double coilCentralAngle)
        {
            return CalculateHypotenuseSin((innerCoilTopWidth - innerCoilBottomWidth)/2, coilCentralAngle/2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerCoilTopWidth"></param>
        /// <param name="innerCoilBottomWidth"></param>
        /// <param name="innerCoilSideLength"></param>
        /// <returns></returns>
        public static double CalculateInnerCoilCircumference(double innerCoilTopWidth, double innerCoilBottomWidth,
            double innerCoilSideLength)
        {
            return innerCoilTopWidth + innerCoilBottomWidth + (2*innerCoilSideLength);
        }

        #region Calculation of the inner coil angular error

        /// <summary>
        /// This method calculates the angular error in the inner coil top. 
        /// </summary>
        /// <param name="innerRadius"></param>
        /// <param name="topCoilAngle"></param>
        /// <returns></returns>
        public static double CalculateInnerCoilTopCornerAngularError(double innerRadius, double topCoilAngle)
        {
            return CalculateAdjacentTan(innerRadius, topCoilAngle/2);
        }

        /// <summary>
        /// This method calculates the angular error in the inner coil bottom. 
        /// </summary>
        /// <param name="innerRadius"></param>
        /// <param name="bottomCoilAngle"></param>
        /// <returns></returns>
        public static double CalculateInnerCoilBottomCornerAngularError(double innerRadius, double bottomCoilAngle)
        {
            return CalculateAdjacentTan(innerRadius, bottomCoilAngle/2);
        }

        #endregion

        #region Calculation of the inner coil rounded corner correction

        /// <summary>
        /// This method calculates the inner coil rounded correction for the top segment.
        /// </summary>
        /// <param name="innerRadius"></param>
        /// <param name="bottomCoilAngle"></param>
        /// <returns></returns>
        public static double CalculateInnerCoilTopCornerRoundedCorrection(double innerRadius, double bottomCoilAngle)
        {
            return ((Math.PI*innerRadius*2)/360)*bottomCoilAngle;
        }

        /// <summary>
        /// This method calculates the inner coil rounded correction for the bottom segment.
        /// </summary>
        /// <param name="innerRadius"></param>
        /// <param name="topCoilAngle"></param>
        /// <returns></returns>
        public static double CalculateInnerCoilBottomCornerRoundedCorrection(double innerRadius, double topCoilAngle)
        {
            return ((Math.PI*innerRadius*2)/360)*topCoilAngle;
        }

        #endregion

        #region Calculation of the inner coil circumference (rounded)

        /// <summary>
        /// This method calculates the circumference based on rounded corners.
        /// </summary>
        /// <param name="innerCoilCircumferenceAngular"></param>
        /// <param name="innerCoilTopCornerAngularError"></param>
        /// <param name="innerCoilTopCornerRounded"></param>
        /// <param name="innerCoilBottomCornerAngularError"></param>
        /// <param name="innerCoilBottomCornerRounded"></param>
        /// <returns></returns>
        public static double CalculateInnerCoilCicumferenceRounded(double innerCoilCircumferenceAngular,
            double innerCoilTopCornerAngularError, double innerCoilTopCornerRounded,
            double innerCoilBottomCornerAngularError,
            double innerCoilBottomCornerRounded)
        {
            double angularError = (4*innerCoilTopCornerAngularError) + (4*innerCoilBottomCornerAngularError);
            double roundedCorrection = (2*innerCoilTopCornerRounded) + (2*innerCoilBottomCornerRounded);
            return innerCoilCircumferenceAngular - angularError + roundedCorrection;
        }

        #endregion

        #region Calculation of the outer coil radius

        /// <summary>
        /// This method calculates the outer coil radius.
        /// </summary>
        /// <param name="innerCoilRadius"></param>
        /// <param name="coilLegWidth"></param>
        /// <returns></returns>
        public static double CalculateOuterCoilRadius(double innerCoilRadius, double coilLegWidth)
        {
            return innerCoilRadius + coilLegWidth;
        }

        #endregion

        #region Calculation of the outer coil angular error

        /// <summary>
        /// This method calculates the angular error in the inner coil top. 
        /// </summary>
        /// <param name="outerRadius"></param>
        /// <param name="topCoilAngle"></param>
        /// <returns></returns>
        public static double CalculateOuterCoilTopCornerAngularError(double outerRadius, double topCoilAngle)
        {
            return CalculateAdjacentTan(outerRadius, topCoilAngle/2);
        }

        /// <summary>
        /// This method calculates the angular error in the inner coil bottom. 
        /// </summary>
        /// <param name="outerRadius"></param>
        /// <param name="bottomCoilAngle"></param>
        /// <returns></returns>
        public static double CalculateOuterCoilBottomCornerAngularError(double outerRadius, double bottomCoilAngle)
        {
            return CalculateAdjacentTan(outerRadius, bottomCoilAngle/2);
        }

        #endregion

        #region Calculation of the outer coil rounded corner correction

        /// <summary>
        /// This method calculates the inner coil rounded correction for the top segment.
        /// </summary>
        /// <param name="outerRadius"></param>
        /// <param name="bottomCoilAngle"></param>
        /// <returns></returns>
        public static double CalculateOuterCoilTopCornerRoundedCorrection(double outerRadius, double bottomCoilAngle)
        {
            return ((Math.PI*outerRadius*2)/360)*bottomCoilAngle;
        }

        /// <summary>
        /// This method calculates the inner coil rounded correction for the bottom segment.
        /// </summary>
        /// <param name="outerRadius"></param>
        /// <param name="topCoilAngle"></param>
        /// <returns></returns>
        public static double CalculateOuterCoilBottomCornerRoundedCorrection(double outerRadius, double topCoilAngle)
        {
            return ((Math.PI*outerRadius*2)/360)*topCoilAngle;
        }

        #endregion

        #region Calculation of the outer coil circumference (rounded)

        /// <summary>
        /// This method calculates the circumference based on rounded corners.
        /// </summary>
        /// <param name="outerCoilCircumferenceAngular"></param>
        /// <param name="outerCoilTopCornerAngularError"></param>
        /// <param name="outerCoilTopCornerRounded"></param>
        /// <param name="outerCoilBottomCornerAngularError"></param>
        /// <param name="outerCoilBottomCornerBounded"></param>
        /// <returns></returns>
        public static double CalculateOuterCoilCicumferenceRounded(double outerCoilCircumferenceAngular,
            double outerCoilTopCornerAngularError, double outerCoilTopCornerRounded,
            double outerCoilBottomCornerAngularError,
            double outerCoilBottomCornerBounded)
        {
            double angularError = (4*outerCoilTopCornerAngularError) + (4*outerCoilBottomCornerAngularError);
            double roundedCorrection = (2*outerCoilTopCornerRounded) + (2*outerCoilBottomCornerBounded);
            return outerCoilCircumferenceAngular - angularError + roundedCorrection;
        }

        #endregion

        #region Calculation of average coil length

        /// <summary>
        /// This method calculates the average coil circumference.
        /// </summary>
        /// <param name="innerCoilCircumference"></param>
        /// <param name="outerCoilCircumference"></param>
        /// <returns></returns>
        public static double CalculateAverageCoilCircumferenceRounded(double innerCoilCircumference,
            double outerCoilCircumference)
        {
            return (innerCoilCircumference + outerCoilCircumference)/2;
        }

        #endregion

        #region Calculation of coil surface

        /// <summary>
        /// This method calculates the surface of a coil side.
        /// </summary>
        /// <param name="coilLegWidth"> The coil leg width (mm)</param>
        /// <param name="averageCoilCircumference"> The average coil circumference (mm)</param>
        /// <returns>The surface of a single coil side (cm2)</returns>
        public static double CalculateCoilSurfaceRounded(double coilLegWidth, double averageCoilCircumference)
        {
            return coilLegWidth*averageCoilCircumference;
        }

        #endregion

        #region Calculation of coil segment lengths

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outerCoilTopAngular"></param>
        /// <param name="outerCoilTopRounded"></param>
        /// <returns></returns>
        public static double CalculateInnerOuterTopRounded(double outerCoilTopAngular, double outerCoilTopRounded)
        {
            return outerCoilTopAngular - (2*outerCoilTopRounded);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outerCoilBottomAngular"></param>
        /// <param name="outerCoilBottomRounded"></param>
        /// <returns></returns>
        public static double CalculateInnerOuterBottomRounded(double outerCoilBottomAngular,
            double outerCoilBottomRounded)
        {
            return outerCoilBottomAngular - (2*outerCoilBottomRounded);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outerCoilSideAngular"></param>
        /// <param name="outerCoilTopCornerAngularError"></param>
        /// <param name="innerCoilTopCornerAngularError"></param>
        /// <returns></returns>
        public static double CalculateInnerOuterSideRounded(double outerCoilSideAngular,
            double outerCoilTopCornerAngularError, double innerCoilTopCornerAngularError)
        {
            return outerCoilSideAngular - (outerCoilTopCornerAngularError/2) - (innerCoilTopCornerAngularError/2);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coilCount"></param>
        /// <param name="statorInnerRadius"></param>
        /// <param name="statorOuterRadius"></param>
        /// <param name="betweenCoilDistance"></param>
        /// <returns></returns>
        public static Tuple<double, double, double, double, double, double> CalculateCoilOuterDimensionsAngular(
            int coilCount, double statorInnerRadius, double statorOuterRadius, double betweenCoilDistance)
        {
            var coilCenterAngle = CalculateCentralCoilAngle(coilCount);
            var outerCoilSegmentTopWidth = CalculateOuterCoilSegmentTopWidth(coilCenterAngle, statorOuterRadius);
            var outerCoilSegmentBottomWidth = CalculateOuterCoilSegmentBottomWidth(coilCenterAngle, statorInnerRadius);
            var betweenCoilDistanceLevel = CalculateBetweenCoilWidth(betweenCoilDistance, coilCenterAngle);
            var outerCoilTopWidth = CalculateOuterCoilTopWidth(outerCoilSegmentTopWidth, betweenCoilDistanceLevel);
            var outerCoilBottomWidth = CalculateOuterCoilBottomWidth(outerCoilSegmentBottomWidth,
                betweenCoilDistanceLevel);
            var outerCoilSide = CalculateOuterCoilSideLength(outerCoilTopWidth, outerCoilBottomWidth, coilCenterAngle);

            var outerCoilCircumference = outerCoilTopWidth + outerCoilBottomWidth + (2*outerCoilSide);

            var coilAngularOuterDimensions =
                new Tuple<double, double, double, double, double, double>(outerCoilSegmentTopWidth,
                    outerCoilSegmentBottomWidth, outerCoilTopWidth, outerCoilBottomWidth, outerCoilSide,
                    outerCoilCircumference);
            return coilAngularOuterDimensions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coilCount"></param>
        /// <param name="rotorInnerRadius"></param>
        /// <param name="rotorOuterRadius"></param>
        /// <param name="coilLegWidth"></param>
        /// <param name="betweenCoilDistance"></param>
        /// <returns></returns>
        public static Tuple<double, double, double, double, double, double> CalculateCoilInnerDimensionsAngular(
            int coilCount, double rotorInnerRadius, double rotorOuterRadius, double coilLegWidth,
            double betweenCoilDistance)
        {
            var coilCenterAngle = CalculateCentralCoilAngle(coilCount);
            var betweenCoilDistanceLevel = CalculateBetweenCoilWidth(betweenCoilDistance, coilCenterAngle);
            var coilLegWidthLevel = CalculateCoilLegWidth(coilLegWidth, coilCenterAngle);

            var innerCoilSegmentTopWidth = CalculateInnerCoilSegmentTopWidth(rotorOuterRadius, coilCenterAngle);
            var innerCoilSegmentBottomWidth = CalculateInnerCoilSegmentBottomWidth(rotorInnerRadius, coilCenterAngle);

            var innerCoilTopWidth = CalculateInnerCoilTopWidth(innerCoilSegmentTopWidth, coilLegWidthLevel,
                betweenCoilDistanceLevel);
            var innerCoilBottomWidth = CalculateInnerCoilBottomWidth(innerCoilSegmentBottomWidth, coilLegWidthLevel,
                betweenCoilDistanceLevel);
            var innerCoilSide = CalculateInnerCoilSideLength(innerCoilTopWidth, innerCoilBottomWidth, coilCenterAngle);
            var innerCoilCircumference = innerCoilTopWidth + innerCoilBottomWidth + (2*innerCoilSide);


            var coilAngularInnerDimensions =
                new Tuple<double, double, double, double, double, double>(innerCoilSegmentTopWidth,
                    innerCoilSegmentBottomWidth, innerCoilTopWidth, innerCoilBottomWidth, innerCoilSide,
                    innerCoilCircumference);
            return coilAngularInnerDimensions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coilCentralAngle"></param>
        /// <param name="coilAngularOuterDimensions"></param>
        /// <param name="coilAngularInnerDimensions"></param>
        /// <param name="innerCoilRadius"></param>
        /// <param name="coilLegWidth"></param>
        /// <returns></returns>
        public static Tuple<double, double, double, double> CalculateCoilRoundedVariables(double innerCoilRadius,
            double coilLegWidth, double coilCentralAngle,
            Tuple<double, double, double, double, double, double> coilAngularInnerDimensions,
            Tuple<double, double, double, double, double, double> coilAngularOuterDimensions)
        {

            var coilTopAngle = StatorDimensionsDynamic.CalculateTopCoilAngle(coilCentralAngle);
            var coilBottomAngle = StatorDimensionsDynamic.CalculateBottomCoilAngle(coilCentralAngle);

            //var coilLegWidthLevel = StatorDimensionsDynamic.CalculateCoilLegWidth(coilLegWidth, coilCentralAngle);
            var coilOuterRadius = innerCoilRadius + coilLegWidth;

            // Inner coil
            var innerCoilTopError = CalculateInnerCoilTopCornerAngularError(innerCoilRadius, coilTopAngle);
            var innerCoilBottomError = CalculateInnerCoilBottomCornerAngularError(innerCoilRadius, coilBottomAngle);
            var innerCoilTopCorrection = CalculateInnerCoilTopCornerRoundedCorrection(innerCoilRadius, coilBottomAngle);
            var innerCoilBottomCorrection = CalculateInnerCoilBottomCornerRoundedCorrection(innerCoilRadius,
                coilTopAngle);
            var innerCoilCircumference = CalculateInnerCoilCicumferenceRounded(coilAngularInnerDimensions.Item6,
                innerCoilTopError, innerCoilTopCorrection, innerCoilBottomError, innerCoilBottomCorrection);

            //Outer coil
            var outerCoilTopError = CalculateOuterCoilTopCornerAngularError(coilOuterRadius, coilTopAngle);
            var outerCoilBottomError = CalculateOuterCoilBottomCornerAngularError(coilOuterRadius, coilBottomAngle);
            var outerCoilTopCorrection = CalculateOuterCoilTopCornerRoundedCorrection(coilOuterRadius, coilBottomAngle);
            var outerCoilBottomCorrection = CalculateOuterCoilBottomCornerRoundedCorrection(coilOuterRadius,
                coilTopAngle);
            var outerCoilCircumference = CalculateOuterCoilCicumferenceRounded(coilAngularOuterDimensions.Item6,
                outerCoilTopError, outerCoilTopCorrection, outerCoilBottomError, outerCoilBottomCorrection);

            var coilAverageTurnLength = CalculateAverageCoilCircumferenceRounded(innerCoilCircumference,
                outerCoilCircumference);

            var coilSideSurface = CalculateCoilSurfaceRounded(coilLegWidth, coilAverageTurnLength);

            var coilRoundedVariables = new Tuple<double, double, double, double>(innerCoilCircumference,
                outerCoilCircumference, coilAverageTurnLength, coilSideSurface);
            return coilRoundedVariables;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerCoilRadius"></param>
        /// <param name="coilLegWidth"></param>
        /// <param name="coilCentralAngle"></param>
        /// <param name="coilAngularInnerDimensions"></param>
        /// <param name="coilAngularOuterDimensions"></param>
        /// <returns></returns>
        public static Tuple<double, double, double, double, double, double> CalculateCoilStraightDimensions(double innerCoilRadius,
            double coilLegWidth, double coilCentralAngle, Tuple<double, double, double, double, double, double> coilAngularInnerDimensions,
            Tuple<double, double, double, double, double, double> coilAngularOuterDimensions)
        {
            var coilTopAngle = StatorDimensionsDynamic.CalculateTopCoilAngle(coilCentralAngle);
            var coilBottomAngle = StatorDimensionsDynamic.CalculateBottomCoilAngle(coilCentralAngle);

            var coilLegWidthLevel = StatorDimensionsDynamic.CalculateCoilLegWidth(coilLegWidth, coilCentralAngle);
            var coilOuterRadius = innerCoilRadius + coilLegWidth;

            // Inner coil
            var innerCoilTopError = CalculateInnerCoilTopCornerAngularError(innerCoilRadius, coilTopAngle);
            var innerCoilBottomError = CalculateInnerCoilBottomCornerAngularError(innerCoilRadius, coilBottomAngle);

            var innerTopSection = coilAngularInnerDimensions.Item3 - (2*innerCoilTopError);
            var innerCoilBottomSection = coilAngularInnerDimensions.Item4 - (2 * innerCoilBottomError);
            var innerCoilSideSection = coilAngularInnerDimensions.Item5 - innerCoilTopError - innerCoilBottomError;

            //Outer coil
            var outerCoilTopError = CalculateOuterCoilTopCornerAngularError(coilOuterRadius, coilTopAngle);
            var outerCoilBottomError = CalculateOuterCoilBottomCornerAngularError(coilOuterRadius, coilBottomAngle);

            var outerTopSection = coilAngularOuterDimensions.Item3 - (2 * outerCoilTopError);
            var outerCoilBottomSection = coilAngularOuterDimensions.Item4 - (2 * outerCoilBottomError);
            var outerCoilSideSection = coilAngularOuterDimensions.Item5 - outerCoilTopError - outerCoilBottomError;

            var coilStraightDimensions = new Tuple<double, double, double, double, double, double>(innerTopSection, innerCoilBottomSection, innerCoilSideSection, outerTopSection, outerCoilBottomSection, outerCoilSideSection);
            return coilStraightDimensions;
        }
    }
}
