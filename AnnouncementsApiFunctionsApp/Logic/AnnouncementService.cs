using AnnouncementsApiFunctionsApp.Domain;
using AnnouncementsApiFunctionsApp.Dtos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<bool> DeleteAsync(int id)
        {
            var announcement = await GetByIdFromContext(id);

            if (announcement == null)
            {
                return false;
            }

            _context.Announcement.Remove(announcement);
            int deleted = await _context.SaveChangesAsync();

            return deleted == 1;
        }

        public async Task<AnnouncementResponse> GetById(int id)
        {
            var announcement = await GetByIdFromContext(id);

            var result = _mapper.Map<AnnouncementResponse>(announcement);

            return result;
        }

        public async Task<List<AnnouncementResponse>> GetListAsync(string searchText, string typeFilter, int pageNumber, int elementsOnPage)
        {
            List<Announcement> announcements;

            // filter
            if (typeFilter != null)
            {
                announcements = await _context.Announcement
                    .Where(x => x.AnnouncementType == typeFilter).ToListAsync();
            }
            else
            {
                announcements = await _context.Announcement.ToListAsync();
            }

            // search
            if (searchText != null)
            {
                var searchKeys = searchText.Split(' ');
                announcements = announcements.Where(x =>
                    searchKeys.Any(key => x.Title.Contains(key))).ToList();
            }

            // paging
            if (pageNumber > 0 && elementsOnPage > 0)
            {
                announcements = announcements.Skip((pageNumber - 1) * elementsOnPage)
                  .Take(elementsOnPage).ToList();
            }

            var result = _mapper.Map<List<AnnouncementResponse>>(announcements);

            return result;
        }

        public async Task<bool> UpdateAsync(UpdateAnnouncementRequest request)
        {
            var announcement = await GetByIdFromContext(request.Id);

            if (announcement == null)
            {
                return false;
            }

            announcement = _mapper.Map<UpdateAnnouncementRequest, Announcement>(request, announcement);

            _context.Announcement.Update(announcement);
            int updated = await _context.SaveChangesAsync();

            return updated == 1;
        }

        private async Task<Announcement> GetByIdFromContext(int id) =>
            await _context.Announcement.SingleOrDefaultAsync(x => x.Id == id);
    }
}
