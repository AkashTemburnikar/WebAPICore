using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Models;

namespace WebApiCore.Services
{
    public class ProductService : IService<Product, int>
    {
        private readonly AppApiDBContext context;
        public ProductService(AppApiDBContext context)
        {
            this.context = context;
        }
        public async Task<Product> CreateAsync(Product entity)
        {
            var res = await context.Products.AddAsync(entity);
            await context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<bool> DeleteAync(int id)
        {
            var prod = await context.Products.FindAsync(id);
            if (prod == null)
            {
                return false;
            }
            context.Products.Remove(prod);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Product>> GetAsync()
        {
            var prods = await context.Products.ToListAsync();
            return prods;
        }

        public async Task<Product> GetAsync(int id)
        {
            var prod = await context.Products.FindAsync(id);
            return prod;
        }

        public async Task<Product> UpdateAsync(int id, Product entity)
        {
            var prod = await context.Products.FindAsync(id);
            if (prod != null)
            {
                prod.ProductId = entity.ProductId;
                prod.Price = entity.Price;
                prod.Price = entity.Price;
                prod.Manufacturer = entity.Manufacturer;
                await context.SaveChangesAsync();
                return prod;
            }
            return prod;
        }
    }
}
