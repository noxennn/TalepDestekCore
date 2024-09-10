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
    public class RequestActivityManager:IRequestActivityService
    {
        IRequestActivityDal _requestActivityDal;

		public RequestActivityManager(IRequestActivityDal requestActivityDal)
		{
			_requestActivityDal = requestActivityDal;
		}

		public void TDelete(RequestActivity t)
        {
            _requestActivityDal.Delete(t);
        }

        public RequestActivity TGetByID(int id)
        {
           return _requestActivityDal.GetByID(id);
        }

        public List<RequestActivity> TGetList()
        {
            return _requestActivityDal.GetList();
        }

        public void TInsert(RequestActivity t)
        {
            _requestActivityDal.Insert(t);
        }

        public void TUpdate(RequestActivity t)
        {
            _requestActivityDal.Update(t);
        }
    }
}
