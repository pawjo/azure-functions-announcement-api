using AnnouncementsApiFunctionsApp.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnnouncementsApiFunctionsApp
{
    public interface IAnnouncementService
    {
        Task<Result<int>> AddAsync(AddAnnouncementRequest request);

        Task<Result<bool>> DeleteAsync(int id);

        Task<Result<AnnouncementResponse>> GetByIdAsync(int id);

        Task<Result<List<AnnouncementResponse>>> GetListAsync(string searchText, string typeFilter, int pageNumber, int elementsOnPage);

        Task<Result<bool>> UpdateAsync(UpdateAnnouncementRequest request);
    }
}
