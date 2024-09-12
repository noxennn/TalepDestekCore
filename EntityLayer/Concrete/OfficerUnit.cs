using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
	public class OfficerUnit
	{
		[Key]
		public int OfficerUnitID { get; set; }

		public int UserID { get; set; }  // Kullanıcının AspNetUser tablosundaki ID'si
		public AppUser User { get; set; }   // Foreign key olarak AppUser

		public int UnitID { get; set; }     // Birim ID'si
		public Unit Unit { get; set; }      // Foreign key olarak Unit
	}
}