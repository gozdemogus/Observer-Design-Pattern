using System;
using Microsoft.AspNetCore.Identity;

namespace BaseIdentity.PresentationLayer.DAL
{
	public class AppRole:IdentityRole<int>
	{
		public AppRole()
		{
		}
	}
}

