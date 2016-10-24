using System.Collections.Generic;
using System.Linq;

namespace TplPlayground.CommandFileProcessor.Model
{
    public class CommandSection : FileSection
    {
        public CommandSection(
            string filePath,
            int sectionIndex,
            string commandName,
            IEnumerable<string> lines)
            : base(filePath, sectionIndex)
        {
            this.CommandName = commandName;
            this.Lines = lines;
        }

        /// <summary>
        /// Gets the name of the command.
        /// </summary>
        public string CommandName
        {
            get;
        }

        /// <summary>
        /// Gets the lines making up the command section.
        /// </summary>
        public IEnumerable<string> Lines
        {
            get;
        }

        public override string ToString() =>
            $"{FilePath}: {CommandName} (Index: {Index}, Lines: {Lines.Count()})";
    }
}
