namespace TalepDestekCore.Areas.Admin.Models
{
	public class UserViewModel
	{
		public int UserID { get; set; }
		public string? ImageURL { get; set; } 
		public string Name { get; set; } 
		public string Surname { get; set; } 
		public string? Gender { get; set; }
		public string? Email { get; set; }

		public string UserName { get; set; }

		public string PhoneNumber { get; set; }
		
		public string UserRole { get; set; }
	}
}
