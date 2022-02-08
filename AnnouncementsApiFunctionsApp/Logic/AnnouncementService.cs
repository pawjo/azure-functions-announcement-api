using AnnouncementsApiFunctionsApp.Dtos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AnnouncementsApiFunctionsApp
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly AnnouncementFAContext _context;
        private readonly IMapper _mapper;

        public AnnouncementService(AnnouncementFAContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AnnouncementResponse[]> GetList()
        {
            var announcements = await _context.Announcement.ToListAsync();

            var result = _mapper.Map<AnnouncementResponse[]>(announcements);

            return result;
        }
    }
}
