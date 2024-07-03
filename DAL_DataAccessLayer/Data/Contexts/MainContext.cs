using DAL_DataAccessLayer.Data.ModelsConfigurations;
using DAL_DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;



namespace DAL_DataAccessLayer.Data.Contexts
{
    public class MainContext : IdentityDbContext<AppUser>
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public MainContext(DbContextOptions<MainContext> options) : base(options) { }
        // Apply configurations means that we are applying the configurations that we have created in the ModelConfigurations folder
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}
