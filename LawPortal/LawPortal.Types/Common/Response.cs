using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LawPortal.Types.Common
{
    public class Response
    {
        private List<Message> messages;
        public List<Message> Messages
        {
            get
            {
                if (messages == null) messages = new List<Message>();
                return messages;
            }
            set { messages = value; }
        }

        public bool Success { get { return !Messages.Any(x => x.Level == MessageLevel.Error); } }
    }

    public class GenericResponse<T> : Response where T : new()
    {
        public T Value { get; set; }
    }

    public class Message
    {
        public string Code { get; set; }
        public string Text { get; set; }
        public MessageLevel Level { get; set; }
    }

    public enum MessageLevel
    {
        Error,
        Warning,
        Message
    }
}