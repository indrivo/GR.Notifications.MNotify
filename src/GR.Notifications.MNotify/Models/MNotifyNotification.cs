namespace GR.Notifications.MNotify.Models
{
    public class MNotifyNotification
    {
        public string NotificationType { get; set; }
        public MNotifyPerson Sender { get; set; }
        public MNotifyPerson Recipient { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}