using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Vocabulary.Model;

namespace Vocabulary.Converters
{
    /// <summary>
    /// Class for converting values to display colored learning progress.
    /// </summary>
    /// <owner>Mariia Yelisieieva</owner>
    /// <seealso cref="System.Windows.Data.IValueConverter" />
    public class ProgressColorConverter : IValueConverter
    {
        /// <summary>
        /// Converts values from <see cref="LearningProgress" /> to <see cref="SolidColorBrush" /> to display colored progress.
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
            LearningProgress progress = (LearningProgress) value;

            SolidColorBrush result = new SolidColorBrush(Colors.Gray);
            switch (progress)
            {
                case LearningProgress.NotLearned:
                    result = new SolidColorBrush(Colors.Red);
                    break;
                case LearningProgress.InProgress:
                    result = new SolidColorBrush(Colors.Yellow);
                    break;
                case LearningProgress.Learned:
                    result = new SolidColorBrush(Colors.Green);
                    break;
            }

            return result;
        }

        /// <summary>
        /// Converts values from <see cref="SolidColorBrush" /> to <see cref="LearningProgress" />. Throws <see cref="NotImplementedException" />.
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
