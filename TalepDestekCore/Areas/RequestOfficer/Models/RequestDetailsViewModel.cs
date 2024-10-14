using EntityLayer.Concrete;

namespace TalepDestekCore.Areas.RequestOfficer.Models
{
	public class RequestDetailsViewModel
	{
		public Request Request { get; set; }
		public List<RequestActivity>? RequestActivities { get; set; }
		public RequestActivity OfficerActivity { get; set; }
	}
}
