using DAL_DataAccessLayer.Models;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace PL_PresentationLayerMVC.ViewModels
{
	public class EmployeeViewModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Name is required")] // Mapped with database
		[MaxLength(50, ErrorMessage = "Name can't be more than 50 characters")] // Mapped with database
		[MinLength(5, ErrorMessage = "Name can't be less than 5 characters")] // Not Mapped with database
		public string Name { get; set; }
		[Range(18, 35)] // Not Mapped with database
		public int? Age { get; set; }
		[RegularExpression("^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
			ErrorMessage = "Address Must be like 123-Street-City-Country")] // Not Mapped with database
		[Required]
		public string Address { get; set; }
		[DataType(DataType.Currency)] // Not Mapped with database (it's decimal(18,2) in database) [The currency is used to view the value as currency]
		public decimal Salary { get; set; }
		[Display(Name = "Is Active")]
		public bool IsActive { get; set; }
		[EmailAddress] // Not Mapped with database, just to take format of email
		[Required]
		public string Email { get; set; }
		[Phone] // Not Mapped with database, just to take format of phone number
		[Display(Name = "Phone Number")]
		public string Phone { get; set; }
		public Gender Gender { get; set; }
		[Display(Name = "Employee Type")]
		[Required(ErrorMessage = "Employee Type is required")]
		public EmployeeType? EmployeeType { get; set; }
		[Display(Name = "Hire Date")] // Not Mapped with database, just to change the name of the property in front end
		public DateTime HireDate { get; set; }
		[Display(Name = "Creation Date")]
		public int? DepartmentID { get; set; }
		public Department Department { get; set; }
		public IFormFile Image { get; set;}
		public string ImageName { get; set; }
	}
}
