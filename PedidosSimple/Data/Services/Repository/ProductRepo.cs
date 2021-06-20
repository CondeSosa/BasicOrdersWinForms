using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PedidosSimple.Data.Services.Repository
{
   public class ProductRepo : IProductService
    {
        private readonly AppDbContext context;
        public ProductRepo()
        {
            this.context = new AppDbContext();
        }

        public async Task<bool> Add(Product Data)
        {
            try
            {
                await context.Product.AddAsync(Data);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int Id)
        {
            try
            {
                var result = await context.Product.FindAsync(Id);
                if (result != null)
                {
                    context.Product.Remove(result);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception) { }
            return false;
        }

        public async Task<IEnumerable<Product>> Fill()
        {
            return await context.Product.ToListAsync();
        }

        public async Task<Product> GetById(int Id)
        {
            return await context.Product.FindAsync(Id);
        }

        public async Task<bool> Update(Product Data)
        {
            try
            {
                context.Product.Update(Data);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
