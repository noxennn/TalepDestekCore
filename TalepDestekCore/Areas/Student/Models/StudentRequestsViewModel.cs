using EntityLayer.Concrete;

namespace TalepDestekCore.Areas.Student.Models
{
	public class StudentRequestsViewModel
	{
		public List<Request> ActiveRequests { get; set; }
		public List<Request> InactiveRequests { get; set; }
		public bool ShowInactiveRequests { get; set; }
	}
}
