using System;

namespace FLiNG_Trainer.core.eventArgs;

public class MessageEventSender
{
    public event EventHandler<MessageEventArgs> MessageSent;
    public void SendMessage(string message)
    {
        MessageSent?.Invoke(this, new MessageEventArgs(message));
    }
}
