using AnnouncementsApiFunctionsApp.Domain;
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

        public async Task<bool> AddAsync(AddAnnouncementRequest request)
        {
            if (request == null || request.Title == null
                   || request.AnnouncementType == null)
            {
                return false;
            }

            var announcement = _mapper.Map<Announcement>(request);

            await _context.Announcement.AddAsync(announcement);
            int added = await _context.SaveChangesAsync();

            return added == 1;
        }

        public async Task<AnnouncementResponse[]> GetListAsync()
        {
            var announcements = await _context.Announcement.ToListAsync();

            var result = _mapper.Map<AnnouncementResponse[]>(announcements);

            return result;
        }
    }
}
