using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vocabulary.Model;

namespace VocabularyUnitTest.Model
{
    /// <summary>
    /// The class to test <see cref="Word"/>
    /// </summary>
    /// <owner>Mariia Yelisieieva</owner>
    [TestClass]
    public class WordTest
    {
        /// <summary>
        /// Checks whether the <see cref="LearningProgress"/> changed from not learned to in progress.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void Word_ChangeProgress_NotLearnedSelected_ChangedToInProgress()
        {
            LearningProgress result = Word.ChangeLearningProgress(LearningProgress.NotLearned);

            Assert.AreEqual(LearningProgress.InProgress, result);
        }

        /// <summary>
        /// Checks whether the <see cref="LearningProgress"/> changed from in progress to learned.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void Word_ChangeProgress_InProgressSelected_ChangedToLearned()
        {
            LearningProgress result = Word.ChangeLearningProgress(LearningProgress.InProgress);

            Assert.AreEqual(LearningProgress.Learned, result);
        }

        /// <summary>
        /// Checks whether the <see cref="LearningProgress"/> changed from learned to not learned.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void Word_ChangeProgress_LearnedSelected_ChangedToNotLearned()
        {
            LearningProgress result = Word.ChangeLearningProgress(LearningProgress.Learned);

            Assert.AreEqual(LearningProgress.NotLearned, result);
        }
    }
}
