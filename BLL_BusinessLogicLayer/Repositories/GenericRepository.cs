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
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private protected readonly MainContext dbContext;

		public GenericRepository(MainContext dbContext)
			=> this.dbContext = dbContext;


		public int Add(T item)
		{
			dbContext.Add(item);
			return dbContext.SaveChanges();
		}

		public int Delete(T item)
		{
			dbContext.Remove(item);
			return dbContext.SaveChanges();
		}

		public virtual IEnumerable<T> GetAll()
		{
			return dbContext.Set<T>().AsNoTracking().ToList();
		}

		public T GetById(int id)
		{
			//var item = dbContext.Set<T>().Local.Where(d => d.Id == id).FirstOrDefault();
			//if (item == null)
			//	item = dbContext.Set<T>().Where(d => d.Id == id).FirstOrDefault();
			//return item;
			return dbContext.Set<T>().Find(id);
		}

		public int Update(T item)
		{
			dbContext.Update(item);
			return dbContext.SaveChanges();
		}
	}
}
