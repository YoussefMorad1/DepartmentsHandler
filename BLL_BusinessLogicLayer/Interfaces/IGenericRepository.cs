using DAL_DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_BusinessLogicLayer.Interfaces
{
	public interface IGenericRepository<T> where T : class
	{
		IEnumerable<T> GetAll();
		T GetById(int id);
		int Add(T department);
		int Update(T department);
		int Delete(T department);
	}
}
