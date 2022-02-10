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
        private const string routeWithId = baseRoute + "/{id:int}";
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
            var result = await _service.AddAsync(addRequest);

            if (result.ErrorCode == 400)
            {
                return new BadRequestObjectResult(result.ErrorMessage);
            }
            else if (result.IsError)
            {
                return new StatusCodeResult(result.ErrorCode);
            }

            return new OkObjectResult($"New id: " + result.Response);
        }

        [FunctionName("DeleteAnnouncement")]
        public async Task<IActionResult> DeleteAsync(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = routeWithId)] HttpRequest req, int id)
        {
            var result = await _service.DeleteAsync(id);

            if (result.ErrorCode == 404)
            {
                return new NotFoundObjectResult(result.ErrorMessage);
            }
            else if (result.IsError)
            {
                return new StatusCodeResult(result.ErrorCode);
            }

            return new OkObjectResult(successResponseText + result.Response);
        }

        [FunctionName("GetAnnouncementById")]
        public async Task<IActionResult> GetByIdAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = routeWithId)] HttpRequest req, int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result.IsError)
            {
                return new NotFoundObjectResult(result.ErrorMessage);
            }

            return new OkObjectResult(result.Response);
        }

        [FunctionName("GetAnnouncementList")]
        public async Task<IActionResult> GetListAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = baseRoute + "/get-list")] HttpRequest req)
        {
            string searchText = req.Query["searchText"];
            string typeFilter = req.Query["announcementType"];

            int pageNumber = 0;
            int.TryParse(req.Query["pageNumber"], out pageNumber);

            int elementsOnPage = 0;
            int.TryParse(req.Query["elementsOnPage"], out elementsOnPage);

            var result = await _service.GetListAsync(searchText, typeFilter, pageNumber, elementsOnPage);

            if (result.IsError)
            {
                return new BadRequestObjectResult(result.ErrorMessage);
            }

            return new OkObjectResult(result.Response);
        }

        [FunctionName("UpdateAnnouncement")]
        public async Task<IActionResult> UpdateAsync(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = baseRoute)] HttpRequest req)
        {
            string requestBody = String.Empty;
            using (StreamReader streamReader = new StreamReader(req.Body))
            {
                requestBody = await streamReader.ReadToEndAsync();
            }

            var updateRequest = JsonConvert.DeserializeObject<UpdateAnnouncementRequest>(requestBody);
            var result = await _service.UpdateAsync(updateRequest);

            if (result.ErrorCode == 404)
            {
                return new NotFoundObjectResult(result.ErrorMessage);
            }
            else if (result.IsError)
            {
                return new StatusCodeResult(result.ErrorCode);
            }

            return new OkObjectResult(successResponseText + result.Response);
        }
    }
}
