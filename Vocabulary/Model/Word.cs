using System;
using GalaSoft.MvvmLight;

namespace Vocabulary.Model
{
    /// <summary>
    /// A word entity class.
    /// </summary>
    /// <owner>Mariia Yelisieieva</owner>
    /// <seealso cref="GalaSoft.MvvmLight.ObservableObject" />
    public class Word : ObservableObject
    {
        /// <summary>
        /// Changes the learning progress from the current one to the next one.
        /// </summary>
        /// <param name="progress">The word learning progress.</param>
        /// <returns></returns>
        /// <owner>Mariia Yelisieieva</owner>
        public static LearningProgress ChangeLearningProgress(LearningProgress progress)
        {
            int valuesNumber = Enum.GetNames(typeof(LearningProgress)).Length;
            int newProgressInt = ((int)progress + 1) % valuesNumber;
            progress = (LearningProgress)newProgressInt;
            return progress;
        }

        /// <summary>
        /// The Chinese word.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        private string chinese;

        /// <summary>
        /// Gets or sets the Chinese word.
        /// </summary>
        /// <value>
        /// The Chinese word.
        /// </value>
        /// <owner>Mariia Yelisieieva</owner>
        public string Chinese
        {
            set { Set(() => Chinese, ref chinese, value); }
            get { return this.chinese; }
        }

        /// <summary>
        /// The English word.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        private string english;

        /// <summary>
        /// Gets or sets the English word - the translation of the Chinese word.
        /// </summary>
        /// <value>
        /// The english.
        /// </value>
        /// <owner>Mariia Yelisieieva</owner>
        public string English
        {
            set { Set(() => English, ref english, value); }
            get { return this.english; }
        }

        /// <summary>
        /// Gets or sets the entity identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        /// <owner>Mariia Yelisieieva</owner>
        public int Id { set; get; }

        /// <summary>
        /// The pinyin of the Chinese word.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        private string pinyin;

        /// <summary>
        /// Gets or sets the pinyin - transcription of the Chinese word.
        /// </summary>
        /// <value>
        /// The pinyin.
        /// </value>
        /// <owner>Mariia Yelisieieva</owner>
        public string Pinyin
        {
            set { Set(() => Pinyin, ref pinyin, value); }
            get { return this.pinyin; }
        }

        /// <summary>
        /// The word learning progress.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        private LearningProgress progress;

        /// <summary>
        /// Gets or sets the word <see cref="LearningProgress"/>.
        /// </summary>
        /// <value>
        /// The progress.
        /// </value>
        /// <owner>Mariia Yelisieieva</owner>
        public LearningProgress Progress
        {
            set { Set(() => Progress, ref progress, value); }
            get { return this.progress; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Word"/> class.
        /// </summary>
        /// <owner>Mariia Yelisieieva</owner>
        public Word()
        {
                
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Word"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="chinese">The Chinese word.</param>
        /// <param name="pinyin">The pinyin - the transcription of the Chinese word.</param>
        /// <param name="english">The English word.</param>
        /// <param name="progress">The word learning progress.</param>
        /// <owner>Mariia Yelisieieva</owner>
        public Word(int id, string chinese, string pinyin, string english, LearningProgress progress = LearningProgress.NotLearned)
        {
            this.Id = id;
            this.Chinese = chinese;
            this.Pinyin = pinyin;
            this.English = english;
            this.Progress = progress;
        }
    }
}
