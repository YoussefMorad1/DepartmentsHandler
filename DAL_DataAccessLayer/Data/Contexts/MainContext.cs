using DAL_DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_DataAccessLayer.Data.Contexts
{
    class MainContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(@"Server=.;Database=DepartmentsHandlerDB;Trusted_Connection=True;");

    }
}
