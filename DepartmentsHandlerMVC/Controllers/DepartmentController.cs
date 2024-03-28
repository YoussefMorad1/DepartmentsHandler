using BLL_BusinessLogicLayer.Interfaces;
using DAL_DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace PL_PresentationLayerMVC.Controllers
{
    public class DepartmentController : Controller
    {
        #region Fields & Properties
        private readonly IGenericRepository<Department> departmentRepository;
        #endregion

        #region Constructor
        public DepartmentController(IGenericRepository<Department> departmnetRepository)
            => this.departmentRepository = departmnetRepository;
        #endregion

        #region Index/GetAllDepartments operations
        public IActionResult Index()
            => View(departmentRepository.GetAll());
        #endregion

        #region Create Operation
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                int count = departmentRepository.Add(department);
                if (count > 0)
                    TempData["Message"] = "Department Added Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }
        #endregion

        #region Common Methods [GetViewWithDepartment] 
        private IActionResult GetViewWithDepartment(string viewName, int? id)
        {
            if (!id.HasValue)
                return NotFound();
            var department = departmentRepository.GetById(id.Value);
            if (department == null)
                return NotFound();
            return View(viewName, department);
        }
        #endregion

        #region Showing Details Operation
        public IActionResult ShowDetails(int? id)
            => GetViewWithDepartment("Details", id);
        #endregion

        #region Edit Operation
        public IActionResult Edit(int? id)
            => GetViewWithDepartment("Edit", id);
        [HttpPost]
        public IActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                int count = departmentRepository.Update(department);
                if (count > 0)
                    TempData["Message"] = "Department Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }
        #endregion

        #region Delete Operation
        // Returns Delete View of Department
        public IActionResult Delete(int? id)
            => GetViewWithDepartment("Delete", id);

        // Gets Department from Database and Deletes it
        [HttpPost]
        public IActionResult DeleteById(int? id)
        {
            if (!id.HasValue)
				return NotFound();
            var department = departmentRepository.GetById(id.Value);
            if (department == null)
                return NotFound();
            return Delete(department);
        }
        [HttpPost]
        public IActionResult Delete(Department department)
        {
			int count = departmentRepository.Delete(department);
            if (count > 0)
                TempData["Message"] = "Department Deleted Successfully";
			return RedirectToAction(nameof(Index));
		}
        #endregion
    }
}
