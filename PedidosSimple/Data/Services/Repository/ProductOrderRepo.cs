using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidosSimple.Data.Services.Repository
{
   public class ProductOrderRepo : IProductOrderService
    {
        private readonly AppDbContext context;
        public ProductOrderRepo()
        {
            this.context = new AppDbContext();
        }

        public async Task<bool> Add(IEnumerable<ProductOrder> Data)
        {
            try
            {
                await context.ProductOrder.AddRangeAsync(Data);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

       public async Task<bool> DeleteOrder(int OrderId)
        {
                try
                {
                    var result = await context.ProductOrder.Where(x => x.OrderId == OrderId).ToListAsync();
                    if (result != null)
                    {
                        context.ProductOrder.RemoveRange(result);
                        await context.SaveChangesAsync();
                        return true;
                    }
                }
                catch (Exception) { }
                return false;
            }
    }
}
