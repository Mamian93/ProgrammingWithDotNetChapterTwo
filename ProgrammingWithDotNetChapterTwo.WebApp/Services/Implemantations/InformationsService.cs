using AutoMapper;
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
    public class InformationsService : IInformationsService
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public InformationsService(ApplicationDbContext context,
                    ILogger<InformationsService> logger,
                    IMapper mapper)
        {
            this.context = context;
            this.logger = logger;
            this.mapper = mapper;
        }

        public ResponseDTO AddInformation(InformationDTO informationDTO)
        {
            logger.LogInformation("Executing AddInformation method");

            try
            {
                context.Information.Add(mapper.Map<Information>(informationDTO));
                context.SaveChanges();
            }
            catch (Exception e)
            {
                return new ResponseDTO() { Code = 400, Message = e.Message, Status = "Error during add information" };
            }

            return new ResponseDTO() { Code = 200, Message = "Added information to db", Status = "Success" };
        }

        public ResponseDTO DeleteInformation(string mail)
        {
            logger.LogInformation("Executing DeleteBill method");

            var informationToRemove = context.Information.Where(i => i.User.Email == mail).SingleOrDefault();

            if (informationToRemove == null)
            {
                return new ResponseDTO() { Code = 400, Message = $"Information about user mail {mail} doesn't exist in db", Status = "Error" };
            }

            try
            {
                context.Information.Remove(informationToRemove);
            }
            catch (Exception e)
            {
                return new ResponseDTO() { Code = 400, Message = e.Message, Status = "Error during delete information" };
            }

            return new ResponseDTO() { Code = 200, Message = "Delete information from db", Status = "Success" };
        }

        public ResponseDTO EditInformation(InformationDTO informationDTO)
        {
            logger.LogInformation("Executing EditBill method");

            if (context.Information.Where(b => b.Id == informationDTO.Id).Count() == 0)
            {
                return new ResponseDTO() { Code = 400, Message = $"Information about id {informationDTO.Id} doesn't exist in db", Status = "Error" };
            }

            try
            {
                context.Information.Update(mapper.Map<Information>(informationDTO));
                context.SaveChanges();
            }
            catch (Exception e)
            {
                return new ResponseDTO() { Code = 400, Message = e.Message, Status = "Error" };
            }

            return new ResponseDTO() { Code = 200, Message = "Edit information in db", Status = "Success" };
        }

        public InformationsDTO GetAllByUser(string mail)
        {
            var result = context.Information.Where(b => b.User.Email == mail).ToList();

            InformationsDTO informationDTO = new InformationsDTO() { };
            informationDTO.informationList = new List<InformationDTO>();

            foreach (Information information in result)
            {
                informationDTO.informationList.Add(mapper.Map<InformationDTO>(information));
            }

            return informationDTO;
        }
    }
}
