using System;
using System.Collections.Generic;
using System.Diagnostics;

// ReSharper disable once RedundantUsingDirective

namespace AxialFluxGeneratorDesigner
{
    /// <summary>
    /// This class can be used to design a Axial Flux Permanent Magnet Generator.
    /// </summary>
    public class Afpm
    {
        #region Axial Flux designer properties

        #region Front end properties

        /// <summary>
        /// The turbine rpm max is the is the maximum revolutions per minute (rpm) the wind turbine (and thus the generator) shaft will rotate.
        /// This value depends on the tip ratio and the wind speed.
        /// </summary>
        public int FrontEndRpmMax { get; set; }

        /// <summary>
        /// The turbine rpm min is the is the minimal revolutions per minute (rpm) the wind turbine (and thus the generator) shaft will rotate.
        /// This value depends on the tip ratio and the wind speed.
        /// </summary>
        public int FrontEndRpmMin { get; set; }

        /// <summary>
        /// The torque of the front end (Nm) at the maximal power and rpm.
        /// </summary>
        public double FrontEndTorque { get; set; }

        /// <summary>
        /// The power coefficient (C<sub>p</sub>) is a measure of how efficiently the wind turbine converts the energy in the
        /// wind into electricity (usually 35 to 45 %). This value is default set to 0.35 (35%).
        /// To find the coefficient of power at a given wind speed, all you have to do is divide the electricity produced by the total energy available in the wind at that speed.
        /// <para> </para>
        /// Wind turbines extract energy by slowing down the wind. For a wind turbine to be 100% efficient it would need to stop
        /// 100% of the wind - but then the rotor would have to be a solid disk and it would not turn and no kinetic energy would be
        /// converted.On the other extreme, if you had a wind turbine with just one rotor blade, most of the wind passing through
        /// the area swept by the turbine blade would miss the blade completely and so the kinetic energy would be kept by the wind.
        /// </summary>
        public double TurbineMaximumPowerCoefficient { get; set; } = 0.35;

        /// <summary>
        /// The turbine rotor radius (R<sub>turbine</sub>) is the radius of the wind turbine blades (m).
        /// </summary>
        public double TurbineRotorRadius { get; set; }

        /// <summary>
        /// The speed tip ratio for the maximal rpm (ans so maximal wind speed).
        /// The tip-speed ratio (lambda) or Tip speed Ratio for wind turbines is the ratio between the tangential speed of the tip of a blade and the actual velocity of the wind, v. The tip-speed ratio is related to efficiency, with the optimum varying with blade design.
        /// Higher tip speeds result in higher noise levels and require stronger blades due to large centrifugal forces.
        /// </summary>

        public double TurbineSpeedTipRatioMax { get; set; } = 7;

        /// <summary>
        /// The speed tip ratio for the minimal rpm (and so minimal wind speed).
        /// The tip-speed ratio (lambda) or Tip speed Ratio for wind turbines is the ratio between the tangential speed of the tip of a blade and the actual velocity of the wind, v. The tip-speed ratio is related to efficiency, with the optimum varying with blade design.
        /// Higher tip speeds result in higher noise levels and require stronger blades due to large centrifugal forces.
        /// </summary>
        public double TurbineSpeedTipRatioMin { get; set; } = 8.75;

        /// <summary>
        /// The turbine maximal wind speed (m/s) that the turbine will experience.
        /// </summary>
        public double TurbineWindspeedMax { get; set; } = 10;

        /// <summary>
        /// The turbine minimal wind speed (m/s) that the turbine will experience.
        /// </summary>
        public double TurbineWindspeedMin { get; set; } = 3;

        /// <summary>
        /// The air density (kg/m<sup>3</sup>). This value is altitude dependent.
        /// </summary>
        public double TurbineAirDensity { get; set; } = 1.20;

        /// <summary>
        /// The voltage drop (V) that is caused by the length and diameter of the phase wires from the coil to the diode bridge.
        /// </summary>
        public double PhaseWireVoltageDrop { get; set; }

        /// <summary>
        /// The length (m) of a phase wire to the diode bridge rectifier.
        /// </summary>
        public double PhaseWireLength { get; set; }

        /// <summary>
        /// The diameter (mm) of a phase wire to the diode bridge rectifier.
        /// </summary>
        public double PhaseWireDiameter { get; set; }

        /// <summary>
        /// The resistance (Ohm) of a phase wire to the diode bridge rectifier.
        /// </summary>
        public double PhaseWireResistance { get; set; }

        /// <summary>
        /// The voltage drop (V) that is caused by the length and diameter of the wires from the diode bridge to the grid inverter/ battery.
        /// </summary>
        public double RectifierWireVoltageDrop { get; set; }

        /// <summary>
        /// The length (m) of a wire from the diode bridge to the grid inverter/ battery.
        /// </summary>
        public double RectifierWireLength { get; set; }

        /// <summary>
        /// The resistance (Ohm) of a wire from the diode bridge to the grid inverter/ battery.
        /// </summary>
        public double RectifierWireResistance { get; set; }

        /// <summary>
        /// The diameter (mm) of a wire from the diode bridge to the grid inverter/ battery.
        /// </summary>
        public double RectifierWireDiameter { get; set; }

        #endregion Front end properties

        #region Generator properties

        /// <summary>
        /// The minimal DC voltage output voltage (V).
        /// This value is default set to 200 volt.
        /// </summary>
        public double DcVoltageMin { get; set; } = 200;

        /// <summary>
        /// The maximal DC voltage output voltage (V).
        /// This value is default set to 700 volt.
        /// </summary>
        public double DcVoltageMax { get; set; } = 700;

        /// <summary>
        /// The maximum power (W) that the generator has to be capable to produce.
        /// </summary>
        public double GeneratorPower { get; set; } = 3000;

        /// <summary>
        /// The efficiency of the generator (%). This value is default set to 90%.
        /// </summary>
        public double GeneratorEfficiency { get; set; } = 0.9;

        /// <summary>
        /// The mechanical gap between the coil surface and the magnet surface. This value is default set to 3. Try to reduce this value as much as possible.
        /// However, keep in mind that the coils can become warm/hot and expand! This could lead to coils touching the magnets and thus damage.
        /// </summary>
        public double MechamicalGap { get; set; } = 3;

        /// <summary>
        /// This property determines the type of energy storage that is used.
        /// 0 = Battery
        /// 1 = grid
        /// This property is necessary because depending on the energy storage type different calculations are done
        /// </summary>
        public int GeneratorEnergyStorageConnection { get; set; }

        /// <summary>
        /// This property determines the front end type that is used to drive the generator.
        /// <para> </para>
        /// 0 = Wind turbine
        /// 1 = Other
        /// <para> </para>
        /// This property is necessary because depending on the front end type different calculations are done.
        /// </summary>
        public int GeneratorFrontEnd { get; set; }

        #endregion Generator properties

        #region Stator properties

        /// <summary>
        /// The maximal phase voltage that a sing phase has to produce.
        /// </summary>
        public double PhaseVoltageMax { get; set; }

        /// <summary>
        /// The minimal phase voltage that a sing phase has to produce.
        /// </summary>
        public double PhaseVoltageMin { get; set; }

        /// <summary>
        /// The phase count of the generator. The phase count is set to 3 and cannot be changed.
        /// This because the designer only works with 3-phase generators.
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

        #endregion Stator properties

        #region Stator coil properties

        /// <summary>
        /// The cross sectional area of a coil (mm<sup>2</sup>)
        /// </summary>
        public double CoilCrossSectionalArea { get; set; }

        /// <summary>
        /// The width of a coil leg (mm)
        /// </summary>
        public double CoilLegWidth { get; set; }

        /// <summary>
        /// In power engineering, winding factor is what makes the rms generated voltage in a three-phase AC electrical generator become lesser.
        /// This is because the armature winding of each phase is distributed in a number of slots. Since the emf induced in different slots are not in phase, their phase sum is less than their numerical sum.
        /// This reduction factor is called distribution factor Kd. Another factor that can reduce the winding factor is when the slot pitch is smaller than the pole pitch, called pitch factor Kp.
        /// <para> </para>
        /// The winding factor can be calculated as Kw = Kd * Kp.
        /// <para> </para>
        /// Most of the three-phase machines have winding factor values between 0.85 and 0.95.
        /// </summary>
        public double CoilWindingCoefficient { get; set; } = 0.95;

        /// <summary>
        /// The heat coefficient (W/m<sup>2</sup>) describes the heat dissipation of a coil surface. After the coils are casted in the resin two sides are exposed to the air.
        /// If no proper cooling is provides make sure that this value is lower then 3000 (2000 - 2500).
        /// The heat coefficient in the article is 0.3 (W/cm<sup>2</sup>). However has to be converted to (W/m<sup>2</sup>) (0.3 (W/cm<sup>2</sup>) becomes 3000 (W/m<sup>2</sup>) (multiplied by 10000)).
        /// </summary>
        public double CoilHeatCoefficient { get; set; } = 3000;

        /// <summary>
        /// Is the fraction of the core window area that is filled by copper.
        /// This value depends mainly on how good the coil is made.
        /// </summary>
        public double CoilFillFactor { get; set; } = 0.55;

        //TODO: Add more information
        /// <summary>
        /// The inductance of the coil is the ability to store energy in a magnetic field.
        /// </summary>
        public double CoilInductance { get; set; }

        /// <summary>
        /// The resistance of the coil (Ohm).
        /// </summary>
        public double CoilResistance { get; set; }

        /// <summary>
        /// The total wire length of a single coil (m).
        /// </summary>
        public double CoilWireLength { get; set; }

        /// <summary>
        /// The amount of turn per coil.
        /// </summary>
        public int CoilTurns { get; set; }

        /// <summary>
        /// The diameter of the coil wire (mm).
        /// </summary>
        public double CoilWireDiameter { get; set; }

        /// <summary>
        /// The thickness of the coil (mm).
        /// </summary>
        public double CoilThickness { get; set; }

        /// <summary>
        /// Current Density (A/mm<sup>2</sup>)is the measurement of electric current (charge flow in amperes) per unit area of cross-section (m2).
        /// </summary>
        public double MaxCurrentDensity { get; set; }

        /// <summary>
        /// The phase current is the current that flows through the coil at the maximal rpm.
        /// </summary>
        public double PhaseCurrent { get; set; }

        /// <summary>
        /// The phase current is the maximal current (phase current  + 10%) that flows through the coil.
        /// This can be caused by e.g. a storm or other factors. To prevent failure due to overheating this should be taken into consideration.
        /// </summary>
        public double MaxPhaseCurrent { get; set; }

        #endregion Stator coil properties

        #region Rotor properties

        /// <summary>
        /// Coercive field strength (Hc) (A/m) describes the force that is necessary to completely demagnetize a magnet.
        /// Simply said: the higher this number is, the better a magnet retains its magnetism when exposed to an opposing magnetic field.
        /// </summary>
        public double MagnetCoerciveFieldStrength { get; set; }

        /// <summary>
        /// The total amount of magnets on two rotor plates
        /// </summary>
        public int MagnetCount { get; set; }

        /// <summary>
        /// The distance between individual magnets (mm).
        /// </summary>
        public double MagnetDistance { get; set; }

        /// <summary>
        /// The magnet grade determines the .....
        /// </summary>
        public string MagnetGrade { get; set; }

        /// <summary>
        /// The magnetic flux density of a magnet is also called "B field" or "magnetic induction". It is measured in Tesla (SI unit) or gauss (10 000 gauss = 1 Tesla).
        /// A permanent magnet produces a B field in its core and in its external surroundings.
        /// <para> </para>
        /// A B field strength with a direction can be attributed to each point within and outside of the magnet.
        /// If you position a small compass needle in the B field of a magnet, it orients itself toward the field direction.
        /// The justifying force is proportional to the strength of the B field.
        /// </summary>
        public double MagnetFluxDensity { get; set; }

        /// <summary>
        /// The length of a magnet (mm). Default set to 30.
        /// </summary>
        public double MagnetLength { get; set; } = 30;

        /// <summary>
        /// ??
        /// </summary>
        public double MagnetPoleArcPitch { get; set; }

        /// <summary>
        /// The flux (T) of a magnet to the coil (with mechanical gap included).
        /// </summary>
        public double MagnetPoleFlux { get; set; }

        /// <summary>
        /// This list contains magnet grades with the associated Magnet remanent flux density (T) and the Magnet coercive field strength (A/m).
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
        /// TODO: Add documentation!
        /// </summary>
        public double MagnetRemanentFluxDensity { get; set; }

        /// <summary>
        /// the magnet thickness (mm).
        /// </summary>
        public double MagnetThickness { get; set; } = 10;

        /// <summary>
        /// The magnet width (mm).
        /// </summary>
        public double MagnetWidth { get; set; } = 46;

        /// <summary>
        /// TODO: Add documentation!
        /// </summary>
        public double RotorInnerOuterRadiusRatio { get; set; }

        /// <summary>
        /// TODO: Add documentation!
        /// </summary>
        public double RotorInnerRadius { get; set; }

        /// <summary>
        /// TODO: Add documentation!
        /// </summary>
        public double RotorOuterRadius { get; set; }

        /// <summary>
        /// TODO: Add documentation!
        /// </summary>
        public double RotorThickness { get; set; }

        #endregion Rotor properties

        #region Rectifier properties

        /// <summary>
        /// TODO: Add documentation!
        /// </summary>
        public double RectifierDiodeVoltageDrop { get; set; } = 1.4;

        #endregion Rectifier properties

        #endregion Axial Flux designer properties

        #region Axial Flux Designer methods

        #region Front end methods

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
        ///     This method calculates the turbine rotor radius to achieve the nominal power.
        /// </summary>
        /// <param name="generatorNominalPower"> The maximal generator power (W)</param>
        /// <param name="airDensity"> The air density (kg/m3)</param>
        /// <param name="maximumPowerCoefficient">The power coefficient (0.35 (35%)) is normal</param>
        /// <param name="windSpeed">The maximum wind speed (m/s)</param>
        /// <param name="generatorEfficiency"> The generator efficiency (0.9 (90%)) is normal</param>
        /// <returns>The rotor radius (m)</returns>
        public double CalculateTurbineRotorRadius(double generatorNominalPower, double airDensity,
            double maximumPowerCoefficient, double windSpeed, double generatorEfficiency)
        {
            var aerodynamicPower = generatorNominalPower / generatorEfficiency;

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
        /// This method calculates the maximal battery voltage based on the ratio between the max and min rpm times the minimal phase voltage.
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

        #endregion Front end methods

        #region Stator and coil methods

        /// <summary>
        /// This method calculated the coil angle (Deg).
        /// </summary>
        /// <param name="coilCount">The total amount of coils.</param>
        /// <returns>The angle for each coil (deg).</returns>
        public double CalculateCoilAngle(int coilCount)
        {
            if (coilCount == 0)
            {
                return 0;
            }
            else
            {
                // ReSharper disable once PossibleLossOfFraction
                return 360 / coilCount;
            }
        }

        /// <summary>
        ///     This method calculates the phase voltage for a 3 phase Y-configuration from the provided DC voltage.
        ///     Vdc = ((3*SQRT(2))/PI) * Vrms
        ///     Vdc = 1.35 * Vrms
        /// </summary>
        /// <param name="dcVoltage">DC voltage (V)</param>
        /// <param name="diodeVoltageDrop">Drop voltage losses in various power cables (V)</param>
        /// <returns>Phase voltage (rms) (V)</returns>
        public double CalculatePhaseVoltage(double dcVoltage, double diodeVoltageDrop)
        {
            //return (dc_voltage + 1.4) / (Math.Sqrt(3) * (Math.Sqrt(2) * (3 / Math.PI)));
            return (dcVoltage + diodeVoltageDrop) / (Math.Sqrt(3) * (Math.Sqrt(2)));
        }

        /// <summary>
        /// This method calculates the corrected (for voltage drop due to power lines and diode rectifier) DC voltage.
        /// </summary>
        /// <param name="phaseVoltage">The phase voltage (V)</param>
        /// <param name="diodeVoltageDrop">Drop voltage losses in various power cables (V)</param>
        /// <returns></returns>
        public double CalculateDcVoltage(double phaseVoltage, double diodeVoltageDrop)
        {
            return (phaseVoltage * (Math.Sqrt(3) * (Math.Sqrt(2))) - RectifierDiodeVoltageDrop - diodeVoltageDrop);
        }

        /// <summary>
        ///     This method calculates the inductance of a coil (mH).
        ///     http://coil32.net/multi-layer-coil.html
        /// <para> </para>
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
        ///     This method calculates the resistance of a copper wire.
        /// </summary>
        /// <param name="wireLength"></param>
        /// <param name="wireDiameter"></param>
        /// <returns>The resistance of the copper wire (Ohm)</returns>
        public double CalculateWireResistance(double wireLength, double wireDiameter)
        {
            const double copperResistivity = 0.0000000168;
            var wireRadius = wireDiameter / 2;
            var wireResistance = (copperResistivity * wireLength) / (Math.PI * Math.Pow(MillimetersToMeters(wireRadius), 2));

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

            if (double.IsNaN(voltageDrop) || double.IsInfinity(voltageDrop))
            {
                voltageDrop = 0.0;
            }

            return voltageDrop;
        }

        /// <summary>
        ///     This method calculates the resistance of a coil (ohm).
        /// </summary>
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
            const double ksat = 1;
            var statorThickness = CalculateStatorThickness(magnetThickness, gap);
            const double vacuumPermeability = (0.4 * Math.PI) / 1000000;
            var coilPermeability = (1 / vacuumPermeability) * (remanentFluxDensity / (coerciveFieldStrength * 1000));
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
        /// <param name="axialThickness">The stator thickness (m)</param>
        /// <returns>Returns the coil leg width (mm)</returns>
        public double CalculateCoilLegWidth(double maxPhaseCurrent, int coilWindings, double axialThickness)
        {
            const double copperResistivity = 0.0000000168;
            var coilLegWidth = (maxPhaseCurrent * coilWindings) /
                               Math.Sqrt((2 * CoilHeatCoefficient * CoilFillFactor * MillimetersToMeters(axialThickness)) /
                                         copperResistivity);
            return coilLegWidth * 1000;
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
            //Debug.WriteLine("----------Inner Coil dimensions----------");

            var coilAngle = CalculateCoilAngle(coilCount);

            //Calculate the minimal circumference
            var circumferenceMin = ((coilLegWidth * 2) * coilCount) + (coilCount * coilGap);
            //Debug.WriteLine("circumference_min: " + circumferenceMin);

            var radiusMin = (circumferenceMin / Math.PI) / 2;
            //Debug.WriteLine("radius_min: " + radiusMin);

            var lowerMinArc = CalculateCoilArcLength(coilAngle, radiusMin);

            var radiusMax = radiusMin + magnetHeight + (2 * coilLegWidth);
            //Debug.WriteLine("radius_max: " + radiusMax);

            var upperMinArc = CalculateCoilArcLength(coilAngle, radiusMax) - (2 * coilLegWidth);
            //Debug.WriteLine("upper_min_arc: " + upperMinArc);

            var coilHeightMin = magnetHeight;
            //Debug.WriteLine("coilHeightMin: " + coilHeightMin);

            var coilInnerCircumference = (coilHeightMin * 2) + lowerMinArc + upperMinArc;
            //Debug.WriteLine("coilInnerCircumference: " + coilInnerCircumference);

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
            //Debug.WriteLine("----------Inner Coil dimensions----------");

            var coilAngle = CalculateCoilAngle(coilCount);

            //Calculate the minimal circumference
            var circumferenceMin = ((coilLegWidth * 2) * coilCount) + (coilCount * coilGap);
            //Debug.WriteLine("circumference_min: " + circumferenceMin);

            var radiusMin = (circumferenceMin / Math.PI) / 2;
            //Debug.WriteLine("radius_min: " + radiusMin);

            var lowerMaxArc = CalculateCoilArcLength(coilAngle, radiusMin);
            //Debug.WriteLine("lower_max_arc: " + lowerMaxArc);

            var radiusMax = radiusMin + magnetHeight + (2 * coilLegWidth);
            //Debug.WriteLine("radius_max: " + radiusMax);

            var upperMaxArc = CalculateCoilArcLength(coilAngle, radiusMax);
            //Debug.WriteLine("upper_max_arc: " + upperMaxArc);

            var coilHeightMax = (coilLegWidth * 2) + magnetHeight;
            //Debug.WriteLine("coilHeightMax: " + coilHeightMax);

            var coilOuterCircumference = (coilHeightMax * 2) + lowerMaxArc + upperMaxArc;
            //Debug.WriteLine("coilOuterCircumference: " + coilOuterCircumference);

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

        #endregion Stator and coil methods

        #region Rotor methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phaseCount"></param>
        /// <param name="coilPhaseCount"></param>
        /// <returns></returns>
        public int CalculateCoilCount(int phaseCount, int coilPhaseCount)
        {
            return phaseCount * coilPhaseCount;
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

        #endregion Rotor methods

        #region New coil calculations


        /// <summary>
        /// This method calculates the inner radius of the stator.
        /// </summary>
        /// <param name="coilCount">The total amount of coils.</param>
        /// <param name="coilLegWidth">The coil leg width (mm)</param>
        /// <param name="betweenCoilDistance">The distance between two coils (mm)</param>
        /// <returns>The inner radius of the stator (mm)</returns>
        public double CalculateStatorInnerRadius(double coilCount, double coilLegWidth, double betweenCoilDistance)
        {
            return CalculateNGonInnerRadius(coilLegWidth + betweenCoilDistance, coilCount);
        }

        /// <summary>
        /// This method calculates the outer radius of the stator.
        /// </summary>
        /// <param name="coilCount">The total amount of coils.</param>
        /// <param name="coilLegWidth">The coil leg width (mm)</param>
        /// <param name="betweenCoilDistance">The distance between two coils (mm)</param>
        /// <param name="magnetHeight">The height of the used magnet (mm)</param>
        /// <returns>The outer radius of the stator (mm)</returns>
        public double CalculateStatorOuterRadius(double coilCount, double coilLegWidth, double betweenCoilDistance, double magnetHeight)
        {
            double rotorInnerRadius = CalculateNGonInnerRadius(coilLegWidth + betweenCoilDistance, coilCount);
            return rotorInnerRadius + coilLegWidth + magnetHeight + coilLegWidth;
        }

        /// <summary>
        /// This method calculated the length of a single vertex of a polygon. 
        /// This method is mainly used to calculate the outer vertex length of a coil.
        /// </summary>
        /// <param name="innerNGonRadius">The inner radius of the polygon (mm)</param>
        /// <param name="verticesCount"> The amount of vertices</param>
        /// <returns>The length of a vertex (mm)</returns>
        public double CalculateCoilVertexLength(double innerNGonRadius, int verticesCount)
        {
            return innerNGonRadius * (2 * Math.Sin(Math.PI / verticesCount));
        }

        /// <summary>
        /// Calculate the outer radius of a polygon
        /// Excircle radius (re):  = a / ( 2 * sin(π/n) ) is the official formula. 
        /// Where a is the vertex length and n is the amount of vertices.
        /// </summary>
        /// <param name="vertexLenght">The vertex length (mm)</param>
        /// <param name="verticesCount">The amount of vertices</param>
        /// <returns>The outer radius of a polygon (mm)</returns>
        public double CalculateNGonOuterRadius(double vertexLenght, double verticesCount)
        {
            return vertexLenght / (2 * Math.Sin(Math.PI / verticesCount));
        }

        /// <summary>
        /// Calculate the inner radius of a polygon
        /// Incircle radius (ri):  = a / ( 2 * tan(π/n) ) is the official formula. 
        /// Where a is the vertex length and n is the amount of vertices.
        /// </summary>
        /// <param name="vertexLenght">The vertex length (mm)</param>
        /// <param name="verticesCount">The amount of vertices</param>
        /// <returns></returns>
        public double CalculateNGonInnerRadius(double vertexLenght, double verticesCount)
        {
            return vertexLenght / (2 * Math.Tan(Math.PI / verticesCount));
        }

        /// <summary>
        /// This method calculates the outer radius of a corner of a coil
        /// </summary>
        /// <param name="coilInnerRadius"></param>
        /// <param name="coilLegWidth"></param>
        /// <returns></returns>
        public double CalculateCoilOuterRadius(double coilInnerRadius, double coilLegWidth)
        {
            return coilInnerRadius + coilLegWidth;
        }

        #endregion

        #region General methods

        /// <summary>
        /// </summary>
        /// <param name="mm"></param>
        /// <returns></returns>
        private double MillimetersToMeters(double mm)
        {
            return mm / 1000;
        }

        private void DebugPrint(bool debug, string text, double value)
        {
            Debug.WriteLine(text + ": " + value);
        }
        #endregion General methods

        #region Generator methods

        /// <summary>
        /// This method can be called to update all calculations.
        /// The principle is that the generator is driven by an external force applied to the generator shaft. This force is expressed and RPM and has a minimal and maximal value. The input RPM depends on various factors.
        /// To accommodate the user there is the possibility to calculate the RPM values using an turbine. However, there is also the possibility to enter direct RPM values.This can be useful if another device is used (e.g. water wheel or Stirling engine).
        /// The input devices are called the front end of the generator or generator input. Besides two input options there is also the possibility to store/return the produced energy. There is the possibility to store the produced energy in a battery or the produced energy is returned to the grid using an inverter.
        /// In case of a battery we only know the minimal voltage we want (the battery voltage). The maximal depends on the wind. Based on those.....
        /// - Wind turbine + battery calculations:
        /// Calculate the turbine radius.
        /// Calculate the min and max rpm based on the wind speed and tip ratio.
        /// Calculate the minimal phase voltage using the battery voltage (e.g. 48 volt)
        /// Calculate the maximal phase voltage by multiplying the minimal phase voltage with the ratio between the max and min rpm (RPM Max / RPM Min).
        /// - Other + battery calculations:
        ///  Calculate the maximal phase voltage by multiplying the minimal phase voltage (e.g. 48 volt) with the ratio between the set max and min rpm (RPM Max / RPM Min).
        /// - Wind turbine + grid calculations:
        /// Calculate the turbine radius.
        /// Calculate the minimal AND maximal phase voltage using the inverter min and max voltages.
        /// Calculate the minimal rpm by multiplying the minimal rpm with the ratio between the min and max phase voltage (Phase voltage min / Phase voltage max).
        /// Calculate the minimal wind speed based on this minimal RPM value.
        /// - Other + grid calculations:
        /// Calculate the minimal AND maximal phase voltage using the inverter min and max voltages.
        /// Calculate the minimal rpm by multiplying the minimal rpm with the ratio between the min and max phase voltage (Phase voltage min / Phase voltage max).
        /// Calculate the minimal wind speed based on this minimal RPM value.
        /// </summary>
        public void UpdateCalculations(bool debug)
        {
            //TODO: Check calculations
            PhaseWireVoltageDrop = VoltageDrop(PhaseWireLength, PhaseWireDiameter, MaxPhaseCurrent, 3);
            PhaseWireResistance = CalculateWireResistance(PhaseWireLength, PhaseWireDiameter);

            //TODO: Check max phase current?
            RectifierWireVoltageDrop = VoltageDrop(RectifierWireLength, RectifierWireDiameter, MaxPhaseCurrent, 1);
            RectifierWireResistance = CalculateWireResistance(RectifierWireLength, RectifierWireDiameter);

            //Battery connection
            if (GeneratorEnergyStorageConnection == 0)
            {
                //Turbine
                if (GeneratorFrontEnd == 0)
                {
                    PhaseVoltageMin = CalculatePhaseVoltage(DcVoltageMin, RectifierDiodeVoltageDrop + RectifierWireVoltageDrop) + PhaseWireVoltageDrop;
                    TurbineRotorRadius = CalculateTurbineRotorRadius(GeneratorPower, TurbineAirDensity, TurbineMaximumPowerCoefficient, TurbineWindspeedMax, GeneratorEfficiency);
                    FrontEndRpmMin = CalculateTurbineOptimalRotationSpeed(TurbineWindspeedMin, TurbineSpeedTipRatioMin, TurbineRotorRadius);
                    FrontEndRpmMax = CalculateTurbineOptimalRotationSpeed(TurbineWindspeedMax, TurbineSpeedTipRatioMax, TurbineRotorRadius);
                    PhaseVoltageMax = CalculateBatteryVoltage(FrontEndRpmMin, FrontEndRpmMax, PhaseVoltageMin);
                }
                //Other
                else if (GeneratorFrontEnd == 1)
                {
                    PhaseVoltageMin = ((CalculatePhaseVoltage(DcVoltageMin, RectifierDiodeVoltageDrop + RectifierWireVoltageDrop) + PhaseWireVoltageDrop) / GeneratorEfficiency);
                    //TODO: Correct for voltage drop?
                    PhaseVoltageMax = CalculateBatteryVoltage(FrontEndRpmMin, FrontEndRpmMax, PhaseVoltageMin) / GeneratorEfficiency;
                }
            }
            //Grid connection
            if (GeneratorEnergyStorageConnection == 1)
            {
                //Turbine
                if (GeneratorFrontEnd == 0)
                {
                    PhaseVoltageMin = CalculatePhaseVoltage(DcVoltageMin, RectifierDiodeVoltageDrop + RectifierWireVoltageDrop) + PhaseWireVoltageDrop;
                    PhaseVoltageMax = CalculatePhaseVoltage(DcVoltageMax, RectifierDiodeVoltageDrop + RectifierWireVoltageDrop) + PhaseWireVoltageDrop;
                    TurbineRotorRadius = CalculateTurbineRotorRadius(GeneratorPower, TurbineAirDensity, TurbineMaximumPowerCoefficient, TurbineWindspeedMax, GeneratorEfficiency);
                    FrontEndRpmMax = CalculateTurbineOptimalRotationSpeed(TurbineWindspeedMax, TurbineSpeedTipRatioMax, TurbineRotorRadius);
                    FrontEndRpmMin = CalculateGridRpm(PhaseVoltageMin, PhaseVoltageMax, FrontEndRpmMax);
                    TurbineWindspeedMin = CalculateTurbineOptimalWindSpeed(FrontEndRpmMin, TurbineRotorRadius, TurbineSpeedTipRatioMin);
                }
                //Other
                else if (GeneratorFrontEnd == 1)
                {
                    PhaseVoltageMin = (CalculatePhaseVoltage(DcVoltageMin, RectifierDiodeVoltageDrop + RectifierWireVoltageDrop) + PhaseWireVoltageDrop) / GeneratorEfficiency;
                    PhaseVoltageMax = (CalculatePhaseVoltage(DcVoltageMax, RectifierDiodeVoltageDrop + RectifierWireVoltageDrop) + PhaseWireVoltageDrop) / GeneratorEfficiency;
                    FrontEndRpmMin = CalculateGridRpm(PhaseVoltageMin, PhaseVoltageMax, FrontEndRpmMax);
                }
            }

            FrontEndTorque = CalculateTorque(GeneratorPower, FrontEndRpmMax);

            MaxPhaseCurrent = CalculateMaximumPhaseCurrent(GeneratorPower, 286);
            //Debug.WriteLine("Max phase current (A): " + MaxPhaseCurrent);

            CoilThickness = CalculateStatorThickness(MagnetThickness, MechamicalGap);
            //Debug.WriteLine("Stator thickness (mm): " + CoilThickness);

            MagnetFluxDensity = CalculateMagnetFluxDensity(MagnetRemanentFluxDensity, MagnetCoerciveFieldStrength,
                MagnetThickness, MechamicalGap);
            //Debug.WriteLine("Magnet flux density (T): " + MagnetFluxDensity);

            RotorThickness = MagnetThickness;

            MagnetPoleFlux = CalculateMaximumPoleFlux(MagnetFluxDensity, MagnetWidth, MagnetLength);
            //Debug.WriteLine("Magnet pole flux: " + MagnetPoleFlux);

            CoilCount = CalculateCoilCount(PhaseCount, CoilsPerPhase);

            MagnetCount = CalculatePolePairs(CoilCount);
            //Debug.WriteLine("Magnet count: " + MagnetCount * 2);

            CoilTurns = CalculateCoilWindings(PhaseVoltageMin, MagnetCount, FrontEndRpmMin, CoilsPerPhase, MagnetPoleFlux,
                CoilWindingCoefficient);
            //Debug.WriteLine("Coil turns: " + CoilTurns);

            CoilLegWidth = CalculateCoilLegWidth(MaxPhaseCurrent, CoilTurns, CoilThickness);
            //Debug.WriteLine("Coil leg width: " + CoilLegWidth);
            //Debug.WriteLine("Generator RPM: " + GeneratorRpm);

            CoilCrossSectionalArea = CalculateCoilCrossSectionalArea(CoilLegWidth, CoilThickness, CoilTurns);
            //Debug.WriteLine("Coil cross sectional area: " + CoilCrossSectionalArea);

            MaxCurrentDensity = CalculateMaximumCurrentDensity(MaxPhaseCurrent, CoilCrossSectionalArea);

            CoilWireDiameter = CalculateCoilWireDiameter(CoilCrossSectionalArea);
            //Debug.WriteLine("Coil wire diameter: " + CoilWireDiameter);

            RotorInnerRadius = CalculateGeneratorInnerRadius(CoilCount, CoilLegWidth, MagnetCount, MagnetWidth);
            //Debug.WriteLine("Rotor outer radius: " + RotorInnerRadius);

            RotorOuterRadius = CalculateCalculateGeneratorOuterRadius(RotorInnerRadius, MagnetLength);
            //Debug.WriteLine("Rotor inner radius: " + RotorOuterRadius);

            RotorInnerOuterRadiusRatio = CalculateGeneratorInnerOuterRadiusRatio(RotorInnerRadius, RotorOuterRadius);
            //Debug.WriteLine("Rotor inner outer radius ratio: " + RotorInnerOuterRadiusRatio);

            var coilInnerDimension = CoilInnerDimensions(CoilLegWidth, CoilCount, 10, 1, MagnetLength);
            var coilOuterDimension = CoilOuterDimensions(CoilLegWidth, CoilCount, 10, 1, MagnetLength);

            MagnetDistance = coilOuterDimension.Item2;
            MagnetPoleArcPitch = CalculateMagnetPoleArcPitch(MagnetWidth, MagnetDistance);

            CoilWireLength = CalculateCoilWireLength(CoilTurns, coilInnerDimension.Item4, coilOuterDimension.Item4);
            //Debug.WriteLine("CoilWireLength: " + CoilWireLength);

            CoilResistance = CalculateCoilResistance(CoilWireLength, CoilWireDiameter);
            //Debug.WriteLine("CoilResistance: " + CoilResistance);

            CoilInductance = CalculateCoilInductance(CoilTurns, CoilWireDiameter, CoilThickness);
            //Debug.WriteLine("CoilInductance: " + CoilInductance);
        }

        #endregion Generator methods

        #endregion Axial Flux Designer methods
    }
}