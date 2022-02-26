using AutoMapper;
using FluentValidation;
using MediatR;
using HiringCodingTestApis.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.Answer
{
    public class AnswerCreate : IRequest<int>
    {
        public string UserId { get; set; }
        public int? ExamId { get; set; }
        public int? QueId { get; set; }
        public int? AnsOption { get; set; }
        public string Answer { get; set; }
        public bool? Flag { get; set; }
        public int? ScheduleId { get; set; }
    }

    public class AnswerCreateValidator : AbstractValidator<AnswerCreate>
    {
        public AnswerCreateValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User Id is mandatory.");
            RuleFor(x => x.ExamId).NotNull().WithMessage("Exam Id is mandatory.");
            RuleFor(x => x.QueId).NotNull().WithMessage("QueId is mandatory.");
            //RuleFor(x => x.AnsOption).NotNull().WithMessage("Answer Option is mandatory.");
            //RuleFor(x => x.Answer).NotEmpty().WithMessage("Answer is mandatory.");
        }
    }

    public class AnswerCreateHandler : IRequestHandler<AnswerCreate,int>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;
        public AnswerCreateHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }
        public async Task<int> Handle(AnswerCreate request, CancellationToken cancellationToken)
        {
            var det = _mapper.Map<AnswerCreate, Answers>(request);
            _interviewContext.Answers.Add(det);
            await _interviewContext.SaveChangesAsync();
            return det.AnsId;
        }
    }
}
