using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IOfficerUnitService : IGenericService<OfficerUnit>
    {
        public List<int> TGetUnitIDsByOfficerID(int userId);
        public void TRemoveOfficerUnit(int unitID,int officerID);
        public void TAddOfficerUnit(int unitID,int officerID);
    }
}
