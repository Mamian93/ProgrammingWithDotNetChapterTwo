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
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService usersService;
        private readonly ILogger<UsersController> logger;

        public UsersController(IUsersService usersService, ILogger<UsersController> logger)
        {
            this.usersService = usersService;
            this.logger = logger;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllUsers()
        {
            logger.LogInformation("Executing GetAllUsers controller");

            return Ok(usersService.GetAllUsers());
        }

        [HttpPut]
        [Route("Edit")]
        public ActionResult EditUser([FromBody]UserDTO userDTO)
        {
            logger.LogInformation("Executing EditUser controller");

            return Ok(usersService.EditUser(userDTO).Result);
        }
    }
}
