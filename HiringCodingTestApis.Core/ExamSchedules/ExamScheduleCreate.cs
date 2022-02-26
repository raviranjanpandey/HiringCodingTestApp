using AutoMapper;
using FluentValidation;
using MediatR;
using HiringCodingTestApis.Core.DTO;
using HiringCodingTestApis.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.ExamSchedules
{
    public class ExamScheduleCreate : IRequest<int>
    {
        public int? GroupId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? NumOfQuestions { get; set; }
        public bool? Active { get; set; }
        public List<SchExamDto> ExamDetails { get; set; }
        public string UserId { get; set; }
        public int? TestDuration { get; set; }
        public bool? Isresult { get; set; }
        public bool? Isanswer { get; set; }
        public bool? Isquestimer { get; set; }
    }

    public class ExamScheduleCreateValidator : AbstractValidator<ExamScheduleCreate>
    {
        public ExamScheduleCreateValidator()
        {
            RuleFor(x => x.GroupId).NotNull().GreaterThan(0).WithMessage("GroupId invalid.");
            RuleFor(x => x.StartDate).NotNull().WithMessage("StartDate can not be null.");
            RuleFor(x => x.EndDate).NotNull().WithMessage("EndDate can not be null.");
            RuleFor(x => x.Active).NotNull().WithMessage("Active can not be null.");
            RuleForEach(x => x.ExamDetails).ChildRules(exam =>
            {
                exam.RuleFor(x => x.ExamId).Must(examid => examid != null && examid > 0).WithMessage("Exam Id is mandatory.");
                exam.RuleFor(x => x.GroupId).GreaterThan(0).WithMessage("Group id is mandatory.");
            });
        }
    }
    public class ExamScheduleCreateHandler : IRequestHandler<ExamScheduleCreate, int>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;
        public ExamScheduleCreateHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(ExamScheduleCreate request, CancellationToken cancellationToken)
        {
            var det = _mapper.Map<ExamScheduleCreate, ExamSchedule>(request);
            _interviewContext.ExamSchedule.Add(det);
            await _interviewContext.SaveChangesAsync();
            return det.ScheduleId;
        }
    }
}
