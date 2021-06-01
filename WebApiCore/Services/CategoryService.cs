using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Models;

namespace WebApiCore.Services
{
    public class CategoryService : IService<Category, int>
    {
        private readonly AppApiDBContext context;
        public CategoryService(AppApiDBContext context)
        {
            this.context = context;
        }
        public async Task<Category> CreateAsync(Category entity)
        {
            var res = await context.Categories.AddAsync(entity);
            await context.SaveChangesAsync();
            return res.Entity;

        }

        public async Task<bool> DeleteAync(int id)
        {
            var cat = await context.Categories.FindAsync(id);
            if(cat == null)
            {
                return false;
            }
            context.Categories.Remove(cat);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Category>> GetAsync()
        {
            var cats = await context.Categories.ToListAsync();
            return cats;
        }

        public async Task<Category> GetAsync(int id)
        {
            var cat = await context.Categories.FindAsync(id);
            return cat;
        }

        public async Task<Category> UpdateAsync(int id, Category entity)
        {
            var cat = await context.Categories.FindAsync(id);
            if(cat != null)
            {
                cat.CategoryId = entity.CategoryId;
                cat.CategoryName = entity.CategoryName;
                cat.BasePrice = entity.BasePrice;
                await context.SaveChangesAsync();
                return cat;
            }
            return cat;
        }
    }
}
