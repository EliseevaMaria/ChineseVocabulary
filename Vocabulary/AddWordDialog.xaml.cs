using System;
using System.Windows;
using Vocabulary.Model;
using Vocabulary.ViewModel;

namespace Vocabulary
{
    /// <summary>
    /// Interaction logic for AddWordDialog.xaml
    /// </summary>
    /// <owner>Mariia Yelisieieva</owner>
    public partial class AddWordDialog : Window
    {
        /// <summary>
        /// The ViewModel instance for the model interaction logic.
        /// </summary>
        private AddWordViewModel AddViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddWordDialog"/> class.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        public AddWordDialog()
        {
            InitializeComponent();

            this.AddViewModel = (AddWordViewModel) this.DataContext;

            this.AddViewModel.Chinese = "";
            this.AddViewModel.Pinyin = "";
            this.AddViewModel.English = "";
            this.AddViewModel.Progress = LearningProgress.NotLearned;
        }

        /// <summary>
        /// Handles the Click event of the ProgressButton control and calls ChangeProgressCommand execution.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        /// <owner>Mariia Yelisieieva</owner>
        private void ProgressButton_Click(object sender, RoutedEventArgs e)
        {
            this.AddViewModel.ChangeProgressCommand.Execute(null);
        }

        /// <summary>
        /// Handles the Click event of the Button control and calls the AddWordCommand execution.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        /// <owner>Mariia Yelisieieva</owner>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.AddViewModel.AddWord.Execute(null);
        }
    }
}
