namespace TplPlayground.CommandFileProcessor.Model
{
    public abstract class FileSection
    {
        protected FileSection(string filePath, int sectionIndex)
        {
            this.FilePath = filePath;
            this.Index = sectionIndex;
        }

        /// <summary>
        /// Gets the path to the file containing the command.
        /// </summary>
        public string FilePath
        {
            get;
        }

        /// <summary>
        /// Gets the index of the command section within the file.
        /// </summary>
        public int Index
        {
            get;
        }
    }
}
