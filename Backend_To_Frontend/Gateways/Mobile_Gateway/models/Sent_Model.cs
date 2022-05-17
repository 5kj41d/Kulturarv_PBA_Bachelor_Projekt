using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Version 1 Sent_Model. Includes routing key and message.
/// </summary>
public class Sent_Model
{
    [NotNull]
    public string routing_key {set; get;}
    [NotNull]
    public string message {set; get;}
}