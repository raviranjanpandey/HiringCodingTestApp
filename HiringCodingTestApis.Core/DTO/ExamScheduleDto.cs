using System;
using System.Collections.Generic;

namespace HiringCodingTestApis.Core.DTO
{
    public class ExamScheduleList
    {
        public List<ExamScheduleDto> ExamSchedules { get; set; }
    }

    public class ExamDetScheduleList
    {
        public IList<ExamDetScheduleDto> ExamSchedules { get; set; }
    }

    public class ExamScheduleDto
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
        public List<SchExamDto> ExamDetails { get; set; }
    }
    public class ExamDetScheduleDto
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
        public List<SchDetDto> ExamDetails { get; set; }

    }
    public class ExamDetail
    {
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public string Description { get; set; }
        public List<QuestionMastersDto> Questions { get; set; }
    }

    public class ScheduleExamDetails
    {
        public ExamDetScheduleDto ScheduleDetails { get; set; }
        public List<ExamDetail> Details { get; set; }
    }
}
