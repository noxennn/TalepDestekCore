using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IOfficerUnitDal: IGenericDal<OfficerUnit>
    {
        public List<int> GetUnitIDsByOfficerID(int userId);

        public void RemoveOfficerUnit(int unitID,int officerID);
        public void AddOfficerUnit(int unitID,int officerID);
    }
}
