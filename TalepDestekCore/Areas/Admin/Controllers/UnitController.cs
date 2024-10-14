using BusinessLayer.Abstract;
using DTOLayer.DTOs.UnitDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using TalepDestekCore.Areas.Admin.Models;
using X.PagedList.Extensions;

namespace TalepDestekCore.Areas.Admin.Controllers
{

	[Area("Admin")]
	[Route("Admin/Unit")]
	public class UnitController : Controller
	{

		private readonly IUnitService _unitService;

		public UnitController(IUnitService unitService)
		{
			_unitService = unitService;
		}

		[Route("Index")]
		public IActionResult Index(bool showInactiveUnits = false)
		{

			var activeUnits = _unitService.TGetListofActiveUnits();

			var inactiveUnits = _unitService.TGetListofInactiveUnits();


			var unitView = new UnitViewModel
			{
				ActiveUnits = activeUnits,
				InactiveUnits = inactiveUnits,
				ShowInactiveUnits = showInactiveUnits
			};
			return View(unitView);
		}

		[HttpGet]
		[Route("CreateUnit")]
		public IActionResult CreateUnit()
		{
			return View();
		}


		[HttpPost]
		[Route("CreateUnit")]
		public IActionResult CreateUnit(CreateUnitDTO createUnitDTO)
		{
			if (ModelState.IsValid)
			{
				_unitService.TInsert(new Unit()
				{
					UnitName = createUnitDTO.UnitName,

				});
				return RedirectToAction("Index");
			}
			return View();
		}



		[Route("DeactivateUnit/{id}")]
		public IActionResult DeactivateUnit(int id)
		{
			var unit = _unitService.TGetByID(id);
			unit.Status = false; // Birimi pasife al
			_unitService.TUpdate(unit);

			// Pasif birimler tablosunu seçili olarak göndermek için
			return RedirectToAction("Index");
		}



		[Route("ActivateUnit/{id}")]
		public IActionResult ActivateUnit(int id)
		{
			var unit = _unitService.TGetByID(id);
			unit.Status = true; // Birimi aktif et
			_unitService.TUpdate(unit);

			// Aktif birimler tablosunu seçili olarak göndermek için
			return RedirectToAction("Index");
		}

		[HttpGet]
		[Route("EditUnit/{id}")]
		public IActionResult EditUnit(int id)
		{
			var unit = _unitService.TGetByID(id);
			var viewModel = new EditUnitDTO
			{
				UnitID = unit.UnitID,
				UnitName = unit.UnitName,
			};
			return View(viewModel);
		}

		[HttpPost]
		[Route("EditUnit/{id}")]
		public IActionResult EditUnit(EditUnitDTO editUnitDTO)
		{

			var unit = _unitService.TGetByID(editUnitDTO.UnitID);

			unit.UnitName = editUnitDTO.UnitName;
			_unitService.TUpdate(unit);

			return RedirectToAction("Index");
		}
	}
}
