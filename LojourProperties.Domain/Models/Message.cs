using static LojourProperties.Domain.Models.Enum;

namespace LojourProperties.Domain.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Mail { get; set; }
        public string Title { get; set; }
        public int Retries { get; set; }
        public NotificationStatus NotificationStatus { get; set; }
        public NotificationType NotificationType { get; set; }
    }
}
