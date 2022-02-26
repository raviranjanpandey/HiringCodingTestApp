using FluentValidation;
using MediatR;
using HiringCodingTestApis.Core.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HiringCodingTestApis.Core.Answer
{
    public class AnswersDelete : IRequest<bool>
    {
        public string UserId { get; set; }
        public int ExamId { get; set; }
    }

    public class AnswerDeleteValidator : AbstractValidator<AnswersDelete>
    {
        public AnswerDeleteValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Please select User id to delete.");
            RuleFor(x => x.ExamId).NotEmpty().WithMessage("Please select ExamId id to delete.");
        }
    }

    public class AnswersDeleteHandler : IRequestHandler<AnswersDelete, bool>
    {
        private readonly InterviewContext _interviewContext;
        public AnswersDeleteHandler(InterviewContext interviewContext)
        {
            _interviewContext = interviewContext;
        }

        public async Task<bool> Handle(AnswersDelete request, CancellationToken cancellationToken)
        {
            var existing = await _interviewContext.Answers.
                           Where(x => x.UserId == request.UserId && x.ExamId == request.ExamId).
                           ToListAsync();

            if (existing == null) return false;
            _interviewContext.Answers.RemoveRange(existing);
            return await _interviewContext.SaveChangesAsync() > 0;
        }
    }
}
