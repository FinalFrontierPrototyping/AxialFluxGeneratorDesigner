using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AxialFluxGeneratorDesigner
{
    /// <summary>
    /// 
    /// </summary>
    public class Afpm
    {


        //TODO test
        //Generator variables
        /// <summary>
        /// </summary>
        public double DcVoltageMin { get; set; } = 200;

        /// <summary>
        /// </summary>
        public double DcVoltageMax { get; set; } = 700;

        /// <summary>
        /// </summary>
        public double GeneratorPower { get; set; } = 3000;

        /// <summary>
        /// </summary>
        public int GeneratorRpm { get; set; } = 500;

        /// <summary>
        /// </summary>
        public double GeneratorEfficiency { get; set; } = 0.9;

        /// <summary>
        /// </summary>
        public double MechamicalGap { get; set; } = 3;

        /// <summary>
        /// </summary>
        public double PhaseVoltageMax { get; set; }

        //Stator variables
        /// <summary>
        /// </summary>
        public double PhaseVoltageMin { get; set; }

        /// <summary>
        /// </summary>
        public double RotorInnerOuterRadiusRatio { get; set; }

        /// <summary>
        /// </summary>
        public double RotorInnerRadius { get; set; }

        /// <summary>
        /// </summary>
        public double RotorOuterRadius { get; set; }

        /// <summary>
        /// </summary>
        public double RotorThickness { get; set; }


        #region Stator properties

        /// <summary>
        /// The phase count is set to 3.
        /// </summary>
        public int PhaseCount { get; } = 3;

        /// <summary>
        ///     The coil count is the total amount of coils for the generator.
        ///     So 5 coils/phase and 3 phases results in a coil count of 5 * 3 = 15
        /// </summary>
        public int CoilCount { get; set; }

        /// <summary>
        ///     The coils per phase are the amount of coils in each phase.
        /// </summary>
        public int CoilsPerPhase { get; set; } = 5;

        /// <summary>
        ///     The phase current is the maximal current that flows through the coil.
        /// </summary>
        public double PhaseCurrent { get; set; }

        #endregion

        #region Stator coil properties

        /// <summary>
        /// </summary>
        public double CoilCrossSectionalArea { get; set; }

        /// <summary>
        /// </summary>
        public double CoilLegWidth { get; set; } = 10;

        /// <summary>
        /// </summary>
        public double CoilWindingCoefficient { get; set; } = 0.95;

        /// <summary>
        /// </summary>
        public double CoilHeatCoefficient { get; set; } = 0.30;

        /// <summary>
        /// </summary>
        public double CoilFillFactor { get; set; } = 0.55;

        /// <summary>
        /// </summary>
        public double CoilInductance { get; set; }

        /// <summary>
        /// </summary>
        public double CoilResistance { get; set; }

        /// <summary>
        /// </summary>
        public double CoilWireLength { get; set; }

        /// <summary>
        /// </summary>
        public int CoilTurns { get; set; }

        /// <summary>
        /// </summary>
        public double CoilWireDiameter { get; set; }

        /// <summary>
        /// </summary>
        public double CoilThickness { get; set; }

        #endregion

        #region Rotor properties

        /// <summary>
        /// </summary>
        public double MagnetCoerciveFieldStrength { get; set; }

        /// <summary>
        /// </summary>
        public int MagnetCount { get; set; }

        /// <summary>
        /// </summary>
        public double MagnetDistance { get; set; }

        /// <summary>
        /// </summary>
        public double MagnetFluxDensity { get; set; }

        /// <summary>
        /// </summary>
        public double MagnetLength { get; set; } = 30;

        /// <summary>
        /// </summary>
        public double MagnetPoleArcPitch { get; set; }

        /// <summary>
        /// </summary>
        public double MagnetPoleFlux { get; set; }

        /// <summary>
        /// </summary>
        public List<Tuple<string, double, double>> MagnetProperties = new List<Tuple<string, double, double>>
        {
            new Tuple<string, double, double>("N30", 1.1, 808.0),
            new Tuple<string, double, double>("N33", 1.155, 848.0),
            new Tuple<string, double, double>("N35", 1.19, 887.5),
            new Tuple<string, double, double>("N38", 1.24, 887.5),
            new Tuple<string, double, double>("N40", 1.275, 927.5),
            new Tuple<string, double, double>("N42", 1.305, 927.5),
            new Tuple<string, double, double>("N45", 1.345, 927.5),
            new Tuple<string, double, double>("N48", 1.395, 927.5),
            new Tuple<string, double, double>("N50", 1.430, 927.5),
            new Tuple<string, double, double>("N52", 1.445, 927.5)
        };

        /// <summary>
        /// </summary>
        public double MagnetRemanentFluxDensity { get; set; }

        //Rotor variables
        /// <summary>
        /// </summary>
        public double MagnetThickness { get; set; } = 10;

        /// <summary>
        /// </summary>
        public double MagnetWidth { get; set; } = 46;

        /// <summary>
        /// </summary>
        public double MaxCurrentDensity { get; set; }

        /// <summary>
        /// </summary>
        public double MaxPhaseCurrent { get; set; }

        #endregion

        #region Front end Turbine properties

        /// <summary>
        /// </summary>
        public double TurbineMaximumPowerCoefficient { get; set; } = 0.35;

        /// <summary>
        /// </summary>
        public double TurbineRotorRadius { get; set; }

        /// <summary>
        /// </summary>
        public int TurbineRpmMax { get; set; }

        /// <summary>
        /// </summary>
        public int TurbineRpmMin { get; set; }

        /// <summary>
        /// </summary>
        public double TurbineSpeedTipRatioMax { get; set; } = 7;

        /// <summary>
        /// </summary>
        public double TurbineSpeedTipRatioMin { get; set; } = 8.75;

        /// <summary>
        /// </summary>
        public double TurbineWindspeedMax { get; set; } = 10;

        /// <summary>
        /// </summary>
        public double TurbineWindspeedMin { get; set; } = 3;

        /// <summary>
        /// </summary>
        public double TurbineAirDensity { get; set; } = 1.20;

        /// <summary>
        /// </summary>
        public double FrontEndTorque { get; set; }

        /// <summary>
        /// </summary>
        public int OtherRpmMin { get; set; } = 300;

        /// <summary>
        /// </summary>
        public int OtherRpmMax { get; set; } = 500;

        /// <summary>
        /// </summary>
        public double PhaseWireVoltageDrop { get; set; }

        /// <summary>
        /// </summary>
        public double PhaseWireLength { get; set; } = 0;

        /// <summary>
        /// </summary>
        public double PhaseWireDiameter { get; set; } = 0;

        /// <summary>
        /// </summary>
        public double PhaseWireResistance { get; set; } = 0;

        /// <summary>
        /// </summary>
        public double RectifierWireVoltageDrop { get; set; }

        /// <summary>
        /// </summary>
        public double RectifierWireLength { get; set; }

        /// <summary>
        /// </summary>
        public double RectifierWireResistance { get; set; }

        /// <summary>
        /// </summary>
        public double RectifierWireDiameter { get; set; }

        #endregion

        /// <summary>
        /// This property determines the type of energy storage that is used.
        /// 0 = Battery
        /// 1 = grid
        /// This property is necessary because depending on the energy storage type different calculations are done
        /// </summary>
        public int GeneratorEnergyStorageConnection { get; set; }

        /// <summary>
        /// This property determines the front end type that is used to drive the generator.
        /// 0 = Wind turbine
        /// 1 = Other
        /// This property is necessary because depending on the front end type different calculations are done.
        /// </summary>
        public int GeneratorFrontEnd { get; set; }

        /// <summary>
        ///     This method calculates the inductance of a coil (mH).
        ///     The distance between \f$(x_1,y_1)\f$ and \f$(x_2,y_2)\f$ is\f$\sqrt{(x_2-x_1)^2+(y_2-y_1)^2}\f$.
        ///     http://coil32.net/multi-layer-coil.html
        /// </summary>
        /// <param name="windingCount">The winding count of the coil</param>
        /// <param name="coilDiameter">The diameter of the coil (mm)</param>
        /// <param name="coilThickness">Thickness of the coil (mm)</param>
        /// <returns>The inductance of the coil (mH)</returns>
        public double CalculateCoilInductance(int windingCount, double coilDiameter, double coilThickness)
        {
            const int permeabilirtCoreMaterial = 1;
            const int relativePermeability = 1;

            return (Math.Pow(windingCount, 2) * permeabilirtCoreMaterial * relativePermeability * Math.PI *
                    Math.Pow(MillimetersToMeters(coilDiameter / 2), 2)) / MillimetersToMeters(coilThickness);
        }

        /// <summary>
        ///     This method calculated the coil inductance based on the Maxwell calculation (Experimental).
        ///     http://electronbunker.ca/eb/CalcMethods1b.html
        ///     http://electronbunker.ca/eb/CalcMethods1c.html
        /// </summary>
        /// <param name="wireDiameter"></param>
        /// <param name="turnsLayer"></param>
        /// <param name="layerNumber"></param>
        /// <param name="innerCoilRadius"></param>
        /// <param name="axialPitch"></param>
        /// <param name="radialPitch"></param>
        /// <returns></returns>
        public double CalculateCoilInductanceMaxwell(double wireDiameter, double turnsLayer, double layerNumber,
            double innerCoilRadius, double axialPitch, double radialPitch)
        {
            var g = Math.Exp(-0.25f) * wireDiameter / 2;
            var m = 0.0;
            var nxMin = 1;
            double r1;

            //Calculate all mutual inductances
            for (var ny = 0; ny <= layerNumber - 1; ny++)
            {
                for (var nx = nxMin; nx <= turnsLayer - 1; nx++)
                {
                    var mf = (ny == 0 || nx == 0) ? 2 : 4; //multiplication factor
                    var x = nx * axialPitch;
                    var mult = mf * (turnsLayer - nx);

                    for (var y = 0; y <= layerNumber - ny - 1; y++)
                    {
                        r1 = innerCoilRadius + (y) * radialPitch;
                        var r2 = innerCoilRadius + (y + ny) * radialPitch;
                        m = m + mult * CalculateMut(r1, r2, x);
                    }
                }
                nxMin = 0;
            }

            //Calculate all self inductances
            for (var y = 0; y <= layerNumber - 1; y++)
            {
                r1 = innerCoilRadius + y * radialPitch;
                m = m + turnsLayer * CalculateMut(r1, r1, g);
            }
            return (m);
        }

        /// <summary>
        /// </summary>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public double CalculateMut(double r1, double r2, double x)
        {
            var a = Math.Sqrt(Math.Pow((r1 + r2), 2) + Math.Pow(x, 2));
            var b = Math.Sqrt(Math.Pow((r1 - r2), 2) + Math.Pow(x, 2));
            var c = a - b;
            var ci = 1;
            var cs = c * c;
            double co;

            do
            {
                var ao = (a + b) / 2;
                b = Math.Sqrt(a * b);
                a = ao;
                co = c;
                c = a - b;
                ci = 2 * ci;
                cs = cs + ci * c * c;
            } while (c < co);

            return 0.0005 * Math.Pow(Math.PI, 2) * cs / a;
        }

        /// <summary>
        ///     This method calculates the resistance of a copper wire.
        /// </summary>
        /// <param name="wireLength"></param>
        /// <param name="wireDiameter"></param>
        /// <returns>The resistance of the copper wire (Ohm)</returns>
        public double CalculateWireResistance(double wireLength, double wireDiameter)
        {
            const double copperResistivity = 0.0000000168;
            double wireResistance = 0;
            var wireRadius = wireDiameter / 2;
            wireResistance = (copperResistivity * wireLength) / (Math.PI * Math.Pow(MillimetersToMeters(wireRadius), 2));

            if (Double.IsNaN(wireResistance) || Double.IsInfinity(wireResistance))
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
        public double VoltageDrop(double wireLength, double wireDiameter, double wireCurrent, int phaseType)
        {
            var wireResistance = CalculateWireResistance(wireLength, wireDiameter);
            double voltageDrop = 1000;

            if (phaseType == 1)
            {
                voltageDrop = wireCurrent * (2 * wireResistance);
            }
            else if (phaseType == 3)
            {
                voltageDrop = wireCurrent * (Math.Sqrt(3) * wireResistance);
            }

            if (Double.IsNaN(voltageDrop) || Double.IsInfinity(voltageDrop))
            {
                voltageDrop = 0.0;
            }

            return voltageDrop;
        }

        /// <summary>
        ///     This method calculates the resistance of a coil (ohm) using the following formula
        ///     \f$R_{Coil} = \frac{\text{Copper resistivity (\Omega.m)} \times \text{Wire length (m)}}{\Pi \times \text{radius
        ///     (m)}^2 }\f$
        /// </summary>
        /// <example>
        /// </example>
        /// <param name="coilWireLength">The wire length of the coil (m)</param>
        /// <param name="wireDiameter">The diameter of the wire (mm)</param>
        /// <returns>The resistance of the coil (ohm)</returns>
        public double CalculateCoilResistance(double coilWireLength, double wireDiameter)
        {
            const double copperResistivity = 0.0000000168;
            var wireRadius = wireDiameter / 2;
            return (copperResistivity * coilWireLength) / (Math.PI * Math.Pow(MillimetersToMeters(wireRadius), 2));
        }

        /// <summary>
        ///     This method calculates the wire length of a coil.
        /// </summary>
        /// <param name="windingCount">The winding count of the coil</param>
        /// <param name="insideCircumference">The inside circumference of the coil (mm)</param>
        /// <param name="outsideCircumference">The outside circumference of the coil (mm)</param>
        /// <returns>The wire length of the coil (m)</returns>
        public double CalculateCoilWireLength(int windingCount, double insideCircumference, double outsideCircumference)
        {
            return (((insideCircumference + outsideCircumference) / 2) * windingCount) / 1000;
        }

        /// <summary>
        ///     This method calculates the turbine rotor radius to achieve the nominal power.
        /// </summary>
        /// <param name="generatorNominalPower"> The maximal generator power (W)</param>
        /// <param name="airDensity"> The air density (kg/m3)</param>
        /// <param name="maximumPowerCoefficient">The generator efficiency (0.9 (90%)) is normal</param>
        /// <param name="windSpeed">The maximum wind speed (m/s)</param>
        /// <returns>The rotor radius (m)</returns>
        public double CalculateTurbineRotorRadius(double generatorNominalPower, double airDensity,
            double maximumPowerCoefficient, double windSpeed)
        {
            var aerodynamicPower = generatorNominalPower / GeneratorEfficiency;

            return
                Math.Sqrt((2 * aerodynamicPower) / (Math.PI * airDensity * maximumPowerCoefficient * Math.Pow(windSpeed, 3)));
        }

        /// <summary>
        ///     This method calculates the turbine RPM.
        /// </summary>
        /// <param name="windSpeed">The wind speed (m/s)</param>
        /// <param name="tipSpeedRatio">The tip speed ratio</param>
        /// <param name="turbineRotorRadius">The radius of the rotor (m)</param>
        /// <returns>The RPM</returns>
        public int CalculateTurbineOptimalRotationSpeed(double windSpeed, double tipSpeedRatio,
            double turbineRotorRadius)
        {
            return (int)((60 * windSpeed * tipSpeedRatio) / (2 * Math.PI * turbineRotorRadius));
        }

        /// <summary>
        ///     This method calculates the wind speed based on the phase voltage ratio (min/max).
        /// </summary>
        /// <param name="phaseVoltageMin">The minimum phase voltage (v)</param>
        /// <param name="phaseVoltageMax">The maximum phase voltage (v)</param>
        /// <param name="nominalRpm">The nominal RPM of the rotor</param>
        /// <returns>Returns the minimum rpm</returns>
        public int CalculateGridRpm(double phaseVoltageMin, double phaseVoltageMax, int nominalRpm)
        {
            return (int)((phaseVoltageMin / phaseVoltageMax) * nominalRpm);
        }

        /// <summary>
        /// This method calculates the maximal battery voltage based on the ratio between the max and min rpd times the minimal phase voltage.
        /// </summary>
        /// <param name="rpmMin"></param>
        /// <param name="rpmMax"></param>
        /// <param name="minimalPhaseVoltage"></param>
        /// <returns>The maximal phase voltage</returns>
        public double CalculateBatteryVoltage(double rpmMin, double rpmMax, double minimalPhaseVoltage)
        {
            return (rpmMax / rpmMin) * minimalPhaseVoltage;
        }

        /// <summary>
        ///     This method calculates the wind speed.
        /// </summary>
        /// <param name="speedRpm">The rotational speed (RPM)</param>
        /// <param name="turbineRotorRadius">The radius of the rotor (m)</param>
        /// <param name="tipSpeedRatio">The tip speed ratio</param>
        /// <returns>The wind speed (m/s)</returns>
        public double CalculateTurbineOptimalWindSpeed(double speedRpm, double turbineRotorRadius, double tipSpeedRatio)
        {
            return (2 * Math.PI * speedRpm * turbineRotorRadius) / (60 * tipSpeedRatio);
        }

        /// <summary>
        ///     This method calculates the phase voltage for a 3 phase Y-configuration from the provided DC voltage.
        ///     Vdc = ((3*SQRT(2))/PI) * Vrms
        ///     Vdc = 1.35 * Vrms
        /// </summary>
        /// <param name="dcVoltage">DC voltage (V)</param>
        /// <returns>Phase voltage (rms) (V)</returns>
        public double CalculatePhaseVoltage(double dcVoltage)
        {
            //return (dc_voltage + 1.4) / (Math.Sqrt(3) * (Math.Sqrt(2) * (3 / Math.PI)));
            return (dcVoltage + 1.4) / (Math.Sqrt(3) * (Math.Sqrt(2)));
        }

        /// <summary>
        ///     This method calculates the amount of magnets.
        /// </summary>
        /// <param name="coilCount">The total amount of coils</param>
        /// <returns>The amount of magnets</returns>
        public int CalculatePolePairs(int coilCount)
        {
            var poleCount = (((coilCount * 2) / 0.5) / 3);
            return (int)poleCount;
        }

        /// <summary>
        ///     This method calculates the thickness of the stator for a dual rotor system.
        /// </summary>
        /// <param name="magnetThickness">Thickness of a single magnet (mm)</param>
        /// <param name="gap">The mechanical gap between the rotor and the stator (mm)</param>
        /// <returns>Stator thickness (mm)</returns>
        public double CalculateStatorThickness(double magnetThickness, double gap)
        {
            var coilThickness = (2 * magnetThickness) - (2 * gap);
            return coilThickness;
        }

        /// <summary>
        ///     This method calculates the flux density of a magnet at a certain distance (gap).
        /// </summary>
        /// <param name="remanentFluxDensity">The remanent density of the magnet (T)</param>
        /// <param name="coerciveFieldStrength">The coercive field strength of the magnet (A/m)(</param>
        /// <param name="magnetThickness">The magnet thickness (mm)</param>
        /// <param name="gap">Gap between the magnet surface and the stator (mm).</param>
        /// <returns>Magnet flux density (T)</returns>
        public double CalculateMagnetFluxDensity(double remanentFluxDensity, double coerciveFieldStrength,
            double magnetThickness, double gap)
        {
            double ksat = 1;
            var statorThickness = CalculateStatorThickness(magnetThickness, gap);
            var vacuum_permeability = (0.4 * Math.PI) / 1000000;
            var coilPermeability = (1 / vacuum_permeability) * (remanentFluxDensity / (coerciveFieldStrength * 1000));
            var fluxDensity = remanentFluxDensity /
                              (1 +
                               (coilPermeability *
                                (MillimetersToMeters(gap) + (0.5 * MillimetersToMeters(statorThickness))) /
                                MillimetersToMeters(magnetThickness))) * ksat;
            return fluxDensity;
        }

        /// <summary>
        ///     This method calculates the magnet flux density for the magnet area.
        /// </summary>
        /// <param name="fluxDensity">The magnet flux density (T)</param>
        /// <param name="magnetWidth">The magnet width (mm)</param>
        /// <param name="magnetLength">The magnet length (mm)</param>
        /// <returns>Maximum pole flux (T)</returns>
        public double CalculateMaximumPoleFlux(double fluxDensity, double magnetWidth, double magnetLength)
        {
            return fluxDensity * MillimetersToMeters(magnetWidth) * MillimetersToMeters(magnetLength);
        }

        /// <summary>
        ///     This method calculated the amount of coil windings.
        /// </summary>
        /// <param name="phaseVoltage"></param>
        /// <param name="magnets">The total amount of magnets</param>
        /// <param name="poleFlux">The flux maximum pole flux (T)</param>
        /// <param name="rpm">The amount of RPM</param>
        /// <param name="coilsPhase">The amount of coils per phase</param>
        /// <param name="coilWindingFactor"></param>
        /// <returns>The total amount of coil windings</returns>
        public int CalculateCoilWindings(double phaseVoltage, int magnets, double rpm, int coilsPhase, double poleFlux,
            double coilWindingFactor)
        {
            return (int)((Math.Sqrt(2) * phaseVoltage) / (coilsPhase * 2 * Math.PI * coilWindingFactor * rpm * poleFlux * magnets / 120));
        }

        /// <summary>
        ///     This method calculates the width of the coil leg.
        /// </summary>
        /// <param name="maxPhaseCurrent">The max. phase current (A)</param>
        /// <param name="coilWindings">The amount of coil windings (n)</param>
        /// <param name="axialThickness">The stator thickness (mm)</param>
        /// <returns>Returns the coil leg width (mm)</returns>
        public double CalculateCoilLegWidth(double maxPhaseCurrent, int coilWindings, double axialThickness)
        {
            const double copperResistivity = 0.0000000168;
            var coilLegWidth = (maxPhaseCurrent * coilWindings) /
                               Math.Sqrt((2 * CoilHeatCoefficient * CoilFillFactor * MillimetersToMeters(axialThickness)) /
                                         copperResistivity);
            return coilLegWidth * 10;
        }

        /// <summary>
        ///     This method calculates the area of a coil wire.
        /// </summary>
        /// <param name="coilWidth">The width of the coil (mm)</param>
        /// <param name="statorThickness">The stator thickness (mm)</param>
        /// <param name="coilWindings">The amount of coil windings (n)</param>
        /// <returns>The area of the coil surface (mm2)</returns>
        public double CalculateCoilCrossSectionalArea(double coilWidth, double statorThickness, int coilWindings)
        {
            var coilCrossSeactionalArea = (CoilFillFactor * coilWidth * statorThickness) / coilWindings;
            return coilCrossSeactionalArea;
        }

        /// <summary>
        /// This method calculates the maximal phase current.
        /// </summary>
        /// <param name="generatorNominalPower"></param>
        /// <param name="phaseVoltageCutin"></param>
        /// <returns></returns>
        public double CalculateMaximumPhaseCurrent(double generatorNominalPower, double phaseVoltageCutin)
        {
            return (1.1 * generatorNominalPower) / (3 * phaseVoltageCutin * GeneratorEfficiency);
        }

        /// <summary>
        ///     This method calculates the maximum current density of the coil wire.
        /// </summary>
        /// <param name="maxPhaseCurrent">The maximum current that can flow trough the coil</param>
        /// <param name="crossSectionalArea">The cross sectional area (mm2)</param>
        /// <returns>The maximum current density (m2)</returns>
        public double CalculateMaximumCurrentDensity(double maxPhaseCurrent, double crossSectionalArea)
        {
            var maximumCurrentDensity = maxPhaseCurrent / crossSectionalArea;
            return maximumCurrentDensity;
        }

        /// <summary>
        ///     This method calculates the coil wire diameter.
        /// </summary>
        /// <param name="crossSectionalArea"> The cross sectional area of the coil (mm2)</param>
        /// <returns>The coil wire diameter (mm)</returns>
        public double CalculateCoilWireDiameter(double crossSectionalArea)
        {
            var coilWireDiameter = Math.Sqrt((4 * crossSectionalArea) / Math.PI);
            return coilWireDiameter;
        }

        /// <summary>
        /// </summary>
        /// <param name="magnetWidth"></param>
        /// <param name="magnetsDistance"></param>
        /// <returns></returns>
        public double CalculateMagnetPoleArcPitch(double magnetWidth, double magnetsDistance)
        {
            return MagnetWidth / magnetsDistance;
        }

        /// <summary>
        /// </summary>
        /// <param name="totalCoils"></param>
        /// <param name="coilWidth"></param>
        /// <param name="polePairs"></param>
        /// <param name="magnetWidth"></param>
        /// <returns></returns>
        public double CalculateGeneratorInnerRadius(int totalCoils, double coilWidth, int polePairs, double magnetWidth)
        {
            return (((2 * totalCoils) * coilWidth) + polePairs * magnetWidth) / (2 * Math.PI);
        }

        /// <summary>
        /// </summary>
        /// <param name="generatorInnerRadius"></param>
        /// <param name="magnetLength"></param>
        /// <returns></returns>
        public double CalculateCalculateGeneratorOuterRadius(double generatorInnerRadius, double magnetLength)
        {
            return ((2 * generatorInnerRadius) + (2 * magnetLength)) / 2;
        }

        /// <summary>
        /// </summary>
        /// <param name="generatorInnerRadius"></param>
        /// <param name="generatorOuterRadius"></param>
        /// <returns></returns>
        public double CalculateGeneratorInnerOuterRadiusRatio(double generatorInnerRadius, double generatorOuterRadius)
        {
            return generatorInnerRadius / generatorOuterRadius;
        }

        /// <summary>
        /// </summary>
        /// <param name="mm"></param>
        /// <returns></returns>
        private double MillimetersToMeters(double mm)
        {
            return mm / 1000;
        }

        /// <summary>
        /// </summary>
        /// <param name="coilCount"></param>
        /// <returns></returns>
        public double CalculateCoilAngle(int coilCount)
        {
            // ReSharper disable once PossibleLossOfFraction
            return 360 / coilCount;
        }

        /// <summary>
        ///     This method calculates the inner coil dimensions
        /// </summary>
        /// <param name="coilLegWidth">The leg width of the coil (mm)</param>
        /// <param name="coilCount">The total amount of coils (for all three phases)</param>
        /// <param name="coilGap">The bottom gap between the coil legs (mm)</param>
        /// <param name="betweenCoilGap">The gap between two coils (mm)</param>
        /// <param name="magnetHeight">The height of the magnet (mm)</param>
        /// <returns>A tuple containing the coil dimensions (mm)</returns>
        public Tuple<double, double, double, double> CoilInnerDimensions(double coilLegWidth, int coilCount,
            double coilGap, double betweenCoilGap, double magnetHeight)
        {
            Debug.WriteLine("----------Inner Coil dimensions----------");

            var coilAngle = CalculateCoilAngle(coilCount);

            //Calculate the minimal circumference 
            var circumferenceMin = ((coilLegWidth * 2) * coilCount) + (coilCount * coilGap);
            //Debug.WriteLine("circumference_min: " + circumferenceMin);

            var radiusMin = (circumferenceMin / Math.PI) / 2;
            Debug.WriteLine("radius_min: " + radiusMin);

            var lowerMinArc = CalculateCoilArcLength(coilAngle, radiusMin);

            var radiusMax = radiusMin + magnetHeight + (2 * coilLegWidth);
            Debug.WriteLine("radius_max: " + radiusMax);

            var upperMinArc = CalculateCoilArcLength(coilAngle, radiusMax) - (2 * coilLegWidth);
            Debug.WriteLine("upper_min_arc: " + upperMinArc);

            var coilHeightMin = magnetHeight;
            Debug.WriteLine("coilHeightMin: " + coilHeightMin);

            var coilInnerCircumference = (coilHeightMin * 2) + lowerMinArc + upperMinArc;
            Debug.WriteLine("coilInnerCircumference: " + coilInnerCircumference);

            return Tuple.Create(lowerMinArc, upperMinArc, coilHeightMin, coilInnerCircumference);
        }

        /// <summary>
        ///     This method calculates the outer coil dimensions
        /// </summary>
        /// <param name="coilLegWidth">The leg width of the coil (mm)</param>
        /// <param name="coilCount">The total amount of coils (for all three phases)</param>
        /// <param name="coilGap">The bottom gap between the coil legs (mm)</param>
        /// <param name="betweenCoilGap">The gap between two coils (mm)</param>
        /// <param name="magnetHeight">The height of the magnet (mm)</param>
        /// <returns>A tuple containing the coil dimensions (mm)</returns>
        public Tuple<double, double, double, double> CoilOuterDimensions(double coilLegWidth, int coilCount,
            double coilGap, double betweenCoilGap, double magnetHeight)
        {
            Debug.WriteLine("----------Inner Coil dimensions----------");

            var coilAngle = CalculateCoilAngle(coilCount);

            //Calculate the minimal circumference 
            var circumferenceMin = ((coilLegWidth * 2) * coilCount) + (coilCount * coilGap);
            //Debug.WriteLine("circumference_min: " + circumferenceMin);

            var radiusMin = (circumferenceMin / Math.PI) / 2;
            Debug.WriteLine("radius_min: " + radiusMin);

            var lowerMaxArc = CalculateCoilArcLength(coilAngle, radiusMin);
            Debug.WriteLine("lower_max_arc: " + lowerMaxArc);

            var radiusMax = radiusMin + magnetHeight + (2 * coilLegWidth);
            Debug.WriteLine("radius_max: " + radiusMax);

            var upperMaxArc = CalculateCoilArcLength(coilAngle, radiusMax);
            Debug.WriteLine("upper_max_arc: " + upperMaxArc);

            var coilHeightMax = (coilLegWidth * 2) + magnetHeight;
            Debug.WriteLine("coilHeightMax: " + coilHeightMax);

            var coilOuterCircumference = (coilHeightMax * 2) + lowerMaxArc + upperMaxArc;
            Debug.WriteLine("coilOuterCircumference: " + coilOuterCircumference);

            return Tuple.Create(lowerMaxArc, upperMaxArc, coilHeightMax, coilOuterCircumference);
        }

        /// <summary>
        ///     This method calculates the (upper or lower) arc length of a coil.
        /// </summary>
        /// <param name="angle">The angle of the coil section (Deg)</param>
        /// <param name="radius">The radius (mm)</param>
        /// <returns>The arc length (mm)</returns>
        private double CalculateCoilArcLength(double angle, double radius)
        {
            return 2 * Math.PI * radius * (angle / 360);
        }

        /// <summary>
        ///     This method calculated the (upper or lower) line length of a coil.
        /// </summary>
        /// <param name="angle">The angle of the coil section (Deg)</param>
        /// <param name="radius">The radius (mm)</param>
        /// <returns>The line length (mm)</returns>
        private double CalculateCoilLineLength(double angle, double radius)
        {
            return 2 * radius * Math.Sin((angle * Math.PI) / 360);
        }

        /// <summary>
        ///     This method calculates the tangent of a coil corner. In this way a rounded corner can be created using an arc.
        /// </summary>
        /// <param name="radius">The radius (mm)</param>
        /// <returns>The distance from the angle to the tangent of the circle (mm)</returns>
        private double CalculateCircleTangent(double radius)
        {
            return (1 + Math.Sqrt(2)) * radius;
        }

        /// <summary>
        /// This method calculates the torque based on the power (W) and RPM
        /// </summary>
        /// <param name="power">The power (Watt)</param>
        /// <param name="rpm">The rpm</param>
        /// <returns>The torque (Nm)</returns>
        public double CalculateTorque(double power, int rpm)
        {
            return power / ((2 * Math.PI * rpm) / 60);
        }

        /// <summary>
        ///     This method can be called to update all calculations.
        /// </summary>
        public void UpdateCalculations()
        {

            PhaseVoltageMin = CalculatePhaseVoltage(DcVoltageMin);
            TurbineRotorRadius = CalculateTurbineRotorRadius(GeneratorPower, TurbineAirDensity, TurbineMaximumPowerCoefficient, TurbineWindspeedMax);
            TurbineRpmMax = CalculateTurbineOptimalRotationSpeed(TurbineWindspeedMax, TurbineSpeedTipRatioMax, TurbineRotorRadius);
            FrontEndTorque = CalculateTorque(GeneratorPower, TurbineRpmMax);

            //Battery connection
            if (GeneratorEnergyStorageConnection == 0)
            {
                //Turbine
                if (GeneratorFrontEnd == 0)
                {
                    TurbineRpmMin = CalculateTurbineOptimalRotationSpeed(TurbineWindspeedMin, TurbineSpeedTipRatioMin, TurbineRotorRadius);
                    PhaseVoltageMax = CalculateBatteryVoltage(TurbineRpmMin, TurbineRpmMax, PhaseVoltageMin);
                }
                //Other
                else if (GeneratorFrontEnd == 1)
                {
                    PhaseVoltageMax = CalculateBatteryVoltage(OtherRpmMin, OtherRpmMax, PhaseVoltageMin);
                }
            }
            //Grid connection
            if (GeneratorEnergyStorageConnection == 1)
            {
                PhaseVoltageMax = CalculatePhaseVoltage(DcVoltageMax);

                //Turbine
                if (GeneratorFrontEnd == 0)
                {
                    TurbineRpmMin = CalculateGridRpm(PhaseVoltageMin, PhaseVoltageMax, TurbineRpmMax);
                    TurbineWindspeedMin = CalculateTurbineOptimalWindSpeed(TurbineRpmMin, TurbineRotorRadius, TurbineSpeedTipRatioMin);
                }
                //Other
                else if (GeneratorFrontEnd == 1)
                {
                    OtherRpmMin = CalculateGridRpm(PhaseVoltageMin, PhaseVoltageMax, OtherRpmMax);
                }
            }

            MaxPhaseCurrent = CalculateMaximumPhaseCurrent(GeneratorPower, 286);
            Debug.WriteLine("Max phase current (A): " + MaxPhaseCurrent);

            CoilThickness = CalculateStatorThickness(MagnetThickness, MechamicalGap);
            Debug.WriteLine("Stator thickness (mm): " + CoilThickness);

            MagnetFluxDensity = CalculateMagnetFluxDensity(MagnetRemanentFluxDensity, MagnetCoerciveFieldStrength,
                MagnetThickness, MechamicalGap);
            Debug.WriteLine("Magnet flux density (T): " + MagnetFluxDensity);

            RotorThickness = MagnetThickness;

            MagnetPoleFlux = CalculateMaximumPoleFlux(MagnetFluxDensity, MagnetWidth, MagnetLength);
            Debug.WriteLine("Magnet pole flux: " + MagnetPoleFlux);

            //Create method!
            CoilCount = CoilsPerPhase * PhaseCount;

            MagnetCount = CalculatePolePairs(CoilCount);
            Debug.WriteLine("Magnet count: " + MagnetCount * 2);

            CoilTurns = CalculateCoilWindings(PhaseVoltageMin, MagnetCount, TurbineRpmMin, CoilsPerPhase, MagnetPoleFlux,
                CoilWindingCoefficient);
            Debug.WriteLine("Coil turns: " + CoilTurns);

            CoilLegWidth = CalculateCoilLegWidth(MaxPhaseCurrent, CoilTurns, CoilThickness);
            Debug.WriteLine("Coil leg width: " + CoilLegWidth);
            Debug.WriteLine("Generator RPM: " + GeneratorRpm);

            CoilCrossSectionalArea = CalculateCoilCrossSectionalArea(CoilLegWidth, CoilThickness, CoilTurns);
            Debug.WriteLine("Coil cross sectional area: " + CoilCrossSectionalArea);

            MaxCurrentDensity = CalculateMaximumCurrentDensity(MaxPhaseCurrent, CoilCrossSectionalArea);

            CoilWireDiameter = CalculateCoilWireDiameter(CoilCrossSectionalArea);
            Debug.WriteLine("Coil wire diameter: " + CoilWireDiameter);

            RotorInnerRadius = CalculateGeneratorInnerRadius(CoilCount, CoilLegWidth, MagnetCount, MagnetWidth);
            Debug.WriteLine("Rotor outer radius: " + RotorInnerRadius);

            RotorOuterRadius = CalculateCalculateGeneratorOuterRadius(RotorInnerRadius, MagnetLength);
            Debug.WriteLine("Rotor inner radius: " + RotorOuterRadius);

            RotorInnerOuterRadiusRatio = CalculateGeneratorInnerOuterRadiusRatio(RotorInnerRadius, RotorOuterRadius);
            Debug.WriteLine("Rotor inner outer radius ratio: " + RotorInnerOuterRadiusRatio);

            var coilInnerDimension = CoilInnerDimensions(CoilLegWidth, CoilCount, 10, 1, MagnetLength);
            var coilOuterDimension = CoilOuterDimensions(CoilLegWidth, CoilCount, 10, 1, MagnetLength);

            MagnetDistance = coilOuterDimension.Item2;
            MagnetPoleArcPitch = CalculateMagnetPoleArcPitch(MagnetWidth, MagnetDistance);

            CoilWireLength = CalculateCoilWireLength(CoilTurns, coilInnerDimension.Item4, coilOuterDimension.Item4);
            Debug.WriteLine("CoilWireLength: " + CoilWireLength);

            CoilResistance = CalculateCoilResistance(CoilWireLength, CoilWireDiameter);
            Debug.WriteLine("CoilResistance: " + CoilResistance);

            CoilInductance = CalculateCoilInductance(CoilTurns, CoilWireDiameter, CoilThickness);
            Debug.WriteLine("CoilInductance: " + CoilInductance);

            PhaseWireVoltageDrop = VoltageDrop(PhaseWireLength, PhaseWireDiameter, MaxPhaseCurrent, 3);
            PhaseWireResistance = CalculateWireResistance(PhaseWireLength, PhaseWireDiameter);

            //TODO: Check max phase current?
            RectifierWireVoltageDrop = VoltageDrop(RectifierWireLength, RectifierWireDiameter, MaxPhaseCurrent, 1);
            RectifierWireResistance = CalculateWireResistance(RectifierWireLength, RectifierWireDiameter);
        }


    }
}