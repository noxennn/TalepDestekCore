using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs.ProfileDTOs
{

	public class ChangePasswordDTO
	{
		public string NewPassword { get; set; }
		public string ConfirmPassword { get; set; }
		public string CurrentPassword { get; set; }
		public string? ErrorMessage { get; set; }
		public bool Success { get; set; }
	}


}
