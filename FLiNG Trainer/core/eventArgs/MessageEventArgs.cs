using System;

namespace FLiNG_Trainer.core.eventArgs;

public class MessageEventArgs : EventArgs
{
    public string Message { get; set; }

    public MessageEventArgs(string message)
    {
        Message = message;
    }
}
