using AutoMapper;
using BLL_BusinessLogicLayer.Interfaces;
using DAL_DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using PL_PresentationLayerMVC.ViewModels;
using System.Collections.Generic;

namespace PL_PresentationLayerMVC.Controllers
{
	public class EmployeeController : Controller
	{
		#region Fields & Properties
		private readonly IEmployeeRepository employeeRepository;
		private readonly IGenericRepository<Department> departmentRepository;
		private readonly IMapper mapper;
		#endregion

		#region Constructor
		public EmployeeController(IEmployeeRepository employeeRepository,
			IGenericRepository<Department> departmentRepository, IMapper mapper)
		{
			this.employeeRepository = employeeRepository;
			this.departmentRepository = departmentRepository;
			this.mapper = mapper;
		}
		#endregion

		#region Index/GetAllEmployees operations
		public IActionResult Index()
		{
			var employees = employeeRepository.GetAll();
			var employeesVm = mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
			return View(employeesVm);
		}
		#endregion

		#region Create Operation
		public IActionResult Create()
		{
			ViewBag.Departments = departmentRepository.GetAll();
			return View();
		}

		[HttpPost]
		public IActionResult Create(EmployeeViewModel employeeVm)
		{
			if (ModelState.IsValid)
			{
				var employee = mapper.Map<Employee>(employeeVm);
				int count = employeeRepository.Add(employee);
				if (count > 0)
					TempData["Message"] = "Employee Added Successfully";
				return RedirectToAction(nameof(Index));
			}
			return Create();
		}
		#endregion

		#region Common Methods [GetViewWithEmployee] 
		private IActionResult GetViewWithEmployee(string viewName, int? id)
		{
			if (!id.HasValue)
				return NotFound();
			var employee = employeeRepository.GetById(id.Value);
			var employeeVm = mapper.Map<EmployeeViewModel>(employee);
			if (employee == null)
				return NotFound();
			return View(viewName, employeeVm);
		}
		#endregion

		#region Showing Details Operation
		public IActionResult ShowDetails(int? id)
			=> GetViewWithEmployee("Details", id);
		#endregion

		#region Edit Operation
		public IActionResult Edit(int? id)
		{
			ViewBag.Departments = departmentRepository.GetAll();
			return GetViewWithEmployee("Edit", id);
		}
		public IActionResult EditPageWithViewModel(EmployeeViewModel employeeVm)
		{
			ViewBag.Departments = departmentRepository.GetAll();
			return View("Edit", employeeVm);
		}
		[HttpPost]
		public IActionResult Edit(EmployeeViewModel employeeVm)
		{
			if (ModelState.IsValid)
			{
				var employee = mapper.Map<Employee>(employeeVm);
				int count = employeeRepository.Update(employee);
				if (count > 0)
					TempData["Message"] = "Employee Updated Successfully";
				return RedirectToAction(nameof(Index));
			}
			return EditPageWithViewModel(employeeVm);
		}
		#endregion

		#region Delete Operation
		// Returns Delete View of Employee
		public IActionResult Delete(int? id)
			=> GetViewWithEmployee("Delete", id);

		// Gets Employee from Database and Deletes it
		// Used with the bootstrap modal to delete an employee when knowing only id
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

