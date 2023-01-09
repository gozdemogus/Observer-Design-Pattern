using System;
using BaseIdentity.PresentationLayer.DAL;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BaseIdentity.PresentationLayer.ObserverDesignPattern
{
	//IUserObserver olayların tetikleneceği methodu tutuyor. Burada kullanıcının eklenmesi olayı.
	public class UserObserverCreateDiscount:IUserObserver
	{
        private readonly IServiceProvider _serviceProvider;

        public UserObserverCreateDiscount(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void CreateUser(AppUser appUser)
        {
            var logger = _serviceProvider.GetRequiredService<ILogger<UserObserverCreateDiscount>>();
            var scoped = _serviceProvider.CreateScope(); //kaydedecek alanı yaratacak
            var context = scoped.ServiceProvider.GetRequiredService<Context>();

            context.Discounts.Add(new Discount
            {
                Rate = 25,
                UserID = appUser.Id
            });
            context.SaveChanges();
            logger.LogInformation($"Yeni kayıt olan kullanıcı:{appUser.Name + " " + appUser.Surname} için %25 oranında bir indirim kodu tanımlandı.");
        }
    }
}

