﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingWithDotNetChapterTwo.WebApp.ModelsDTO
{
    public class BillsDTO
    {
        public IList<BillDTO> billList { get; set; }
    }
}
