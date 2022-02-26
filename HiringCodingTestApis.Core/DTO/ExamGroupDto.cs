using System.Collections.Generic;

namespace HiringCodingTestApis.Core.DTO
{
    public class ExamGroupList
    {
        public List<ExamGroupDto> Exams { get; set; }
    }

    public class ExamGroupDto
    {
        public int GroupId { get; set; }
        public List<ExamMasterDto> Subjects { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string Createdbyuser { get; set; }

    }
}
