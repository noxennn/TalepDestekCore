using DTOLayer.DTOs.OfficerDTOs;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IAppUserService:IGenericService<AppUser>
    {
		public List<AssignOfficerDTO> TGetAssignableOfficerList(List<int> OfficerIDs);

		public string TGetUserNameByUserID(int UserID);


	}
}
