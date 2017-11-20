using System;
using System.Collections.Generic;
using Vocabulary.Model;

namespace Vocabulary.Helpers
{
    /// <summary>
    /// Interface which realizations provides working with a words source.
    /// </summary>
    /// <owner>Mariia Yelisieieva</owner>
    public interface IWordsService
    {
        /// <summary>
        /// Adds the specified word.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        /// <param name="word">The word to add.</param>
        void Add(Word word);

        /// <summary>
        /// Words that were got from a source.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        List<Word> AllWords { set; get; }

        /// <summary>
        /// Gets the words.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        /// <returns>List of words.</returns>
        List<Word> GetWords();

        /// <summary>
        /// Removes the specified word.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        /// <param name="word">The word to remove.</param>
        void Remove(Word word);

        /// <summary>
        /// Updates the specified word.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        /// <param name="word">The word with changed fields to update.</param>
        void Update(Word word);
    }
}
