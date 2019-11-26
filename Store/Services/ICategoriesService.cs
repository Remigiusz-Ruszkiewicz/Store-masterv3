using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Services
{
    public interface ICategoriesService
    {
        Task<ProductCategory> AddAsync(ProductCategory category);
        Task<ICollection<ProductCategory>> GetAllAsync();
    }
}
