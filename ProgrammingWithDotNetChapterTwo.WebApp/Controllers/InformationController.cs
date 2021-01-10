using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProgrammingWithDotNetChapterTwo.WebApp.ModelsDTO;
using ProgrammingWithDotNetChapterTwo.WebApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingWithDotNetChapterTwo.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformationController : ControllerBase
    {
        private readonly IInformationsService informationsService;
        private readonly ILogger<InformationController> logger;

        public InformationController(IInformationsService informationsService, ILogger<InformationController> logger)
        {
            this.informationsService = informationsService;
            this.logger = logger;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Add")]
        public IActionResult AddInformation([FromBody]InformationDTO informationDTO)
        {
            logger.LogInformation("Executing AddInformation controller");

            return Ok(informationsService.AddInformation(informationDTO));
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("Edit")]
        public ActionResult EditInformation([FromBody]InformationDTO informationDTO)
        {
            logger.LogInformation("Executing EditInformation controller");

            return Ok(informationsService.EditInformation(informationDTO));
        }

        [AllowAnonymous]
        [HttpDelete]
        [Route("Delete/{informationId}")]
        public IActionResult DeleteInformation(string informationId)
        {
            logger.LogInformation("Executing DeleteInformation controller");

            return Ok(informationsService.DeleteInformation(informationId));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAll/{userId}")]
        public IActionResult GetAllByUser(string userId)
        {
            logger.LogInformation("Executing GetAllByUser controller");

            return Ok(informationsService.GetAllByUser(userId));
        }
    }
}
