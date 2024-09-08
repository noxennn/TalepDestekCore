using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Category
    {
        [Key] 
        public int CategoryID { get; set; } // Kategori numarası
        public string? CategoryName { get; set; }   // Kategori Adı
        public bool? Status { get; set; } = true;  // Kategori durumu (aktif,pasif)
        public ICollection<Request> Requests { get; set; }
    }
}
