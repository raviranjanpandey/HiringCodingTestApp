using FluentValidation;
using MediatR;
using HiringCodingTestApis.Core.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.QuestionsMaster
{
    public class QuestionMastersDelete : IRequest<bool>
    {
        public int QueId { get; set; }
    }

    public class QuestionMasterDeleteValidator : AbstractValidator<QuestionMastersDelete>
    {
        public QuestionMasterDeleteValidator()
        {
            RuleFor(x => x.QueId).GreaterThan(0).WithMessage("Please select valid question id to delete.");
        }
    }

    public class QuestionMastersDeleteHandler : IRequestHandler<QuestionMastersDelete, bool>
    {
        private readonly InterviewContext _interviewContext;
        public QuestionMastersDeleteHandler(InterviewContext interviewContext)
        {
            _interviewContext = interviewContext;
        }

        public async Task<bool> Handle(QuestionMastersDelete request, CancellationToken cancellationToken)
        {
            var existing = await _interviewContext.QuestionMaster.FindAsync(request.QueId);
            if (existing == null) return false;
            _interviewContext.QuestionMaster.Remove(existing);
            return await _interviewContext.SaveChangesAsync() > 0;
        }
    }
}
