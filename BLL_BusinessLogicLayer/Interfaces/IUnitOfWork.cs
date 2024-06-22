using DAL_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_BusinessLogicLayer.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		IEmployeeRepository Employees { get; }
		IGenericRepository<Department> Departments { get; }
		int Complete();
	}
}
