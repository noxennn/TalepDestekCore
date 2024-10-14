using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class RequestManager:IRequestService
    {
        IRequestDal _requestDal;

		public RequestManager(IRequestDal requestDal)
		{
			_requestDal = requestDal;
		}

        public List<Request> TGetActiveUserRequestsForOfficer(int userID, List<int> userUnitIDs)
        {
            return _requestDal.GetActiveUserRequestsForOfficer(userID, userUnitIDs);
        }

        public List<Request> TGetInactiveUserRequestsForOfficer(int userID, List<int> userUnitIDs)
        {
            return _requestDal.GetInactiveUserRequestsForOfficer(userID,userUnitIDs);
        }

        public void TDelete(Request t)
        {
            _requestDal.Delete(t);
        }

		public List<Request> TGetActiveUserRequests(int id)
		{
			return _requestDal.GetActiveUserRequests(id);
		}

		public Request TGetByID(int id)
        {
            return _requestDal.GetByID(id);
        }

		public List<Request> TGetInactiveUserRequests(int id)
		{
			return _requestDal.GetInactiveUserRequests(id);
		}

		public List<Request> TGetAllActiveUserRequests()
		{
			return _requestDal.GetAllActiveUserRequests();
		}

		public List<Request> TGetAllInactiveUserRequests()
		{
			return _requestDal.GetAllInactiveUserRequests();
		}

		public List<Request> TGetList()
        {
            return _requestDal.GetList();
        }

		public Request TGetRequestInfo(int id)
		{
            return _requestDal.GetRequestInfo(id);
		}

		public void TInsert(Request t)
        {
            _requestDal.Insert(t);
        }

        public void TUpdate(Request t)
        {
            _requestDal.Update(t);
        }

		public int TGetUnitIDByRequestID(int requestID)
		{
			return _requestDal.GetUnitIDByRequestID(requestID);
		}
		public int TGetCountPendingRequestForRequestOwner(int userID)
		{
			return _requestDal.GetCountPendingRequestForRequestOwner(userID);
		}

		public int TGetCountActiveRequestForRequestOwner(int userID)
		{
			return _requestDal.GetCountActiveRequestForRequestOwner(userID);
		}

		public int TGetCountInactiveRequestForRequestOwner(int userID)
		{
			return _requestDal.GetCountInactiveRequestForRequestOwner(userID);
		}

		public int TGetCountPendingRequestForRequestOfficer(int userID, List<int> userUnitIDs)
		{
			return _requestDal.GetCountPendingRequestForRequestOfficer(userID ,userUnitIDs);
		}

		public int TGetCountActiveRequestForRequestOfficer(int userID, List<int> userUnitIDs)
		{
			return _requestDal.GetCountActiveRequestForRequestOfficer(userID, userUnitIDs);
		}

		public int TGetCountInactiveRequestForRequestOfficer(int userID, List<int> userUnitIDs)
		{
			return _requestDal.GetCountInactiveRequestForRequestOfficer(userID, userUnitIDs);
		}

		public int TGetCountAllPendingRequests()
		{
			return _requestDal.GetCountAllPendingRequests();
		}

		public int TGetCountAllActiveRequests()
		{
			return _requestDal.GetCountAllActiveRequests();
		}

		public int TGetCountAllInactiveRequests()
		{
			return _requestDal.GetCountAllInactiveRequests();
		}

		
	}
}
