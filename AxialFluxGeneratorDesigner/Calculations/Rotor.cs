using System;
using System.Windows.Input;

namespace AxialFluxGeneratorDesigner.Calculations
{
    /// <summary>
    /// </summary>
    public static class Rotor
    {
        /// <summary>
        ///     This method calculates the amount of magnets.
        /// </summary>
        /// <param name="coilCount">The total amount of coils</param>
        /// <returns>The amount of magnets</returns>
        public static int CalculateMagnetCount(int coilCount)
        {
            var poleCount = coilCount*2/0.5/3;
            return (int) poleCount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="poleArcToPolePitchRatio"></param>
        /// <param name="magnetWidth"></param>
        /// <returns></returns>
        public static double CalculateMagnetPoleToPolePitch(double poleArcToPolePitchRatio, double magnetWidth)
        {
            return magnetWidth/poleArcToPolePitchRatio;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="magnetPoleToPolePitch"></param>
        /// <param name="magnetWidth"></param>
        /// <returns></returns>
        public static double CalculateBetweenPoleDistance(double magnetPoleToPolePitch, double magnetWidth)
        {
            return magnetPoleToPolePitch - magnetWidth;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="magnetCount"></param>
        /// <returns></returns>
        public static double CalculateMagnetCentralAngle(double magnetCount)
        {
            return 360/magnetCount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="magnetCentralAngle"></param>
        /// <param name="poleArcToPolePitchRatio"></param>
        /// <returns></returns>
        public static double CalculateMagnetSegmentAngle(double magnetCentralAngle, double poleArcToPolePitchRatio)
        {
            return magnetCentralAngle*poleArcToPolePitchRatio;
        }
   
        /// <summary>
        /// 
        /// </summary>
        /// <param name="magnetCentralAngle"></param>
        /// <param name="magnetSegmentAngle"></param>
        /// <returns></returns>
        public static double CalculateBetweenMagnetSegmentAngle(double magnetCentralAngle,  double magnetSegmentAngle)
        {
            return magnetCentralAngle - magnetSegmentAngle;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="magnetWidth"></param>
        /// <param name="magnetSegmentAngle"></param>
        /// <returns></returns>
        public static double CalculateRotorInnerRadius(double magnetWidth, double magnetSegmentAngle)
        {

            return StatorDimensionsStatic.CalculateAdjacentTan(magnetWidth/2, magnetSegmentAngle/2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rotorInnerRadius"></param>
        /// <param name="magnetHeight"></param>
        /// <returns></returns>
        public static double CalculateRotorOuterRadius(double rotorInnerRadius, double magnetHeight)
        {

            return rotorInnerRadius + magnetHeight;
        }

        /// <summary>
        ///     This method calculates the flux density of a magnet at a certain distance (gap).
        /// </summary>
        /// <param name="remanentFluxDensity">The remanent density of the magnet (T)</param>
        /// <param name="coerciveFieldStrength">The coercive field strength of the magnet (A/m)(</param>
        /// <param name="magnetThickness">The magnet thickness (mm)</param>
        /// <param name="gap">Gap between the magnet surface and the stator (mm).</param>
        /// <returns>Magnet flux density (T)</returns>
        public static double CalculateMagnetFluxDensity(double remanentFluxDensity, double coerciveFieldStrength,
            double magnetThickness, double gap)
        {
            const double ksat = 1;
            var statorThickness = Stator.CalculateStatorThickness(magnetThickness, gap);
            const double vacuumPermeability = 0.4*Math.PI/1000000;
            var coilPermeability = 1/vacuumPermeability*(remanentFluxDensity/(coerciveFieldStrength*1000));
            var fluxDensity = remanentFluxDensity/
                              (1 +
                               coilPermeability*
                               (Common.MillimetersToMeters(gap) + 0.5*Common.MillimetersToMeters(statorThickness))/
                               Common.MillimetersToMeters(magnetThickness))*ksat;
            return fluxDensity;
        }

        /// <summary>
        ///     This method calculates the magnet flux density for the magnet area.
        /// </summary>
        /// <param name="fluxDensity">The magnet flux density (T)</param>
        /// <param name="magnetWidth">The magnet width (mm)</param>
        /// <param name="magnetLength">The magnet length (mm)</param>
        /// <returns>Maximum pole flux (T)</returns>
        public static double CalculateMaximumPoleFlux(double fluxDensity, double magnetWidth, double magnetLength)
        {
            return fluxDensity*Common.MillimetersToMeters(magnetWidth)*Common.MillimetersToMeters(magnetLength);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerRotorRadius"></param>
        /// <param name="outerRotorRadius"></param>
        /// <returns></returns>
        public static double CalculateRotorRadiusRatio(double innerRotorRadius, double outerRotorRadius)
        {
            return innerRotorRadius / outerRotorRadius;
        }

        ///// <summary>
        ///// </summary>
        ///// <param name="magnetWidth"></param>
        ///// <param name="magnetsDistance"></param>
        ///// <returns></returns>
        //[Obsolete]
        //public static double CalculateMagnetPoleArcPitch(double magnetWidth, double magnetsDistance)
        //{
        //    return magnetWidth/magnetsDistance;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="statorInnerRadius"></param>
        ///// <param name="coiLegWidth"></param>
        ///// <returns></returns>
        //[Obsolete]
        //public static double CalculateRotorInnerRadius(double statorInnerRadius, double coiLegWidth)
        //{

        //    return statorInnerRadius + coiLegWidth;
        //}
    }
}