using Core.DTOs;
using Core.Models;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly TimesheetContext context;

        public ClientRepository(TimesheetContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ClientEditDTO>> GetAllClientAsync()
        {
            return await context.Clients.Where(x => x.IsDaleted == false).Select(data => new ClientEditDTO { ClientId = data.ClientId, Email = data.Email, 
            Name = data.Name, Website = data.Website}).ToListAsync(); 
        }

        public async Task<Client?> GetClientById(int clientId)
        {
            return await context.Clients.Where(x => x.ClientId == clientId && x.IsDaleted == false).FirstOrDefaultAsync();
        }

        public async Task<Client?> InsertClient(Client client)
        {
            await context.Clients.AddAsync(client);
            var result = await context.SaveChangesAsync();
            return client;
        }

        public async Task<Client?> UpdateClient(Client client)
        {
            var data = await context.Clients.Where(x => x.ClientId == client.ClientId).FirstOrDefaultAsync();
            if(data != null)
            {
                data.Name = client.Name;
                data.Email = client.Email;
                data.Website = client.Website;
                data.ModifiedBy = client.ModifiedBy;
                data.ModifiedOn = client.ModifiedOn;
                data.ModifiedFrom = client.ModifiedFrom;
                var result = await context.SaveChangesAsync();
                return client;
            }
            return null;
        }

        public async Task<DataUpdateResponse> DeleteClient(Client client)  
        {
            int result = 0;
            DataUpdateResponse response = new DataUpdateResponse();
            var data = await context.Clients.Where(x => x.ClientId == client.ClientId && x.IsDaleted == false).FirstOrDefaultAsync();
            if(data == null)
            {
                response.Description = "Not Found";
            }
            else
            {
                data.IsDaleted = client.IsDaleted;
                data.DeletedOn = client.DeletedOn;
                data.DeletedBy = client.DeletedBy;
                data.DeletedFrom = client.DeletedFrom;
                result = await context.SaveChangesAsync();
                response.Description = "Record deleted";
            }
            response.Status = Convert.ToBoolean(result);
            response.RecordCount = result;  
            return response;
        }
    }
}
