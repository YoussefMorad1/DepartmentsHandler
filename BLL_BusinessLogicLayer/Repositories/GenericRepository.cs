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


		public async Task AddAsync(T item)
		{
			await dbContext.AddAsync(item);
		}

		public void Delete(T item)
		{
			dbContext.Remove(item);
		}

		public async virtual Task<IEnumerable<T>> GetAllAsync()
		{
			return await dbContext.Set<T>().AsNoTracking().ToListAsync();
		}

		public async Task<T> GetByIdAsync(int id)
		{
			//var item = dbContext.Set<T>().Local.Where(d => d.Id == id).FirstOrDefault();
			//if (item == null)
			//	item = dbContext.Set<T>().Where(d => d.Id == id).FirstOrDefault();
			//return item;
			return await dbContext.Set<T>().FindAsync(id);
		}

		public void Update(T item)
		{
			dbContext.Update(item);
		}
	}
}
