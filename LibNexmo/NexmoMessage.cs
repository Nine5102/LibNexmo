using System;
using System.Runtime.Serialization;

namespace LibNexmo
{
    /// <summary>
    /// This class represents a message to send to someone.
    /// </summary>
    public class NexmoMessage
    {
        /// <summary>
        /// The number/text his message should originate from
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// The phone number this message should be sent to
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// The contents of the message
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Public parameterless constructor
        /// </summary>
        public NexmoMessage() { }

        /// <summary>
        /// Copy-constructor, needed since this class is mutable
        /// </summary>
        /// <param name="orig">The NexmoMessage to copy</param>
        internal NexmoMessage(NexmoMessage orig)
        {
            this.From = orig.From;
            this.To = orig.To;
            this.Text = orig.Text;
        }
    }
}
