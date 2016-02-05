using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AxialFluxGeneratorDesigner.Calculations
{
    /// <summary>
    /// </summary>
    public static class StatorDimensions
    {
        #region Trigonometric functions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oppositeSide"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static double CalculateHypotenuseSin(double oppositeSide, double angle)
        {
            return oppositeSide/Math.Sin(Common.DegToRad(angle));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oppositeSide"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static double CalculateAdjacentTan(double oppositeSide, double angle)
        {
            return oppositeSide/Math.Tan(Common.DegToRad(angle));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adjacentSide"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static double CalculateOppositeTan(double adjacentSide, double angle)
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
            return 360.0 / coilCount;
        }

        /// <summary>
        /// </summary>
        /// <param name="centralCoilAngle"></param>
        /// <returns></returns>
        public static double CalculateTopCoilAngle(double centralCoilAngle)
        {
            // ReSharper disable once PossibleLossOfFraction
            return (180 - centralCoilAngle) / 2;
        }

        /// <summary>
        /// </summary>
        /// <param name="centralCoilAngle"></param>
        /// <returns></returns>
        public static double CalculateBottomCoilAngle(double centralCoilAngle)
        {
            // ReSharper disable once PossibleLossOfFraction
            return 180 - (180 - centralCoilAngle) / 2;
        }

        #endregion

        #region Calculation of outer coil bottom (angular)

        /// <summary>
        /// This method calculates the coil leg width parallel to the coil bottom.
        /// </summary>
        /// <param name="coilLegWidth"></param>
        /// <param name="topCoilAngle"></param>
        /// <returns></returns>
        public static double CalculateCoilLegWidth(double coilLegWidth, double topCoilAngle)
        {
            return CalculateHypotenuseSin(coilLegWidth,topCoilAngle);
        }

        /// <summary>
        ///  This method calculates the between coil width parallel to the coil bottom.
        /// </summary>
        /// <param name="betweenCoilWidth"></param>
        /// <param name="topCoilAngle"></param>
        /// <returns></returns>
        public static double CalculateBetweenCoilWidth(double betweenCoilWidth, double topCoilAngle)
        {
            return CalculateHypotenuseSin(betweenCoilWidth, topCoilAngle);
        }

        /// <summary>
        /// This method calculates the coil segment width (coil bottom + between coil space)
        /// </summary>
        /// <param name="coilLegWidth"></param>
        /// <param name="betweenCoilWidth"></param>
        /// <returns></returns>
        public static double CalculateOuterCoilSegmentBottom(double coilLegWidth, double betweenCoilWidth)
        {
            return coilLegWidth * 2 + betweenCoilWidth;
        }

        /// <summary>
        /// This method calculates the outer coil bottom width
        /// </summary>
        /// <param name="coilLegWidth"></param>
        /// <returns></returns>
        public static double CalculateOuterCoilBottomWidth(double coilLegWidth)
        {
            return coilLegWidth * 2;
        }

        #endregion

        #region Calculation of outer coil top (angular)

        /// <summary>
        /// This method calculates the width of the outer coil top.
        /// </summary>
        /// <param name="coilTopAngle"></param>
        /// <param name="outerCoilHeight"></param>
        /// <param name="outerCoilBottom"></param>
        /// <returns></returns>
        public static double CalculateOuterCoilTop(double coilTopAngle, double outerCoilHeight, double outerCoilBottom)
        {
            double topCoilSegment = CalculateAdjacentTan(outerCoilHeight, coilTopAngle);
            return (2*topCoilSegment) + outerCoilBottom;
        }

        #endregion

        #region Calculation of the outer coil side (angular)

        /// <summary>
        /// This method calculates the outer coil side length
        /// </summary>
        /// <param name="outerCoilHeight"></param>
        /// <param name="topCoilAngle"></param>
        /// <returns></returns>
        public static double CalculateOuterCoilside(double outerCoilHeight, double topCoilAngle)
        {
            return CalculateHypotenuseSin(outerCoilHeight, topCoilAngle);
        }

        #endregion

        #region Calculation of the outer coil circumference (angular)

        /// <summary>
        /// This method calculates the angular circumference of the outer coil.
        /// </summary>
        /// <param name="outerCoilTopCorrected"></param>
        /// <param name="outerCoilBottomCorrected"></param>
        /// <param name="outerCoilside"></param>
        /// <returns></returns>
        public static double CalculateOuterCoilCicumferenceAngular(double outerCoilTopCorrected,
            double outerCoilBottomCorrected, double outerCoilside)
        {
            return outerCoilTopCorrected + outerCoilBottomCorrected + 2 * outerCoilside;
        }

        #endregion

        #region Calculation of stator inner innerRadius

        /// <summary>
        /// This method calculates the stator inner radius.
        /// </summary>
        /// <param name="outerCoilBottomSegmentWidth"></param>
        /// <param name="topCoilAngle"></param>
        /// <returns></returns>
        public static double CalculateStatorInnerRadius(double outerCoilBottomSegmentWidth, double topCoilAngle)
        {
            return CalculateOppositeTan(outerCoilBottomSegmentWidth/2, topCoilAngle);
        }

        #endregion

        #region Calculation of stator outer outerRadius

        /// <summary>
        /// This method calculated the outer radius of the stator.
        /// </summary>
        /// <param name="statorInnerRadius"></param>
        /// <param name="coilLegWidth"></param>
        /// <param name="magnetHeight"></param>
        /// <param name="magnetHeightPercentage"></param>
        /// <returns></returns>
        public static double CalculateStatorOuterRadius(double statorInnerRadius, double coilLegWidth,
            double magnetHeight, int magnetHeightPercentage)
        {
            return statorInnerRadius + coilLegWidth + coilLegWidth + magnetHeight/100*magnetHeightPercentage;
        }

        #endregion

        #region Calculation of the inner coil bottom (angular)

        /// <summary>
        /// This method calculates the inner coil bottom width.
        /// </summary>
        /// <param name="coilLegWidth"></param>
        /// <param name="topCoilAngle"></param>
        /// <returns></returns>
        public static double CalculateInnerCoilBottom(double coilLegWidth, double topCoilAngle)
        {
            return CalculateAdjacentTan(coilLegWidth, topCoilAngle) * 2;
        }

        #endregion

        #region Calculation of the inner coil top (angular)

        /// <summary>
        /// This method calculates the inner coil top width.
        /// </summary>
        /// <param name="coilLegWidth"></param>
        /// <param name="topCoilAngle"></param>
        /// <param name="magnetHeight"></param>
        /// <param name="magnetHeightPercentage"></param>
        /// <returns></returns>
        public static double CalculateInnerCoilTop(double coilLegWidth, double topCoilAngle, double magnetHeight,
            int magnetHeightPercentage)
        {
            var length = coilLegWidth + magnetHeight / 100 * magnetHeightPercentage;
            return CalculateAdjacentTan(length, topCoilAngle) * 2;
        }

        #endregion

        #region Calculation of the inner coil side (angular)

        /// <summary>
        /// This method calculates the side length of the inner coil.
        /// </summary>
        /// <param name="innerCoilHeight"></param>
        /// <param name="topCoilAngle"></param>
        /// <returns></returns>
        public static double CalculateInnerCoilside(double innerCoilHeight, double topCoilAngle)
        {
            return CalculateHypotenuseSin(innerCoilHeight, topCoilAngle);
        }

        #endregion

        #region Calculation of the inner coil circumference (angular)

        /// <summary>
        /// </summary>
        /// <param name="innerCoilTop"></param>
        /// <param name="innerCoilBottom"></param>
        /// <param name="innerCoilside"></param>
        /// <returns></returns>
        public static double CalculateInnerCoilCicumferenceAngular(double innerCoilTop, double innerCoilBottom,
            double innerCoilside)
        {
            return innerCoilTop + innerCoilBottom + 2 * innerCoilside;
        }

        #endregion

        #region Calculation of the inner coil angular error

        /// <summary>
        /// This method calculates the angular error in the inner coil top. 
        /// </summary>
        /// <param name="innerRadius"></param>
        /// <param name="topCoilAngle"></param>
        /// <returns></returns>
        public static double CalculateInnerCoilTopCornerAngularError(double innerRadius, double topCoilAngle)
        {
            return CalculateAdjacentTan(innerRadius, topCoilAngle/2)*2;
        }

        /// <summary>
        /// This method calculates the angular error in the inner coil bottom. 
        /// </summary>
        /// <param name="innerRadius"></param>
        /// <param name="bottomCoilAngle"></param>
        /// <returns></returns>
        public static double CalculateInnerCoilBottomCornerAngularError(double innerRadius, double bottomCoilAngle)
        {
            return CalculateAdjacentTan(innerRadius, bottomCoilAngle/2)*2;
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
            double innerCoilTopCornerAngularError, double innerCoilTopCornerRounded, double innerCoilBottomCornerAngularError,
            double innerCoilBottomCornerRounded)
        {
            double angularError = (2*innerCoilTopCornerAngularError) + (2*innerCoilBottomCornerAngularError);
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
            return CalculateAdjacentTan(outerRadius, topCoilAngle / 2) * 2;
        }

        /// <summary>
        /// This method calculates the angular error in the inner coil bottom. 
        /// </summary>
        /// <param name="outerRadius"></param>
        /// <param name="bottomCoilAngle"></param>
        /// <returns></returns>
        public static double CalculateOuterCoilBottomCornerAngularError(double outerRadius, double bottomCoilAngle)
        {
            return CalculateAdjacentTan(outerRadius , bottomCoilAngle / 2) * 2;
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
            return ((Math.PI *outerRadius* 2) / 360) * bottomCoilAngle;
        }

        /// <summary>
        /// This method calculates the inner coil rounded correction for the bottom segment.
        /// </summary>
        /// <param name="outerRadius"></param>
        /// <param name="topCoilAngle"></param>
        /// <returns></returns>
        public static double CalculateOuterCoilBottomCornerRoundedCorrection(double outerRadius, double topCoilAngle)
        {
            return ((Math.PI * outerRadius* 2) / 360) * topCoilAngle;
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
            double outerCoilTopCornerAngularError, double outerCoilTopCornerRounded, double outerCoilBottomCornerAngularError,
            double outerCoilBottomCornerBounded)
        {
            double angularError = (2 * outerCoilTopCornerAngularError) + (2 * outerCoilBottomCornerAngularError);
            double roundedCorrection = (2 * outerCoilTopCornerRounded) + (2 * outerCoilBottomCornerBounded);
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
            return outerCoilTopAngular - outerCoilTopRounded;
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
            return outerCoilBottomAngular - outerCoilBottomRounded;
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
        /// This method calculates the coil dimensions.
        /// </summary>
        /// <param name="coilCount"></param>
        /// <param name="magnetHeight"></param>
        /// <param name="coilLegWidth"></param>
        /// <param name="betweenCoilDistance"></param>
        /// <param name="innerCoilRadius"></param>
        /// <returns></returns>
        public static Tuple<double, double, double, double, double, double> CalculateStatorDimensions(int coilCount, double magnetHeight, double coilLegWidth, double betweenCoilDistance, double innerCoilRadius)
        {
            var centralAngle = CalculateCentralCoilAngle(coilCount);
            var bottomAngle = CalculateBottomCoilAngle(centralAngle);
            var topAngle = CalculateTopCoilAngle(centralAngle);

            var coillegwidthLevel = CalculateCoilLegWidth(coilLegWidth, topAngle);
            var betweenCoilDistanceLevel = CalculateBetweenCoilWidth(betweenCoilDistance, topAngle);
            var coilBottomSegment = CalculateOuterCoilSegmentBottom(coillegwidthLevel, betweenCoilDistanceLevel);

            var outerCoilBottom = CalculateOuterCoilBottomWidth(coillegwidthLevel);
            var outerCoilHeight = (coilLegWidth)*2 + magnetHeight;
            var outerCoilTop = CalculateOuterCoilTop(topAngle, outerCoilHeight, outerCoilBottom);
            var outerCoilSide = CalculateOuterCoilside(outerCoilHeight, topAngle);
            var outerCoilCircumference = CalculateOuterCoilCicumferenceAngular(outerCoilTop, outerCoilBottom,
                outerCoilSide);

            var innerCoilBottom = CalculateInnerCoilBottom(coilLegWidth, topAngle);
            var innerCoilHeight = magnetHeight;
            var innerCoilTop = CalculateInnerCoilTop(coilLegWidth, topAngle, innerCoilHeight, 100);
            var innerCoilSide = CalculateInnerCoilside(innerCoilHeight, topAngle);
            var innerCoilCircumference = CalculateInnerCoilCicumferenceAngular(innerCoilTop, innerCoilBottom,
                innerCoilSide);

            var innerCoilTopError = CalculateInnerCoilTopCornerAngularError(innerCoilRadius, topAngle);
            var innerCoilTopCorrection = CalculateInnerCoilTopCornerRoundedCorrection(innerCoilRadius, bottomAngle);
            var innerCoilBottomError = CalculateInnerCoilBottomCornerAngularError(innerCoilRadius, bottomAngle);
            var innerCoilBottomCorrection = CalculateInnerCoilBottomCornerRoundedCorrection(innerCoilRadius, topAngle);
            var innerCoilCircumferenceCorrected = CalculateInnerCoilCicumferenceRounded(innerCoilCircumference,
                innerCoilTopError, innerCoilTopCorrection, innerCoilBottomError, innerCoilBottomCorrection);

            var outerCoilRadius = CalculateOuterCoilRadius(innerCoilRadius, coilLegWidth);
            var outerCoilTopError = CalculateOuterCoilTopCornerAngularError(outerCoilRadius, topAngle);
            var outerCoilTopCorrection = CalculateOuterCoilTopCornerRoundedCorrection(outerCoilRadius, bottomAngle);
            var outerCoilBottomError = CalculateOuterCoilBottomCornerAngularError(outerCoilRadius, bottomAngle);
            var outerCoilBottomCorrection = CalculateOuterCoilBottomCornerRoundedCorrection(outerCoilRadius, topAngle);
            var outerCoilCircumferenceCorrected = CalculateOuterCoilCicumferenceRounded(outerCoilCircumference, outerCoilTopError, outerCoilTopCorrection, outerCoilBottomError, outerCoilBottomCorrection);

            //var innerOuterCoilTopRounded;
            //var innerOuterCoilBottomRounded;
            //var innerOuterCoilSideRounded;

            var averageCoilCircumference = CalculateAverageCoilCircumferenceRounded(innerCoilCircumferenceCorrected, outerCoilCircumferenceCorrected);
            var coilSurface = CalculateCoilSurfaceRounded(coilLegWidth, averageCoilCircumference);

            var statorInnerRadius = CalculateStatorInnerRadius(coilBottomSegment,topAngle);
            var statorOuterRadius = CalculateStatorOuterRadius(statorInnerRadius, coilLegWidth, magnetHeight, 100);

            var statorDimensions = new Tuple<double, double, double, double, double, double>(innerCoilCircumferenceCorrected, outerCoilCircumferenceCorrected, averageCoilCircumference, coilSurface, statorInnerRadius, statorOuterRadius);

            return statorDimensions;
        }
    }
}