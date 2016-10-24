using System.Collections.Generic;
using System.Linq;

namespace TplPlayground.CommandFileProcessor.Model
{
    public class HeaderSection : FileSection
    {
        public HeaderSection(
            string filePath,
            int sectionIndex,
            IEnumerable<string> lines)
            : base(filePath, sectionIndex)
        {
            this.Lines = lines;
        }

        /// <summary>
        /// Gets the lines making up the command section.
        /// </summary>
        public IEnumerable<string> Lines
        {
            get;
        }

        public override string ToString() =>
            $"{FilePath}: Header (Index: {Index}, Lines: {Lines.Count()})";
    }
}
