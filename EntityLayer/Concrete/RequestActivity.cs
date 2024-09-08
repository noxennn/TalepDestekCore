using System;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class RequestActivity
    {
        [Key]
        public int RequestActivityID { get; set; }

        public int? RequestID { get; set; }  // Talebin IDsi
        public Request Request { get; set; }


        public int? ActivityStatusID { get; set; }  // Talebin Durumu (Başlangıç = Beklemede =id 2)
        public Activity Activity { get; set; }


        public int? AssignedUserID { get; set; } //Talebe atanan yetkili ID
        public AppUser AssignedUser { get; set; }


        public int? RequestActivityUserID { get; set; }  //Talebe işlem yapan yetkili ID
        public AppUser RequestActivityUser { get; set; }


        public int? NewActivityStatusID { get; set; }        //İşlem Sonucu Talebin yeni Durumu
        public Activity NewActivity { get; set; }


        public string? ActivityDescription { get; set; }   // İşlemin açıklaması
        public DateTime? ActivityDate { get; set; }   // İşlemin yapıldığı tarih   
        

        public int? Priority { get; set; }           // Öncelik seviyesi
        public string? FileUrl { get; set; }         // İşleme ait dosya url
        public string? FileName { get; set; }         // İşleme ait dosya adı
        public bool? Status { get; set; } = true; 
    }
}
