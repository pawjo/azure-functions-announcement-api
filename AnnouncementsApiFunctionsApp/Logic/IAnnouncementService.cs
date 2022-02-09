using AnnouncementsApiFunctionsApp.Dtos;
using System.Threading.Tasks;

namespace AnnouncementsApiFunctionsApp
{
    public interface IAnnouncementService
    {
        public Task<AnnouncementResponse[]> GetListAsync();

        public Task<bool> AddAsync(AddAnnouncementRequest request);

        public Task<bool> DeleteAsync(int id);

        public Task<AnnouncementResponse> GetById(int id);
    }
}
