using System;

namespace AxialFluxGeneratorDesigner.Calculations
{
    /// <summary>
    /// </summary>
    public static class Stator
    {
        /// <summary>
        ///     This method calculates the phase voltage for a 3 phase Y-configuration from the provided DC voltage.
        ///     Vdc = ((3*SQRT(2))/PI) * Vrms
        ///     Vdc = 1.35 * Vrms
        /// </summary>
        /// <param name="dcVoltage">DC voltage (V)</param>
        /// <param name="diodeVoltageDrop">Drop voltage losses in various power cables (V)</param>
        /// <returns>Phase voltage (rms) (V)</returns>
        public static double CalculatePhaseVoltage(double dcVoltage, double diodeVoltageDrop)
        {
            //return (dc_voltage + 1.4) / (Math.Sqrt(3) * (Math.Sqrt(2) * (3 / Math.PI)));
            return (dcVoltage + diodeVoltageDrop)/(Math.Sqrt(3)*Math.Sqrt(2));
        }

        /// <summary>
        ///     This method calculates the corrected (for voltage drop due to power lines and diode rectifier) DC voltage.
        /// </summary>
        /// <param name="phaseVoltage">The phase voltage (V)</param>
        /// <param name="diodeVoltageDrop">Drop voltage losses in various power cables (V)</param>
        /// <param name="rectifierVoltageDrop"></param>
        /// <returns></returns>
        public static double CalculateDcVoltage(double phaseVoltage, double diodeVoltageDrop,
            double rectifierVoltageDrop)
        {
            return phaseVoltage*(Math.Sqrt(3)*Math.Sqrt(2)) - rectifierVoltageDrop - diodeVoltageDrop;
        }

        /// <summary>
        ///     This method calculates the inductance of a coil (mH).
        ///     http://coil32.net/multi-layer-coil.html
        /// </summary>
        /// <param name="windingCount">The winding count of the coil</param>
        /// <param name="coilDiameter">The diameter of the coil (mm)</param>
        /// <param name="coilThickness">Thickness of the coil (mm)</param>
        /// <returns>The inductance of the coil (mH)</returns>
        public static double CalculateCoilInductance(int windingCount, double coilDiameter, double coilThickness)
        {
            const int permeabilirtCoreMaterial = 1;
            const int relativePermeability = 1;

            return Math.Pow(windingCount, 2)*permeabilirtCoreMaterial*relativePermeability*Math.PI*
                   Math.Pow(Common.MillimetersToMeters(coilDiameter/2), 2)/Common.MillimetersToMeters(coilThickness);
        }

        /// <summary>
        ///     This method calculates the resistance of a copper wire.
        /// </summary>
        /// <param name="wireLength">Length of the wire (m)</param>
        /// <param name="wireDiameter"> Diameter of the wire (mm)</param>
        /// <returns>The resistance of the copper wire (Ohm)</returns>
        public static double CalculateWireResistance(double wireLength, double wireDiameter)
        {
            const double copperResistivity = 0.0000000168;
            var wireRadius = wireDiameter/2;
            var wireResistance = copperResistivity*wireLength/
                                 (Math.PI*Math.Pow(Common.MillimetersToMeters(wireRadius), 2));

            if (double.IsNaN(wireResistance) || double.IsInfinity(wireResistance))
            {
                wireResistance = 0.0;
            }

            return wireResistance;
        }

        /// <summary>
        ///     This method calculates the voltage drop across a three phase power line.
        /// </summary>
        /// <param name="wireLength">The length of a phase wire (m)</param>
        /// <param name="wireDiameter">The wire diameter (mm)</param>
        /// <param name="wireCurrent">The current flowing through the phase wire (A)</param>
        /// <param name="phaseType">The phase type. 1 for AC or DC. 3 for three phase.</param>
        /// <returns>The voltage drop across a single phase wire (V).</returns>
        public static double VoltageDrop(double wireLength, double wireDiameter, double wireCurrent, int phaseType)
        {
            var wireResistance = CalculateWireResistance(wireLength, wireDiameter);
            double voltageDrop = 1000;

            if (phaseType == 1)
            {
                voltageDrop = wireCurrent*(2*wireResistance);
            }
            else if (phaseType == 3)
            {
                voltageDrop = wireCurrent*(Math.Sqrt(3)*wireResistance);
            }

            if (double.IsNaN(voltageDrop) || double.IsInfinity(voltageDrop))
            {
                voltageDrop = 0.0;
            }

            return voltageDrop;
        }

        /// <summary>
        ///     This method calculates the thickness of the stator for a dual rotor system.
        /// </summary>
        /// <param name="magnetThickness">Thickness of a single magnet (mm)</param>
        /// <param name="gap">The mechanical gap between the rotor and the stator (mm)</param>
        /// <returns>Stator thickness (mm)</returns>
        public static double CalculateStatorThickness(double magnetThickness, double gap)
        {
            var coilThickness = 2*magnetThickness - 2*gap;
            return coilThickness;
        }

        /// <summary>
        ///     This method calculates the amount of coil windings.
        /// </summary>
        /// <param name="phaseVoltage"></param>
        /// <param name="magnets">The total amount of magnets</param>
        /// <param name="poleFlux">The flux maximum pole flux (T)</param>
        /// <param name="rpm">The amount of RPM</param>
        /// <param name="coilsPhase">The amount of coils per phase</param>
        /// <param name="coilWindingFactor"></param>
        /// <returns>The total amount of coil windings</returns>
        public static int CalculateCoilWindings(double phaseVoltage, int magnets, double rpm, int coilsPhase,
            double poleFlux,
            double coilWindingFactor)
        {
            return (int) (Math.Sqrt(2)*phaseVoltage/(coilsPhase*2*Math.PI*coilWindingFactor*rpm*poleFlux*magnets/120));
        }

        /// <summary>
        ///     This method calculates the width of the coil leg.
        /// </summary>
        /// <param name="maxPhaseCurrent">The max. phase current (A)</param>
        /// <param name="coilWindings">The amount of coil windings (n)</param>
        /// <param name="axialThickness">The stator thickness (m)</param>
        /// <param name="coilHeatCoefficient"></param>
        /// <param name="coilFillFactor"></param>
        /// <returns>Returns the coil leg width (mm)</returns>
        public static double CalculateCoilLegWidth(double maxPhaseCurrent, int coilWindings, double axialThickness,
            double coilHeatCoefficient, double coilFillFactor)
        {
            const double copperResistivity = 0.0000000168;
            var coilLegWidth = maxPhaseCurrent*coilWindings/
                               Math.Sqrt(2*coilHeatCoefficient*coilFillFactor*Common.MillimetersToMeters(axialThickness)/
                                         copperResistivity);
            return coilLegWidth*1000;
        }

        /// <summary>
        ///     This method calculates the width of the coil leg.
        /// </summary>
        /// <param name="maxPhaseCurrent">The max. phase current (A)</param>
        /// <param name="coilWindings">The amount of coil windings (n)</param>
        /// <param name="axialThickness">The stator thickness (m)</param>
        /// <param name="currentDensity"></param>
        /// <param name="coilFillFactor"></param>
        /// <returns>Returns the coil leg width (mm)</returns>
        public static double CalculateCoilLegWidthMod(double maxPhaseCurrent, int coilWindings, double axialThickness,
            double currentDensity, double coilFillFactor)
        {
            var coilLegWidth = ((maxPhaseCurrent/currentDensity)*coilWindings)/(coilFillFactor*axialThickness);
            return coilLegWidth;
        }

        /// <summary>
        ///     This method calculates the area of a coil wire.
        /// </summary>
        /// <param name="coilWidth">The width of the coil (mm)</param>
        /// <param name="statorThickness">The stator thickness (mm)</param>
        /// <param name="coilWindings">The amount of coil windings (n)</param>
        /// <param name="coilFillFactor"></param>
        /// <returns>The area of the coil surface (mm2)</returns>
        public static double CalculateCoilCrossSectionalArea(double coilWidth, double statorThickness, int coilWindings,
            double coilFillFactor)
        {
            var coilCrossSeactionalArea = coilFillFactor*coilWidth*statorThickness/coilWindings;
            return coilCrossSeactionalArea;
        }

        /// <summary>
        ///     This method calculates the maximal phase current.
        /// </summary>
        /// <param name="generatorNominalPower"></param>
        /// <param name="phaseVoltageCutin"></param>
        /// <param name="generatorEfficiency"></param>
        /// <returns></returns>
        public static double CalculateMaximumPhaseCurrent(double generatorNominalPower, double phaseVoltageCutin,
            double generatorEfficiency)
        {
            return 1.1*generatorNominalPower/(3*phaseVoltageCutin*generatorEfficiency);
        }

        /// <summary>
        ///     This method calculates the maximum current density of the coil wire.
        /// </summary>
        /// <param name="maxPhaseCurrent">The maximum current that can flow trough the coil</param>
        /// <param name="crossSectionalArea">The cross sectional area (mm2)</param>
        /// <returns>The maximum current density (m2)</returns>
        public static double CalculateMaximumCurrentDensity(double maxPhaseCurrent, double crossSectionalArea)
        {
            var maximumCurrentDensity = maxPhaseCurrent/crossSectionalArea;
            return maximumCurrentDensity;
        }

        /// <summary>
        ///     This method calculates the coil wire diameter.
        /// </summary>
        /// <param name="crossSectionalArea"> The cross sectional area of the coil (mm2)</param>
        /// <returns>The coil wire diameter (mm)</returns>
        public static double CalculateCoilWireDiameter(double crossSectionalArea)
        {
            var coilWireDiameter = Math.Sqrt(4*crossSectionalArea/Math.PI);
            return coilWireDiameter;
        }

        /// <summary>
        /// This method calculates the copper cross section. This method is used to calculate the coil leg width using the modified method.
        /// </summary>
        /// <param name="maxCurrentDensity"></param>
        /// <param name="maxCurrent"></param>
        /// <returns></returns>
        public static double CalculateCopperCrossSection(double maxCurrentDensity, double maxCurrent)
        {
            return maxCurrent/maxCurrentDensity;
        }
    }
}