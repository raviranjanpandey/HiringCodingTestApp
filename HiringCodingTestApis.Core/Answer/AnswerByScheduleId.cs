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

namespace HiringCodingTestApis.Core.Answer
{
    public class AnswerByScheduleId : IRequest<AnswersList>
    {
        public int ScheduleId { get; set; }
        public string UserId { get; set; }
        public  AnswerByScheduleId(int scheduleId, string userId)
        {
            ScheduleId = scheduleId;
            UserId = userId;
        }

    }

    public class ExamResultGetByScheduleIdValidator : AbstractValidator<AnswerByScheduleId>
    {
        public ExamResultGetByScheduleIdValidator()
        {
            RuleFor(x => x.ScheduleId).NotEmpty().WithMessage("ScheduleId must not be zero or empty.");
        }
    }

    public class AnswerByScheduleIdHandler : IRequestHandler<AnswerByScheduleId, AnswersList>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;
        public AnswerByScheduleIdHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }

        public async Task<AnswersList> Handle(AnswerByScheduleId request, CancellationToken cancellationToken)
        {
            List<AnswerDto> answerdto = new List<AnswerDto>();
            var existing = await _interviewContext.Answers.Include(x => x.Schedule).Include(x => x.Que).Where(x => x.ScheduleId == request.ScheduleId && x.UserId == request.UserId).ToListAsync();
            foreach (var answer in existing)
            {
                answerdto.Add(new AnswerDto
                {
                    AnsId = answer.AnsId,
                    UserId = answer.UserId,
                    ExamId = answer.ExamId,
                    QueId = answer.QueId,
                    AnsOption = answer.AnsOption,
                    CorrectAnswer = answer.Que.CorrectOption,
                    Answer = answer.Answer,
                    Flag = answer.Flag,
                    ScheduleId = answer.ScheduleId,


                });
            }
            return new AnswersList
            {
                Answers = answerdto
            };
        }
    }
}