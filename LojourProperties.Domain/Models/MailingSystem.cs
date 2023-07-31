using static LojourProperties.Domain.Models.Enum;

namespace LojourProperties.Domain.Models
{
    public class MailingSystem
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Receipient { get; set; }
        public string Content { get; set; } 
        public int Retries { get; set; }
        public NotificationStatus NotificationStatus { get; set; }
        public NotificationType NotificationType { get; set; }
    }
}
