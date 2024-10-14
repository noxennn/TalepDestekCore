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
	public class EfRequestActivityDal : GenericRepository<RequestActivity>, IRequestActivityDal
	{
		public List< RequestActivity> GetRequestActivityByRequestID(int requestID)
		{
			using (var c = new Context())
			{
				return c.RequestActivities
							 .Where(a => a.RequestID == requestID && a.Status == true)
							 .Include(a=>a.Activity) //ActivityName
							 .Include(a=> a.RequestActivityUser) //User name surname
							 .OrderByDescending(a => a.ActivityDate) // Tarihe göre en yeniyi ilk sırada getirir
							 .ToList();
			}

			
		}
	}
}
