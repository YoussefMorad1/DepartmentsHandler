using DAL_DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_DataAccessLayer.Data.ModelsConfigurations
{
	internal class EmployeeConfigration : IEntityTypeConfiguration<Employee>
	{
		public void Configure(EntityTypeBuilder<Employee> builder)
		{
			builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).UseIdentityColumn();
			builder.Property(e => e.Name).IsRequired().HasMaxLength(50);
			builder.Property(e => e.Salary).HasColumnType("decimal(12,2)");
			builder.Property(e => e.Email).IsRequired();
			builder.Property(e => e.Address).IsRequired();
			// New Important Fluent API method to convert property between and to DB
			builder.Property(e => e.Gender).HasConversion(
											(g) => g.ToString(), // Convert the Enum to string when saving in database
											(str) => (Gender)Enum.Parse(typeof(Gender), str, true) // Convert the string to Enum when reading from database
										);
			builder.Property(e => e.EmployeeType).HasConversion(
											(g) => g.ToString(),
											(str) => (EmployeeType)Enum.Parse(typeof(EmployeeType), str, true) 
										);
		}

	}
}
