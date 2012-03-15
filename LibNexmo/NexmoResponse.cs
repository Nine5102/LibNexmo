using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace LibNexmo
{
    /// <summary>
    /// Represents a response from nexmo when attempting to send a message.
    /// </summary>
    [DataContract]
    public class NexmoResponse
    {
        /// <summary>
        /// Amount of messages that were sent.
        /// </summary>
        [DataMember(Name="message-count")]
        public int MessageCount { get; internal set; }

        /// <summary>
        /// True if all the messages were successfully sent.
        /// </summary>
        [IgnoreDataMember]
        public bool Success { get { return Messages.All(s => s.Status == NexmoResponseCode.Success); } }

        /// <summary>
        /// The list of messages that were sent.
        /// </summary>
        [DataMember(Name = "messages")]
        public NexmoResponseMsg[] Messages { get; set; }

        public override string ToString()
        {
            string str =  string.Format(System.Globalization.CultureInfo.CurrentCulture, "{{ MessageCount: {0}, Success: {1} }}\n", MessageCount, Success);
            foreach (var m in Messages)
                str += m.ToString() + "\n";
            return str;
        }
    }

    /// <summary>
    /// Represents a single message that was sent.
    /// </summary>
    [DataContract]
    public class NexmoResponseMsg
    {
        /// <summary>
        /// See <see cref="LibNexmo.NexmoResponseCode"/>
        /// </summary>
        [DataMember(Name="status")]
        public NexmoResponseCode Status { get; internal set; }

        /// <summary>
        /// The message id if this message
        /// </summary>
        [DataMember(Name="message-id")]
        public string MessageId { get; internal set; }

        /// <summary>
        /// Custom reference (not used by this library)
        /// </summary>
        [DataMember(Name="client-ref")]
        public string ClientRef { get; internal set; }

        /// <summary>
        /// Remaining balance after this message was sent
        /// </summary>
        [DataMember(Name="remaining-balance")]
        public string RemainingBalance { get; internal set; }

        /// <summary>
        /// Price of sending this message
        /// </summary>
        [DataMember(Name="message-price")]
        public string MessagePrice { get; internal set; }

        /// <summary>
        /// Textual representation of the error that occurred during sending (if any)7
        /// </summary>
        [DataMember(Name="error-text")]
        public string ErrorText { get; internal set; }

        public override string ToString()
        {
            return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{{\n\tStatus: \"{0}\", \n\tRemainingBalance: \"{1}\",\n\tMessagePrice: \"{2}\"\n}}",
                Status, RemainingBalance, MessagePrice);
        }
    }

    /// <summary>
    /// Enum representing the possible error codes from nexmo.
    /// </summary>
    public enum NexmoResponseCode : int
    {
        /// <summary>
        /// The message was successfully accepted for delivery by nexmo
        /// </summary>
        Success                     = 00,

        /// <summary>
        /// You have exceeded the submission capacity allowed on this account, please back-off and retry
        /// </summary>
        Throttled                   = 01,

        /// <summary>
        /// Your request is incomplete and missing some mandatory parameters
        /// </summary>
        MissingParams               = 02,

        /// <summary>
        /// The value of one or more parameters is invalid
        /// </summary>
        InvalidParams               = 03,

        /// <summary>
        /// The username / password you supplied is either invalid or disabled
        /// </summary>
        InvalidCredentials          = 04,

        /// <summary>
        /// An error has occurred in the nexmo platform whilst processing this message
        /// </summary>
        InternalError               = 05,

        /// <summary>
        /// The Nexmo platform was unable to process this message, for example, an un-recognized number prefix
        /// </summary>
        InvalidMessage              = 06,

        /// <summary>
        /// The number you are trying to submit to is blacklisted and may not receive messages
        /// </summary>
        NumberBarred                = 07,

        /// <summary>
        /// The username you supplied is for an account that has been barred from submitting messages
        /// </summary>
        PartnerAccountBarred        = 08,

        /// <summary>
        /// Your pre-pay account does not have sufficient credit to process this message
        /// </summary>
        PartnerQuotaExceeded        = 09,

        /// <summary>
        /// The number of simultaneous connections to the platform exceeds the capabilities of your account
        /// </summary>
        TooManyExistingBinds        = 10,

        /// <summary>
        /// This account is not provisioned for REST submission, you should use SMPP instead
        /// </summary>
        AccountNotEnabledForRest    = 11,

        /// <summary>
        /// Applies to Binary submissions, where the length of the UDH and the message body combined exceed 140 octets
        /// </summary>
        MessageTooLong              = 12,

        /// <summary>
        /// The sender address (from parameter) was not allowed for this message
        /// </summary>
        InvalidSenderAddress        = 15,

        /// <summary>
        /// The ttl parameter values is invalid
        /// </summary>
        InvalidTTL                  = 16
    }
}
