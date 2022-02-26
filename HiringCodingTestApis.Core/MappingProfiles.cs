using AutoMapper;
using HiringCodingTestApis.Core.Answer;
using HiringCodingTestApis.Core.AssignedUser;
using HiringCodingTestApis.Core.DTO;
using HiringCodingTestApis.Core.ExamResults;
using HiringCodingTestApis.Core.ExamSchedules;
using HiringCodingTestApis.Core.ExamsMaster;
using HiringCodingTestApis.Core.Models;
using HiringCodingTestApis.Core.QuestionsMaster;

namespace HiringCodingTestApis.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ExamMasterCreate, ExamMaster>();
            CreateMap<ExamMasterUpdate, ExamMaster>();
            CreateMap<ExamMaster, ExamMasterDto>();

            CreateMap<QuestionMastersCreate, QuestionMaster>();
            CreateMap<QuestionMasterCreateRange, QuestionMaster>();
            CreateMap<QuestionMastersUpdate, QuestionMaster>();
            CreateMap<QuestionMaster, QuestionMastersDto>();

            CreateMap<AnswerCreate, Answers>();
            CreateMap<AnswerCreateRange, Answers>();
            CreateMap<AnswerUpdate, Answers>();
            CreateMap<Answers, AnswerDto>();

            CreateMap<ExamResultCreate, Results>();
            CreateMap<ExamResultUpdate, Results>();
            CreateMap<Results, ExamResultDto>();

            CreateMap<ExamScheduleCreate, ExamSchedule>();
            CreateMap<ExamScheduleUpdate, ExamSchedule>();

            CreateMap<SchExamDto, Scheduledexamdetails>().ReverseMap();
            CreateMap<CreateUserDto, AspNetUsers>();
            CreateMap<AspNetUsers, CreateUserDto>();

            CreateMap<AssignedUserCreate, AssignedUsers>();
            CreateMap<AssignedUserUpdate, AssignedUsers>();
        }
    }
}
