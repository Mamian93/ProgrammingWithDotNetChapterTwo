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
    public class BillsService : IBillsService
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public BillsService(ApplicationDbContext context,
                            ILogger<BillsService> logger,
                            IMapper mapper)
        {
            this.context = context;
            this.logger = logger;
            this.mapper = mapper;
        }

        public ResponseDTO AddBill(BillDTO billDTO)
        {
            logger.LogInformation("Executing AddBill method");

            try
            {
                var a = mapper.Map<Bill>(billDTO);
                context.Bill.Add(a);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                return new ResponseDTO() { Code = 400, Message = e.Message, Status = "Error during add bill" };
            }

            return new ResponseDTO() { Code = 200, Message = "Added bill to db", Status = "Success" };
        }

        public ResponseDTO DeleteBill(string mail)
        {
            logger.LogInformation("Executing DeleteBill method");

            var billToRemove = context.Bill.Where(b => b.User.Email == mail).SingleOrDefault();

            if (billToRemove == null)
            {
                return new ResponseDTO() { Code = 400, Message = $"Bill about user mail {mail} doesn't exist in db", Status = "Error" };
            }

            try
            {
                context.Bill.Remove(billToRemove);
            }
            catch (Exception e)
            {
                return new ResponseDTO() { Code = 400, Message = e.Message, Status = "Error during delete bill" };
            }

            return new ResponseDTO() { Code = 200, Message = "Delete bill from db", Status = "Success" };
        }

        public ResponseDTO EditBill(BillDTO billDTO)
        {
            logger.LogInformation("Executing EditBill method");

            if (context.Bill.Where(b => b.Name == billDTO.Name).Count() == 0)
            {
                return new ResponseDTO() { Code = 400, Message = $"Bill about id {billDTO.Name} doesn't exist in db", Status = "Error" };
            }

            try
            {
                context.Bill.Update(mapper.Map<Bill>(billDTO));
                context.SaveChanges();
            }
            catch (Exception e)
            {
                return new ResponseDTO() { Code = 400, Message = e.Message, Status = "Error" };
            }

            return new ResponseDTO() { Code = 200, Message = "Edit bill in db", Status = "Success" };
        }

        public BillsDTO GetAllBillByUser(string mail)
        {
            var result = context.Bill.Where(b => b.User.Email == mail).ToList();

            BillsDTO billsDTO = new BillsDTO() { };
            billsDTO.billList = new List<BillDTO>();

            foreach (Bill bill in result)
            {
                billsDTO.billList.Add(mapper.Map<BillDTO>(bill));
            }
            billsDTO.billList = billsDTO.billList.OrderBy(o => o.Year).Reverse().ToList();

            return billsDTO;
        }
    }
}
