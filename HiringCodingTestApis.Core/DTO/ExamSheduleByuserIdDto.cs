using System;

namespace HiringCodingTestApis.Core.DTO
{
    public class ExamSheduleByuserIdDto
    {
        public int ScheduleId { get; set; }
        public int? GroupId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? NumOfQuestions { get; set; }
        public bool? Active { get; set; }
        public string GroupName { get; set; }
        public string UserId { get; set; }
        public int? TestDuration { get; set; }
        public bool? Isresult { get; set; }
        public bool? Isanswer { get; set; }
        public bool? Isquestimer { get; set; }
        public string Createdbyuser { get; set; }
        public string ExamDetails { get; set; }

    }
    public class SheduleByuserIdDto
    {
        public int SedId { get; set; }
        public int? ExamId { get; set; }
        public int? GroupId { get; set; }
        public int? ScheduleId { get; set; }
        public int? Questnos { get; set; }
        public int? PassingMarksPercentage { get; set; }
    }
}
