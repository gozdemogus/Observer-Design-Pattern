using System;
using Microsoft.AspNetCore.Identity;

namespace BaseIdentity.PresentationLayer.DAL
{
	public class AppUser:IdentityUser<int>
	{
		public AppUser()
		{
		}

		public string Name { get; set; }
		public string Surname { get; set; }
		public string Description { get; set; }
	}
}

