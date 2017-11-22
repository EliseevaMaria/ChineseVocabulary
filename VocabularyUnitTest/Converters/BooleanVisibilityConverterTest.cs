using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vocabulary.Converters;
using Vocabulary.Model;

namespace VocabularyUnitTest.Converters
{
    /// <summary>
    /// The class to test <see cref="BooleanVisibilityConverter"/>.
    /// </summary>
    /// <owner>Mariia Yelisieieva</owner>
    [TestClass]
    public class BooleanVisibilityConverterTest
    {
        /// <summary>
        /// Checks whether false is converted to Visibility.Visible.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void BooleanVisibilityConverter_Convert_False_ReturnVisible()
        {
            BooleanVisibilityConverter converter = new BooleanVisibilityConverter();

            Visibility result = (Visibility)converter.Convert(false, typeof(bool), new Object(), CultureInfo.CurrentCulture);

            Assert.AreEqual(Visibility.Visible, result);
        }

        /// <summary>
        /// Checks whether true is converted to Visibility.Collapsed.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void BooleanVisibilityConverter_Convert_True_ReturnCollapsed()
        {
            BooleanVisibilityConverter converter = new BooleanVisibilityConverter();

            Visibility result = (Visibility)converter.Convert(true, typeof(bool), new Object(), CultureInfo.CurrentCulture);

            Assert.AreEqual(Visibility.Collapsed, result);
        }

        /// <summary>
        /// Checks whether the back conversion implemented.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void BooleanVisibilityConverter_ConvertBack_DefaultParameters_ThrowException()
        {
            ProgressColorConverter converter = new ProgressColorConverter();

            converter.ConvertBack(new Object(), typeof(SolidColorBrush), new Object(), CultureInfo.CurrentCulture);
        }
    }
}
