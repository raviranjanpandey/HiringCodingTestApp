using AutoMapper;
using FluentValidation;
using MediatR;
using HiringCodingTestApis.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.ExamResults
{
    public class ExamResultUpdate : IRequest<int>
    {
        public int ResId { get; set; }
        public string UserId { get; set; }
        public int? ExamId { get; set; }
        public int? GroupId { get; set; }
        public int? CorrectAnswer { get; set; }
        public int? WrongAnswer { get; set; }
        public int? ScheduleId { get; set; }
        public int? SkippedAnswer { get; set; }
        public int? FinalResult { get; set; }
        public bool? Flag { get; set; }
    }
    public class ExamResultUpdateValidator : AbstractValidator<ExamResultUpdate>
    {
        public ExamResultUpdateValidator()
        {
            RuleFor(x => x.ResId).NotEmpty().WithMessage("ResId can not be empty.");
            RuleFor(x => x.ExamId).NotNull().GreaterThan(0).WithMessage("ExamId invalid.");
            RuleFor(x => x.GroupId).NotNull().GreaterThan(0).WithMessage("GroupId invalid.");
        }
    }
    public class ExamResultUpdateHandler : IRequestHandler<ExamResultUpdate, int>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;
        public ExamResultUpdateHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }
        public async Task<int> Handle(ExamResultUpdate request, CancellationToken cancellationToken)
        {
            var existing = await _interviewContext.Results.FindAsync(request.ResId);
            if (existing == null) return 0;

            _mapper.Map(request, existing);

            if (await _interviewContext.SaveChangesAsync() > 0)
            {
                return existing.ResId;
            }

            return 0;
        }
    }
}
