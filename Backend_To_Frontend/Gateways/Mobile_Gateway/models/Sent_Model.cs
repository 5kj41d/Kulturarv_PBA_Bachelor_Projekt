using System;
using System.Diagnostics.CodeAnalysis;

public class Sent_Model
{
    [NotNull]
    public string _routing_key {set; get;}
    [NotNull]
    public string _message {set; get;}
    
    /// <summary>
    /// Version 1 Sent_Model. Includes routing key and message.
    /// </summary>
    public Sent_Model(string routing_key, string message)
    {
        _routing_key = routing_key;
        _message = message;
    }

    //Should user information be used and saved? 
    //Maybe use validator pattern? 
}