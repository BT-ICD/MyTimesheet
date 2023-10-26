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
        Task<ClientContact?> GetClientContactById(int contactId);
        Task <ClientContact?> InsertClientContact (ClientContact clientContacts);
        Task<ClientContact?> UpdateClientContact (ClientContact clientContacts);
        Task<DataUpdateResponse> DeleteClientContact (ClientContact clientContacts);
    }
}
