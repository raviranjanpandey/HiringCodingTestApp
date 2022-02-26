using FluentValidation;
using MediatR;
using HiringCodingTestApis.Core.Models;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HiringCodingTestApis.Core.ExamResults
{
    public class ExamResultDelete : IRequest<bool>
    {
        public string UserId { get; set; }
    }

    public class ExamResultDeleteValidator : AbstractValidator<ExamResultDelete>
    {
        public ExamResultDeleteValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId must not be zero or empty.");
        }
    }

    public class ExamResultDeleteHandler : IRequestHandler<ExamResultDelete, bool>
    {
        private readonly InterviewContext _interviewContext;
        public ExamResultDeleteHandler(InterviewContext interviewContext)
        {
            _interviewContext = interviewContext;
        }

        public async Task<bool> Handle(ExamResultDelete request, CancellationToken cancellationToken)
        {
            var existing = await _interviewContext.Results.Where(x => x.UserId == request.UserId).ToListAsync();
            if (existing == null) return false;
            _interviewContext.Results.RemoveRange(existing);
            return await _interviewContext.SaveChangesAsync() > 0;
        }
    }
}
