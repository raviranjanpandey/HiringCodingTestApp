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
    public class GetExamResultByUserId : Query<ExamResultList>
    {
        public string UserId { get; set; }
        public GetExamResultByUserId(string userId, int? take, int? skip) : base(take, skip)
        {
            UserId = userId;
        }
    }
   

    public class GetExamResultByUserIdHandler : IRequestHandler<GetExamResultByUserId, ExamResultList>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;

        public GetExamResultByUserIdHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }

        public async Task<ExamResultList> Handle(GetExamResultByUserId request, CancellationToken cancellationToken)
        {
            List<ExamResultDto> examResultDtos = new List<ExamResultDto>();
            var examResults = await _interviewContext.Results.Include(x => x.Exam).Where(x => x.UserId == request.UserId).Skip((int)request.Skip).Take((int)request.Take).ToListAsync();
            foreach (var exams in examResults)
            {
                examResultDtos.Add(new ExamResultDto
                {

                    ResId = exams.ResId,
                    UserId = exams.UserId,
                    ExamId = exams.ExamId,
                    ExamName = exams.Exam.ExamName,
                    ScheduleId = exams.ScheduleId,
                    GroupId = exams.GroupId,
                    CorrectAnswer = exams.CorrectAnswer,
                    WrongAnswer = exams.WrongAnswer,
                    SkippedAnswer = exams.SkippedAnswer,
                    FinalResult = exams.FinalResult,
                    Flag = exams.Flag,
                    Email = exams.User.Email
                });
            }
            return new ExamResultList
            {
                Results = examResultDtos
            };
        }
    }
}