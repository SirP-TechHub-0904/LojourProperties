using LojourProperties.Domain.Data;
using LojourProperties.Domain.Dtos.NotifyDtos;
using LojourProperties.Domain.Models;
using static LojourProperties.Domain.Models.Enum;

namespace JurayUniversal.Application.Repository.NotifyRegister
{
    public class NotifyRegisterService : INotifyRegisterService
    {
        private readonly ApplicationDbContext _context;

        public NotifyRegisterService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task RegisterNotification(NotifyDto obj)
        {
            var settings = "";
            //var settings = await _context.SuperSettings.FirstOrDefaultAsync();
            if (obj.IsEmail == true)
            {
                //get the email template and assign it

                string template = settings;
                //string template = settings.EmailTemplate;

                template = template.Replace("{title}", obj.Title);
                template = template.Replace("{subtitle}", obj.NotificationTitle);
                template = template.Replace("{name}", obj.Name);
                template = template.Replace("{MESSAGE}", obj.Content);

                MailingSystem ms = new MailingSystem();
                ms.Receipient = obj.Receipient;
                ms.Title = obj.Title;
                ms.Content = template;
                ms.Retries = 0; ms.NotificationType = NotificationType.Email;
                ms.NotificationStatus = NotificationStatus.NotSent;
                _context.MailingSystems.Add(ms);


            }
            else
            {
                // string messagecontent = settings.SMSTemplate;
                // string smstemplate = settings.SMSTemplate;

                //smstemplate = smstemplate.Replace("{MESSAGE}", obj.Content);

                // MailingSystem ms = new MailingSystem();
                // ms.Receipient = obj.Receipient;
                // ms.Title = obj.Title;
                // ms.Content = smstemplate;
                // ms.Retries = 0; ms.NotificationType = NotificationType.SMS;
                // ms.NotificationStatus = NotificationStatus.NotSent;
                // _context.MailingSystems.Add(ms);

            }
            await _context.SaveChangesAsync();
        }
    }
}
