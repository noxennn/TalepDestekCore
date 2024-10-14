using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using DTOLayer.DTOs.OfficerDTOs;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
	public class EFAppUserDal : GenericRepository<AppUser>, IAppUserDal
	{
		public List<AssignOfficerDTO> GetAssignableOfficerList(List<int> OfficerIDs)
		{
			using (var c = new Context())
			{
				return c.Users.Where(x => OfficerIDs.Contains(x.Id)).Select(x => new AssignOfficerDTO
				{
					AssignableOfficerID = x.Id,
					AssignableOfficerName = x.Name + " " + x.Surname
				})
				.ToList();
			}
		}

		public string GetUserNameByUserID(int UserID)
		{
			using (var c = new Context())
			{
				return c.Users.Where(x => x.Id == UserID).Select(
					x => x.Name + " " + x.Surname).FirstOrDefault();
			}
		}
	}
}