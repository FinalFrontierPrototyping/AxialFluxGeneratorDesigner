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
        /// 
        /// </summary>
        /// <param name="debug"></param>
        /// <param name="varName"></param>
        /// <param name="var"></param>
        public static void DebugPrint(bool debug, string varName, double var)
        {
            if (debug)
            {
                Debug.WriteLine(varName + ": " + var);
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="debug"></param>
        ///// <param name="var"></param>
        //public static void DebugPrint(bool debug, int var)
        //{
        //    Debug.WriteLine(nameof(var) + ": " + var);
        //}
    }
}