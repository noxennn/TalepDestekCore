﻿using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DTOLayer.DTOs.OfficerDTOs;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AppUserManager:IAppUserService
    {
        IAppUserDal _appUserDal;

		public AppUserManager(IAppUserDal appUserDal)
		{
			_appUserDal = appUserDal;
		}

		public void TDelete(AppUser t)
        {
            _appUserDal.Delete(t);
        }

		public List<AssignOfficerDTO> TGetAssignableOfficerList(List<int> OfficerIDs)
		{
			return _appUserDal.GetAssignableOfficerList(OfficerIDs);
		}
		public string TGetUserNameByUserID(int UserID)
        {
            return _appUserDal.GetUserNameByUserID(UserID);
        }

		public AppUser TGetByID(int id)
        {
            return _appUserDal.GetByID(id);
        }

        public List<AppUser> TGetList()
        {
            return _appUserDal.GetList();
        }

        public void TInsert(AppUser t)
        {
            _appUserDal.Insert(t);
        }

        public void TUpdate(AppUser t)
        {
            _appUserDal.Update(t);
        }
    }
}
