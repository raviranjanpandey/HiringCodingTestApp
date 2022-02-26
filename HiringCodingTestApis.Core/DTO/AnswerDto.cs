using System.Collections.Generic;

namespace HiringCodingTestApis.Core.DTO
{
    public class AnswersList
    {
        public List<AnswerDto> Answers { get; set; }
    }
    public class AnswerDto
    {
        public int AnsId { get; set; }
        public string UserId { get; set; }
        public int? ExamId { get; set; }
        public int? QueId { get; set; }
        public int? AnsOption { get; set; }
        public short? CorrectAnswer { get; set; }
        public string Answer { get; set; }
        public bool? Flag { get; set; }
        public int? ScheduleId { get; set; }
    }
}
