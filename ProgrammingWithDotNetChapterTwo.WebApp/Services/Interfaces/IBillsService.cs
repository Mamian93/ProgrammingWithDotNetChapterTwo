using ProgrammingWithDotNetChapterTwo.WebApp.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingWithDotNetChapterTwo.WebApp.Services.Interfaces
{
    public interface IBillsService
    {
        ResponseDTO AddBill(BillDTO billDTO);
        ResponseDTO EditBill(BillDTO billDTO);
        ResponseDTO DeleteBill(string mail);
        BillsDTO GetAllBillByUser(string mail);
    }
}
