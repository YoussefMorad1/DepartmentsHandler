using BLL_BusinessLogicLayer.Interfaces;
using DAL_DataAccessLayer.Data.Contexts;
using DAL_DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_BusinessLogicLayer.Repositories
{
	class DepartmentsRepository : IDepartmentsRepository
	{
		private readonly MainContext dbContext;

		public DepartmentsRepository(MainContext dbContext)
			=> this.dbContext = dbContext;


		public int Add(Department department)
		{
			dbContext.Add(department);
			return dbContext.SaveChanges();
		}

		public int Delete(Department department)
		{
			dbContext.Remove(department);
			return dbContext.SaveChanges();
		}

		public IEnumerable<Department> GetAll()
		{
			return dbContext.Departments.ToList();
		}

		public Department GetById(int id)
		{
			//var department = dbContext.Departments.Local.Where(d => d.Id == id).FirstOrDefault();
			//if (department == null)
			//	department = dbContext.Departments.Where(d => d.Id == id).FirstOrDefault();
			//return department;
			return dbContext.Departments.Find(id);
		}

		public int Update(Department department)
		{
			dbContext.Update(department);
			return dbContext.SaveChanges();
		}
	}
}
