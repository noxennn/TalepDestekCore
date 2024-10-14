using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace DataAccessLayer.Concrete
{
    public class Context :IdentityDbContext<AppUser,AppRole,int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=YusufEmre\\SQLEXPRESS;Database=TalepDestekCore;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");
        }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestActivity> RequestActivities { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<OfficerUnit> OfficerUnits { get; set; }



        //Veritabanı İlişki yapılandırmaları

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			base.OnModelCreating(modelBuilder);
			//Request ilişkileri

			//RequestUserID Foreign Key From AppUser.UserID


			modelBuilder.Entity<Request>()
                .HasOne(r=>r.User)
                .WithMany(u=>u.SentRequests)
                .HasForeignKey(r=>r.RequestUserID)
                .OnDelete(DeleteBehavior.Restrict);

            //RequestCategoryID Foreign Key From Category.CategoryID

            modelBuilder.Entity<Request>()
                .HasOne(r => r.Category)
                .WithMany(c => c.Requests)
                .HasForeignKey(r => r.RequestCategoryID)
                .OnDelete(DeleteBehavior.Restrict);

            //RequestLastActivityID Foreign Key From Activity.ActivityID

            modelBuilder.Entity<Request>()
                .HasOne(r => r.Activity)
                .WithMany(a => a.Requests)
                .HasForeignKey(r => r.RequestLastActivityID)
                .OnDelete(DeleteBehavior.Restrict);

            //RequestUnitID Foreign Key From Unit.UnitID

            modelBuilder.Entity<Request>()
                .HasOne(r=>r.Unit)
                .WithMany(u=>u.Requests)
                .HasForeignKey(r=>r.RequestUnitID)
                .OnDelete(DeleteBehavior.Restrict);

            //AssignedUserID Foreign Key From AppUser.UserID

            modelBuilder.Entity<Request>()
                .HasOne(r => r.AssignedUser)
                .WithMany(a => a.AssignedRequests)
                .HasForeignKey(r => r.AssignedUserID)
                .OnDelete(DeleteBehavior.Restrict);


            //RequestActivity ilişkileri
            //RequestID Foreign Key From Request.RequestID

            modelBuilder.Entity<RequestActivity>()
                .HasOne(r => r.Request)
                .WithMany(a => a.RequestActivities)
                .HasForeignKey(r => r.RequestID)
                .OnDelete(DeleteBehavior.Restrict);

            //ActivityStatusID Foreign Key From Activity.ActivityID

            modelBuilder.Entity<RequestActivity>()
                .HasOne(r => r.Activity)
                .WithMany(a => a.RequestActivities)
                .HasForeignKey(r => r.ActivityStatusID)
                .OnDelete(DeleteBehavior.Restrict);

            //AssignedUserID Foreign Key From AppUser.UserID

            modelBuilder.Entity<RequestActivity>()
                .HasOne(r => r.AssignedUser)
                .WithMany(a => a.AssignedActivities)
                .HasForeignKey(r => r.AssignedUserID)
                .OnDelete(DeleteBehavior.Restrict);

            //RequestActivityUserID Foreign Key From AppUser.UserID

            modelBuilder.Entity<RequestActivity>()
                .HasOne(r => r.RequestActivityUser)
                .WithMany(u => u.RequestActivities)
                .HasForeignKey(r => r.RequestActivityUserID)
                .OnDelete(DeleteBehavior.Restrict);


			//Unit Claims
			//NewActivityStatusID Foreign Key From Activity.ActivityID

			// AppUser ve UnitClaims arasındaki ilişki
			modelBuilder.Entity<OfficerUnit>()
				.HasOne(uc => uc.User) // UnitClaims sınıfındaki User ile ilişkiyi tanımlar
				.WithMany(u => u.OfficerUnits) 
				.HasForeignKey(uc => uc.UserID) 
				.OnDelete(DeleteBehavior.Cascade); // Silindiğinde ilgili UnitClaims'i de sil


			// Unit ve UnitClaims arasındaki ilişki
			modelBuilder.Entity<OfficerUnit>()
				.HasOne(uc => uc.Unit) // UnitClaims sınıfındaki Unit ile ilişkiyi tanımlar
				.WithMany(u=>u.OfficerUnits) 
				.HasForeignKey(uc => uc.UnitID) 
				.OnDelete(DeleteBehavior.Cascade); // Silindiğinde ilgili UnitClaims'i de sil

		}
    }


    

}
