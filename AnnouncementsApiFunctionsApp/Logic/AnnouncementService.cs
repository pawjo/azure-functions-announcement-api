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

        private const string notFoundMessage = "Announcement not found";

        public AnnouncementService(AnnouncementFAContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<int>> AddAsync(AddAnnouncementRequest request)
        {
            if (request == null
                || request.Title == null || request.Title == string.Empty
                || request.AnnouncementType == null
                || request.AnnouncementType == string.Empty)
            {
                return new Result<int>(400, "Incomplete object");
            }

            var announcement = _mapper.Map<Announcement>(request);

            await _context.Announcement.AddAsync(announcement);
            int added = await _context.SaveChangesAsync();

            if (added == 1)
            {
                return new Result<int>(announcement.Id);
            }
            else
            {
                return new Result<int>(418, "Jestem czajnikiem no i co");
            }
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var announcement = await GetByIdFromContext(id);

            if (announcement == null)
            {
                return new Result<bool>(404, notFoundMessage);
            }

            _context.Announcement.Remove(announcement);
            int deleted = await _context.SaveChangesAsync();

            if (deleted != 1)
            {
                return new Result<bool>(500, "Delete error");
            }

            return new Result<bool>(true);
        }

        public async Task<Result<AnnouncementResponse>> GetById(int id)
        {
            var announcement = await GetByIdFromContext(id);

            if (announcement == null)
            {
                return new Result<AnnouncementResponse>(404, notFoundMessage);
            }

            var result = _mapper.Map<AnnouncementResponse>(announcement);

            return new Result<AnnouncementResponse>(result);
        }

        public async Task<Result<List<AnnouncementResponse>>> GetListAsync(string searchText, string typeFilter, int pageNumber, int elementsOnPage)
        {
            List<Announcement> announcements;

            // filter
            if (typeFilter != null)
            {
                announcements = await _context.Announcement
                    .Where(x => x.AnnouncementType == typeFilter).ToListAsync();

                if (announcements.Count == 0)
                    return new Result<List<AnnouncementResponse>>(400, "Wrong announcement type");
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
                    SearchFilter(x, searchKeys)).ToList();
            }

            // paging
            if (pageNumber > 0 && elementsOnPage > 0)
            {
                announcements = announcements.Skip((pageNumber - 1) * elementsOnPage)
                  .Take(elementsOnPage).ToList();

                if (announcements.Count == 0)
                    return new Result<List<AnnouncementResponse>>(400, "Wrong page number");
            }
            else if (pageNumber == 0 && elementsOnPage > 0)
            {
                return new Result<List<AnnouncementResponse>>(400, "Wrong page number");
            }
            else if (pageNumber > 0 && elementsOnPage == 0)
            {
                return new Result<List<AnnouncementResponse>>(400, "Wrong elements on page parameter");
            }

            var result = _mapper.Map<List<AnnouncementResponse>>(announcements);

            return new Result<List<AnnouncementResponse>>(result);
        }

        public async Task<Result<bool>> UpdateAsync(UpdateAnnouncementRequest request)
        {
            var announcement = await GetByIdFromContext(request.Id);

            if (announcement == null)
            {
                return new Result<bool>(404, notFoundMessage);
            }

            announcement = _mapper.Map<UpdateAnnouncementRequest, Announcement>(request, announcement);

            _context.Announcement.Update(announcement);
            int updated = await _context.SaveChangesAsync();

            if (updated != 1)
            {
                return new Result<bool>(500, "Update error");
            }

            return new Result<bool>(true);
        }

        private async Task<Announcement> GetByIdFromContext(int id) =>
            await _context.Announcement.SingleOrDefaultAsync(x => x.Id == id);

        private static bool SearchFilter(Announcement announcement, string[] searchKeys)
        {
            string titleToLower = announcement.Title.ToLower();
            return searchKeys.Any(key => titleToLower.Contains(key.ToLower()));
        }
    }
}
