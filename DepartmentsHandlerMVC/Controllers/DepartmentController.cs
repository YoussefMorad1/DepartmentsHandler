using BLL_BusinessLogicLayer.Interfaces;
using DAL_DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PL_PresentationLayerMVC.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        #region Fields & Properties
		private readonly IUnitOfWork unitOfWork;
		#endregion

		#region Constructor
		public DepartmentController(IUnitOfWork unitOfWork)
		    => this.unitOfWork = unitOfWork;
		#endregion

		#region Index/GetAllDepartments operations
		public async Task<IActionResult> Index()
            => View(await unitOfWork.Departments.GetAllAsync());
        #endregion

        #region Create Operation
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                await unitOfWork.Departments.AddAsync(department);
                int count = await unitOfWork.CompleteAsync();
                if (count > 0)
                    TempData["Message"] = "Department Added Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }
        #endregion

        #region Common Methods [GetViewWithDepartment] 
        private async Task<IActionResult> GetViewWithDepartment(string viewName, int? id)
        {
            if (!id.HasValue)
                return NotFound();
            var department = await unitOfWork.Departments.GetByIdAsync(id.Value);
            if (department == null)
                return NotFound();
            return View(viewName, department);
        }
        #endregion

        #region Showing Details Operation
        public Task<IActionResult> ShowDetails(int? id)
            => GetViewWithDepartment("Details", id);
        #endregion

        #region Edit Operation
        public Task<IActionResult> Edit(int? id)
            => GetViewWithDepartment("Edit", id);
        [HttpPost]
        public async Task<IActionResult> Edit(Department department)
        {
            if (ModelState.IsValid)
            {
				unitOfWork.Departments.Update(department);
				int count = await unitOfWork.CompleteAsync();
                if (count > 0)
                    TempData["Message"] = "Department Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }
        #endregion

        #region Delete Operation
        // Returns Delete View of Department
        public Task<IActionResult> Delete(int? id)
            => GetViewWithDepartment("Delete", id);

        // Gets Department from Database and Deletes it
        [HttpPost]
        public async Task<IActionResult> DeleteById(int? id)
        {
            if (!id.HasValue)
				return NotFound();
            var department = await unitOfWork.Departments.GetByIdAsync(id.Value);
            if (department == null)
                return NotFound();
            return await Delete(department);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Department department)
        {
			unitOfWork.Departments.Delete(department);
			int count = await unitOfWork.CompleteAsync();
            if (count > 0)
                TempData["Message"] = "Department Deleted Successfully";
			return RedirectToAction(nameof(Index));
		}
        #endregion
    }
}
