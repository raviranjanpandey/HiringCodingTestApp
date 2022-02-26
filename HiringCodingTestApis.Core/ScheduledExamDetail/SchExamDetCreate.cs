using AutoMapper;
using FluentValidation;
using MediatR;
using HiringCodingTestApis.Core.DTO;
using HiringCodingTestApis.Core.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.ScheduledExamDetail
{
    public class SchExamDetCreate : IRequest<bool>
    {
        public List<SchExamDto> ExamDetails { get; set; }
    }

    public class SchExamDetCreateHandler : IRequestHandler<SchExamDetCreate, bool>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;

        public SchExamDetCreateHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }
        public class SchExamDetCreateRangeCommandValidator : AbstractValidator<SchExamDetCreate>
        {
            public SchExamDetCreateRangeCommandValidator()
            {
                RuleForEach(x => x.ExamDetails).ChildRules(exam =>
                {
                    exam.RuleFor(x => x.ExamId).Must(examid => examid != null && examid > 0).WithMessage("Exam Id is mandatory.");
                    exam.RuleFor(x => x.ScheduleId).GreaterThan(0).WithMessage("ScheduleId is mandatory.");
                    exam.RuleFor(x => x.GroupId).GreaterThan(0).WithMessage("Group id is mandatory.");
                });
            }
        }
        public async Task<bool> Handle(SchExamDetCreate request, CancellationToken cancellationToken)
        {
            List<Scheduledexamdetails> listToSave = _mapper.Map<List<SchExamDto>, List<Scheduledexamdetails>>(request.ExamDetails);
            if (listToSave != null && listToSave.Count > 0)
            {
                _interviewContext.Scheduledexamdetails.AddRange(listToSave);
                return await _interviewContext.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
