using System.Collections.Generic;

namespace HiringCodingTestApis.Core.DTO
{
    public class ExamResultList
    {
        public List<ExamResultDto> Results { get; set; }
    }
    public class ExamResultDto
    {
        public int ResId { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public int? ExamId { get; set; }
        public string ExamName { get; set; }
        public int? ScheduleId { get; set; }
        public int? GroupId { get; set; }
        public int? CorrectAnswer { get; set; }
        public int? WrongAnswer { get; set; }
        public int? SkippedAnswer { get; set; }
        public int? FinalResult { get; set; }
        public bool? Flag { get; set; }
    }
}
