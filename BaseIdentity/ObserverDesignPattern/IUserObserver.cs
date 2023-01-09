using System;
using BaseIdentity.PresentationLayer.DAL;

namespace BaseIdentity.PresentationLayer.ObserverDesignPattern
{
	public interface IUserObserver
	{
        void CreateUser(AppUser appUser);
    }
}

