using ProgrammingWithDotNetChapterTwo.WebApp.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingWithDotNetChapterTwo.WebApp.Services.Interfaces
{
    public interface IInformationsService
    {
        ResponseDTO AddInformation(InformationDTO informationDTO);
        ResponseDTO EditInformation(InformationDTO informationDTO);
        ResponseDTO DeleteInformation(string mail);
        InformationsDTO GetAllByUser(string userId);
    }
}
