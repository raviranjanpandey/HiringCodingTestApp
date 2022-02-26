using FluentValidation;
using MediatR;
using HiringCodingTestApis.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.ExamSchedules
{
    public class ExamScheduleDelete : IRequest<bool>
    {
        public int ScheduleId { get; set; }
    }

    public class ExamScheduleDeleteValidator : AbstractValidator<ExamScheduleDelete>
    {
        public ExamScheduleDeleteValidator()
        {
            RuleFor(x => x.ScheduleId).NotEmpty().WithMessage("ScheduleId must not be zero.");
        }
    }

    public class ExamScheduleDeleteHandler : IRequestHandler<ExamScheduleDelete, bool>
    {
        private readonly InterviewContext _interviewContext;
        public ExamScheduleDeleteHandler(InterviewContext interviewContext)
        {
            _interviewContext = interviewContext;
        }

        public async Task<bool> Handle(ExamScheduleDelete request, CancellationToken cancellationToken)
        {
            var existing = await _interviewContext.ExamSchedule.FindAsync(request.ScheduleId);
            if (existing == null) return false;
            _interviewContext.ExamSchedule.Remove(existing);
            return await _interviewContext.SaveChangesAsync() > 0;
        }
    }
}
