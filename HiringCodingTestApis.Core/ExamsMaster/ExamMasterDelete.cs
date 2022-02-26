using AutoMapper;
using FluentValidation;
using MediatR;
using HiringCodingTestApis.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.ExamsMaster
{
    public class ExamMasterDelete : IRequest<bool>
    {
        public int ExamId { get; set; }
    }

    public class ExamMasterDeleteValidator : AbstractValidator<ExamMasterDelete>
    {
        public ExamMasterDeleteValidator()
        {
            RuleFor(x => x.ExamId).GreaterThan(0).WithMessage("ExamId must not be zero or empty.");
        }
    }

    public class ExamMasterDeleteHandler : IRequestHandler<ExamMasterDelete, bool>
    {
        private readonly InterviewContext _interviewContext;
        public ExamMasterDeleteHandler(InterviewContext interviewContext)
        {
            _interviewContext = interviewContext;
        }

        public async Task<bool> Handle(ExamMasterDelete request, CancellationToken cancellationToken)
        {
            var existing = await _interviewContext.ExamMaster.FindAsync(request.ExamId);
            if (existing == null) return false;
            _interviewContext.ExamMaster.Remove(existing);
            return await _interviewContext.SaveChangesAsync() > 0;
        }
    }
}
