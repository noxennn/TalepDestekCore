using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Unit
    {
        [Key] 
        public int UnitID { get; set; }

        public string? UnitName { get; set; }  //Birim Adı
        public bool? Status { get; set; } = true;
        public ICollection<Request> Requests { get; set; }

    }
}
