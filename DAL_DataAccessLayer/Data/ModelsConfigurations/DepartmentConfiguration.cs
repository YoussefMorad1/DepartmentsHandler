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
	internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
	{
		public void Configure(EntityTypeBuilder<Department> builder)
		{
			builder.HasKey(d => d.Id);
			builder.Property(d => d.Id).UseIdentityColumn(10, 10);
			builder.Property(d => d.Name).IsRequired().HasMaxLength(50);
			builder.Property(d => d.Code).IsRequired().HasMaxLength(50);
			builder.Property(d => d.DateOfCreation).HasDefaultValueSql("GETDATE()");
		}
	}
}
