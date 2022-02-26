namespace HiringCodingTestApis.Core.DTO
{
    public class SchExamDto
    {
        public int SedId { get; set; }
        public int? ExamId { get; set; }
        public int? GroupId { get; set; }
        public int? ScheduleId { get; set; }
        public int? Questnos { get; set; }
        public int? PassingMarksPercentage { get; set; }
    }
    public class SchDetDto : SchExamDto
    {
        public string ExamName { get; set; }
    }
}
