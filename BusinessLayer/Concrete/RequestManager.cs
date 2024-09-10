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
    public class RequestManager:IRequestService
    {
        IRequestDal _requestDal;

		public RequestManager(IRequestDal requestDal)
		{
			_requestDal = requestDal;
		}

		public void TDelete(Request t)
        {
            _requestDal.Delete(t);
        }

        public Request TGetByID(int id)
        {
            return _requestDal.GetByID(id);
        }

        public List<Request> TGetList()
        {
            return _requestDal.GetList();
        }

        public void TInsert(Request t)
        {
            _requestDal.Insert(t);
        }

        public void TUpdate(Request t)
        {
            _requestDal.Update(t);
        }
    }
}
