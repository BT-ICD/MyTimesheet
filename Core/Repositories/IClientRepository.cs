using Core.DTOs;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IClientRepository
    {
        Task<IEnumerable<ClientEditDTO>> GetAllClientAsync();
        Task<Client?> GetClientById(int clientId);
        Task<Client?> InsertClient(Client client);
        Task<Client?> UpdateClient(Client client);
        Task<DataUpdateResponse> DeleteClient(Client client);
        Task<IEnumerable<ClientLookupDTO>> GetClientLookupAsync();

    }
}
