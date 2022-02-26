using System.Collections.Generic;

namespace HiringCodingTestApis.Core.DTO
{
    public class AnswerByQueDto
    {
        public string Question { get; set; }        
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public short? CorrectOption { get; set; }
        public int? AnsOption { get; set; }
    }
    public class AnswerByQueListDto
    {
        public List<AnswerByQueDto> AnswerByQueList { get; set; }
    }
}
