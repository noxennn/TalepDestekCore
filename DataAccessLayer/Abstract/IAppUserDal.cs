using DTOLayer.DTOs.OfficerDTOs;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IAppUserDal:IGenericDal<AppUser>
    {
        public List<AssignOfficerDTO> GetAssignableOfficerList(List<int> OfficerIDs);

        public string GetUserNameByUserID(int UserID);

	}
}
