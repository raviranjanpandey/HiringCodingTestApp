using AutoMapper;
using FluentValidation;
using MediatR;
using HiringCodingTestApis.Core.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.ExamSchedules
{
    public class ExamScheduleUpdate : IRequest<int>
    {
        public int ScheduleId { get; set; }
        public int? GroupId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? NumOfQuestions { get; set; }
        public bool? Active { get; set; }
       // public string UserId { get; set; }
        public int? TestDuration { get; set; }
        public bool? Isresult { get; set; }
        public bool? Isanswer { get; set; }
        public bool? Isquestimer { get; set; }
    }

    public class ExamScheduleUpdateValidator : AbstractValidator<ExamScheduleUpdate>
    {
        public ExamScheduleUpdateValidator()
        {
            RuleFor(x => x.ScheduleId).NotNull().GreaterThan(0).WithMessage("ScheduleId invalid.");
            RuleFor(x => x.GroupId).NotNull().GreaterThan(0).WithMessage("GroupId invalid.");
            RuleFor(x => x.StartDate).NotNull().WithMessage("StartDate can not be null.");
            RuleFor(x => x.EndDate).NotNull().WithMessage("EndDate can not be null.");
            RuleFor(x => x.Active).NotNull().WithMessage("Active can not be null.");
        }
    }
    public class ExamScheduleUpdateHandler : IRequestHandler<ExamScheduleUpdate, int>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;

        public ExamScheduleUpdateHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(ExamScheduleUpdate request, CancellationToken cancellationToken)
        {
            var existing = await _interviewContext.ExamSchedule.FindAsync(request.ScheduleId);
            if (existing == null) return 0;

            _mapper.Map(request, existing);

            if (await _interviewContext.SaveChangesAsync() > 0)
            {
                return existing.ScheduleId;
            }

            return 0;
        }
    }
}
