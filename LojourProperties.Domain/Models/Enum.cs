using System.ComponentModel;

namespace LojourProperties.Domain.Models
{
    public class Enum
    {
        
            public enum PropertyStatus
        {
            [Description("None")]
            None = 0,

            [Description("Publish")]
            Publish = 2,

            [Description("Unpublish")]
            Unpublish = 3,

        }  
        public enum EmailType
        {
            [Description("None")]
            None = 0,

            [Description("ChangeEmail")]
            ChangeEmail = 2,

            [Description("VerifyEmail")]
            VerifyEmail = 3,


        }
         public enum UserStatus
        {
            [Description("Pending")]
            Pending = 0,

            [Description("Active")]
            Active = 2,
            [Description("Suspended")]
            Suspended = 3,

            [Description("Leave")]
            Leave = 4,
            [Description("Deleted")]
            Deleted = 6,
        }
        public enum ActivityStatus
        {
            [Description("Available")]
            Available = 0,

            [Description("Sold")]
            Sold = 2,

            [Description("Rented")]
            Rented = 3,

            [Description("Leased")]
            Leased = 4,


        }

        public enum ProfileType
        {
            [Description("None")]
            None = 0,

            [Description("Agent")]
            Agent = 2,

            [Description("LandLord")]
            LandLord = 3,

        }
          public enum PropertyLink
        {
            [Description("None")]
            None = 0,

            [Description("Platinum")]
            Platinum = 2,

            [Description("Diamond")]
            Diamond = 3,
              [Description("Gold")]
            Gold = 4,

        }

        public enum PagePosition
        {
            [Description("None")]
            None = 0,

            [Description("Top")]
            Top = 2,

            [Description("Menu")]
            Menu = 3,
            [Description("Footer")]
            Footer = 4,
        }


        public enum DocumentPermission
        {
            [Description("None")]
            None = 0,

            [Description("Private")]
            Private = 2,

            [Description("Public")]
            Public = 3,

            [Description("Hide")]
            Hide = 4,

        }

        public enum NotificationStatus
        {
            [Description("NotDefind")]
            NotDefind = 0,
            [Description("Sent")]
            Sent = 1,

            [Description("NotSent")]
            NotSent = 2,


        }
        public enum NotificationType
        {
            [Description("NotDefind")]
            NotDefind = 0,
            [Description("SMS")]
            SMS = 1,

            [Description("Email")]
            Email = 2


        }
    }
}