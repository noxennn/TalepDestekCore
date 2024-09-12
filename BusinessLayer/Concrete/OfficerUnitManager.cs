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
    public class OfficerUnitManager : IOfficerUnitService
    {
        IOfficerUnitDal _officerUnitDal;

        public OfficerUnitManager(IOfficerUnitDal officerUnitDal)
        {
            _officerUnitDal = officerUnitDal;
        }

        

        public void TDelete(OfficerUnit t)
        {
            _officerUnitDal.Delete(t);
        }

        public OfficerUnit TGetByID(int id)
        {
           return _officerUnitDal.GetByID(id) ;
        }

        public List<OfficerUnit> TGetList()
        {
            return _officerUnitDal.GetList();
        }

        public List<int> TGetUnitIDsByOfficerID(int userId)
        {
            return _officerUnitDal.GetUnitIDsByOfficerID(userId) ;
        }

        public void TInsert(OfficerUnit t)
        {
            _officerUnitDal.Insert(t) ;
        }

        public void TRemoveOfficerUnit(int unitID, int officerID)
        {
            _officerUnitDal.RemoveOfficerUnit(unitID, officerID) ;
        }
        public void TAddOfficerUnit(int unitID, int officerID)
        {
            _officerUnitDal.AddOfficerUnit(unitID , officerID) ;
        }

        public void TUpdate(OfficerUnit t)
        {
            _officerUnitDal.Update(t) ;
        }

    }
}
