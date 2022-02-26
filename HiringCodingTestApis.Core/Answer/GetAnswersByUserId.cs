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
    public class GetAnswersByUserId : IRequest<AnswersList>
    {
        public string UserId { get; set; }
    }

    public class GetAnswersByUserIdValidator : AbstractValidator<GetAnswersByUserId>
    {
        public GetAnswersByUserIdValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Please select valid User id to fetch.");
        }
    }

    public class GetAnswersByUserIdHandler : IRequestHandler<GetAnswersByUserId, AnswersList>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;

        public GetAnswersByUserIdHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }

        public async Task<AnswersList> Handle(GetAnswersByUserId request, CancellationToken cancellationToken)
        {
            List<AnswerDto> answers = new List<AnswerDto>();
            var answersByExam = await _interviewContext.Answers.Include(x => x.Que).Where(x => x.UserId == request.UserId).ToListAsync();
            foreach (var answer in answersByExam)
            {
                answers.Add(new AnswerDto
                {
                    AnsId = answer.AnsId,
                    AnsOption = answer.AnsOption,
                    Answer = answer.Answer,
                    CorrectAnswer = answer.Que.CorrectOption,
                    ExamId = answer.ExamId,
                    Flag = answer.Flag,
                    QueId = answer.QueId,
                    UserId = answer.UserId
                });
            }
            return new AnswersList
            {
                Answers = answers
            };
        }
    }
}
