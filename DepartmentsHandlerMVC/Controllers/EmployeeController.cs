﻿using AutoMapper;
using BLL_BusinessLogicLayer.Interfaces;
using DAL_DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using PL_PresentationLayerMVC.Helpers;
using PL_PresentationLayerMVC.ViewModels;
using System.Collections.Generic;

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
		public IActionResult Index(string SearchValue)
		{
			IEnumerable<Employee> employees;
			if (string.IsNullOrEmpty(SearchValue))
				employees = unitOfWork.Employees.GetAll();
			else
				employees = unitOfWork.Employees.SearchEmployeesByNames(SearchValue);
			var employeesVm = mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
			return View(employeesVm);
		}
		#endregion

		#region Create Operation
		public IActionResult Create()
		{
			ViewBag.Departments = unitOfWork.Departments.GetAll();
			return View();
		}

		[HttpPost]
		public IActionResult Create(EmployeeViewModel employeeVm)
		{
			if (ModelState.IsValid)
			{
				employeeVm.ImageName = DocumentSettings.UploadFile(employeeVm.Image, "Images");
				var employee = mapper.Map<Employee>(employeeVm);
				unitOfWork.Employees.Add(employee);
				int count = unitOfWork.Complete();
				if (count > 0)
				{
					TempData["Message"] = "Employee Added Successfully";
				}
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
			var employee = unitOfWork.Employees.GetById(id.Value);
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
			ViewBag.Departments = unitOfWork.Departments.GetAll();
			return GetViewWithEmployee("Edit", id);
		}
		public IActionResult EditPageWithViewModel(EmployeeViewModel employeeVm)
		{
			ViewBag.Departments = unitOfWork.Departments.GetAll();
			return View("Edit", employeeVm);
		}
		[HttpPost]
		public IActionResult Edit(EmployeeViewModel employeeVm)
		{
			if (ModelState.IsValid)
			{
				employeeVm.ImageName = DocumentSettings.UploadFile(employeeVm.Image, "Images");
				var employee = mapper.Map<Employee>(employeeVm);
				unitOfWork.Employees.Update(employee);
				int count = unitOfWork.Complete();
				if (count > 0)
				{
					TempData["Message"] = "Employee Updated Successfully";
				}
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
			var employee = unitOfWork.Employees.GetById(id.Value);
			if (employee == null)
				return NotFound();
			return DeleteByEmployee(employee);
		}
		[HttpPost]
		public IActionResult Delete(EmployeeViewModel employeeVm)
			=> DeleteByEmployee(mapper.Map<Employee>(employeeVm));

		public IActionResult DeleteByEmployee(Employee employee)
		{
			unitOfWork.Employees.Delete(employee);
			int count = unitOfWork.Complete();
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

