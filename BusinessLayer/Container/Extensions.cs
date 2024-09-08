using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Container
{
    public static class Extensions
    {
        public static void ContainerDependencies(IServiceCollection services)
        {
            services.AddScoped<IActivityService, ActivityManager>();
            services.AddScoped<IActivityDal, EfActivityDal>();

            services.AddScoped<IAppUserService, AppUserManager>();
            services.AddScoped<IAppUserDal, EFAppUserDal>();

            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICategoryDal, EfCategoryDal>();

            services.AddScoped<IRequestActivityService, RequestActivityManager>();
            services.AddScoped<IRequestActivityDal, EfRequestActivityDal>();

            services.AddScoped<IRequestService, RequestManager>();
            services.AddScoped<IRequestDal, EfRequestDal>();

            services.AddScoped<IActivityService, ActivityManager>();
            services.AddScoped<IActivityDal, EfActivityDal>();

            services.AddScoped<IUnitService, UnitManager>();
            services.AddScoped<IUnitDal, EfUnitDal>();

        }
    }
}
