using System;
using System.Collections.Generic;
using System.Text;

namespace HiringCodingTestApis.Core.DTO
{
   public class ExamGroupByUserIdDto
    {
        
         public int GroupId { get; set; }
        public string Subjects { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }        
        public string Createdbyuser { get; set; }

    }
}
