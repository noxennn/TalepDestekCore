using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfOfficerUnitDal : GenericRepository<OfficerUnit>, IOfficerUnitDal
    {
        public List<int> GetUnitIDsByOfficerID(int userId)
        {
            using (var  c = new Context())
            {
               
                return c.OfficerUnits.Where(x => x.UserID == userId).Select(x => x.UnitID).ToList();

            }
        }
		public List<int> GetOfficerIDsByUnitID(int unitID)
		{
            using (var c = new Context())
            {
                return c.OfficerUnits.Where(x=>x.UnitID==unitID).Select(x=>x.UserID).ToList();
            }
		}

		public void RemoveOfficerUnit(int unitID, int officerID)
        {
            using (var context = new Context())
            {
                var officerUnitsToRemove = context.OfficerUnits
                    .Where(x => x.UnitID == unitID && x.UserID == officerID)
                    .ToList();

                context.OfficerUnits.RemoveRange(officerUnitsToRemove);
                context.SaveChanges();
            }
        }
        public void AddOfficerUnit(int unitID, int officerID)
        {
            using (var context = new Context())
            {
                // Yeni OfficerUnit varlığı oluştur
                var officerUnit = new OfficerUnit
                {
                    UnitID = unitID,
                    UserID = officerID
                };

                // Varlığı ekle
                context.OfficerUnits.Add(officerUnit);
                context.SaveChanges();
            }
        }

		
	}
}
