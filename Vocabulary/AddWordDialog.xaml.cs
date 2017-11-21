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
        /// Initializes a new instance of the <see cref="AddWordDialog"/> class.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        public AddWordDialog()
        {
            InitializeComponent();

            AddWordViewModel addViewModel = (AddWordViewModel) this.DataContext;

            addViewModel.Chinese = "";
            addViewModel.Pinyin = "";
            addViewModel.English = "";
            addViewModel.Progress = LearningProgress.NotLearned;
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
        }
    }
}
