using AutoMapper;
using Store.Contracts.V1.Responses;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.MapperProfiles
{
    public class ModelToResponse : Profile
    {
        public ModelToResponse()
        {
            CreateMap<Product, ProductResponse>().ForMember(dest => dest.CategoryName,x=>x.MapFrom(src => src.Category.CategoryName));
        }
    }
}
