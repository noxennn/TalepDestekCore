using EntityLayer.Concrete;

namespace TalepDestekCore.Areas.RequestOfficer.Models
{
    public class RequestsForOfficerViewModel
    {
        public List<Request> ActiveRequests { get; set; }
        public List<Request> InactiveRequests { get; set; }

        public bool ShowInactiveRequests { get; set; }
    }
}
