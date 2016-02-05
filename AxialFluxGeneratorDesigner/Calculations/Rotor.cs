using System;
using System.Windows.Input;

namespace AxialFluxGeneratorDesigner.Calculations
{
    /// <summary>
    /// </summary>
    public static class Rotor
    {
        /// <summary>
        /// </summary>
        /// <param name="phaseCount"></param>
        /// <param name="coilPhaseCount"></param>
        /// <returns></returns>
        public static int CalculateCoilCount(int phaseCount, int coilPhaseCount)
        {
            return phaseCount*coilPhaseCount;
        }

        /// <summary>
        ///     This method calculates the amount of magnets.
        /// </summary>
        /// <param name="coilCount">The total amount of coils</param>
        /// <returns>The amount of magnets</returns>
        public static int CalculatePolePairs(int coilCount)
        {
            var poleCount = coilCount*2/0.5/3;
            return (int) poleCount;
        }

        /// <summary>
        /// </summary>
        /// <param name="magnetWidth"></param>
        /// <param name="magnetsDistance"></param>
        /// <returns></returns>
        public static double CalculateMagnetPoleArcPitch(double magnetWidth, double magnetsDistance)
        {
            return magnetWidth/magnetsDistance;
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
        /// <param name="coilCount"></param>
        /// <param name="bottomCoilSegmentLength"></param>
        /// <returns></returns>
        public static double CalculateRotorInnerRadius(int coilCount, double bottomCoilSegmentLength)
        {
            var centralAngle = StatorDimensions.CalculateCentralCoilAngle(coilCount);
            var topAngle = StatorDimensions.CalculateTopCoilAngle(centralAngle);
            return StatorDimensions.CalculateOppositeTan(bottomCoilSegmentLength/2, topAngle);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerRotorRadius"></param>
        /// <param name="coilHeight"></param>
        /// <returns></returns>
        public static double CalculateRotorOuterRadius(double innerRotorRadius, double coilHeight)
        {
            return innerRotorRadius + coilHeight;
        }
    }
}