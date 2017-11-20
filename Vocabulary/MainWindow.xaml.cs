using System;
using System.Windows;
using System.Windows.Controls;
using Vocabulary.Model;
using Vocabulary.ViewModel;

namespace Vocabulary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// <owner>Mariia Yelisieieva</owner>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The ViewModel instance for the models interaction logic.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        private MainViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        public MainWindow()
        {
            InitializeComponent();

            this.viewModel = (MainViewModel) this.DataContext;
        }

        /// <summary>
        /// Handles the SelectionChanged event of the wordsListBox control and calls SelectCommand execution.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        /// <owner>Mariia Yelisieieva</owner>
        private void wordsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox senderListBox = (ListBox) sender;
            this.viewModel.SelectCommand.Execute((Word) senderListBox.SelectedItem);
        }

        /// <summary>
        /// Handles the TextChanged event of the wordTextBoxes control and calls UpdateCommand execution.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        /// <owner>Mariia Yelisieieva</owner>
        private void WordTextBoxes_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.viewModel.UpdateCommand.RaiseCanExecuteChanged();
        }
    }
}
