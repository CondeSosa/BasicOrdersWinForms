using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidosSimple.Data.Services.Repository
{
   public class OrderRepo : IOrdersService
    {
        private readonly AppDbContext context;
        public OrderRepo()
        {
            this.context = new AppDbContext();
        }

        public async Task<Order> Add(Order Data)
        {
            try
            {
                await context.Order.AddAsync(Data);
                await context.SaveChangesAsync();
                return Data;
            }
            catch (Exception)
            {
                return Data;
            }
        }

        public async Task<bool> Delete(int Id)
        {
            try
            {
                var result = await context.Order.FindAsync(Id);
                if (result != null)
                {
                    context.Order.Remove(result);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception) { }
            return false;
        }

        public async Task<IEnumerable<Order>> Fill()
        {
            return await context.Order.Include(x => x.Client).Include(x => x.Products).ThenInclude(x => x.Product).ToListAsync();
        }

        public async Task<Order> GetById(int Id)
        {
            return await context.Order.Include(x => x.Client).Include(x => x.Products).ThenInclude(x => x.Product).FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<bool> IsClientInOrder(int ClientId)
        {
            var result = await context.Order.FirstOrDefaultAsync(x => x.ClientId == ClientId);
            if(result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> IsProductInOrder(int ProductId)
        {
            var result = await context.Order.Include(x => x.Products).FirstOrDefaultAsync(x => x.Products.First().ProductId == ProductId);
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
