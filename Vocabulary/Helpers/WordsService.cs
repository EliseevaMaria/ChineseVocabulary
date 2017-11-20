using System;
using System.Collections.Generic;
using System.Linq;
using Vocabulary.Model;

namespace Vocabulary.Helpers
{
    /// <summary>
    /// Class for working with the words database.
    /// </summary>
    /// <owner>Mariia Yelisieieva</owner>
    /// <seealso cref="Vocabulary.Helpers.IWordsService" />
    public class WordsService : IWordsService
    {
        /// <summary>
        /// Adds the specified word to the database.
        /// </summary>
        /// <param name="word">The word to add.</param>
        /// <owner>Mariia Yelisieieva</owner>
        public void Add(Word word)
        {
            using (var context = new WordContext())
            {
                context.Words.Add(word);
                context.SaveChanges();

                AllWords = context.Words.ToList();
            }
        }

        /// <summary>
        /// Words that were got from the database.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        public List<Word> AllWords { get; set; }

        /// <summary>
        /// Gets the words from the database.
        /// </summary>
        /// <returns>
        /// List of words.
        /// </returns>
        /// <owner>Mariia Yelisieieva</owner>
        public List<Word> GetWords()
        {
            using (var context = new WordContext())
            {
                AllWords = context.Words.ToList();
            }

            return AllWords;
        }

        /// <summary>
        /// Removes the specified word from the database.
        /// </summary>
        /// <param name="word">The word to remove.</param>
        /// <owner>Mariia Yelisieieva</owner>
        public void Remove(Word word)
        {
            using (var context = new WordContext())
            {
                var customer = context.Words.Single(x => x.Id == word.Id);
                context.Words.Remove(customer);
                context.SaveChanges();

                AllWords = context.Words.ToList();
            }
        }

        /// <summary>
        /// Updates the specified word from the database.
        /// </summary>
        /// <param name="word">The word with changed fields to update.</param>
        /// <owner>Mariia Yelisieieva</owner>
        public void Update(Word word)
        {
            using (var context = new WordContext())
            {
                AllWords = context.Words.ToList();

                Word wordToUpdate = AllWords.Single(x => x.Id == word.Id);

                wordToUpdate.Chinese = word.Chinese;
                wordToUpdate.Pinyin = word.Pinyin;
                wordToUpdate.English = word.English;
                wordToUpdate.Progress = word.Progress;

                context.SaveChanges();
            }
        }
    }
}
