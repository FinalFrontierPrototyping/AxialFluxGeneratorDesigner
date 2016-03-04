using System;

namespace AxialFluxGeneratorDesigner.Calculations
{
    /// <summary>
    ///     This method takes a default value with the min and max limits. If the value exceeds these limits it is corrected.
    /// </summary>

    public class GeneratorProperty<T> where T : IComparable<T>
    {
        private T _value;

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public GeneratorProperty(string name, T defaultValue, T min, T max)
        {
            Name = name;
            _value = defaultValue;
            Min = min;
            Max = max;
        }

        /// <summary>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        public T Min { get; set; }

        /// <summary>
        /// </summary>
        public T Max { get; set; }

        /// <summary>
        /// </summary>
        public T Value
        {
            get { return _value; }
            set
            {
                if (Max.CompareTo(value) < 0)
                    _value = Max;
                else if (Min.CompareTo(value) > 0)
                    _value = Min;
                else
                    _value = value;
            }
        }
    }
}