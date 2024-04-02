using FLiNG_Trainer.core.eventArgs;
using System.Windows;

namespace FLiNG_Trainer.core.listener;

public class MessageEventListener
{
    public delegate void MessageReceivedEventHandler(string message);
    public event MessageReceivedEventHandler MessageReceived;
    public MessageEventListener(MessageEventSender sender)
    {
        sender.MessageSent += OnMessageReceived;
    }

    private void OnMessageReceived(object sender, MessageEventArgs e)
    {
        MessageReceived?.Invoke(e.Message);
    }
}
