using BLL_BusinessLogicLayer.Interfaces;
using DAL_DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace PL_PresentationLayerMVC.Controllers
{
	public class EmployeeController : Controller
	{
		#region Fields & Properties
		private readonly IEmployeeRepository employeeRepository;
		#endregion

		#region Constructor
		public EmployeeController(IEmployeeRepository departmnetRepository)
			=> this.employeeRepository = departmnetRepository;
		#endregion

		#region Index/GetAllEmployees operations
		public IActionResult Index()
			=> View(employeeRepository.GetAll());
		#endregion

		#region Create Operation
		public IActionResult Create() => View();

		[HttpPost]
		public IActionResult Create(Employee employee)
		{
			if (ModelState.IsValid)
			{
				int count = employeeRepository.Add(employee);
				if (count > 0)
					TempData["Message"] = "Employee Added Successfully";
				return RedirectToAction(nameof(Index));
			}
			return View(employee);
		}
		#endregion

		#region Common Methods [GetViewWithEmployee] 
		private IActionResult GetViewWithEmployee(string viewName, int? id)
		{
			if (!id.HasValue)
				return NotFound();
			var employee = employeeRepository.GetById(id.Value);
			if (employee == null)
				return NotFound();
			return View(viewName, employee);
		}
		#endregion

		#region Showing Details Operation
		public IActionResult ShowDetails(int? id)
			=> GetViewWithEmployee("Details", id);
		#endregion

		#region Edit Operation
		public IActionResult Edit(int? id)
			=> GetViewWithEmployee("Edit", id);
		[HttpPost]
		public IActionResult Edit(Employee employee)
		{
			if (ModelState.IsValid)
			{
				int count = employeeRepository.Update(employee);
				if (count > 0)
					TempData["Message"] = "Employee Updated Successfully";
				return RedirectToAction(nameof(Index));
			}
			return View(employee);
		}
		#endregion

		#region Delete Operation
		// Returns Delete View of Employee
		public IActionResult Delete(int? id)
			=> GetViewWithEmployee("Delete", id);

		// Gets Employee from Database and Deletes it
		[HttpPost]
		public IActionResult DeleteById(int? id)
		{
			if (!id.HasValue)
				return NotFound();
			var employee = employeeRepository.GetById(id.Value);
			if (employee == null)
				return NotFound();
			return Delete(employee);
		}
		[HttpPost]
		public IActionResult Delete(Employee employee)
		{
			int count = employeeRepository.Delete(employee);
			if (count > 0)
				TempData["Message"] = "Employee Deleted Successfully";
			return RedirectToAction(nameof(Index));
		}
		#endregion
	}
}

