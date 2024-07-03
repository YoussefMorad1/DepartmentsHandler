using DAL_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_BusinessLogicLayer.Interfaces
{
	public interface IEmployeeRepository : IGenericRepository<Employee>
	{
		IQueryable<Employee> GetEmployeesByAddress(string address);
		IQueryable<Employee> SearchEmployeesByNames(string name);
	}
}
