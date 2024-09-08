using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Request
    {
        [Key]
        public int RequestID { get; set; }
       

        public int? RequestUserID { get; set; }  // Talebi gönderen kullanıcı ID'si
        public AppUser User { get; set; } // foreign key


        public int? RequestCategoryID { get; set; }  // Talebin kategorisi
        public Category Category { get; set; }  // foreign key


        public int? RequestLastActivityID { get; set; }  // Son aktivite (işlem)
        public Activity Activity { get; set; }  // foreign key


        public int? RequestUnitID { get; set; }  // Talebin ait olduğu birim
        public Unit Unit { get; set; }  // foreign key


        public int? AssignedUserID { get; set; } // Talebe atanan kullanıcı  Başlangıçta null, daha sonra atanmış kişi
        public AppUser AssignedUser { get; set; }  // foreign key


        public string? RequestTitle { get; set; }  // Talep başlığı
        public string? RequestDescription { get; set; }  // Talep açıklaması
        public DateTime? RequestDate { get; set; }  // Talep tarihi
       

        // Dosya URL'si (GUID'li URL)
        public string? RequestFileUrl { get; set; }

        // Dosya adı (orijinal adı)
        public string? RequestFileName { get; set; }  // Dosya adı

        public bool? Status { get; set; } = true;

        public ICollection<RequestActivity> RequestActivities { get; set; }
    }
}
