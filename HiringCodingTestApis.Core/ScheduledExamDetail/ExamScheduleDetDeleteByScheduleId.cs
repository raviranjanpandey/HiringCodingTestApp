using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HiringCodingTestApis.Core.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.ScheduledExamDetail
{
    public class ExamScheduleDetDeleteByScheduleId : IRequest<bool>
    {
        public int ScheduleId { get; set; }
    }

    public class ExamScheduleDetDeleteByScheduleIdValidator : AbstractValidator<ExamScheduleDetDeleteByScheduleId>
    {
        public ExamScheduleDetDeleteByScheduleIdValidator()
        {
            RuleFor(x => x.ScheduleId).NotEmpty().WithMessage("ScheduleId must not be zero.");
        }
    }

    public class ExamScheduleDetDeleteByScheduleIdHandler : IRequestHandler<ExamScheduleDetDeleteByScheduleId, bool>
    {
        private readonly InterviewContext _interviewContext;
        public ExamScheduleDetDeleteByScheduleIdHandler(InterviewContext interviewContext)
        {
            _interviewContext = interviewContext;
        }

        public async Task<bool> Handle(ExamScheduleDetDeleteByScheduleId request, CancellationToken cancellationToken)
        {
            var existing = await _interviewContext.Scheduledexamdetails.Where(a => a.ScheduleId == request.ScheduleId).ToListAsync();
            if (existing == null) return false;
            _interviewContext.Scheduledexamdetails.RemoveRange(existing);
            return await _interviewContext.SaveChangesAsync() > 0;
        }
    }
}
