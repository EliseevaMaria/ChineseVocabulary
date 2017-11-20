using System;
using System.Data.Entity;

namespace Vocabulary.Model
{
    /// <summary>
    /// The database context class for a word entity.
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbContext" />
    /// <owner>Mariia Yelisieieva</owner>
    public class WordContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WordContext"/> class.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        public WordContext() : base()
        {
                
        }

        /// <summary>
        /// Gets or sets the database set of words.
        /// </summary>
        /// <value>
        /// The words.
        /// </value>
        /// <owner>Mariia Yelisieieva</owner>
        public DbSet<Word> Words { get; set; }
    }
}
