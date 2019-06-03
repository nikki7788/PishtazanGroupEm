using AutoMapper;
using Model.Entities;
using Model.Models.Countries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Infrastructure
{
    class AutoMaperProfile : Profile
    {

        public AutoMaperProfile()
        {
            // CreateMap<TSource, TDestination>()
         
            CreateMap<CountryDto, Country>()
                 .ForMember(c => c.Name, cdt => cdt.MapFrom(src => src.Name))
    .ForMember(c => c.Id, opt => opt.MapFrom(src => src.Id));


            // CreateMap<Book,AddEditBookViewModel>();
        }
    }
}
