using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PedidosSimple.Data.Services
{
    public interface IProductOrderService
    {
        Task<bool> Add(IEnumerable<ProductOrder> Data);
        Task<bool> DeleteOrder(int OrderId);
    }
}
