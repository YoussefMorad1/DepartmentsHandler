using BLL_BusinessLogicLayer.Interfaces;
using DAL_DataAccessLayer.Data.Contexts;
using DAL_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_BusinessLogicLayer.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly MainContext dbContext;
		private IEmployeeRepository employeeRepository;
		private IGenericRepository<Department> departmentRepository;

		public UnitOfWork(MainContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public IEmployeeRepository Employees
		{
			get
			{
				if (employeeRepository == null)
					employeeRepository = new EmployeeRepository(dbContext);
				return employeeRepository;
			}
		}

		public IGenericRepository<Department> Departments
		{
			get
			{
				if (departmentRepository == null)
					departmentRepository = new GenericRepository<Department>(dbContext);
				return departmentRepository;
			}
		}
		public async Task<int> CompleteAsync() =>  await dbContext.SaveChangesAsync();
		public void Dispose() => dbContext.Dispose();
	}
}
