using Prism.Logging;

namespace TplPlayground.Core.Logging
{
    public struct LogMessage
    {
        public LogMessage(string message, Category category, Priority priority)
        {
            this.Message = message;
            this.Category = category;
            this.Priority = priority;
        }

        public Category Category { get; }

        public string Message { get; }

        public Priority Priority { get; }
    }
}
