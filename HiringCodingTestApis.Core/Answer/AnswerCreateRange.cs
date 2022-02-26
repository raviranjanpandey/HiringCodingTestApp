using AutoMapper;
using FluentValidation;
using MediatR;
using HiringCodingTestApis.Core.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.Answer
{
    public class AnswerCreateRange
    {
        public string UserId { get; set; }
        public int? ExamId { get; set; }
        public int? QueId { get; set; }
        public int? AnsOption { get; set; }
        public string Answer { get; set; }
        public bool? Flag { get; set; }
    }

    public class AnswerCreateRangeCommand : IRequest<bool>
    {
        public List<AnswerCreateRange> AnswerList { get; set; }
    }

    public class AnswerCreateRangeCommandValidator : AbstractValidator<AnswerCreateRangeCommand>
    {
        public AnswerCreateRangeCommandValidator()
        {
            RuleForEach(x => x.AnswerList).ChildRules(answer =>
            {
                answer.RuleFor(x => x.UserId).NotEmpty().WithMessage("User Id is mandatory.");
                answer.RuleFor(x => x.ExamId).NotNull().WithMessage("Exam Id is mandatory.");
                answer.RuleFor(x => x.QueId).NotNull().WithMessage("QueId is mandatory.");
                //answer.RuleFor(x => x.AnsOption).NotNull().WithMessage("Answer Option is mandatory.");
                //answer.RuleFor(x => x.Answer).NotEmpty().WithMessage("QueId is mandatory.");
            });
        }
    }

    public class AnswerCreateRangeCommandHandler : IRequestHandler<AnswerCreateRangeCommand, bool>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;

        public AnswerCreateRangeCommandHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }

        public async Task<bool> Handle(AnswerCreateRangeCommand request, CancellationToken cancellationToken)
        {
            var det = _mapper.Map<List<AnswerCreateRange>, List<Answers>>(request.AnswerList);
            _interviewContext.Answers.AddRange(det);
            return await _interviewContext.SaveChangesAsync() > 0;
        }
    }
}
