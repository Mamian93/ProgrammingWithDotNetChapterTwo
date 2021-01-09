using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ProgrammingWithDotNetChapterTwo.WebApp.Data;
using ProgrammingWithDotNetChapterTwo.WebApp.Models;
using ProgrammingWithDotNetChapterTwo.WebApp.ModelsDTO;
using ProgrammingWithDotNetChapterTwo.WebApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingWithDotNetChapterTwo.WebApp.Services.Implemantations
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public UsersService(ApplicationDbContext context,
                            ILogger<UsersService> logger,
                            IMapper mapper)
        {
            this.context = context;
            this.logger = logger;
            this.mapper = mapper;
        }

        async public Task<ResponseDTO> EditUser(UserDTO userDTO)
        {
            logger.LogInformation("Executing EditUserByMail method");
            var user = context.AplicationUser.Where(b => b.Id == userDTO.Id).SingleOrDefault();

            if (user == null)
            {
                return new ResponseDTO() { Code = 400, Message = $"ApplicationUser about id {userDTO.Id} doesn't exist in db", Status = "Error" };
            }

            user.IdPaid = userDTO.IsPaid;
            user.Email = userDTO.Mail;
            user.UserName = userDTO.Name;
            user.PasswordHash = userDTO.Password;
            user.PhoneNumber = userDTO.TelNumber;

            try
            {
                context.AplicationUser.Update(user);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                return new ResponseDTO() { Code = 400, Message = e.Message, Status = "Error" };
            }

            return new ResponseDTO() { Code = 200, Message = "Edit applicationUser in db", Status = "Success" };
        }

        public UsersDTO GetAllUsers()
        {
            logger.LogInformation("Executing GetAllUsers method");

            var result = context.AplicationUser.ToList();

            UsersDTO userDTO = new UsersDTO() { };
            userDTO.usersList = new List<UserDTO>();

            foreach (ApplicationUser user in result)
            {
                userDTO.usersList.Add(this.mapper.Map<UserDTO>(user));
            }

            return userDTO;
        }

        public ResponseAfterAutDTO GetIdAndRoleForUserById(string mail)
        {
            logger.LogInformation("Executing GetIdAndRoleForUserById method");

            var user = context.AplicationUser.Where(u => u.Email == mail).SingleOrDefault();
            var roleid = context.UserRoles.Where(r => r.UserId == user.Id).FirstOrDefault().RoleId;
            var roleName = context.Roles.Where(r => r.Id == roleid).SingleOrDefault().Name;
            var isAdmin = (roleName == "Admin") ? true : false;

            return new ResponseAfterAutDTO { Code = 200, Message = "User logged", Status = "Success", IdUser = user.Id, Mail = mail, IsAdmin = isAdmin };
        }
    }
}
