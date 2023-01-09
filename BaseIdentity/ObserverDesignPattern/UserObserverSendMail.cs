using System;
using BaseIdentity.PresentationLayer.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace BaseIdentity.PresentationLayer.ObserverDesignPattern
{
	public class UserObserverSendMail:IUserObserver
	{
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;


        public UserObserverSendMail(IServiceProvider serviceProvider, IConfiguration configuration = null)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

      
        public void CreateUser(AppUser appUser)
        {
            var logger = _serviceProvider.GetRequiredService<ILogger<UserObserverSendMail>>();
            //string mailKey = _configuration["MailKey"];

            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress("Observer Design", "goezdem6@gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", appUser.Email);
            mimeMessage.To.Add(mailboxAddressTo);

            var bodyBuilder = new BodyBuilder();

            mimeMessage.Body = bodyBuilder.ToMessageBody();

            mimeMessage.Subject = "Hoşgeldin Indirim Kodu";

            var body = new TextPart("plain")
            {
                Text = "Indirim kodunuz GIFT01"
            };

            mimeMessage.Body = body;

            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 587, false);
                smtp.Authenticate("goezdem6@gmail.com", ""); //kod
                smtp.Send(mimeMessage);
                smtp.Disconnect(true);
            }

            logger.LogInformation($"{appUser.Name + " " + appUser.Surname} isimli kullanıcının {appUser.Email} adlı adresine mail gönderildi.");
        }
    }
}

