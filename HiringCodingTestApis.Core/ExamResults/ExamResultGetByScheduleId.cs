using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HiringCodingTestApis.Core.DTO;
using HiringCodingTestApis.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.ExamResults
{
    public class ExamResultGetByScheduleId : Query<ExamResultList>
    {
        public int ScheduleId { get; set; }
        public ExamResultGetByScheduleId(int scheduleId,int? take, int? skip) : base(take, skip)
        {
            ScheduleId = scheduleId;
        }
    }

    public class ExamResultGetByScheduleIdValidator : AbstractValidator<ExamResultGetByScheduleId>
    {
        public ExamResultGetByScheduleIdValidator()
        {
            RuleFor(x => x.ScheduleId).NotEmpty().WithMessage("ScheduleId must not be zero or empty.");
        }
    }

    public class ExamResultGetByScheduleIdHandler : IRequestHandler<ExamResultGetByScheduleId, ExamResultList>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;
        public ExamResultGetByScheduleIdHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }

        public async Task<ExamResultList> Handle(ExamResultGetByScheduleId request, CancellationToken cancellationToken)
        {
            var existing = await _interviewContext.Results.Include(x => x.Exam).Include(x=>x.User).Where(x => x.ScheduleId == request.ScheduleId).Skip((int)request.Skip).Take((int)request.Take).ToListAsync();
            if (existing == null) return new ExamResultList();
            return new ExamResultList
            {
                Results = (from det in existing
                           select new ExamResultDto
                           {
                               ResId = det.ResId,
                               UserId = det.UserId,
                               ExamId = det.ExamId,
                               ExamName = det.Exam.ExamName,
                               ScheduleId = det.ScheduleId,
                               GroupId = det.GroupId,
                               CorrectAnswer = det.CorrectAnswer,
                               WrongAnswer = det.WrongAnswer,
                               SkippedAnswer = det.SkippedAnswer,
                               FinalResult = det.FinalResult,
                               Flag = det.Flag,
                               Email = det.User.Email
                           }).ToList()
            };
        }
    }
}


