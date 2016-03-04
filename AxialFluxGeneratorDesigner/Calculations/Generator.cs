#region

using System;
using System.Collections.Generic;

#endregion

namespace AxialFluxGeneratorDesigner.Calculations
{
    /// <summary>
    /// </summary>
    public class Generator
    {
        #region Front end properties

        /// <summary>
        ///     The turbine rpm max is the is the maximum revolutions per minute (rpm) the wind turbine (and thus the generator)
        ///     shaft will rotate.
        ///     This value depends on the tip ratio and the wind speed.
        /// </summary>
        public GeneratorProperty<double> FrontEndRpmMax = new GeneratorProperty<double>(nameof(FrontEndRpmMax), 100, 1,
            10000);

        /// <summary>
        ///     The turbine rpm min is the is the minimal revolutions per minute (rpm) the wind turbine (and thus the generator)
        ///     shaft will rotate.
        ///     This value depends on the tip ratio and the wind speed.
        /// </summary>
        public GeneratorProperty<double> FrontEndRpmMin = new GeneratorProperty<double>(nameof(FrontEndRpmMin), 100, 1,
            10000);

        /// <summary>
        ///     The torque of the front end (Nm) at the maximal power and rpm.
        /// </summary>
        public double FrontEndTorque { get; set; }

        /// <summary>
        ///     The power coefficient (C<sub>p</sub>) is a measure of how efficiently the wind turbine converts the energy in the
        ///     wind into electricity (usually 35 to 45 %). This value is default set to 0.35 (35%).
        ///     To find the coefficient of power at a given wind speed, all you have to do is divide the electricity produced by
        ///     the total energy available in the wind at that speed.
        ///     <para> </para>
        ///     Wind turbines extract energy by slowing down the wind. For a wind turbine to be 100% efficient it would need to
        ///     stop
        ///     100% of the wind - but then the rotor would have to be a solid disk and it would not turn and no kinetic energy
        ///     would be
        ///     converted.On the other extreme, if you had a wind turbine with just one rotor blade, most of the wind passing
        ///     through
        ///     the area swept by the turbine blade would miss the blade completely and so the kinetic energy would be kept by the
        ///     wind.
        /// </summary>
        public GeneratorProperty<double> TurbineMaximumPowerCoefficient =
            new GeneratorProperty<double>(nameof(TurbineMaximumPowerCoefficient), 0.35, 0.0, 1.0);

        /// <summary>
        ///     The turbine rotor radius (R<sub>turbine</sub>) is the radius of the wind turbine blades (m).
        /// </summary>
        public double TurbineRotorRadius { get; set; }

        /// <summary>
        ///     The speed tip ratio for the maximal rpm (ans so maximal wind speed).
        ///     The tip-speed ratio (lambda) or Tip speed Ratio for wind turbines is the ratio between the tangential speed of the
        ///     tip of a blade and the actual velocity of the wind, v. The tip-speed ratio is related to efficiency, with the
        ///     optimum varying with blade design.
        ///     Higher tip speeds result in higher noise levels and require stronger blades due to large centrifugal forces.
        /// </summary>
        public GeneratorProperty<double> TurbineSpeedTipRatioMax =
            new GeneratorProperty<double>(nameof(TurbineSpeedTipRatioMax), 8.75, 1, 100);

        /// <summary>
        ///     The speed tip ratio for the minimal rpm (and so minimal wind speed).
        ///     The tip-speed ratio (lambda) or Tip speed Ratio for wind turbines is the ratio between the tangential speed of the
        ///     tip of a blade and the actual velocity of the wind, v. The tip-speed ratio is related to efficiency, with the
        ///     optimum varying with blade design.
        ///     Higher tip speeds result in higher noise levels and require stronger blades due to large centrifugal forces.
        /// </summary>
        public GeneratorProperty<double> TurbineSpeedTipRatioMin =
            new GeneratorProperty<double>(nameof(TurbineSpeedTipRatioMin), 7.0, 1, 100);

        /// <summary>
        ///     The turbine maximal wind speed (m/s) that the turbine will experience.
        /// </summary>
        public GeneratorProperty<double> TurbineWindspeedMax = new GeneratorProperty<double>(
            nameof(TurbineWindspeedMax), 10.0, 0.1, 100);


        /// <summary>
        ///     The turbine minimal wind speed (m/s) that the turbine will experience.
        /// </summary>
        public GeneratorProperty<double> TurbineWindspeedMin = new GeneratorProperty<double>(
            nameof(TurbineWindspeedMin), 10.0, 0.1, 100);

        /// <summary>
        ///     The air density (kg/m<sup>3</sup>). This value is altitude dependent.
        /// </summary>
        public GeneratorProperty<double> TurbineAirDensity = new GeneratorProperty<double>(nameof(TurbineAirDensity),
            1.0, 1.0, 2.0);

        /// <summary>
        ///     The voltage drop (V) that is caused by the length and diameter of the phase wires from the coil to the diode
        ///     bridge.
        /// </summary>
        public double PhaseWireVoltageDrop { get; set; }

        /// <summary>
        ///     The length (m) of a phase wire to the diode bridge rectifier.
        /// </summary>
        public GeneratorProperty<double> PhaseWireLength = new GeneratorProperty<double>(nameof(PhaseWireLength), 0, 0,
            1000);

        /// <summary>
        ///     The diameter (mm) of a phase wire to the diode bridge rectifier.
        /// </summary>
        public GeneratorProperty<double> PhaseWireDiameter = new GeneratorProperty<double>(nameof(PhaseWireDiameter),
            0.0, 0.0, 10.0);

        /// <summary>
        ///     The resistance (Ohm) of a phase wire to the diode bridge rectifier.
        /// </summary>
        public double PhaseWireResistance { get; set; }

        /// <summary>
        ///     The voltage drop (V) that is caused by the length and diameter of the wires from the diode bridge to the grid
        ///     inverter/ battery.
        /// </summary>
        public double RectifierWireVoltageDrop { get; set; }

        /// <summary>
        ///     The length (m) of a wire from the diode bridge to the grid inverter/ battery.
        /// </summary>
        public GeneratorProperty<double> RectifierWireLength = new GeneratorProperty<double>(
            nameof(RectifierWireLength), 0, 0, 1000);

        /// <summary>
        ///     The resistance (Ohm) of a wire from the diode bridge to the grid inverter/ battery.
        /// </summary>
        public double RectifierWireResistance { get; set; }

        /// <summary>
        ///     The diameter (mm) of a wire from the diode bridge to the grid inverter/ battery.
        /// </summary>
        public GeneratorProperty<double> RectifierWireDiameter =
            new GeneratorProperty<double>(nameof(RectifierWireDiameter), 0.0, 0.0, 10.0);

        #endregion Front end properties

        #region Generator properties

        /// <summary>
        ///     The minimal DC voltage output voltage (V).
        ///     This value is default set to 200 volt.
        /// </summary>
        public GeneratorProperty<double> DcInverterVoltageMin =
            new GeneratorProperty<double>(nameof(DcInverterVoltageMin), 200, 1, 1000);

        /// <summary>
        ///     The maximal DC voltage output voltage (V).
        ///     This value is default set to 700 volt.
        /// </summary>
        public GeneratorProperty<double> DcInverterVoltageMax =
            new GeneratorProperty<double>(nameof(DcInverterVoltageMax), 700, 1, 1000);

        //TODO: Change in code
        /// <summary>
        ///     The DC voltage output voltage ob the used battery(V).
        ///     This value is default set to 48 volt.
        /// </summary>
        public GeneratorProperty<double> DcBatteryVoltage = new GeneratorProperty<double>(nameof(DcBatteryVoltage), 48,
            1, 1000);

        /// <summary>
        ///     The maximum power (W) that the generator has to be capable to produce.
        /// </summary>
        public GeneratorProperty<double> GeneratorPower = new GeneratorProperty<double>(nameof(GeneratorPower), 3000, 1,
            10000);

        /// <summary>
        ///     The efficiency of the generator (%). This value is default set to 90%.
        /// </summary>
        public GeneratorProperty<double> GeneratorEfficiency = new GeneratorProperty<double>(
            nameof(GeneratorEfficiency), 0.9, 0, 1.0);

        /// <summary>
        ///     The mechanical gap between the coil surface and the magnet surface. This value is default set to 3. Try to reduce
        ///     this value as much as possible.
        ///     However, keep in mind that the coils can become warm/hot and expand! This could lead to coils touching the magnets
        ///     and thus damage.
        /// </summary>
        public GeneratorProperty<double> MechamicalGap = new GeneratorProperty<double>(nameof(MechamicalGap), 3, 1, 10);

        /// <summary>
        ///     This property determines the type of energy storage that is used.
        ///     0 = Battery
        ///     1 = grid
        ///     This property is necessary because depending on the energy storage type different calculations are done
        /// </summary>
        public GeneratorProperty<double> GeneratorEnergyStorageConnection =
            new GeneratorProperty<double>(nameof(GeneratorEnergyStorageConnection), 0, 0, 1);

        /// <summary>
        ///     This property determines the front end type that is used to drive the generator.
        ///     <para> </para>
        ///     0 = Wind turbine
        ///     1 = Other
        ///     <para> </para>
        ///     This property is necessary because depending on the front end type different calculations are done.
        /// </summary>
        public GeneratorProperty<double> GeneratorFrontEnd = new GeneratorProperty<double>(nameof(GeneratorFrontEnd), 0,
            0, 1);

        #endregion Generator properties

        #region Stator properties

        /// <summary>
        ///     The stator inner radius is the distance (mm) from the center to the bottom of the coil sequence.
        /// </summary>
        public double StatorInnerRadius { get; set; }

        /// <summary>
        ///     The stator outer radius is the distance (mm) from the center to the top of the coil sequence.
        /// </summary>
        public double StatorOuterRadius { get; set; }

        /// <summary>
        ///     The distance between two coils (mm).
        /// </summary>
        public GeneratorProperty<double> BetweenCoilDistance = new GeneratorProperty<double>(
            nameof(BetweenCoilDistance), 5, 0, 50);

        /// <summary>
        ///     The maximal phase voltage that a sing phase has to produce.
        /// </summary>
        public double PhaseVoltageMax { get; set; }

        /// <summary>
        ///     The minimal phase voltage that a sing phase has to produce.
        /// </summary>
        public double PhaseVoltageMin { get; set; }

        /// <summary>
        ///     The phase count of the generator. The phase count is set to 3 and cannot be changed.
        ///     This because the designer only works with 3-phase generators.
        /// </summary>
        private int PhaseCount { get; } = 3;

        /// <summary>
        ///     The coil count is the total amount of coils for the generator.
        ///     So 5 coils/phase and 3 phases results in a coil count of 5 * 3 = 15
        /// </summary>
        public int CoilCount { get; set; }

        /// <summary>
        ///     The coils per phase are the amount of coils in each phase.
        /// </summary>
        public GeneratorProperty<double> CoilsPerPhase = new GeneratorProperty<double>(nameof(CoilsPerPhase), 5, 1, 10);

        #endregion Stator properties

        #region Stator coil properties

        /// <summary>
        ///     The cross sectional area of a coil (mm<sup>2</sup>)
        /// </summary>
        public double CoilCrossSectionalArea { get; set; }

        /// <summary>
        ///     The width of a coil leg (mm)
        /// </summary>
        public double CoilLegWidth { get; set; }

        /// <summary>
        ///     In power engineering, winding factor is what makes the rms generated voltage in a three-phase AC electrical
        ///     generator become lesser.
        ///     This is because the armature winding of each phase is distributed in a number of slots. Since the emf induced in
        ///     different slots are not in phase, their phase sum is less than their numerical sum.
        ///     This reduction factor is called distribution factor Kd. Another factor that can reduce the winding factor is when
        ///     the slot pitch is smaller than the pole pitch, called pitch factor Kp.
        ///     <para> </para>
        ///     The winding factor can be calculated as Kw = Kd * Kp.
        ///     <para> </para>
        ///     Most of the three-phase machines have winding factor values between 0.85 and 0.95.
        /// </summary>
        public double CoilWindingCoefficient { get; set; } = 0.95;

        /// <summary>
        ///     The heat coefficient (W/m<sup>2</sup>) describes the heat dissipation of a coil surface. After the coils are casted
        ///     in the resin two sides are exposed to the air.
        ///     If no proper cooling is provides make sure that this value is lower then 3000 (2000 - 2500).
        ///     The heat coefficient in the article is 0.3 (W/cm<sup>2</sup>). However has to be converted to (W/m<sup>2</sup>)
        ///     (0.3 (W/cm<sup>2</sup>) becomes 3000 (W/m<sup>2</sup>) (multiplied by 10000)).
        /// </summary>
        public double CoilHeatCoefficient { get; set; }

        /// <summary>
        ///     Is the fraction of the core window area that is filled by copper.
        ///     This value depends mainly on how good the coil is made.
        /// </summary>
        public GeneratorProperty<double> CoilFillFactor = new GeneratorProperty<double>(nameof(CoilFillFactor), 0.55,
            0.1, 1.0);

        //TODO: Add more information
        /// <summary>
        ///     The inductance of the coil is the ability to store energy in a magnetic field.
        /// </summary>
        public double CoilInductance { get; set; }

        /// <summary>
        ///     The resistance of the coil (Ohm).
        /// </summary>
        public double CoilResistance { get; set; }

        /// <summary>
        ///     The total wire length of a single coil (m).
        /// </summary>
        public double CoilWireLength { get; set; }

        /// <summary>
        ///     The amount of turn per coil.
        /// </summary>
        public int CoilTurns { get; set; }

        /// <summary>
        ///     The diameter of the coil wire (mm).
        /// </summary>
        public double CoilWireDiameter { get; set; }

        /// <summary>
        ///     The thickness of the coil (mm).
        /// </summary>
        public double CoilThickness { get; set; }

        /// <summary>
        ///     Current Density (A/mm<sup>2</sup>)is the measurement of electric current (charge flow in amperes) per unit area of
        ///     cross-section (m2).
        /// </summary>
        public GeneratorProperty<double> MaxCurrentDensity = new GeneratorProperty<double>(nameof(MaxCurrentDensity), 5,
            1, 20);

        /// <summary>
        ///     The phase current is the maximal current (phase current  + 10%) that flows through the coil.
        ///     This can be caused by e.g. a storm or other factors. To prevent failure due to overheating this should be taken
        ///     into consideration.
        /// </summary>
        public double MaxPhaseCurrent { get; set; }

        #endregion Stator coil properties

        #region Rotor properties

        /// <summary>
        ///     Coercive field strength (Hc) (A/m) describes the force that is necessary to completely demagnetize a magnet.
        ///     Simply said: the higher this number is, the better a magnet retains its magnetism when exposed to an opposing
        ///     magnetic field.
        /// </summary>
        public double MagnetCoerciveFieldStrength { get; set; }

        /// <summary>
        ///     The total amount of magnets on two rotor plates
        /// </summary>
        public int MagnetCount { get; set; }

        /// <summary>
        ///     The magnet grade determines the .....
        /// </summary>
        public GeneratorProperty<double> MagnetGrade = new GeneratorProperty<double>(nameof(MagnetGrade), 3, 0, 10);

        /// <summary>
        ///     The magnetic flux density of a magnet is also called "B field" or "magnetic induction". It is measured in Tesla (SI
        ///     unit) or gauss (10 000 gauss = 1 Tesla).
        ///     A permanent magnet produces a B field in its core and in its external surroundings.
        ///     <para> </para>
        ///     A B field strength with a direction can be attributed to each point within and outside of the magnet.
        ///     If you position a small compass needle in the B field of a magnet, it orients itself toward the field direction.
        ///     The justifying force is proportional to the strength of the B field.
        /// </summary>
        public double MagnetFluxDensity { get; set; }

        /// <summary>
        ///     The length of a magnet (mm). Default set to 30.
        /// </summary>
        public GeneratorProperty<double> MagnetHeight = new GeneratorProperty<double>(nameof(MagnetHeight), 30, 1, 150);

        //TODO: Add documentation
        /// <summary>
        ///     ??
        /// </summary>
        public GeneratorProperty<double> MagnetPoleArcToPolePitchRatio =
            new GeneratorProperty<double>(nameof(MagnetPoleArcToPolePitchRatio), 2.0/Math.PI, 0.3,
                0.9);

        /// <summary>
        ///     TODO: Add Documentation.
        /// </summary>
        public double MagnetPoleToPolePitch { get; set; }

        /// <summary>
        ///     The distance between individual magnets (mm).
        /// </summary>
        public double MagnetBetweenDistance { get; set; }

        /// <summary>
        /// </summary>
        public double MagnetTotalSegmentAngle { get; set; }

        /// <summary>
        /// </summary>
        public double MagnetSegmentAngle { get; set; }

        /// <summary>
        ///     The flux (T) of a magnet to the coil (with mechanical gap included).
        /// </summary>
        public double MagnetPoleFlux { get; set; }

        /// <summary>
        ///     This list contains magnet grades with the associated Magnet remanent flux density (T) and the Magnet coercive field
        ///     strength (A/m).
        /// </summary>
        public readonly List<Tuple<int, string, double, double>> MagnetProperties = new List
            <Tuple<int, string, double, double>>
        {
            new Tuple<int, string, double, double>(0, "N30", 1.1, 808.0),
            new Tuple<int, string, double, double>(1, "N33", 1.155, 848.0),
            new Tuple<int, string, double, double>(2, "N35", 1.19, 887.5),
            new Tuple<int, string, double, double>(3, "N38", 1.24, 887.5),
            new Tuple<int, string, double, double>(4, "N40", 1.275, 927.5),
            new Tuple<int, string, double, double>(5, "N42", 1.305, 927.5),
            new Tuple<int, string, double, double>(6, "N45", 1.345, 927.5),
            new Tuple<int, string, double, double>(7, "N48", 1.395, 927.5),
            new Tuple<int, string, double, double>(8, "N50", 1.430, 927.5),
            new Tuple<int, string, double, double>(9, "N52", 1.445, 927.5)
        };

        /// <summary>
        ///     TODO: Add documentation!
        /// </summary>
        public double MagnetRemanentFluxDensity { get; set; }

        /// <summary>
        ///     the magnet thickness (mm).
        /// </summary>
        public GeneratorProperty<double> MagnetThickness = new GeneratorProperty<double>(nameof(MagnetThickness), 10, 1,
            50);

        /// <summary>
        ///     The magnet width (mm).
        /// </summary>
        public GeneratorProperty<double> MagnetWidth = new GeneratorProperty<double>(nameof(MagnetWidth), 46, 1, 150);

        /// <summary>
        ///     The ratio between the inner and outer ratio.
        /// </summary>
        public double RotorInnerOuterRadiusRatio { get; set; }

        /// <summary>
        ///     The inner radius (mm) measured from the center to the bottom of the magnet sequence.
        /// </summary>
        public double RotorInnerRadius { get; set; }

        /// <summary>
        ///     The outer radius (mm) measured from the center to the top of the magnet sequence.
        /// </summary>
        public double RotorOuterRadius { get; set; }

        /// <summary>
        ///     The thickness (mm) of the steel rotor plate. To this plate the magnets are mounted.
        /// </summary>
        public double RotorThickness { get; set; }

        /// <summary>
        ///     The angle in between two magnets.
        /// </summary>
        private double MagnetBetweenSegmentAngle { get; set; }

        /// <summary>
        ///     The angle from the center of the stator that the coil covers.
        /// </summary>
        public double CoilAngle { get; set; }

        /// <summary>
        ///     The radius of the inner coil rounding.
        /// </summary>
        private double CoilInnerRadius { get; } = 5;

        /// <summary>
        ///     The radius of the inner coil rounding.
        /// </summary>
        public double CoilAverageTurnLength { get; set; }

        /// <summary>
        ///     The surface of a single coil side (cm2)
        /// </summary>
        public double CoilSideSurface { get; set; }

        /// <summary>
        /// </summary>
        public double CoilInnerTop { get; set; }

        /// <summary>
        /// </summary>
        public double CoilInnerBottom { get; set; }

        /// <summary>
        /// </summary>
        public double CoilInnerSide { get; set; }

        /// <summary>
        /// </summary>
        public double CoilOuterTop { get; set; }

        /// <summary>
        /// </summary>
        public double CoilOuterBottom { get; set; }

        /// <summary>
        /// </summary>
        public double CoilOuterSide { get; set; }

        #endregion Rotor properties

        #region Rectifier properties

        /// <summary>
        ///     The rectifier diode voltage drop is the voltage drop caused by the passive components of the AD to DC rectifier.
        /// </summary>
        public double RectifierDiodeVoltageDrop { get; } = 1.4;

        #endregion Rectifier properties

        /// <summary>
        ///     This method can be called to update all calculations.
        ///     The principle is that the generator is driven by an external force applied to the generator shaft. This force is
        ///     expressed and RPM and has a minimal and maximal value. The input RPM depends on various factors.
        ///     To accommodate the user there is the possibility to calculate the RPM values using an turbine. However, there is
        ///     also the possibility to enter direct RPM values.This can be useful if another device is used (e.g. water wheel or
        ///     Stirling engine).
        ///     The input devices are called the front end of the generator or generator input. Besides two input options there is
        ///     also the possibility to store/return the produced energy. There is the possibility to store the produced energy in
        ///     a battery or the produced energy is returned to the grid using an inverter.
        ///     In case of a battery we only know the minimal voltage we want (the battery voltage). The maximal depends on the
        ///     wind. Based on those.....
        ///     - Wind turbine + battery calculations:
        ///     Calculate the turbine radius.
        ///     Calculate the min and max rpm based on the wind speed and tip ratio.
        ///     Calculate the minimal phase voltage using the battery voltage (e.g. 48 volt)
        ///     Calculate the maximal phase voltage by multiplying the minimal phase voltage with the ratio between the max and min
        ///     rpm (RPM Max / RPM Min).
        ///     - Other + battery calculations:
        ///     Calculate the maximal phase voltage by multiplying the minimal phase voltage (e.g. 48 volt) with the ratio between
        ///     the set max and min rpm (RPM Max / RPM Min).
        ///     - Wind turbine + grid calculations:
        ///     Calculate the turbine radius.
        ///     Calculate the minimal AND maximal phase voltage using the inverter min and max voltages.
        ///     Calculate the minimal rpm by multiplying the minimal rpm with the ratio between the min and max phase voltage
        ///     (Phase voltage min / Phase voltage max).
        ///     Calculate the minimal wind speed based on this minimal RPM value.
        ///     - Other + grid calculations:
        ///     Calculate the minimal AND maximal phase voltage using the inverter min and max voltages.
        ///     Calculate the minimal rpm by multiplying the minimal rpm with the ratio between the min and max phase voltage
        ///     (Phase voltage min / Phase voltage max).
        ///     Calculate the minimal wind speed based on this minimal RPM value.
        /// </summary>
        public void UpdateCalculations(bool debug)
        {
            var running = true;
            var counter = 0;
            var previousPhaseVoltageMin = 100000.00;

            while(running)
            {
                var voltageDrop = RectifierDiodeVoltageDrop + RectifierWireVoltageDrop +
                                  Stator.CalculateDcVoltage(PhaseWireVoltageDrop);

                //Battery connection
                if ((int) GeneratorEnergyStorageConnection.Value == 0)
                {
                    //Turbine
                    if ((int) GeneratorFrontEnd.Value == 0)
                    {
                        
                        PhaseVoltageMin = Stator.CalculatePhaseVoltage(DcBatteryVoltage.Value, voltageDrop);
                        TurbineRotorRadius = FrontEndCalculations.CalculateTurbineRotorRadius(GeneratorPower.Value,
                            TurbineAirDensity.Value,
                            TurbineMaximumPowerCoefficient.Value, TurbineWindspeedMax.Value, GeneratorEfficiency.Value);
                        FrontEndRpmMin.Value =
                            FrontEndCalculations.CalculateTurbineOptimalRotationSpeed(TurbineWindspeedMin.Value,
                                TurbineSpeedTipRatioMin.Value,
                                TurbineRotorRadius);
                        FrontEndRpmMax.Value =
                            FrontEndCalculations.CalculateTurbineOptimalRotationSpeed(TurbineWindspeedMax.Value,
                                TurbineSpeedTipRatioMax.Value, TurbineRotorRadius);
                        PhaseVoltageMax = FrontEndCalculations.CalculateBatteryVoltage(FrontEndRpmMin.Value,
                            FrontEndRpmMax.Value,
                            PhaseVoltageMin);
                    }
                    //Other
                    else if ((int) GeneratorFrontEnd.Value == 1)
                    {
                        PhaseVoltageMin =
                            Stator.CalculatePhaseVoltage(DcBatteryVoltage.Value,voltageDrop);
                        PhaseVoltageMax =
                            FrontEndCalculations.CalculateBatteryVoltage(FrontEndRpmMin.Value, FrontEndRpmMax.Value,
                                PhaseVoltageMin);
                    }
                }
                //Grid connection
                else if ((int) GeneratorEnergyStorageConnection.Value == 1)
                {
                    //Turbine
                    if ((int) GeneratorFrontEnd.Value == 0)
                    {
                        PhaseVoltageMin =
                            Stator.CalculatePhaseVoltage(DcInverterVoltageMin.Value,voltageDrop);
                        PhaseVoltageMax =
                            Stator.CalculatePhaseVoltage(DcInverterVoltageMax.Value, voltageDrop);
                        TurbineRotorRadius = FrontEndCalculations.CalculateTurbineRotorRadius(GeneratorPower.Value,
                            TurbineAirDensity.Value,
                            TurbineMaximumPowerCoefficient.Value, TurbineWindspeedMax.Value, GeneratorEfficiency.Value);
                        FrontEndRpmMax.Value =
                            FrontEndCalculations.CalculateTurbineOptimalRotationSpeed(TurbineWindspeedMax.Value,
                                TurbineSpeedTipRatioMax.Value, TurbineRotorRadius);
                        FrontEndRpmMin.Value = FrontEndCalculations.CalculateGridRpm(PhaseVoltageMin, PhaseVoltageMax,
                            (int) FrontEndRpmMax.Value);
                        TurbineWindspeedMin.Value =
                            FrontEndCalculations.CalculateTurbineOptimalWindSpeed(FrontEndRpmMin.Value,
                                TurbineRotorRadius,
                                TurbineSpeedTipRatioMin.Value);
                    }
                    //Other
                    else if ((int) GeneratorFrontEnd.Value == 1)
                    {
                        PhaseVoltageMin =
                            Stator.CalculatePhaseVoltage(DcInverterVoltageMin.Value, voltageDrop);
                        PhaseVoltageMax =
                            Stator.CalculatePhaseVoltage(DcInverterVoltageMax.Value, voltageDrop);
                        FrontEndRpmMin.Value = FrontEndCalculations.CalculateGridRpm(PhaseVoltageMin, PhaseVoltageMax,
                            (int) FrontEndRpmMax.Value);
                    }
                }

                if (Math.Abs(PhaseVoltageMin - previousPhaseVoltageMin) < 0.001)
                {
                    running = false;
                }

                MaxPhaseCurrent = Stator.CalculateMaximumPhaseCurrent(GeneratorPower.Value, PhaseVoltageMax,
                    GeneratorEfficiency.Value);

                //Calculate stator phase wire voltage drop
                PhaseWireVoltageDrop = Stator.VoltageDrop(PhaseWireLength.Value, PhaseWireDiameter.Value,
                    MaxPhaseCurrent, 3);

                PhaseWireResistance = Stator.CalculateWireResistance(PhaseWireLength.Value, PhaseWireDiameter.Value);

                //Calculate rectifier phase wire drop
                RectifierWireVoltageDrop = Stator.VoltageDrop(RectifierWireLength.Value, RectifierWireDiameter.Value,
                    MaxPhaseCurrent, 1);

                RectifierWireResistance = Stator.CalculateWireResistance(RectifierWireLength.Value,
                    RectifierWireDiameter.Value);

                previousPhaseVoltageMin = PhaseVoltageMin;
                counter += 1;
            }

            FrontEndTorque = FrontEndCalculations.CalculateTorque(GeneratorPower.Value, (int)FrontEndRpmMax.Value);

            CoilCount = Stator.CalculateCoilCount(PhaseCount, (int)CoilsPerPhase.Value);

            MagnetCount = Rotor.CalculateMagnetCount(CoilCount);

            RotorThickness = MagnetThickness.Value;

            CoilThickness = Stator.CalculateStatorThickness(MagnetThickness.Value, MechamicalGap.Value);

            MagnetFluxDensity = Rotor.CalculateMagnetFluxDensity(MagnetRemanentFluxDensity,
                MagnetCoerciveFieldStrength,
                MagnetThickness.Value, MechamicalGap.Value);

            MagnetPoleFlux = Rotor.CalculateMaximumPoleFlux(MagnetFluxDensity, MagnetWidth.Value, MagnetHeight.Value);

            CoilTurns = Stator.CalculateCoilWindings(PhaseVoltageMin, MagnetCount, FrontEndRpmMin.Value,
                (int)CoilsPerPhase.Value,
                MagnetPoleFlux, CoilWindingCoefficient);

            CoilLegWidth = Stator.CalculateCoilLegWidthMod(MaxPhaseCurrent, CoilTurns, CoilThickness,
                MaxCurrentDensity.Value, CoilFillFactor.Value);

            CoilCrossSectionalArea = Stator.CalculateCoilCrossSectionalArea(CoilLegWidth, CoilThickness,
                CoilTurns,
                CoilFillFactor.Value);

            CoilWireDiameter = Stator.CalculateCoilWireDiameter(CoilCrossSectionalArea);

            MagnetPoleToPolePitch = Rotor.CalculateMagnetPoleToPolePitch(MagnetPoleArcToPolePitchRatio.Value,
                MagnetWidth.Value);

            MagnetBetweenDistance = Rotor.CalculateBetweenPoleDistance(MagnetPoleToPolePitch, MagnetWidth.Value);

            MagnetTotalSegmentAngle = Rotor.CalculateMagnetCentralAngle(MagnetCount);

            MagnetSegmentAngle = Rotor.CalculateMagnetSegmentAngle(MagnetTotalSegmentAngle,
                MagnetPoleArcToPolePitchRatio.Value);

            MagnetBetweenSegmentAngle = Rotor.CalculateBetweenMagnetSegmentAngle(MagnetTotalSegmentAngle,
                MagnetSegmentAngle);

            RotorInnerRadius = Rotor.CalculateRotorInnerRadius(MagnetWidth.Value, MagnetSegmentAngle);

            RotorOuterRadius = Rotor.CalculateRotorOuterRadius(RotorInnerRadius, MagnetHeight.Value);

            RotorInnerOuterRadiusRatio = Rotor.CalculateRotorRadiusRatio(RotorInnerRadius, RotorOuterRadius);

            StatorInnerRadius = Stator.CalculateStatorInnerRadius(RotorInnerRadius, CoilLegWidth);

            StatorOuterRadius = Stator.CalculateStatorOuterRadius(RotorOuterRadius, CoilLegWidth);

            CoilAngle = StatorDimensionsStatic.CalculateCentralCoilAngle(CoilCount);

            var coilInnerDimensions = StatorDimensionsDynamic.CalculateCoilInnerDimensionsAngular(CoilCount,
                RotorInnerRadius, RotorOuterRadius, CoilLegWidth, BetweenCoilDistance.Value);

            CoilInnerTop = coilInnerDimensions.Item3;

            CoilInnerBottom = coilInnerDimensions.Item4;

            CoilInnerSide = coilInnerDimensions.Item5;

            var coilOuterDimensions = StatorDimensionsDynamic.CalculateCoilOuterDimensionsAngular(CoilCount,
                StatorInnerRadius, StatorOuterRadius, BetweenCoilDistance.Value);

            CoilOuterTop = coilOuterDimensions.Item3;

            CoilOuterBottom = coilOuterDimensions.Item4;

            CoilOuterSide = coilOuterDimensions.Item5;

            var coilRoundedVariables = StatorDimensionsDynamic.CalculateCoilRoundedVariables(CoilInnerRadius,
                CoilLegWidth, CoilAngle, coilInnerDimensions, coilOuterDimensions);

            CoilAverageTurnLength = coilRoundedVariables.Item3;

            CoilWireLength = Stator.CalculateCoilWireLength(CoilAverageTurnLength, CoilTurns);

            CoilResistance = Stator.CalculateWireResistance(CoilWireLength, CoilWireDiameter);

            CoilInductance = Stator.CalculateCoilInductance(CoilTurns, CoilWireDiameter, CoilThickness);

            CoilSideSurface = coilRoundedVariables.Item4;

            CoilHeatCoefficient = Stator.CalculateCoilHeatCoefficient(CoilSideSurface, CoilResistance, MaxPhaseCurrent);
        }
    }
}