using AnnouncementsApiFunctionsApp.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnnouncementsApiFunctionsApp
{
    public interface IAnnouncementService
    {
        public Task<Result<int>> AddAsync(AddAnnouncementRequest request);

        public Task<Result<bool>> DeleteAsync(int id);

        public Task<Result<AnnouncementResponse>> GetByIdAsync(int id);

        public Task<Result<List<AnnouncementResponse>>> GetListAsync(string searchText, string typeFilter, int pageNumber, int elementsOnPage);

        public Task<Result<bool>> UpdateAsync(UpdateAnnouncementRequest request);
    }
}
