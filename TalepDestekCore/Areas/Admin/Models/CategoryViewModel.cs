using EntityLayer.Concrete;

namespace TalepDestekCore.Areas.Admin.Models
{
	public class CategoryViewModel
	{
		public List<Category> ActiveCategories { get; set; }
		public List<Category> InactiveCategories { get; set; }
		public bool ShowInactiveCategories { get; set; }
	}

}