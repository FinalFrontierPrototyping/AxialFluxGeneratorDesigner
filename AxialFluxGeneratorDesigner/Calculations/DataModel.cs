using System.ComponentModel;
using System.Runtime.CompilerServices;

// ReSharper disable once RedundantUsingDirective

namespace AxialFluxGeneratorDesigner.Calculations
{
    /// <summary>
    ///     This class can be used to design a Axial Flux Permanent Magnet Generator.
    /// </summary>
    public class DataModel : BindableBase
    {
    }

    /// <summary>
    ///     Implementation of <see cref="INotifyPropertyChanged" /> to simplify models.
    ///     http://danrigby.com/2015/09/12/inotifypropertychanged-the-net-4-6-way/
    /// </summary>
    public abstract class BindableBase : INotifyPropertyChanged
    {
        /// <summary>
        ///     Multi cast event for property change notifications.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Checks if a property already matches a desired value.  Sets the property and
        ///     notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">
        ///     Name of the property used to notify listeners.  This
        ///     value is optional and can be provided automatically when invoked from compilers that
        ///     support CallerMemberName.
        /// </param>
        /// <returns>
        ///     True if the value was changed, false if the existing value matched the
        ///     desired value.
        /// </returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return false;
            }

            storage = value;
            // ReSharper disable once ExplicitCallerInfoArgument
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        ///     Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">
        ///     Name of the property used to notify listeners.  This
        ///     value is optional and can be provided automatically when invoked from compilers
        ///     that support <see cref="CallerMemberNameAttribute" />.
        /// </param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}