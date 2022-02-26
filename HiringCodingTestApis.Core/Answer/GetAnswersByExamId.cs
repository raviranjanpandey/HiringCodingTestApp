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
    public class GetAnswersByExamId : IRequest<AnswersList>
    {
        public int ExamId { get; set; }
    }

    public class GetAnswersByExamIdValidator : AbstractValidator<GetAnswersByExamId>
    {
        public GetAnswersByExamIdValidator()
        {
            RuleFor(x => x.ExamId).NotEmpty().WithMessage("Please select valid exam id to fetch.");
        }
    }

    public class GetAnswersByExamIdHandler : IRequestHandler<GetAnswersByExamId, AnswersList>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;

        public GetAnswersByExamIdHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }

        public async Task<AnswersList> Handle(GetAnswersByExamId request, CancellationToken cancellationToken)
        {
            var answersByExam = await _interviewContext.Answers.Where(x => x.ExamId == request.ExamId).ToListAsync();
            return new AnswersList
            {
                Answers = _mapper.Map<List<Answers>, List<AnswerDto>>(answersByExam)
            };
        }
    }
}
