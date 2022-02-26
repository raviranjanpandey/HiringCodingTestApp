using AutoMapper;
using FluentValidation;
using MediatR;
using HiringCodingTestApis.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.Answer
{
    public class AnswerUpdate : IRequest<int>
    {
        public int AnsId { get; set; }
        public string UserId { get; set; }
        public int? ExamId { get; set; }
        public int? QueId { get; set; }
        public int? AnsOption { get; set; }
        public string Answer { get; set; }
        public bool? Flag { get; set; }
        public int? ScheduleId { get; set; }
    }

    public class AnswerUpdateValidator : AbstractValidator<AnswerUpdate>
    {
        public AnswerUpdateValidator()
        {
            RuleFor(x => x.AnsId).NotEmpty().WithMessage("Answer Id is mandatory.");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User Id is mandatory.");
            RuleFor(x => x.ExamId).NotNull().WithMessage("Exam Id is mandatory.");
            //RuleFor(x => x.QueId).NotNull().WithMessage("QueId is mandatory.");
            //RuleFor(x => x.AnsOption).NotNull().WithMessage("Answer Option is mandatory.");
            //RuleFor(x => x.Answer).NotEmpty().WithMessage("Answer is mandatory.");
        }
    }

    public class AnswerUpdateHandler : IRequestHandler<AnswerUpdate, int>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;
        public AnswerUpdateHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }
        public async Task<int> Handle(AnswerUpdate request, CancellationToken cancellationToken)
        {
            var existing = await _interviewContext.Answers.FindAsync(request.AnsId);
            if (existing == null) return 0;

            _mapper.Map(request, existing);

            if (await _interviewContext.SaveChangesAsync() > 0)
            {
                return existing.AnsId;
            }

            return 0;
        }
    }
}
