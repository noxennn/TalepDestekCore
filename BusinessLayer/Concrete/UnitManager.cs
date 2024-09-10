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
    public class UnitManager : IUnitService
    {
        IUnitDal _unitDal;

        public UnitManager(IUnitDal unitDal)
        {
            _unitDal = unitDal;
        }

        public IEnumerable<Unit> TGetListofActiveUnits()
        {
            return _unitDal.GetListByFilter(u => u.Status == true);
        }

        public IEnumerable<Unit> TGetListofInactiveUnits()
        {
            return _unitDal.GetListByFilter(u => u.Status == false);
        }

        public void TDelete(Unit t)
        {
            _unitDal.Delete(t);
        }

        public Unit TGetByID(int id)
        {
            return _unitDal.GetByID(id);
        }

        public List<Unit> TGetList()
        {
            return _unitDal.GetList();
        }

        public void TInsert(Unit t)
        {
            _unitDal.Insert(t);
        }

        public void TUpdate(Unit t)
        {
            _unitDal.Update(t);
        }
    }
}
