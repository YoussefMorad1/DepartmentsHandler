using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_DataAccessLayer.Models
{
	public class Department
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Code is required")]
		public string Code { get; set; }
		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; }
		[Display(Name = "Date of Creation")]
		public DateTime DateOfCreation { get; set; }
	}
}
