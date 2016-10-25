using Prism.Logging;
using System;

namespace TplPlayground.Core.Logging
{
    public struct LogMessage
    {
        public LogMessage(string message, Category category, Priority priority)
        {
            this.Message = message;
            this.Category = category;
            this.Priority = priority;
            this.Time = DateTime.Now.TimeOfDay;
        }

        public Category Category { get; }

        public string Message { get; }

        public Priority Priority { get; }

        public TimeSpan Time { get; }
    }
}
