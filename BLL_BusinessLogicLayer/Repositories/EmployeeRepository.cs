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
	public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
	{
		public EmployeeRepository(MainContext dbContext) : base(dbContext) { }
		public IQueryable<Employee> GetEmployeesByAddress(string address)
		{
			return dbContext.Employees
							.Where(e => e.Address.ToLower() == address.ToLower());
		}
		public override IEnumerable<Employee> GetAll()
		{
			return dbContext.Employees.Include(e => e.Department).AsNoTracking().ToList();
		}
	}
}
