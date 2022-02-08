using AnnouncementsApiFunctionsApp.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using System;
using System.IO;
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

        [FunctionName("AddAnnouncement")]
        public async Task<IActionResult> AddAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
        {
            string requestBody = String.Empty;
            using (StreamReader streamReader = new StreamReader(req.Body))
            {
                requestBody = await streamReader.ReadToEndAsync();
            }

            var addRequest = JsonConvert.DeserializeObject<AddAnnouncementRequest>(requestBody);
            bool response = await _service.AddAsync(addRequest);

            return new OkObjectResult(response);
        }

        [FunctionName("GetAnnouncementList")]
        public async Task<IActionResult> GetList(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req)
        {
            var response = await _service.GetListAsync();

            return new OkObjectResult(response);
        }
    }
}
