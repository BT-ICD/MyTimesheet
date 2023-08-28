﻿using Core.DTOs;
using Core.Models;

namespace Core.Repositories
{
    public interface IDesignationRepository
    {
        Task<IEnumerable<Designation>> GetAllDesignationAsync();
        Task<Designation?> GetDesignationById(int  designationId);

        Task<Designation?> Add(Designation designation);
        Task<Designation?> Edit(Designation designation);
        Task<DataUpdateResponse> Delete(Designation designation);

    }
}