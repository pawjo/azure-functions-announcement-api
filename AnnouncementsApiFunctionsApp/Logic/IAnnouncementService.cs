using AnnouncementsApiFunctionsApp.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnnouncementsApiFunctionsApp
{
    public interface IAnnouncementService
    {
        public Task<bool> AddAsync(AddAnnouncementRequest request);

        public Task<bool> DeleteAsync(int id);

        public Task<AnnouncementResponse> GetById(int id);

        public Task<List<AnnouncementResponse>> GetListAsync(string searchText, string typeFilter, int pageNumber, int elementsOnPage);

        public Task<bool> UpdateAsync(UpdateAnnouncementRequest request);
    }
}
