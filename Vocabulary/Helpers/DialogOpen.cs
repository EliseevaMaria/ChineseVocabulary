using System;

namespace Vocabulary.Helpers
{
    /// <summary>
    /// Class for opening AddWordDialog window.
    /// </summary>
    /// <owner>Mariia Yelisieieva</owner>
    /// <seealso cref="Vocabulary.Helpers.IDialogOpen" />
    public class DialogOpen : IDialogOpen
    {
        /// <summary>
        /// Opens the add window as a dialog.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        public void OpenAddWindowDialog()
        {
            AddWordDialog addDialog = new AddWordDialog();
            addDialog.ShowDialog();
        }
    }
}
