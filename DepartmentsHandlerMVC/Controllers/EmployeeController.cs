using AutoMapper;
using BLL_BusinessLogicLayer.Interfaces;
using DAL_DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using PL_PresentationLayerMVC.Helpers;
using PL_PresentationLayerMVC.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PL_PresentationLayerMVC.Controllers
{
	public class EmployeeController : Controller
	{
		#region Fields & Properties
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;
		#endregion

		#region Constructor
		public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
		{
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
		}
		#endregion

		#region Index/GetAllEmployees operations
		public async Task<IActionResult> Index(string SearchValue)
		{
			IEnumerable<Employee> employees;
			if (string.IsNullOrEmpty(SearchValue))
				employees = await unitOfWork.Employees.GetAllAsync();
			else
				employees = unitOfWork.Employees.SearchEmployeesByNames(SearchValue);
			var employeesVm = mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
			return View(employeesVm);
		}
		#endregion

		#region Create Operation
		public async Task<IActionResult> Create()
		{
			ViewBag.Departments = await unitOfWork.Departments.GetAllAsync();
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(EmployeeViewModel employeeVm)
		{
			if (ModelState.IsValid)
			{
				employeeVm.ImageName = DocumentSettings.UploadFile(employeeVm.Image, "Images");
				var employee = mapper.Map<Employee>(employeeVm);
				await unitOfWork.Employees.AddAsync(employee);
				int count = await unitOfWork.CompleteAsync();
				if (count > 0)
				{
					TempData["Message"] = "Employee Added Successfully";
				}
				return RedirectToAction(nameof(Index));
			}
			return await Create();
		}
		#endregion

		#region Common Methods [GetViewWithEmployee] 
		private async Task<IActionResult> GetViewWithEmployee(string viewName, int? id)
		{
			if (!id.HasValue)
				return NotFound();
			var employee = await unitOfWork.Employees.GetByIdAsync(id.Value);
			var employeeVm = mapper.Map<EmployeeViewModel>(employee);
			if (employee == null)
				return NotFound();
			return View(viewName, employeeVm);
		}
		#endregion

		#region Showing Details Operation
		public async Task<IActionResult> ShowDetails(int? id)
			=> await GetViewWithEmployee("Details", id);
		#endregion

		#region Edit Operation
		public async Task<IActionResult> Edit(int? id)
		{
			ViewBag.Departments = await unitOfWork.Departments.GetAllAsync();
			return await GetViewWithEmployee("Edit", id);
		}
		public async Task<IActionResult> EditPageWithViewModel(EmployeeViewModel employeeVm)
		{
			ViewBag.Departments = await unitOfWork.Departments.GetAllAsync();
			return View("Edit", employeeVm);
		}
		[HttpPost]
		public async Task<IActionResult> Edit(EmployeeViewModel employeeVm)
		{
			if (ModelState.IsValid)
			{
				if (employeeVm.Image != null) // If the user uploaded a new image, else keep the old image
				{
					DocumentSettings.DeleteFile(employeeVm.ImageName, "Images");
					employeeVm.ImageName = DocumentSettings.UploadFile(employeeVm.Image, "Images");
				}
				var employee = mapper.Map<Employee>(employeeVm);
				unitOfWork.Employees.Update(employee);
				int count = await unitOfWork.CompleteAsync();
				if (count > 0)
				{
					TempData["Message"] = "Employee Updated Successfully";
				}
				return RedirectToAction(nameof(Index));
			}
			return await EditPageWithViewModel(employeeVm);
		}
		#endregion

		#region Delete Operation
		// Returns Delete View of Employee
		public Task<IActionResult> Delete(int? id)
			=> GetViewWithEmployee("Delete", id);

		// Gets Employee from Database and Deletes it
		// Used with the bootstrap modal to delete an employee when knowing only id
		[HttpPost]
		public async Task<IActionResult> DeleteById(int? id)
		{
			if (!id.HasValue)
				return NotFound();
			var employee = await unitOfWork.Employees.GetByIdAsync(id.Value);
			if (employee == null)
				return NotFound();
			return await DeleteByEmployee(employee);
		}
		[HttpPost]
		public Task<IActionResult> Delete(EmployeeViewModel employeeVm)
			=> DeleteByEmployee(mapper.Map<Employee>(employeeVm));

		public async Task<IActionResult> DeleteByEmployee(Employee employee)
		{
			unitOfWork.Employees.Delete(employee);
			int count = await unitOfWork.CompleteAsync();
			if (count > 0)
			{
				TempData["Message"] = "Employee Deleted Successfully";
				DocumentSettings.DeleteFile(employee.ImageName, "Images");
			}
			return RedirectToAction(nameof(Index));
		}
		#endregion
	}
}

