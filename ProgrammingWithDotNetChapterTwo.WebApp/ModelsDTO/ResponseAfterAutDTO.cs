using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingWithDotNetChapterTwo.WebApp.ModelsDTO
{
    public class ResponseAfterAutDTO : ResponseDTO
    {
        public bool IsAdmin { get; set; }
        public string IdUser { get; set; }
        public string Mail { get; set; }
    }
}
