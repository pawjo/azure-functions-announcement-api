using AnnouncementsApiFunctionsApp.Dtos;
using System.Threading.Tasks;

namespace AnnouncementsApiFunctionsApp
{
    public interface IAnnouncementService
    {
        public Task<AnnouncementResponse[]> GetList();
    }
}
