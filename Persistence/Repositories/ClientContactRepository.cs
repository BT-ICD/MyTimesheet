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
    public class ClientContactRepository : IClientContactRepository
    {
        private readonly TimesheetContext context;

        public ClientContactRepository(TimesheetContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<ClientContactEditDTO>> GetAllClientContact()
        {
            return await context.ClientContacts.Where(x => x.IsDaleted == false).Select(data => new ClientContactEditDTO { ContactId = data.ContactId, Name = data.Name, Email = data.Email, Mobile = data.Mobile, ClientId = data.ClientId, DesignationId = data.DesignationId, DesignationName = data.designation.DesignationName }).ToListAsync();
        }

        public async Task<ClientContact?> GetClientContactById(int contactId)
        {
            return await context.ClientContacts.Include(x => x.designation).Where(x => x.ContactId == contactId && x.IsDaleted == false).FirstOrDefaultAsync();
        }

        public async Task<ClientContact?> InsertClientContact(ClientContact clientContacts)
        {
            await context.ClientContacts.AddAsync(clientContacts);
            var result = await context.SaveChangesAsync();

            var insertedClientContact = await context.ClientContacts.Include(x => x.designation).FirstOrDefaultAsync(x => x.ContactId == clientContacts.ContactId);

            return insertedClientContact;

           // return clientContacts;
        }

        public async Task<ClientContact?> UpdateClientContact(ClientContact clientContacts)
        {
            var data = await context.ClientContacts.Include(x => x.designation).Where(x=> x.ContactId == clientContacts.ContactId).FirstOrDefaultAsync();
            if (data != null)
            {
                data.Name = clientContacts.Name;
                data.Email = clientContacts.Email;
                data.Mobile = clientContacts.Mobile;
                data.ClientId = clientContacts.ClientId;
                data.DesignationId = clientContacts.DesignationId;
                data.ModifiedBy = clientContacts.ModifiedBy;
                data.ModifiedOn = clientContacts.ModifiedOn;
                data.ModifiedFrom = clientContacts.ModifiedFrom;
                var result = await context.SaveChangesAsync();

                var updatedDesignation = await context.Designations.Where(d => d.DesignationId == clientContacts.DesignationId).FirstOrDefaultAsync();

                // Update the loaded Designation information in the ClientContact
                data.designation = updatedDesignation;


                return data;
            }
            return null;
        }
        public async Task<DataUpdateResponse> DeleteClientContact(ClientContact clientContacts)
        {
            int result = 0;
            DataUpdateResponse response = new DataUpdateResponse();
            var data = await context.ClientContacts.Where(x=>x.ContactId == clientContacts.ContactId && x.IsDaleted== false).FirstOrDefaultAsync();
            if (data == null)
            {
                response.Description = "Not Found";
            }
            else
            {
                data.IsDaleted = clientContacts.IsDaleted;
                data.DeletedOn = clientContacts.DeletedOn;
                data.DeletedBy = clientContacts.DeletedBy;
                data.DeletedFrom = clientContacts.DeletedFrom;
                result = await context.SaveChangesAsync();
                response.Description = "Record deleted";
            }
            response.Status = Convert.ToBoolean(result);
            response.RecordCount = result;
            return response;
        }
    }

    
}
