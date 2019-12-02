using AutoMapper;
using Store.Contracts.V1.Requests;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.MapperProfiles
{
    public class RequestToModelProfile : Profile
    {
        public RequestToModelProfile()
        {
            CreateMap<ProductRequest, Product>();
            CreateMap<CategoryRequest, ProductCategory>();
            CreateMap<PaginationRequest, PaginationFilter>();
        }
    }
}
