using AutoMapper;
using FluentValidation;
using MediatR;
using HiringCodingTestApis.Core.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.ExamResults
{
    public class ExamResultCreate : IRequest<int>
    {
        public string UserId { get; set; }
        public int? ExamId { get; set; }
        public int? ScheduleId { get; set; }
        public int? GroupId { get; set; }
        public int? CorrectAnswer { get; set; }
        public int? WrongAnswer { get; set; }
        public int? SkippedAnswer { get; set; }
        public int? FinalResult { get; set; }
        public bool? Flag { get; set; }
    }

    public class ExamResultCreateValidator : AbstractValidator<ExamResultCreate>
    {
        public ExamResultCreateValidator()
        {
            RuleFor(x => x.ExamId).NotNull().GreaterThan(0).WithMessage("ExamId invalid.");
            RuleFor(x => x.GroupId).NotNull().GreaterThan(0).WithMessage("GroupId invalid.");
        }
    }
    public class ExamResultCreateHandler : IRequestHandler<ExamResultCreate, int>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;
        public ExamResultCreateHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }
        public async Task<int> Handle(ExamResultCreate request, CancellationToken cancellationToken)
        {
            var det = _mapper.Map<ExamResultCreate, Results>(request);
            _interviewContext.Results.Add(det);
            await _interviewContext.SaveChangesAsync();
            return det.ResId;
        }
    }
}
