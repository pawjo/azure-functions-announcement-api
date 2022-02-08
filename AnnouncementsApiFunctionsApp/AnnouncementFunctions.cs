using AnnouncementsApiFunctionsApp.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AnnouncementsApiFunctionsApp
{
    public class AnnouncementFunctions
    {
        private readonly AnnouncementFAContext _context;
        private readonly IMapper _mapper;

        public AnnouncementFunctions(AnnouncementFAContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [FunctionName("GetAnnouncementList")]
        public async Task<IActionResult> GetList(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req)
        {
            var announcements = await _context.Announcement.ToListAsync();

            var response = _mapper.Map<AnnouncementResponse[]>(announcements);

            return new OkObjectResult(response);
        }
    }
}
