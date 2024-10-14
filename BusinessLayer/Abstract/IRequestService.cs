using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IRequestService : IGenericService<Request>
    {
        public Request TGetRequestInfo(int id);
        public List<Request> TGetActiveUserRequests(int id);
        public List<Request> TGetInactiveUserRequests(int id);

        public List<Request> TGetActiveUserRequestsForOfficer(int userID, List<int> userUnitIDs);
        public List<Request> TGetInactiveUserRequestsForOfficer(int userID, List<int> userUnitIDs);
		public List<Request> TGetAllActiveUserRequests();
		public List<Request> TGetAllInactiveUserRequests();
		public int TGetUnitIDByRequestID(int requestID);

		public int TGetCountPendingRequestForRequestOwner(int userID);
		public int TGetCountActiveRequestForRequestOwner(int userID);
		public int TGetCountInactiveRequestForRequestOwner(int userID);

		public int TGetCountPendingRequestForRequestOfficer(int userID, List<int> userUnitIDs);
		public int TGetCountActiveRequestForRequestOfficer(int userID, List<int> userUnitIDs);
		public int TGetCountInactiveRequestForRequestOfficer(int userID, List<int> userUnitIDs);

		public int TGetCountAllPendingRequests();
		public int TGetCountAllActiveRequests();
		public int TGetCountAllInactiveRequests();
	}
}
