using System;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Vocabulary.Helpers;
using Vocabulary.Model;

namespace Vocabulary.ViewModel
{
    /// <summary>
    /// The ViewModel class to interact with <see cref="MainViewModel"/> window and the model layer.
    /// </summary>
    /// <seealso cref="GalaSoft.MvvmLight.ViewModelBase" />
    /// <owner>Mariia Yelisieieva</owner>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// The add command.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        private RelayCommand addCommand;

        /// <summary>
        /// Gets the add command.
        /// </summary>
        /// <value>
        /// The add command.
        /// </value>
        /// <owner>Mariia Yelisieieva</owner>
        public RelayCommand AddCommand
        {
            get
            {
                return this.addCommand
                       ?? (this.addCommand = new RelayCommand(() =>
                       {
                           Word addedWord = null;
                           Messenger.Default.Register<Word>(this, (word) => addedWord = word);

                           dialogOpen.OpenAddWindowDialog();

                           if (addedWord != null)
                           {
                               this.wordsService.Add(addedWord);
                               this.RefreshCommand.Execute(null);
                           }
                           Messenger.Default.Unregister<Word>(this);
                       }));
            }
        }

        /// <summary>
        /// The command to change details of the selected word.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        private RelayCommand changeDetailsCommand;

        /// <summary>
        /// Gets the command to change details of the selected word.
        /// </summary>
        /// <value>
        /// The command to change details of the selected word.
        /// </value>
        /// <owner>Mariia Yelisieieva</owner>
        public RelayCommand ChangeDetailsCommand
        {
            get
            {
                return this.changeDetailsCommand
                       ?? (this.changeDetailsCommand = new RelayCommand(() =>
                       {
                           this.UpdateCommand.RaiseCanExecuteChanged();
                       }));
            }
        }

        /// <summary>
        /// The change progress command.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        private RelayCommand changeProgressCommand;

        /// <summary>
        /// Gets the change progress command.
        /// </summary>
        /// <value>
        /// The change progress command.
        /// </value>
        /// <owner>Mariia Yelisieieva</owner>
        public RelayCommand ChangeProgressCommand
        {
            get
            {
                return this.changeProgressCommand
                       ?? (this.changeProgressCommand = new RelayCommand(() =>
                       {
                           detailedWord.Progress = Word.ChangeLearningProgress(detailedWord.Progress);
                       }));
            }
        }

        /// <summary>
        /// The delete command.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        private RelayCommand deleteCommand;

        /// <summary>
        /// Gets the delete command.
        /// </summary>
        /// <value>
        /// The delete command.
        /// </value>
        /// <owner>Mariia Yelisieieva</owner>
        public RelayCommand DeleteCommand
        {
            get
            {
                return this.deleteCommand
                       ?? (this.deleteCommand = new RelayCommand(() =>
                           {
                               this.wordsService.Remove(this.DetailedWord);
                               this.RefreshCommand.Execute(null);

                               this.DetailedWord = null;
                           },
                           () => this.DetailedWord != null));
            }
        }

        /// <summary>
        /// The <see cref="IDialogOpen"/> heir to open <see cref="AddWordViewModel"/> as a dialog.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        private IDialogOpen dialogOpen;

        /// <summary>
        /// Gets the observable collection to display a word dictionary.
        /// </summary>
        /// <value>
        /// The word dictionary.
        /// </value>
        /// <owner>Mariia Yelisieieva</owner>
        public ObservableCollection<Word> Dictionary { get; private set; }
        
        /// <summary>
        /// The command to get all words from a source.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        private RelayCommand<int> getAllWordsCommand;

        /// <summary>
        /// Gets the command to get all words.
        /// </summary>
        /// <value>
        /// The command to get all words.
        /// </value>
        /// <owner>Mariia Yelisieieva</owner>
        public RelayCommand<int> GetAllWordsCommand
        {
            get
            {
                return this.getAllWordsCommand
                       ?? (this.getAllWordsCommand = new RelayCommand<int>(index =>
                       {
                           this.wordsService.GetWords();
                           this.RefreshCommand.Execute(null);
                       }));
            }
        }

        /// <summary>
        /// Indicates if input fields for a word are enabled to interact with.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        private bool isFieldEnabled;

        /// <summary>
        /// Gets or sets a value indicating whether input fields for a word are enabled to interact with.
        /// </summary>
        /// <value>
        /// <owner>Mariia Yelisieieva</owner>
        ///   <c>true</c> if this fields are enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsFieldEnabled
        {
            get { return isFieldEnabled; }

            set { Set(() => IsFieldEnabled, ref isFieldEnabled, value); }
        }

        /// <summary>
        /// The field that indicates whether in progress of learning words are shown.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        private bool isShownInProgress = true;

        /// <summary>
        /// Gets or sets a value indicating whether in progress of learning words are shown.
        /// </summary>
        /// <value>
        ///   <c>true</c> if in progress of learning words are shown; otherwise, <c>false</c>.
        /// </value>
        /// <owner>Mariia Yelisieieva</owner>
        public bool IsShownInProgress
        {
            get { return isShownInProgress; }

            set
            {
                Set(() => IsShownInProgress, ref isShownInProgress, value);
                this.RefreshCommand.Execute(null);
            }
        }

        /// <summary>
        /// The field that indicates whether learned words are shown.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        private bool isShownLearned = true;

        /// <summary>
        /// Gets or sets a value indicating whether learned words are shown.
        /// </summary>
        /// <value>
        ///   <c>true</c> if learned words are shown; otherwise, <c>false</c>.
        /// </value>
        /// <owner>Mariia Yelisieieva</owner>
        public bool IsShownLearned
        {
            get { return isShownLearned; }

            set
            {
                Set(() => IsShownLearned, ref isShownLearned, value);
                this.RefreshCommand.Execute(null);
            }
        }

        /// <summary>
        /// The field that indicates whether not learned words are shown.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        private bool isShownNotLearned = true;

        /// <summary>
        /// Gets or sets a value indicating whether not learned words are shown.
        /// </summary>
        /// <value>
        ///   <c>true</c> if not learned words are shown; otherwise, <c>false</c>.
        /// </value>
        /// <owner>Mariia Yelisieieva</owner>
        public bool IsShownNotLearned
        {
            get { return isShownNotLearned; }

            set
            {
                Set(() => IsShownNotLearned, ref isShownNotLearned, value);
                this.RefreshCommand.Execute(null);
            }
        }

        /// <summary>
        /// Gets or sets the ListBox selected word.
        /// </summary>
        /// <value>
        /// The ListBox selected word.
        /// </value>
        /// <owner>Mariia Yelisieieva</owner>
        public Word ListBoxSelectedWord { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        public MainViewModel() : this(new WordsService(), new DialogOpen())
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="wordService">The service to interact with a words source.</param>
        /// <param name="dialogOpen">The instance to open add word dialog window.</param>
        /// <owner>Mariia Yelisieieva</owner>
        public MainViewModel(IWordsService wordService, IDialogOpen dialogOpen)
        {
            this.wordsService = wordService;
            this.dialogOpen = dialogOpen;
            this.Dictionary = new ObservableCollection<Word>();
        }

        /// <summary>
        /// The command to refresh the displayed list of words.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        private RelayCommand<int> refreshCommand;

        /// <summary>
        /// Gets the refresh command.
        /// </summary>
        /// <value>
        /// The refresh command.
        /// </value>
        /// <owner>Mariia Yelisieieva</owner>
        public RelayCommand<int> RefreshCommand
        {
            get
            {
                return this.refreshCommand
                       ?? (this.refreshCommand = new RelayCommand<int>(index =>
                       {
                           Dictionary.Clear();
                           
                           var filteredWords = this.wordsService.AllWords.Where(x => (IsShownNotLearned && x.Progress == LearningProgress.NotLearned)
                                                                                     || (IsShownInProgress && x.Progress == LearningProgress.InProgress)
                                                                                     || (IsShownLearned && x.Progress == LearningProgress.Learned));

                           foreach (Word word in filteredWords)
                           {
                               Dictionary.Add(word);
                           }

                           this.DetailedWord = null;
                       }));
            }
        }

        /// <summary>
        /// The command to select a word from the list.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        private RelayCommand selectCommand;

        /// <summary>
        /// Gets the select command.
        /// </summary>
        /// <value>
        /// The select command.
        /// </value>
        /// <owner>Mariia Yelisieieva</owner>
        public RelayCommand SelectCommand
        {
            get
            {
                return this.selectCommand
                       ?? (this.selectCommand = new RelayCommand(() =>
                       {
                           Word updated = null;
                           if (this.ListBoxSelectedWord != null)
                           {
                               updated = new Word()
                               {
                                   Id = this.ListBoxSelectedWord.Id,
                                   Chinese = this.ListBoxSelectedWord.Chinese,
                                   English = this.ListBoxSelectedWord.English,
                                   Pinyin = this.ListBoxSelectedWord.Pinyin,
                                   Progress = this.ListBoxSelectedWord.Progress
                               };
                               this.DetailedWord = updated;
                           }
                       }));
            }
        }

        /// <summary>
        /// The word selected to show details.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        private Word detailedWord;

        /// <summary>
        /// Gets or sets the word to show details of.
        /// </summary>
        /// <value>
        /// The copy of the selected word.
        /// </value>
        /// <owner>Mariia Yelisieieva</owner>
        public Word DetailedWord
        {
            get { return this.detailedWord; }
            set
            {
                this.IsFieldEnabled = value != null;
                Set(() => DetailedWord, ref this.detailedWord, value);

                UpdateCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// The command to update the selected word.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        private RelayCommand updateCommand;

        /// <summary>
        /// Gets the update command.
        /// </summary>
        /// <value>
        /// The update command.
        /// </value>
        /// <owner>Mariia Yelisieieva</owner>
        public RelayCommand UpdateCommand
        {
            get
            {
                return this.updateCommand
                       ?? (this.updateCommand = new RelayCommand(
                           () =>
                           {
                               this.wordsService.Update(this.DetailedWord);
                               this.RefreshCommand.Execute(null);

                               this.DetailedWord = null;
                           },
                           () => this.DetailedWord != null
                                && this.DetailedWord.Chinese != "" && this.DetailedWord.Pinyin != "" && this.DetailedWord.English != ""));
            }
        }

        /// <summary>
        /// The service to interact with a words source.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        private IWordsService wordsService;
    }
}