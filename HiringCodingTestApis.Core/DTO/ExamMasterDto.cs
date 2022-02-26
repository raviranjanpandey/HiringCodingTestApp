using System.Collections.Generic;

namespace HiringCodingTestApis.Core.DTO
{
    public class ExamMasterList
    {
        public List<ExamMasterDto> Exams { get; set; }
    }
    public class ExamMasterDto
    {
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string Createdbyuser { get; set; }

    }
}
