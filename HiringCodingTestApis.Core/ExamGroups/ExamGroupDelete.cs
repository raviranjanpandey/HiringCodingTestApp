using FluentValidation;
using MediatR;
using HiringCodingTestApis.Core.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.ExamGroups
{
    public class ExamGroupDelete : IRequest<bool>
    {
        public int GroupId { get; set; }
    }
    public class ExamGroupDeleteValidator : AbstractValidator<ExamGroupDelete>
    {
        public ExamGroupDeleteValidator()
        {
            RuleFor(x => x.GroupId).GreaterThan(0).WithMessage("Please select a record to delete.");
        }
    }

    public class ExamGroupDeleteHandler : IRequestHandler<ExamGroupDelete, bool>
    {
        private readonly InterviewContext _interviewContext;
        public ExamGroupDeleteHandler(InterviewContext interviewContext)
        {
            _interviewContext = interviewContext;
        }

        public async Task<bool> Handle(ExamGroupDelete request, CancellationToken cancellationToken)
        {
            var existing = await _interviewContext.ExamGroup.FindAsync(request.GroupId);
            if (existing == null) return false;
            _interviewContext.ExamGroup.Remove(existing);
            return await _interviewContext.SaveChangesAsync() > 0;
        }
    }
}
