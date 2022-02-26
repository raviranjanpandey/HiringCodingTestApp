using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HiringCodingTestApis.Core.DTO;
using HiringCodingTestApis.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.ExamSchedules
{
    public class ExamScheduleGetByExamGroup : IRequest<ExamScheduleList>
    {
        public int GroupId { get; set; }
    }

    public class ExamScheduleGetByExamGroupValidator : AbstractValidator<ExamScheduleGetByExamGroup>
    {
        public ExamScheduleGetByExamGroupValidator()
        {
            RuleFor(x => x.GroupId).NotEmpty().WithMessage("GroupId must not be zero or empty.");
        }
    }

    public class ExamScheduleGetByExamGroupHandler : IRequestHandler<ExamScheduleGetByExamGroup, ExamScheduleList>
    {
        private readonly InterviewContext _interviewContext;
        public ExamScheduleGetByExamGroupHandler(InterviewContext interviewContext)
        {
            _interviewContext = interviewContext;
        }

        public async Task<ExamScheduleList> Handle(ExamScheduleGetByExamGroup request, CancellationToken cancellationToken)
        {
            var existing = await _interviewContext.ExamSchedule.Include(x=>x.Group).Where(x => x.GroupId == request.GroupId).ToListAsync();
            if (existing == null) return new ExamScheduleList();
            List<ExamScheduleDto> list = (from detail in existing
                                          select new ExamScheduleDto
                                          {
                                              ScheduleId = detail.ScheduleId,
                                              Active = detail.Active,
                                              EndDate = detail.EndDate,
                                              GroupId = detail.GroupId,
                                              GroupName = detail.Group.GroupName,
                                              StartDate = detail.StartDate,
                                              NumOfQuestions = detail.NumOfQuestions,
                                              UserId=detail.UserId,
                                              TestDuration=detail.TestDuration,
                                              Isresult=detail.Isresult,
                                              Isquestimer = detail.Isquestimer,
                                              Isanswer = detail.Isanswer
                                          }).ToList();
            return new ExamScheduleList
            {
                ExamSchedules = list
            };
        }
    }
}
