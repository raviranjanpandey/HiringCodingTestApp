using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using HiringCodingTestApis.Core.DTO;
using HiringCodingTestApis.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.ExamSchedules
{
    public class GetExamDetailsByScheduleId : IRequest<ScheduleExamDetails>
    {
        public int ScheduleId { get; set; }
    }

    public class GetExamDetailsByScheduleIdValidator : AbstractValidator<GetExamDetailsByScheduleId>
    {
        public GetExamDetailsByScheduleIdValidator()
        {
            RuleFor(x => x.ScheduleId).NotEmpty().WithMessage("ScheduleId must not be zero or empty.");
        }
    }

    public class GetExamDetailsByScheduleIdHandler : IRequestHandler<GetExamDetailsByScheduleId, ScheduleExamDetails>
    {
        private readonly InterviewContext _interviewContext;
        public GetExamDetailsByScheduleIdHandler(InterviewContext interviewContext)
        {
            _interviewContext = interviewContext;
        }

        public async Task<ScheduleExamDetails> Handle(GetExamDetailsByScheduleId request, CancellationToken cancellationToken)
        {
            ScheduleExamDetails result = new ScheduleExamDetails();
            var detail = await _interviewContext.ExamSchedule.Include(x => x.Group)
                                  .Include(x => x.Scheduledexamdetails).
                                  Where(y => y.ScheduleId == request.ScheduleId).FirstOrDefaultAsync();

            if (detail != null)
            {
                var examids = JsonConvert.DeserializeObject<List<int>>(detail.Group.ExamIdJson);
                var exams = await _interviewContext.ExamMaster
                                       .Where(x => examids.Contains(x.ExamId))
                                       .Include(x => x.QuestionMaster)
                                       .ToListAsync();
                result.ScheduleDetails = new ExamDetScheduleDto
                {
                    ScheduleId = detail.ScheduleId,
                    Active = detail.Active,
                    EndDate = detail.EndDate,
                    GroupId = detail.GroupId,
                    GroupName = detail.Group.GroupName,
                    StartDate = detail.StartDate,
                    UserId=detail.UserId,
                    TestDuration = detail.TestDuration,
                    Isresult=detail.Isresult,
                    Isanswer = detail.Isanswer,
                    Isquestimer = detail.Isquestimer,
                    ExamDetails = (from det in detail.Scheduledexamdetails
                                   join exam in exams on det.ExamId equals exam.ExamId
                                   select new SchDetDto
                                   {
                                       ExamId = det.ExamId,
                                       GroupId = det.GroupId,
                                       PassingMarksPercentage = det.PassingMarksPercentage,
                                       Questnos = det.Questnos,
                                       ScheduleId = det.ScheduleId,
                                       SedId = det.SedId,
                                       ExamName = exam.ExamName
                                   }
                                  ).ToList()
                };

                if (examids != null && examids.Count > 0)
                {

                    result.Details = (from det in exams
                                      select new ExamDetail
                                      {
                                          Description = det.Description,
                                          ExamId = det.ExamId,
                                          ExamName = det.ExamName,
                                          Questions = (from ques in det.QuestionMaster
                                                       select new QuestionMastersDto
                                                       {
                                                           QueId = ques.QueId,
                                                           ExamId = ques.ExamId,
                                                           SubModule = ques.SubModule,
                                                           AnswerType = ques.AnswerType,
                                                           Question = ques.Question,
                                                           Option1 = ques.Option1,
                                                           Option2 = ques.Option2,
                                                           Option3 = ques.Option3,
                                                           Option4 = ques.Option4,
                                                           CorrectOption = ques.CorrectOption,
                                                           Seconds = ques.Seconds
                                                       }).ToList()
                                      }).ToList();
                }
            }
            return result;
        }
    }
}
