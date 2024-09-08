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
    public class ActivityManager : IActivityService
    {
        IActivityDal _activityDal;
        public void TDelete(Activity t)
        {
            _activityDal.Delete(t);
        }

        public Activity TGetByID(int id)
        {
            return _activityDal.GetByID(id);
        }

        public List<Activity> TGetList()
        {
            return _activityDal.GetList();
        }

        public void TInsert(Activity t)
        {
            _activityDal.Insert(t);
        }

        public void TUpdate(Activity t)
        {
            _activityDal.Update(t);
        }
    }
}
