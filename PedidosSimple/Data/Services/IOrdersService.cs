using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PedidosSimple.Data.Services
{
   public interface IOrdersService
    {
        Task<Order> Add(Order Data);
        Task<bool> Delete(int Id);
        Task<IEnumerable<Order>> Fill();
        Task<Order> GetById(int Id);
        Task<bool> IsClientInOrder(int ClientId);
        Task<bool> IsProductInOrder(int ProductId);
    }
}
