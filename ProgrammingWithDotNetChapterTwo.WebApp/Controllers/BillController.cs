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
    public class BillController : ControllerBase
    {
        private readonly IBillsService billService;
        private readonly ILogger<BillController> logger;

        public BillController(IBillsService billService, ILogger<BillController> logger)
        {
            this.billService = billService;
            this.logger = logger;
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult AddBill([FromBody]BillDTO billDTO)
        {
            logger.LogInformation("Executing AddBill controller");

            return Ok(billService.AddBill(billDTO));
        }

        [HttpPut]
        [Route("Edit")]
        public ActionResult EditBill([FromBody]BillDTO billDTO)
        {
            logger.LogInformation("Executing EditBill controller");

            return Ok(billService.EditBill(billDTO));
        }

        [AllowAnonymous]
        [HttpDelete]
        [Route("Delete/{mail}")]
        public IActionResult DeleteBill(string mail)
        {
            logger.LogInformation("Executing DeleteBill controller");

            return Ok(billService.DeleteBill(mail));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAll/{mail}")]
        public IActionResult GetAllBillByUser(string mail)
        {
            logger.LogInformation("Executing GetAllBillByUser controller");

            return Ok(billService.GetAllBillByUser(mail));
        }
    }
}
