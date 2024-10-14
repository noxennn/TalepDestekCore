using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IRequestDal:IGenericDal<Request>
    {
        public Request GetRequestInfo(int requestID);
        public List<Request> GetActiveUserRequests(int userID );
        public List<Request> GetInactiveUserRequests(int userID );

        public List<Request> GetActiveUserRequestsForOfficer(int userID ,List<int> userUnitIDs);
        public List<Request> GetInactiveUserRequestsForOfficer(int userID , List<int> userUnitIDs);

		public List<Request> GetAllActiveUserRequests();
		public List<Request> GetAllInactiveUserRequests();

		public int GetUnitIDByRequestID(int requestID);

		public int GetCountPendingRequestForRequestOwner(int userID );
        public int GetCountActiveRequestForRequestOwner(int userID );
        public int GetCountInactiveRequestForRequestOwner(int userID );
		public int GetCountPendingRequestForRequestOfficer(int userID, List<int> userUnitIDs);
		public int GetCountActiveRequestForRequestOfficer(int userID, List<int> userUnitIDs);
		public int GetCountInactiveRequestForRequestOfficer(int userID, List<int> userUnitIDs);

		public int GetCountAllPendingRequests();
		public int GetCountAllActiveRequests();
		public int GetCountAllInactiveRequests();



	}
}
