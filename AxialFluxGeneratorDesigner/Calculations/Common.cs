using System;
using System.Diagnostics;

namespace AxialFluxGeneratorDesigner.Calculations
{
    /// <summary>
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// </summary>
        /// <param name="mm"></param>
        /// <returns></returns>
        public static double MillimetersToMeters(double mm)
        {
            return mm/1000;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        public static double DegToRad(double degrees)
        {
            return Math.PI * degrees / 180.0;
        }

        /// <summary>
        /// </summary>
        /// <param name="debug"></param>
        /// <param name="text"></param>
        public static void DebugPrint(bool debug, string text)
        {
            Debug.WriteLine(text);
        }

        /// <summary>
        /// </summary>
        /// <param name="debug"></param>
        /// <param name="text"></param>
        /// <param name="value"></param>
        public static void DebugPrint(bool debug, string text, double value)
        {
            Debug.WriteLine(text + ": " + value);
        }

        /// <summary>
        /// </summary>
        /// <param name="debug"></param>
        /// <param name="text"></param>
        /// <param name="value"></param>
        public static void DebugPrint(bool debug, string text, int value)
        {
            Debug.WriteLine(text + ": " + value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="debug"></param>
        /// <param name="var"></param>
        public static void DebugPrint(bool debug, double var)
        {
            Debug.WriteLine(nameof(var)+ ": " + var);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="debug"></param>
        /// <param name="var"></param>
        public static void DebugPrint(bool debug, int var)
        {
            Debug.WriteLine(nameof(var) + ": " + var);
        }
    }
}