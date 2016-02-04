using System;

namespace AxialFluxGeneratorDesigner.Calculations
{
    /// <summary>
    /// </summary>
    public static class FrontEndCalculations
    {
        /// <summary>
        ///     This method calculates the max torque based on the power (W) and RPM
        /// </summary>
        /// <param name="power">The power (Watt)</param>
        /// <param name="rpm">The max rpm</param>
        /// <returns>The torque (Nm)</returns>
        public static double CalculateTorque(double power, int rpm)
        {
            return power/(2*Math.PI*rpm/60);
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
        public static double CalculateTurbineRotorRadius(double generatorNominalPower, double airDensity,
            double maximumPowerCoefficient, double windSpeed, double generatorEfficiency)
        {
            var aerodynamicPower = generatorNominalPower/generatorEfficiency;

            return
                Math.Sqrt(2*aerodynamicPower/(Math.PI*airDensity*maximumPowerCoefficient*Math.Pow(windSpeed, 3)));
        }

        /// <summary>
        ///     This method calculates the turbine RPM.
        /// </summary>
        /// <param name="windSpeed">The wind speed (m/s)</param>
        /// <param name="tipSpeedRatio">The tip speed ratio</param>
        /// <param name="turbineRotorRadius">The radius of the rotor (m)</param>
        /// <returns>The RPM</returns>
        public static int CalculateTurbineOptimalRotationSpeed(double windSpeed, double tipSpeedRatio,
            double turbineRotorRadius)
        {
            return (int) (60*windSpeed*tipSpeedRatio/(2*Math.PI*turbineRotorRadius));
        }

        /// <summary>
        ///     This method calculates the wind speed based on the phase voltage ratio (min/max).
        /// </summary>
        /// <param name="phaseVoltageMin">The minimum phase voltage (v)</param>
        /// <param name="phaseVoltageMax">The maximum phase voltage (v)</param>
        /// <param name="nominalRpm">The nominal RPM of the rotor</param>
        /// <returns>Returns the minimum rpm</returns>
        public static int CalculateGridRpm(double phaseVoltageMin, double phaseVoltageMax, int nominalRpm)
        {
            return (int) (phaseVoltageMin/phaseVoltageMax*nominalRpm);
        }

        /// <summary>
        ///     This method calculates the maximal battery voltage based on the ratio between the max and min rpm times the minimal
        ///     phase voltage.
        /// </summary>
        /// <param name="rpmMin"></param>
        /// <param name="rpmMax"></param>
        /// <param name="minimalPhaseVoltage"></param>
        /// <returns>The maximal phase voltage</returns>
        public static double CalculateBatteryVoltage(double rpmMin, double rpmMax, double minimalPhaseVoltage)
        {
            return rpmMax/rpmMin*minimalPhaseVoltage;
        }

        /// <summary>
        ///     This method calculates the wind speed.
        /// </summary>
        /// <param name="speedRpm">The rotational speed (RPM)</param>
        /// <param name="turbineRotorRadius">The radius of the rotor (m)</param>
        /// <param name="tipSpeedRatio">The tip speed ratio</param>
        /// <returns>The wind speed (m/s)</returns>
        public static double CalculateTurbineOptimalWindSpeed(double speedRpm, double turbineRotorRadius,
            double tipSpeedRatio)
        {
            return 2*Math.PI*speedRpm*turbineRotorRadius/(60*tipSpeedRatio);
        }
    }
}