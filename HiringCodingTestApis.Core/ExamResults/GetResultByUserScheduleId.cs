using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HiringCodingTestApis.Core.DTO;
using HiringCodingTestApis.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.ExamResults
{
   public class GetResultByUserScheduleId : IRequest<ExamResultList>
    {
        public int ScheduleId { get; set; }
        public string UserId { get; set; }
        public GetResultByUserScheduleId(int scheduleId, string userId)
        {
            ScheduleId = scheduleId;
            UserId = userId;
        }

    }

    public class GetResultByUserScheduleIdValidator : AbstractValidator<GetResultByUserScheduleId>
    {
        public GetResultByUserScheduleIdValidator()
        {
            RuleFor(x => x.ScheduleId).NotEmpty().WithMessage("ScheduleId must not be zero or empty.");
        }
    }

    public class GetResultByUserScheduleIdHandler : IRequestHandler<GetResultByUserScheduleId, ExamResultList>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;
        public GetResultByUserScheduleIdHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }

        public async Task<ExamResultList> Handle(GetResultByUserScheduleId request, CancellationToken cancellationToken)
        {
            List<AnswerDto> answerdto = new List<AnswerDto>();
            var existing = await _interviewContext.Results.Include(x => x.Exam).Include(x => x.User).Where(x => x.ScheduleId == request.ScheduleId && x.UserId==request.UserId).ToListAsync();
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