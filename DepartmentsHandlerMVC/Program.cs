using BLL_BusinessLogicLayer.Interfaces;
using BLL_BusinessLogicLayer.Repositories;
using DAL_DataAccessLayer.Data.Contexts;
using DAL_DataAccessLayer.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PL_PresentationLayerMVC.MappingProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepartmentsHandlerMVC
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			#region Configure Services (Dependency Injection)
			var services = builder.Services;
			services.AddControllersWithViews();
			services.AddDbContext<MainContext>(
				options => options.UseSqlServer(builder.Configuration.GetConnectionString("DevelopmentConnection"))
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
				.AddEntityFrameworkStores<MainContext>() // Tell the application to use the MainContext for the Identity.
				.AddDefaultTokenProviders() // To generate tokens for password reset, email confirmation, etc.
				;

			services.AddAuthentication(
				CookieAuthenticationDefaults.AuthenticationScheme // The default scheme for cookie authentication. (In case you have multiple schemes, this is the default one.)
				)
				// Add the UserManager, SignInManager, RoleManager.
				.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, // Add the cookie authentication scheme (If didn't specify the scheme, it will use the default scheme). 
				options =>
				{
					options.LoginPath = "Account/Login";
					options.LogoutPath = "Account/Logout";
					options.AccessDeniedPath = "Home/Error";
				}
				);
			#endregion
			#region Configure Middleware
			var app = builder.Build();
			var env = app.Environment;
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
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Account}/{action=Login}/{id?}");
			});
			#endregion
			app.Run();
		}
	}
}
