using BLL_BusinessLogicLayer.Interfaces;
using DAL_DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace PL_PresentationLayerMVC.Controllers
{
	public class DepartmentController : Controller
	{
		private readonly IDepartmentRepository departmnetRepository;
		public DepartmentController(IDepartmentRepository departmnetRepository)
			=> this.departmnetRepository = departmnetRepository;
		public IActionResult Index()
			=> View(departmnetRepository.GetAll());

		public IActionResult Create() => View();

		[HttpPost]
		public IActionResult Create(Department department)
		{
			if (ModelState.IsValid)
			{
				departmnetRepository.Add(department);
				return RedirectToAction(nameof(Index));
			}
			return View(department);
		}
	}
}
