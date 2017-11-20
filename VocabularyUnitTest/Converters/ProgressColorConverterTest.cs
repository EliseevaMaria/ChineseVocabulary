using System;
using System.Globalization;
using System.Windows.Media;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vocabulary.Converters;
using Vocabulary.Model;

namespace VocabularyUnitTest.Converters
{
    /// <summary>
    /// The class to test <see cref="ProgressColorConverter"/>.
    /// </summary>
    /// <owner>Mariia Yelisieieva</owner>
    [TestClass]
    public class ProgressColorConverterTest
    {
        /// <summary>
        /// Checks whether not learned progress is converted to the red.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void ProgressColorConverter_Convert_NotLearned_ReturnRed()
        {
            ProgressColorConverter converter = new ProgressColorConverter();

            SolidColorBrush result = (SolidColorBrush)converter.Convert(LearningProgress.NotLearned, typeof(LearningProgress), new Object(), CultureInfo.CurrentCulture);

            Assert.AreEqual(Color.FromRgb(255, 0, 0), result.Color);
        }

        /// <summary>
        /// Checks whether in progress learning progress is converted to the yellow.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void ProgressColorConverter_Convert_InProgress_ReturnYellow()
        {
            ProgressColorConverter converter = new ProgressColorConverter();

            SolidColorBrush result = (SolidColorBrush)converter.Convert(LearningProgress.InProgress, typeof(LearningProgress), new Object(), CultureInfo.CurrentCulture);

            Assert.AreEqual(Color.FromRgb(255, 255, 0), result.Color);
        }

        /// <summary>
        /// Checks whether learned progress is converted to the green.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void ProgressColorConverter_Convert_Learned_ReturnGreen()
        {
            ProgressColorConverter converter = new ProgressColorConverter();

            SolidColorBrush result = (SolidColorBrush)converter.Convert(LearningProgress.Learned, typeof(LearningProgress), new Object(), CultureInfo.CurrentCulture);

            Assert.AreEqual(Color.FromRgb(0, 128, 0), result.Color);
        }

        /// <summary>
        /// Checks whether the back conversion implemented.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ProgressColorConverter_ConvertBack_DefaultParameters_ThrowException()
        {
            ProgressColorConverter converter = new ProgressColorConverter();

            converter.ConvertBack(new Object(), typeof(SolidColorBrush), new Object(), CultureInfo.CurrentCulture);
        }
    }
}
