using AutoMapper;
using DAL_DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using PL_PresentationLayerMVC.ViewModels;

namespace PL_PresentationLayerMVC.MappingProfiles
{
	public class EmployeeProfile : Profile
	{
		public EmployeeProfile()
		{
			CreateMap<EmployeeViewModel, Employee>()
				/*.ForMember(e => e.DepartmentID, opt => opt.MapFrom(evm => evm.Department.Id))*/
				.ReverseMap()
				;
		}
	}
}
