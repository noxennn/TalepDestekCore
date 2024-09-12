﻿using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
	public class CategoryManager : ICategoryService
	{
		ICategoryDal _categoryDal;

		public CategoryManager(ICategoryDal categoryDal)
		{
			_categoryDal = categoryDal;
		}

		public void TDelete(Category t)
		{
			_categoryDal.Delete(t);
		}

		public Category TGetByID(int id)
		{
			return _categoryDal.GetByID(id);
		}

		public List<Category> TGetList()
		{
			return _categoryDal.GetList();
		}

		public List<Category> TGetListofActiveCategories()
		{
			return _categoryDal.GetListByFilter(u => u.Status == true);
		}

		public List<Category> TGetListofInactiveCategories()
		{
			return _categoryDal.GetListByFilter(u => u.Status == false);
		}

		public void TInsert(Category t)
		{
			_categoryDal.Insert(t);
		}

		public void TUpdate(Category t)
		{
			_categoryDal.Update(t);
		}
	}
}
