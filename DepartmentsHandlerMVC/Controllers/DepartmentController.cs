using BLL_BusinessLogicLayer.Interfaces;
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
	}
}
