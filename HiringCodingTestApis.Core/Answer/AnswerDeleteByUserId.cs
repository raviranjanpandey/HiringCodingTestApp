using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HiringCodingTestApis.Core.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.Answer
{
    public class AnswerDeleteByUserId : IRequest<bool>
    {
        public string UserId { get; set; }
    }

    public class AnswerDeleteByUserIdValidator : AbstractValidator<AnswerDeleteByUserId>
    {
        public AnswerDeleteByUserIdValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Please select User id to delete.");
        }
    }

    public class AnswerDeleteByUserIdHandler : IRequestHandler<AnswerDeleteByUserId, bool>
    {
        private readonly InterviewContext _interviewContext;
        public AnswerDeleteByUserIdHandler(InterviewContext interviewContext)
        {
            _interviewContext = interviewContext;
        }

        public async Task<bool> Handle(AnswerDeleteByUserId request, CancellationToken cancellationToken)
        {
            var existing = await _interviewContext.Answers.
                           Where(x => x.UserId == request.UserId).
                           ToListAsync();

            if (existing == null) return false;
            _interviewContext.Answers.RemoveRange(existing);
            return await _interviewContext.SaveChangesAsync() > 0;
        }
    }
}
