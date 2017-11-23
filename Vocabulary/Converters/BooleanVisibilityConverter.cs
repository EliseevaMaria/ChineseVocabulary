using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Vocabulary.Model;

namespace Vocabulary.Converters
{
    /// <summary>
    /// Class for converting boolean values to UI elements' visible property.
    /// </summary>
    /// <owner>Mariia Yelisieieva</owner>
    /// <seealso cref="System.Windows.Data.IValueConverter" />
    public class BooleanVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts boolean values to the <see cref="Visibility"/> property, if <c>false</c> element is collapsed.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isHidden = (bool) (value ?? true);

            Visibility result = isHidden ? Visibility.Collapsed : Visibility.Visible;

            return result;
        }

        /// <summary>
        /// Converts the <see cref="Visibility"/> property to boolean values, if element is collapsed returns <c>false</c>. Throws <see cref="NotImplementedException" />.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
