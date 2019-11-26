using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly DataContext dbContext;
        public CategoriesService(DataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ProductCategory> AddAsync(ProductCategory category)
        {
            
            dbContext.ProductCategory.Add(category);
            await dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<ICollection<ProductCategory>> GetAllAsync()
        {
            return await dbContext.ProductCategory.ToListAsync();
        }
    }
}
