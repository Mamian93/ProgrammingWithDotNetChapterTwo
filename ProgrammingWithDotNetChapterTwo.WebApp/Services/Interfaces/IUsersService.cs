using ProgrammingWithDotNetChapterTwo.WebApp.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingWithDotNetChapterTwo.WebApp.Services.Interfaces
{
    public interface IUsersService
    {
        UsersDTO GetAllUsers();
        Task<ResponseDTO> EditUser(UserDTO userDTO);
        ResponseAfterAutDTO GetIdAndRoleForUserById(string mail);
    }
}
