using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Activity
    {
        [Key]
        public int ActivityID { get; set; } 
        public string? ActivityName { get; set; } // Hareketin adı (Beklemede,İşlem Devam Ediyor, İptal Edildi,Olumlu , Olumsuz)
        public ICollection<Request> Requests { get; set; }
        public ICollection<RequestActivity> RequestActivities { get; set; }

    }
}
