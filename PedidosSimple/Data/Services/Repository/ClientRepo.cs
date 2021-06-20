using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PedidosSimple.Data.Services.Repository
{
    public class ClientRepo : IClientService
    {
        private readonly AppDbContext context;
        public ClientRepo()
        {
            this.context = new AppDbContext();
        }

        public async Task<bool> Add(Client Data)
        {
            try
            {
               await context.Client.AddAsync(Data);
               await context.SaveChangesAsync();
               return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int Id)
        {
            try
            {
                var result = await context.Client.FindAsync(Id);
                if (result != null)
                {
                    context.Client.Remove(result);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch(Exception) { }
            return false;
        }

        public async Task<IEnumerable<Client>> Fill()
        {
            return await context.Client.ToListAsync();
        }

        public async Task<Client> GetById(int Id)
        {
            return await context.Client.FindAsync(Id);
        }

        public async Task<bool> Update(Client Data)
        {
            try
            {
                context.Client.Update(Data);
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
