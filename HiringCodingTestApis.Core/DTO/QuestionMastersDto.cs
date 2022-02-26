using System.Collections.Generic;

namespace HiringCodingTestApis.Core.DTO
{
    public class QuestionMasterList
    {
        public List<QuestionMastersDto> QuestionsList { get; set; }
    }

    public class QuestionMastersDto
    {
        public int QueId { get; set; }
        public int? ExamId { get; set; }
        public string SubModule { get; set; }
        public string Question { get; set; }
        public short? AnswerType { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public short? CorrectOption { get; set; }
        public short? Seconds { get; set; }
        public string UserId { get; set; }
    }
}
