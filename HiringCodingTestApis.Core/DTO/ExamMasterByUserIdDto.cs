using System;
using System.Collections.Generic;
using System.Text;

namespace HiringCodingTestApis.Core.DTO
{
   public class ExamMasterByUserIdDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }        
        public int ExamId { get; set; }
        public string ExamName { get; set; }
    }
}
