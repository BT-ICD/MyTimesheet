using Core.DTOs;
using Core.Models;

namespace Core.Repositories
{
    public interface IDesignationRepository
    {
        Task<IEnumerable<DesignationEditDTO>> GetAllDesignationAsync();
        Task<Designation?> GetDesignationById(int  designationId);

        //Task<Designation?> IsDesignationNameDuplicateAsync(string designationName);

        Task<Designation?> Add(Designation designation);
        Task<Designation?> Edit(Designation designation);
        Task<DataUpdateResponse> Delete(Designation designation);

    }
}
