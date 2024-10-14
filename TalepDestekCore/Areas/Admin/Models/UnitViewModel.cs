using EntityLayer.Concrete;

namespace TalepDestekCore.Areas.Admin.Models
{
	public class UnitViewModel
	{
		public List<Unit> ActiveUnits { get; set; }
		public List<Unit> InactiveUnits { get; set; }
		public bool ShowInactiveUnits { get; set; }
	}
}
