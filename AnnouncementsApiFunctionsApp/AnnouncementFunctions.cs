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

        private const string baseRoute = "announcement";
        private const string successResponseText = "Success: ";

        public AnnouncementFunctions(IAnnouncementService service)
        {
            _service = service;
        }

        [FunctionName("AddAnnouncement")]
        public async Task<IActionResult> AddAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = baseRoute)] HttpRequest req)
        {
            string requestBody = String.Empty;
            using (StreamReader streamReader = new StreamReader(req.Body))
            {
                requestBody = await streamReader.ReadToEndAsync();
            }

            var addRequest = JsonConvert.DeserializeObject<AddAnnouncementRequest>(requestBody);
            bool response = await _service.AddAsync(addRequest);

            return new OkObjectResult(successResponseText + response);
        }

        [FunctionName("DeleteAnnouncement")]
        public async Task<IActionResult> DeleteAsync(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = baseRoute + "/{id:int}")] HttpRequest req, int id)
        {
            var response = await _service.DeleteAsync(id);

            return new OkObjectResult(successResponseText + response);
        }

        [FunctionName("GetAnnouncementList")]
        public async Task<IActionResult> GetList(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = baseRoute + "/get-list")] HttpRequest req)
        {
            var response = await _service.GetListAsync();

            return new OkObjectResult(response);
        }
    }
}
