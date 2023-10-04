using Core.DTOs;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IClientContactRepository
    {
        Task<IEnumerable<ClientContactEditDTO>> GetAllClientContact();
        Task<ClientContacts?> GetClientContactById(int contactId);
        Task <ClientContacts?> InsertClientContact (ClientContacts clientContacts);
        Task<ClientContacts?> UpdateClientContact (ClientContacts clientContacts);
        Task<DataUpdateResponse> DeleteClientContact (ClientContacts clientContacts);
    }
}
