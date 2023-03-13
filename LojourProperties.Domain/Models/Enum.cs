using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

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

        public enum ActivityStatus
        {
            [Description("None")]
            None = 0,

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