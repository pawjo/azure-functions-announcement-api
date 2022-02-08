using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Threading.Tasks;

namespace AnnouncementsApiFunctionsApp
{
    public class AnnouncementFunctions
    {
        private readonly IAnnouncementService _service;

        public AnnouncementFunctions(IAnnouncementService service)
        {
            _service = service;
        }

        [FunctionName("GetAnnouncementList")]
        public async Task<IActionResult> GetList(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req)
        {
            var response = await _service.GetList();

            return new OkObjectResult(response);
        }
    }
}
