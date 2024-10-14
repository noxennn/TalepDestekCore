using EntityLayer.Concrete;

namespace TalepDestekCore.Areas.Student.Models
{
	public class RequestDetailsViewModel
	{
		public Request Request { get; set; }
		public List<RequestActivity> RequestActivities { get; set; }
	}
}
