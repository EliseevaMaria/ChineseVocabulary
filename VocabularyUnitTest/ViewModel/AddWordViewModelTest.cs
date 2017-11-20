using System;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vocabulary.Model;
using Vocabulary.ViewModel;

namespace VocabularyUnitTest.ViewModel
{
    /// <summary>
    /// The class to test <see cref="AddWordViewModel"/>.
    /// </summary>
    /// <owner>Mariia Yelisieieva</owner>
    [TestClass]
    public class AddWordViewModelTest
    {
        /// <summary>
        /// Checks whether the new word instance is sent with no Chinese word in it.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void AddWordViewModel_AddWord_ChineseEmpty_NotSent()
        {
            AddWordViewModel viewModel = new AddWordViewModel()
            {
                Chinese = "",
                Pinyin = "1",
                English = "1",
                Progress = LearningProgress.InProgress
            };

            bool result = viewModel.AddWord.CanExecute(this);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Checks whether the new word instance is sent with no pinyin in it.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void AddWordViewModel_AddWord_PinyinEmpty_NotSent()
        {
            AddWordViewModel viewModel = new AddWordViewModel()
            {
                Chinese = "1",
                Pinyin = "",
                English = "1",
                Progress = LearningProgress.InProgress
            };

            bool result = viewModel.AddWord.CanExecute(this);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Checks whether the new word instance is sent with no English word in it.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void AddWordViewModel_AddWord_EnglishEmpty_NotSent()
        {
            AddWordViewModel viewModel = new AddWordViewModel()
            {
                Chinese = "1",
                Pinyin = "1",
                English = "",
                Progress = LearningProgress.InProgress
            };

            bool result = viewModel.AddWord.CanExecute(this);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Checks whether the valid new word instance is sent.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void AddWordViewModel_AddWord_ValidFields_Sent()
        {
            Word wordToAdd = new Word
            {
                Chinese = "1",
                Pinyin = "1",
                English = "1",
                Progress = LearningProgress.InProgress
            };
            Word addedWord = null;
            Messenger.Default.Register<Word>(this, (word) => addedWord = word);
            AddWordViewModel viewModel = new AddWordViewModel()
            {
                Chinese = "1",
                Pinyin = "1",
                English = "1",
                Progress = LearningProgress.InProgress
            };

            viewModel.AddWord.Execute(this);

            Assert.AreEqual(wordToAdd.Chinese, addedWord.Chinese);
            Assert.AreEqual(wordToAdd.Pinyin, addedWord.Pinyin);
            Assert.AreEqual(wordToAdd.English, addedWord.English);
            Assert.AreEqual(wordToAdd.Progress, addedWord.Progress);
        }

        /// <summary>
        /// Checks whether the CanExecuteChange event is raised when new Chinese value is set.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void AddWordViewModel_ChineseSet_ValueSet_AddWordCanExecuteChangedRaised()
        {
            bool raised = false;
            AddWordViewModel viewModel = new AddWordViewModel();
            viewModel.AddWord.CanExecuteChanged += (x, y) => { raised = true; };

            viewModel.Chinese = "sd";

            Assert.IsTrue(raised);
        }

        /// <summary>
        /// Checks whether the CanExecuteChange event is raised when new English value is set.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void AddWordViewModel_EnglishSet_ValueSet_AddWordCanExecuteChangedRaised()
        {
            bool raised = false;
            AddWordViewModel viewModel = new AddWordViewModel();
            viewModel.AddWord.CanExecuteChanged += (x, y) => { raised = true; };

            viewModel.English = "sd";

            Assert.IsTrue(raised);
        }

        /// <summary>
        /// Checks whether the CanExecuteChange event is raised when new pinyin value is set.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void AddWordViewModel_PinyinSet_ValueSet_AddWordCanExecuteChangedRaised()
        {
            bool raised = false;
            AddWordViewModel viewModel = new AddWordViewModel();
            viewModel.AddWord.CanExecuteChanged += (x, y) => { raised = true; };

            viewModel.Pinyin = "sd";

            Assert.IsTrue(raised);
        }

        /// <summary>
        /// Checks whether the CanExecuteChange event is raised when new progress value is set.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void AddWordViewModel_ProgressSet_ValueSet_AddWordCanExecuteChangedRaised()
        {
            bool raised = false;
            AddWordViewModel viewModel = new AddWordViewModel();
            viewModel.AddWord.CanExecuteChanged += (x, y) => { raised = true; };

            viewModel.Progress = LearningProgress.InProgress;

            Assert.IsTrue(raised);
        }
    }
}
