using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PedidosSimple.Data.Services
{
    public interface IProductService
    {
        Task<bool> Add(Product Data);
        Task<bool> Update(Product Data);
        Task<bool> Delete(int Id);
        Task<IEnumerable<Product>> Fill();
        Task<Product> GetById(int Id);
    }
}
