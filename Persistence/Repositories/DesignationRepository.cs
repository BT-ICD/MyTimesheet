using Core.DTOs;
using Core.Models;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.DataContext;

namespace Persistence.Repositories
{
    public class DesignationRepository : IDesignationRepository
    {
        private readonly TimesheetContext _context;

        public DesignationRepository(TimesheetContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<DesignationEditDTO>> GetAllDesignationAsync()
        {
            return await _context.Designations.Where(x=>x.IsDaleted==false).Select(data => new DesignationEditDTO{ DesignationId= data.DesignationId, DesignationName= data.DesignationName}).ToListAsync();
        }

        public async Task<Designation?> GetDesignationById(int designationId)
        {
            return await _context.Designations.Where(x=>x.DesignationId== designationId && x.IsDaleted==false).FirstOrDefaultAsync();
        }
        public async Task<Designation?> Add(Designation designation)
        {
            await _context.Designations.AddAsync(designation);
            var result = await _context.SaveChangesAsync();
            return designation;
        }
        public async Task<Designation?> Edit(Designation designation)
        {
            var data  = await _context.Designations.Where(x=>x.DesignationId==designation.DesignationId).FirstOrDefaultAsync();
            if (data != null)
            {
                data.DesignationName = designation.DesignationName;
                data.ModifiedBy = designation.ModifiedBy;
                data.ModifiedOn = designation.ModifiedOn;
                data.ModifiedFrom = designation.ModifiedFrom;
                var result = await _context.SaveChangesAsync();
                return designation;
            }
            return null;
        }

        public async Task<DataUpdateResponse> Delete(Designation designation)
        {
            int result = 0;
            DataUpdateResponse response= new DataUpdateResponse();
            var data = await _context.Designations.Where(x => x.DesignationId == designation.DesignationId && x.IsDaleted==false).FirstOrDefaultAsync();
            if(data == null)
            {
                response.Description = "Not Found";
            }
            else
            {
                data.IsDaleted = designation.IsDaleted;
                data.DeletedOn = designation.DeletedOn;
                data.DeletedBy = designation.DeletedBy;
                data.DeletedFrom = designation.DeletedFrom;
                result = await _context.SaveChangesAsync();
                response.Description = "Record deleted";
                
            }
            response.Status = Convert.ToBoolean(result);
            response.RecordCount = result;
            return response;
        }
    }
}
