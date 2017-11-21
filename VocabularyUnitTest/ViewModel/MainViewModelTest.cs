using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vocabulary.Helpers.Fakes;
using Vocabulary.Model;
using Vocabulary.ViewModel;

namespace VocabularyUnitTest.ViewModel
{
    /// <summary>
    /// The class to test <see cref="MainViewModel"/>.
    /// </summary>
    /// <owner>Mariia Yelisieieva</owner>
    [TestClass]
    public class MainViewModelTest
    {
        /// <summary>
        /// Words helper source.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        private List<Word> allWords = new List<Word>
        {
            new Word(1, "我", "wo", "i", LearningProgress.InProgress),
            new Word(2, "你", "ni", "you", LearningProgress.Learned),
            new Word(3, "好", "hao", "good/well", LearningProgress.InProgress),
            new Word(4, "你好", "ni hao", "hello")
        };

        /// <summary>
        /// Initializes the main view model with a set up dialog open and words service stubs.
        /// </summary>
        /// <param name="openAddDialog">The action to open add dialog.</param>
        /// <param name="allWordsGet">The action to get all words.</param>
        /// <param name="addWord">The action to add a word.</param>
        /// <param name="removeWord">The action to remove a word.</param>
        /// <param name="updateWord">The action to update a word.</param>
        /// <param name="getWords">The action to get words.</param>
        /// <returns>
        /// The initialized instance of <see cref="MainViewModel"/>.
        /// </returns>
        /// <owner>Mariia Yelisieieva</owner>
        private MainViewModel GetMainViewModel(
            FakesDelegates.Action openAddDialog = null,
            FakesDelegates.Func<List<Word>> allWordsGet = null,
            FakesDelegates.Action<Word> addWord = null,
            FakesDelegates.Action<Word> removeWord = null,
            FakesDelegates.Action<Word> updateWord = null,
            FakesDelegates.Func<List<Word>> getWords = null)
        {
            var dialogOpen = new StubIDialogOpen()
            {
                OpenAddWindowDialog = openAddDialog
            };
            var wordsService = new StubIWordsService()
            {
                AllWordsGet = allWordsGet,
                AddWord = addWord,
                GetWords = getWords,
                RemoveWord = removeWord,
                UpdateWord = updateWord
            };
            MainViewModel viewModel = new MainViewModel(wordsService, dialogOpen);

            return viewModel;
        }

        /// <summary>
        /// Checks if a word added after cancellation.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void MainViewModel_Add_Cancelled_NotAdded()
        {
            using (ShimsContext.Create())
            {
                Word addedWord = null;
                MainViewModel vm = GetMainViewModel(
                    openAddDialog: () => Messenger.Default.Send<Word>(null),
                    allWordsGet: () => allWords,
                    addWord: (word) =>
                    {
                        addedWord = word;
                    });

                vm.AddCommand.Execute(null);

                Assert.AreEqual(null, addedWord);
            }
        }
        
        /// <summary>
        /// Checks if a valid word added.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void MainViewModel_Add_ValidWord_Added()
        {
            using (ShimsContext.Create())
            {
                Word addedWord = new Word
                {
                    Id = 1,
                    Chinese = "1",
                    Pinyin = "1",
                    English = "1",
                    Progress = LearningProgress.InProgress
                };
                MainViewModel vm = GetMainViewModel(
                    allWordsGet: () => allWords,
                    addWord: word =>
                    {
                        addedWord = word;
                    },
                    openAddDialog: () => Messenger.Default.Send<Word>(addedWord));

                vm.AddCommand.Execute(null);

                Assert.AreEqual(addedWord, addedWord);
            }
        }

        /// <summary>
        /// Checks if the attempt to change the learning progress without a word selected throws an exception.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        [ExpectedException(typeof(TargetInvocationException))]
        public void MainViewModel_ChangeProgress_NotSelected_CantExecute()
        {
            MainViewModel viewModel = GetMainViewModel();

            viewModel.ChangeProgressCommand.Execute(null);
        }
        
        /// <summary>
        /// Checks if a word can be deleted without a word selected.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void MainViewModel_Delete_NotSelected_CantExecute()
        {
            MainViewModel viewModel = GetMainViewModel();

            bool canExecute = viewModel.DeleteCommand.CanExecute(null);

            Assert.IsFalse(canExecute);
        }
        
        /// <summary>
        /// Checks if a selected word is deleted.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void MainViewModel_Delete_ValidWord_Removed()
        {
            List<Word> toRemove = new List<Word>() { allWords[0] };
            MainViewModel viewModel = GetMainViewModel(
                allWordsGet: () => toRemove,
                removeWord: word => toRemove.Remove(word));
            viewModel.RefreshCommand.Execute(null);
            viewModel.DetailedWord = allWords[0];

            viewModel.DeleteCommand.Execute(null);

            Assert.AreEqual(0, toRemove.Count);
        }

        /// <summary>
        /// Checks if all words are displayed when all progress filters are checked.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void MainViewModel_Refresh_AllProgresses_DictionaryFilled()
        {
            MainViewModel viewModel = GetMainViewModel(
                allWordsGet: () => allWords);

            viewModel.RefreshCommand.Execute(null);

            Assert.AreEqual(viewModel.Dictionary.Count, allWords.Count);
            Assert.AreEqual(allWords[0], viewModel.Dictionary[0]);
            Assert.AreEqual(allWords[1], viewModel.Dictionary[1]);
            Assert.AreEqual(allWords[2], viewModel.Dictionary[2]);
            Assert.AreEqual(allWords[3], viewModel.Dictionary[3]);
        }

        /// <summary>
        /// Checks if just not learned words are displayed when only not learned progress filter is checked.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void MainViewModel_Refresh_NotLearnedOnly_DictionaryFilled()
        {
            MainViewModel viewModel = GetMainViewModel(
                allWordsGet: () => allWords);
            viewModel.IsShownInProgress = false;
            viewModel.IsShownLearned = false;

            viewModel.RefreshCommand.Execute(null);

            bool result = viewModel.Dictionary.All(x => x.Progress == LearningProgress.NotLearned);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Checks if just in progress words are displayed when only in progress filter is checked.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void MainViewModel_Refresh_InProgressOnly_DictionaryFilled()
        {
            MainViewModel viewModel = GetMainViewModel(
                allWordsGet: () => allWords);
            viewModel.IsShownNotLearned = false;
            viewModel.IsShownLearned = false;

            viewModel.RefreshCommand.Execute(null);

            bool result = viewModel.Dictionary.All(x => x.Progress == LearningProgress.InProgress);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Checks if just learned words are displayed when only learned progress filter is checked.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void MainViewModel_Refresh_LearnedOnly_DictionaryFilled()
        {
            MainViewModel viewModel = GetMainViewModel(
                allWordsGet: () => allWords);
            viewModel.IsShownNotLearned = false;
            viewModel.IsShownInProgress = false;

            viewModel.RefreshCommand.Execute(null);

            bool result = viewModel.Dictionary.All(x => x.Progress == LearningProgress.Learned);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Checks if a word can be updated without a word selected.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void MainViewModel_SelectSet_Unselected_CantUpdate()
        {
            MainViewModel vm = GetMainViewModel();

            vm.DetailedWord = null;

            bool canUpdate = vm.UpdateCommand.CanExecute(null);
            Assert.IsFalse(canUpdate);
        }

        /// <summary>
        /// Checks if a word can be deleted without a word selected.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void MainViewModel_SelectSet_Unselected_CantDelete()
        {
            MainViewModel vm = GetMainViewModel();

            vm.DetailedWord = null;

            bool canUpdate = vm.DeleteCommand.CanExecute(null);
            Assert.IsFalse(canUpdate);
        }

        /// <summary>
        /// Checks if a word can be deleted and updated after a word selected.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void MainViewModel_SelectSet_Selected_SelectedWordChosenCanDeleteCanUpdate()
        {
            MainViewModel vm = GetMainViewModel();

            vm.DetailedWord = allWords[0];

            Assert.AreEqual(allWords[0], vm.DetailedWord);
            Assert.IsTrue(vm.DeleteCommand.CanExecute(null));
            Assert.IsTrue(vm.UpdateCommand.CanExecute(null));
        }

        /// <summary>
        /// Checks whether select command doesn't create a selected word when it's set to null.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void MainViewModel_Select_Unselected_NotUpdateSelected()
        {
            MainViewModel vm = GetMainViewModel();
            vm.DetailedWord = null;

            vm.SelectCommand.Execute(null);

            Assert.AreEqual(null, vm.DetailedWord);
        }

        /// <summary>
        /// Checks whether select command creates a selected word.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void MainViewModel_Select_Selected_UpdateSelected()
        {
            MainViewModel vm = GetMainViewModel();
            vm.DetailedWord = allWords[0];
            vm.ListBoxSelectedWord = new Word()
            {
                Id = 1,
                Chinese = "1",
                Pinyin = "2",
                English = "3",
                Progress = LearningProgress.InProgress,
            };

            vm.SelectCommand.Execute(null);

            Assert.AreEqual(1, vm.DetailedWord.Id);
            Assert.AreEqual("1", vm.DetailedWord.Chinese);
            Assert.AreEqual("2", vm.DetailedWord.Pinyin);
            Assert.AreEqual("3", vm.DetailedWord.English);
            Assert.AreEqual(LearningProgress.InProgress, vm.DetailedWord.Progress);
        }

        /// <summary>
        /// Checks if a word can be updated without a word selected.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void MainViewModel_Update_SelectedWordIsNull_CantExecute()
        {
            MainViewModel vm = GetMainViewModel();

            bool canExecute = vm.UpdateCommand.CanExecute(null);

            Assert.IsFalse(canExecute);
        }
        
        /// <summary>
        /// Checks if a word can be updated with an empty Chinese word.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void MainViewModel_Update_ChineseEmpty_CantExecute()
        {
            MainViewModel vm = GetMainViewModel();
            vm.DetailedWord = new Word
            {
                Chinese = "",
                English = "1",
                Pinyin = "2"
            };

            bool canExecute = vm.UpdateCommand.CanExecute(null);

            Assert.IsFalse(canExecute);
        }

        /// <summary>
        /// Checks if a word can be updated with an empty English word.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void MainViewModel_Update_EnglishEmpty_CantExecute()
        {
            MainViewModel vm = GetMainViewModel();
            vm.DetailedWord = new Word
            {
                Chinese = "1",
                English = "",
                Pinyin = "2"
            };

            bool canExecute = vm.UpdateCommand.CanExecute(null);

            Assert.IsFalse(canExecute);
        }

        /// <summary>
        /// Checks if a word can be updated with an empty pinyin.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void MainViewModel_Update_PinyinEmpty_CantExecute()
        {
            MainViewModel vm = GetMainViewModel();
            vm.DetailedWord = new Word
            {
                Chinese = "1",
                English = "2",
                Pinyin = ""
            };

            bool canExecute = vm.UpdateCommand.CanExecute(null);

            Assert.IsFalse(canExecute);
        }

        /// <summary>
        /// Checks if a valid word is updated.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void MainViewModel_Update_ValidWord_Updated()
        {
            Word updated = null;
            MainViewModel vm = GetMainViewModel(
                allWordsGet: () => new List<Word> { allWords[0] },
                updateWord: (word) => { updated = word; });
            vm.DetailedWord = new Word
            {
                Id = 1,
                Chinese = "1",
                English = "2",
                Pinyin = "3",
                Progress = LearningProgress.Learned
            };

            vm.UpdateCommand.Execute(null);

            Assert.AreEqual(null, vm.DetailedWord);
            Assert.AreEqual("1", updated.Chinese);
            Assert.AreEqual("2", updated.English);
            Assert.AreEqual("3", updated.Pinyin);
            Assert.AreEqual(LearningProgress.Learned, updated.Progress);
        }

        /// <summary>
        /// Checks if a valid word is refreshed in the words list after updating.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        [TestMethod]
        public void MainViewModel_Update_ValidWord_Refreshed()
        {
            Word updated = null;
            MainViewModel vm = GetMainViewModel(
                allWordsGet: () => new List<Word> { updated },
                updateWord: (word) => { updated = word; });
            Word selected = new Word
            {
                Id = 1,
                Chinese = "1",
                English = "2",
                Pinyin = "3",
                Progress = LearningProgress.NotLearned
            };
            vm.DetailedWord = new Word
            {
                Id = selected.Id,
                Chinese = selected.Chinese,
                English = selected.English,
                Pinyin = selected.Pinyin,
                Progress = selected.Progress
            };

            vm.UpdateCommand.Execute(null);

            Assert.AreEqual(selected.Chinese, vm.Dictionary[0].Chinese);
            Assert.AreEqual(selected.English, vm.Dictionary[0].English);
            Assert.AreEqual(selected.Pinyin, vm.Dictionary[0].Pinyin);
            Assert.AreEqual(selected.Progress, vm.Dictionary[0].Progress);
        }
    }
}
