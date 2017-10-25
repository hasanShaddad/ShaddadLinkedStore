using AutoMapper;
using Elmatgar.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Elmatgar.Core.Units;
using Elmatgar.ViewModels;

namespace Elmatgar.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }



        [Obsolete("Create a constructor and configure inside of your profile\'s constructor instead. Will be removed in 6.0")]
        protected override void Configure()
        {
            CreateMap<Categories, CategoryViewModel>().ReverseMap();
           

        }
    }
}