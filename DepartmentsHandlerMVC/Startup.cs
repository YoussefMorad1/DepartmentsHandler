using BLL_BusinessLogicLayer.Interfaces;
using BLL_BusinessLogicLayer.Repositories;
using DAL_DataAccessLayer.Data.Contexts;
using DAL_DataAccessLayer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PL_PresentationLayerMVC.MappingProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepartmentsHandlerMVC
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();
			services.AddDbContext<MainContext>(
				options => options.UseSqlServer(Configuration.GetConnectionString("DevelopmentConnection"))
				);
			//services.AddScoped<IGenericRepository<Department>, GenericRepository<Department>>();
			//services.AddScoped<IEmployeeRepository, EmployeeRepository>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddAutoMapper(conf => conf.AddProfile(new EmployeeProfile()));
			services.AddIdentity<AppUser, IdentityRole>(
				options =>
				{
					options.Password.RequireDigit = true;
					options.Password.RequireLowercase = true;
					options.Password.RequireUppercase = true;
				}) // Tell the application to use the AppUser and IdentityRole.
				.AddEntityFrameworkStores<MainContext>(); // Tell the application to use the MainContext for the Identity.
			services.AddAuthentication(); // Add the UserManager, SignInManager, RoleManager.
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
