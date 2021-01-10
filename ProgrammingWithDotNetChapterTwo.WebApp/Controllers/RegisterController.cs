using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProgrammingWithDotNetChapterTwo.WebApp.Models;
using ProgrammingWithDotNetChapterTwo.WebApp.ModelsDTO;
using ProgrammingWithDotNetChapterTwo.WebApp.Services.Interfaces;

namespace ProgrammingWithDotNetChapterTwo.WebApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RegisterController> _logger;
        private readonly IUsersService usersService;

        public RegisterController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterController> logger,
            RoleManager<IdentityRole> roleManager,
            IUsersService usersService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _roleManager = roleManager;
            this.usersService = usersService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<ResponseAfterAutDTO> Login([FromBody]UserDTO user)
        {
            var result = await _signInManager.PasswordSignInAsync(user.Mail, user.Password, false, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
                return usersService.GetIdAndRoleForUserById(user.Mail);
            }
            else
            {
                _logger.LogInformation("User logged failed.");
                return new ResponseAfterAutDTO { Code = 400, Message = "Loggin failed", Status = "Failed" };
            }
        }
      

        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<ResponseAfterAutDTO> Register([FromBody]UserDTO user)
        {
            var newUser = new ApplicationUser { UserName = user.Name, Email = user.Mail, PhoneNumber = user.TelNumber.ToString(), IdPaid = false };
            var result = await _userManager.CreateAsync(newUser, user.Password);
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("Admin"))
                {
                    var role = new IdentityRole("Admin");
                    var res = await _roleManager.CreateAsync(role);

                    if (res.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(newUser, "Admin");
                    }
                }
                else
                {
                    if (!await _roleManager.RoleExistsAsync("User"))
                    {
                        var role = new IdentityRole("User");
                        var res = await _roleManager.CreateAsync(role);
                    }
                    await _userManager.AddToRoleAsync(newUser, "User");
                }
                _logger.LogInformation("User created a new account with password.");

                await _signInManager.SignInAsync(newUser, isPersistent: false);
                _logger.LogInformation("User created a new account with password.");
                return usersService.GetIdAndRoleForUserById(user.Mail); ;
            }
            _logger.LogInformation("User register failed.");
            return new ResponseAfterAutDTO { Code = 400, Message = "Loggin failed", Status = "Failed" };
        }       
    }
}