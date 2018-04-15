namespace Scrummy.Application.Web.Core.ViewModels
{
    public enum MessageType { Error, Warning, Success, Information }

    public class Message
    {
        public MessageType Type { get; set; }

        public string Text { get; set; }
    }
}
