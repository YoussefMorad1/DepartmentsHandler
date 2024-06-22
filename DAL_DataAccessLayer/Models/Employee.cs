using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DAL_DataAccessLayer.Models
{
    public enum Gender
    {
        // [EnumMember(Value = "Male")] // This is used to serialize the enum value as string when saved in database -- Why Do we need this? 
        [Display(Name = "Male")]
        MALE = 1,
        //[EnumMember(Value = "Female")]
        [Display(Name = "Female")]
        FEMALE = 2
    }
    public enum EmployeeType
    {
        // [EnumMember(Value = "Full Time")]
        [Display(Name = "Full Time")]
        FullTime = 1,
        // [EnumMember(Value = "Part Time")]
        [Display(Name = "Part Time")]
        PartTime = 2,
    }
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)] 
        public string Name { get; set; }
        public int? Age { get; set; }
        [Required]
        public string Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
        public Gender Gender { get; set; }
        [Required]
        public EmployeeType? EmployeeType { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
        public int? DepartmentID { get; set; }
        public Department Department { get; set; }
        public string ImageName { get; set; }
    }
}
