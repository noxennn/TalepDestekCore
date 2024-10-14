using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
	public class EfRequestDal : GenericRepository<Request>, IRequestDal
	{


		public List<Request> GetActiveUserRequests(int userID)
		{
			using (var c = new Context())
			{
				return c.Requests.Where(r => r.RequestUserID == userID && r.Status == true)
					.Include(r => r.User)
					.Include(r => r.Category)
					.Include(r => r.Activity)
					.Include(r => r.Unit).ToList();
			}
		}

		public List<Request> GetInactiveUserRequests(int userID)
		{
			using (var c = new Context())
			{
				return c.Requests.Where(r => r.RequestUserID == userID && r.Status == false)
					.Include(r => r.User)
					.Include(r => r.Category)
					.Include(r => r.Activity)
					.Include(r => r.Unit).ToList();
			}
		}

		public List<Request> GetActiveUserRequestsForOfficer(int userID, List<int> userUnitIDs)
		{
			using (var c = new Context())
			{
				return c.Requests.Where(r => userUnitIDs.Contains(r.RequestUnitID)
				&& (r.AssignedUserID == null || r.AssignedUserID == userID)
				&& r.Status == true)
					.Include(r => r.User)
					.Include(r => r.Category)
					.Include(r => r.Activity)
					.Include(r => r.Unit).ToList();
			}

		}





		public List<Request> GetInactiveUserRequestsForOfficer(int userID, List<int> userUnitIDs)
		{
			using (var c = new Context())
			{
				return c.Requests.Where(r => userUnitIDs.Contains(r.RequestUnitID)
				&& (r.AssignedUserID == null || r.AssignedUserID == userID)
				&& r.Status == false)
					.Include(r => r.User)
					.Include(r => r.Category)
					.Include(r => r.Activity)
					.Include(r => r.Unit).ToList();
			}

		}
		public List<Request> GetAllActiveUserRequests()
		{
			using (var c = new Context())
			{
				return c.Requests.Where(r => r.Status == true)
					.Include(r => r.User)
					.Include(r => r.Category)
					.Include(r => r.Activity)
					.Include(r => r.Unit).ToList();
			}
		}

		public List<Request> GetAllInactiveUserRequests()
		{
			using (var c = new Context())
			{
				return c.Requests.Where(r => r.Status == false)
					.Include(r => r.User)
					.Include(r => r.Category)
					.Include(r => r.Activity)
					.Include(r => r.Unit).ToList();
			}
		}



		public Request GetRequestInfo(int requestID)
		{
			using (var c = new Context())
			{
				return c.Requests.Where(r => r.RequestID == requestID)
					.Include(r => r.User)
					.Include(r => r.Category)
					.Include(r => r.Activity)
					.Include(r => r.Unit).FirstOrDefault();

			}
		}

		public int GetUnitIDByRequestID(int requestID)
		{
			using(var c = new Context())
			{
				return c.Requests.Where(r => r.RequestID == requestID).Select(r=>r.RequestUnitID).FirstOrDefault();
			}
		}


		public int GetCountPendingRequestForRequestOwner(int userID)
		{
			using (var c = new Context())
			{
				return c.Requests.Where(r => r.RequestUserID == userID && r.RequestLastActivityID == 1).Count();
			}
		}

		public int GetCountActiveRequestForRequestOwner(int userID)
		{
			using (var c = new Context())
			{
				return c.Requests.Where(r => r.RequestUserID == userID && r.RequestLastActivityID == 2).Count();
			}
		}

		public int GetCountInactiveRequestForRequestOwner(int userID)
		{
			using (var c = new Context())
			{
				return c.Requests.Where(r => r.RequestUserID == userID && r.RequestLastActivityID > 2).Count();
			}
		}


		public int GetCountPendingRequestForRequestOfficer(int userID, List<int> userUnitIDs)
		{
			using (var c = new Context())
			{
				return c.Requests.Where(r => userUnitIDs.Contains(r.RequestUnitID)
				&& (r.AssignedUserID == null || r.AssignedUserID == userID)
				&& r.RequestLastActivityID == 1).Count();

			}
		}
		public int GetCountActiveRequestForRequestOfficer(int userID, List<int> userUnitIDs)
		{
			using (var c = new Context())
			{
				return c.Requests.Where(r => userUnitIDs.Contains(r.RequestUnitID)
				&& (r.AssignedUserID == null || r.AssignedUserID == userID)
				&& r.RequestLastActivityID == 2).Count();

			}
		}
		public int GetCountInactiveRequestForRequestOfficer(int userID, List<int> userUnitIDs)
		{
			using (var c = new Context())
			{
				return c.Requests.Where(r => userUnitIDs.Contains(r.RequestUnitID)
				&& (r.AssignedUserID == null || r.AssignedUserID == userID)
				&& r.RequestLastActivityID > 2).Count();

			}
		}

		public int GetCountAllPendingRequests()
		{
			using (var c = new Context())
			{
				return c.Requests.Where(r => r.RequestLastActivityID == 1).Count();
			}
		}

		public int GetCountAllActiveRequests()
		{
			using (var c = new Context())
			{
				return c.Requests.Where(r => r.RequestLastActivityID == 2).Count();
			}
		}

		public int GetCountAllInactiveRequests()
		{
			using (var c = new Context())
			{
				return c.Requests.Where(r => r.RequestLastActivityID > 2).Count();
			}

		}

		
	}
}
