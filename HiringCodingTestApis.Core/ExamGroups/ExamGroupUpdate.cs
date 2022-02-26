using FluentValidation;
using MediatR;
using Newtonsoft.Json;
using HiringCodingTestApis.Core.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.ExamGroups
{
    public class ExamGroupUpdate : IRequest<int>
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public List<int> ExamIds { get; set; }
        public string UserId { get; set; }

    }

    public class ExamGroupUpdateValidator : AbstractValidator<ExamGroupUpdate>
    {
        public ExamGroupUpdateValidator()
        {
            RuleFor(x => x.GroupId).NotEmpty().WithMessage("Id is manadtory to update.");
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.ExamIds).Must(examids => examids != null && examids.Count > 0).WithMessage("There should be atleast one selection.");
        }
    }

    public class ExamGroupUpdateHandler : IRequestHandler<ExamGroupUpdate, int>
    {
        private readonly InterviewContext _interviewContext;
        public ExamGroupUpdateHandler(InterviewContext interviewContext)
        {
            _interviewContext = interviewContext;
        }

        public async Task<int> Handle(ExamGroupUpdate request, CancellationToken cancellationToken)
        {
            var existing = await _interviewContext.ExamGroup.FindAsync(request.GroupId);

            if (existing != null)
            {
                existing.GroupName = request.Name;
               // existing.UserId = request.UserId;
                existing.ExamIdJson = JsonConvert.SerializeObject(request.ExamIds);
                await _interviewContext.SaveChangesAsync();
                return existing.GroupId;
            }

            return 0;
        }
    }
}
