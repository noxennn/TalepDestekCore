using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class AppUser:IdentityUser<int>
    {

        // TC Kimlik No'sunu AspNetUsers'da UserName ' de tutucağım
        public string? ImageURL { get; set; } // Kullanıcı foto url
        public string? Name { get; set; }  // Kullanıcı Adı
        public string? Surname { get; set; } // Kullanıcı Soyadı
        public string? Gender { get; set; }  //Cinsiyet

        public ICollection<Request> SentRequests { get; set; }
        public ICollection<Request> AssignedRequests { get; set; }

        public ICollection<RequestActivity> AssignedActivities { get; set; }
        public ICollection<RequestActivity> RequestActivities { get; set; }


		public ICollection<OfficerUnit> OfficerUnits { get; set; } // İlişkili birimler
	}
}
