using Prism.Logging;

namespace TplPlayground.Core.Logging
{
    public struct LogMessage
    {
        public string Message { get; }

        public Category Category { get; }

        public Priority Priority { get; }

        public LogMessage(string message, Category category, Priority priority)
        {
            this.Message = message;
            this.Category = category;
            this.Priority = priority;
        }
    }
}
