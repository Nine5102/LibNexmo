LibNexmo
========
LibNexmo is a small C# library for sending SMS-messages via Nexmo.
Usage requires an account at Nexmo.

Usage
-----
A minimal usage example:

    var conn = new NexmoConnection { Username = "abc", Password = "def" };
    var msg = new NexmoMessage { To = "+4712345678", From = "Test", Text = "Hello, World!" };
    var response = conn.SendMessage(msg);

Examine the NexmoResponse object returned by SendMessage to determine success.

Official releases
-----------------
There is a precompiled, signed release of 1.0 available at

http://lantea.net/libnexmo/LibNexmo-1.0.zip

SHA1-sum: a25996e27f3e8d91bb77ece8b2388cd6daeaee12

PublicKeyToken=aaf70252a71bbbb2

