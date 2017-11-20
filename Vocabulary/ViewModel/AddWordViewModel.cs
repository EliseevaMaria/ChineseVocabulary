using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Vocabulary.Model;

namespace Vocabulary.ViewModel
{
    /// <summary>
    /// The ViewModel class to interact with <see cref="AddWordDialog"/> window and the model layer.
    /// </summary>
    /// <owner>Mariia Yelisieieva</owner>
    /// <seealso cref="GalaSoft.MvvmLight.ViewModelBase" />
    public class AddWordViewModel : ViewModelBase
    {
        /// <summary>
        /// The add word command.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        private RelayCommand addWord;

        /// <summary>
        /// Gets the add word command or creates it.
        /// </summary>
        /// <value>
        /// The add word.
        /// </value>
        /// <owner>Mariia Yelisieieva</owner>
        public RelayCommand AddWord
        {
            get
            {
                return this.addWord
                       ?? (this.addWord = new RelayCommand(
                           () => Messenger.Default.Send<Word>(new Word
                           {
                               Chinese = this.Chinese,
                               Pinyin = this.Pinyin,
                               English = this.English,
                               Progress = this.Progress,
                               //IsDirty = false
                           }),
                           () => this.Chinese != "" && this.Pinyin != "" && this.English != ""));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddWordViewModel"/> class.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        public AddWordViewModel()
        {
            this.Chinese = "";
            this.Pinyin = "";
            this.English = "";
            this.Progress = LearningProgress.NotLearned;
        }

        /// <summary>
        /// The change learning progress command.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        private RelayCommand changeProgressCommand;

        /// <summary>
        /// Gets the change learning progress command.
        /// </summary>
        /// <value>
        /// The change learning progress command.
        /// </value>
        /// <owner>Mariia Yelisieieva</owner>
        public RelayCommand ChangeProgressCommand
        {
            get
            {
                return this.changeProgressCommand
                       ?? (this.changeProgressCommand = new RelayCommand(() =>
                       {
                           this.Progress = Word.ChangeLearningProgress(this.Progress);
                       }));
            }
        }

        /// <summary>
        /// The Chinese word to add.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        private string chinese;

        /// <summary>
        /// Gets or sets the Chinese word to add.
        /// </summary>
        /// <value>
        /// The Chinese word to add.
        /// </value>
        /// <owner>Mariia Yelisieieva</owner>
        public string Chinese
        {
            set
            {
                Set(() => Chinese, ref chinese, value);
                this.AddWord.RaiseCanExecuteChanged();
            }
            get { return this.chinese; }
        }

        /// <summary>
        /// The English word to add.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        private string english;

        /// <summary>
        /// Gets or sets the English word to add.
        /// </summary>
        /// <value>
        /// The English word to add.
        /// </value>
        /// <owner>Mariia Yelisieieva</owner>
        public string English
        {
            set
            {
                Set(() => English, ref english, value);
                this.AddWord.RaiseCanExecuteChanged();
            }
            get { return this.english; }
        }

        /// <summary>
        /// The pinyin to add.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        private string pinyin;

        /// <summary>
        /// Gets or sets the pinyin to add.
        /// </summary>
        /// <value>
        /// The pinyin to add.
        /// </value>
        /// <owner>Mariia Yelisieieva</owner>
        public string Pinyin
        {
            set
            {
                Set(() => Pinyin, ref pinyin, value);
                this.AddWord.RaiseCanExecuteChanged();
            }
            get { return this.pinyin; }
        }

        /// <summary>
        /// The learning progress of the new word.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        private LearningProgress progress;

        /// <summary>
        /// Gets or sets the learning progress of the new word.
        /// </summary>
        /// <value>
        /// The learning progress of the new word.
        /// </value>
        /// <owner>Mariia Yelisieieva</owner>
        public LearningProgress Progress
        {
            set
            {
                Set(() => Progress, ref progress, value);
                this.AddWord.RaiseCanExecuteChanged();
            }
            get { return this.progress; }
        }
    }
}
