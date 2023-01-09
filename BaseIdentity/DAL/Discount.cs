using System;
namespace BaseIdentity.PresentationLayer.DAL
{
	public class Discount
	{
		public Discount()
		{
		}

		public int DiscountID { get; set; }
		public int UserID { get; set; }
		public int Rate { get; set; }

	}
}

