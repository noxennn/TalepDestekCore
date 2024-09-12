using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs.AppRoleDTOs
{
	public class AssignRoleDTO
	{
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int SelectedRoleId { get; set; }

    }
}
