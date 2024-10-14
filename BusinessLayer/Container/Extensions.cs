using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules.AppUserValidationRules;
using BusinessLayer.ValidationRules.ProfileValidationRules;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using DTOLayer.DTOs.AppUserDTOs;
using DTOLayer.DTOs.ProfileDTOs;
using FluentValidation;
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

            services.AddScoped<IOfficerUnitService, OfficerUnitManager>();
            services.AddScoped<IOfficerUnitDal, EfOfficerUnitDal>();


            services.AddScoped<IValidator<AppUserSignUpDTO>, AppUserSignUpValidator>();

			services.AddScoped<IValidator<AppUserSignInDTO>, AppUserSignInValidator>();

			services.AddScoped<IValidator<ChangePasswordDTO>, ProfileChangePasswordValidator>();


		}
    }
}
