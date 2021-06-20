using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PedidosSimple.Data.Services
{
    public interface IClientService
    {
        Task<bool> Add(Client Data);
        Task<bool> Update(Client Data);
        Task<bool> Delete(int Id);
        Task<IEnumerable<Client>> Fill();
        Task<Client> GetById(int Id);

    }
}
