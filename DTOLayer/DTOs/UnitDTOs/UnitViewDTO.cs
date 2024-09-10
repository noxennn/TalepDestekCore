using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;

namespace DTOLayer.DTOs.UnitDTOs
{
    public class UnitViewDTO
    {
        public IEnumerable<Unit> ActiveUnits { get; set; }
        public IEnumerable<Unit> InactiveUnits { get; set; }
    }
}
