using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace Elmatgar.Mappings
{
    public class ViewModelToDominMappingProfile : Profile
    {

        public override string ProfileName
        {


            get { return "DomainToViewModelMapping"; }
        }



        [Obsolete("Create a constructor and configure inside of your profile\'s constructor instead. Will be removed in 6.0")]
        protected override void Configure()
        {

            // CreateMap<Employee,EmployeeFormViewModel>().ForMember(x => x.Department , opt => opt.Ignore())

            //   .ForMember(g => g.Name , map => map.MapFrom(vm => vm.Name ))
            // .ForMember(g => g.DepartmentId, map => map.MapFrom(vm => vm.DepartmentId ))
            //;





        }

    }
}