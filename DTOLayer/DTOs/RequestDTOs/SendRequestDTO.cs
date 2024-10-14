using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs.RequestDTOs
{
	public class SendRequestDTO
	{
      
        public int? RequestUserID { get; set; }  // Talebi gönderen kullanıcı ID'si
 
        public int RequestCategoryID { get; set; }  // Talebin kategorisi

        public int? RequestLastActivityID { get; set; }  // Son aktivite (işlem)

        public int RequestUnitID { get; set; }  // Talebin ait olduğu birim

        public int? AssignedUserID { get; set; } // Talebe atanan kullanıcı  Başlangıçta null, daha sonra atanmış kişi

        public string RequestTitle { get; set; }  // Talep başlığı

        public string RequestDescription { get; set; }  // Talep açıklaması

        public DateTime? RequestDate { get; set; }  // Talep tarihi

        // Dosya URL'si (GUID'li URL)
        public string? RequestFileUrl { get; set; }

        // Dosya adı (orijinal adı)
        public string? RequestFileName { get; set; }  // Dosya adı

        public bool? Status { get; set; } = true;

    }
}
