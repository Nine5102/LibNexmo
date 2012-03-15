using System;
using System.Net;
using System.Text;
using System.Web;
using System.Globalization;
using System.Runtime.Serialization.Json;

namespace LibNexmo
{
    /// <summary>
    /// Represents a "connection" to nexmo.
    /// No real connection is made, this class mainly exists
    /// to save your API-credentials so you do not have to
    /// feed this information to every NexmoMessage object.
    /// </summary>
    public class NexmoConnection
    {
        /// <summary>
        /// Base URL to send all requests, https://rest.nexmo.com/sms/json by default.
        /// </summary>
        public Uri BaseUrl { get; set; }

        /// <summary>
        /// Encoding to use, not really useful at the moment.
        /// </summary>
        public Encoding Encoding { get; private set; }

        /// <summary>
        /// Your nexmo API username.
        /// </summary>
        public string Username { private get; set; }

        /// <summary>
        /// Your nexmo API password.
        /// </summary>
        public string Password { private get; set; }

        /// <summary>
        /// Public ctor that sets the BaseURL and Encoding.
        /// </summary>
        public NexmoConnection()
        {
            BaseUrl = new Uri("https://rest.nexmo.com/sms/json");
            Encoding = Encoding.UTF8;
        }

        /// <summary>
        /// Sends the message given.
        /// Note that it may be divided into several messages due to length limits.
        /// </summary>
        /// <param name="message">The message to send</param>
        /// <returns>A <see cref="NexmoResponse"/> object which contains information about the sent message(s)</returns>
        public NexmoResponse SendMessage(NexmoMessage message)
        {
            // Create a copy, since the NexmoMessage class is mutable.
            var copy = new NexmoMessage(message);

            var hwr = HttpWebRequest.Create(BaseUrl) as HttpWebRequest;
            hwr.Method = "POST";
            hwr.ContentType = "application/x-www-form-urlencoded";

            var postdatatxt = string.Format(CultureInfo.InvariantCulture,
                "username={0}&password={1}&from={2}&to={3}&text={4}",
                HttpUtility.UrlEncode(Username),
                HttpUtility.UrlEncode(Password),
                HttpUtility.UrlEncode(copy.From),
                HttpUtility.UrlEncode(copy.To),
                HttpUtility.UrlEncode(copy.Text));

            var postdata = Encoding.GetBytes(postdatatxt);

            hwr.ContentLength = postdata.LongLength;

            var stream = hwr.GetRequestStream();
            stream.Write(postdata, 0, postdata.Length);
            stream.Close();

            try
            {
                using (var response = hwr.GetResponse() as HttpWebResponse)
                {
                    try
                    {
                        var deserializer = new DataContractJsonSerializer(typeof(NexmoResponse));
                        return deserializer.ReadObject(response.GetResponseStream()) as NexmoResponse;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Could not parse the json response from Nexmo", ex);
                    }
                }
            }
            catch (WebException ex)
            {
                throw new Exception("Could not fetch response from Nexmo", ex);
            }
        }
    }
}
