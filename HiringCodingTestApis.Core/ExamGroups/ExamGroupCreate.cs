using FluentValidation;
using MediatR;
using Newtonsoft.Json;
using HiringCodingTestApis.Core.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.ExamGroups
{
    public class ExamGroupCreate : IRequest<int>
    {
        public string Name { get; set; }
        public List<int> ExamIds { get; set; }
        public string UserId { get; set; }
    }

    public class ExamGroupCreateValidator : AbstractValidator<ExamGroupCreate>
    {
        public ExamGroupCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.ExamIds).Must(examids => examids != null && examids.Count > 0).WithMessage("There should be atleast one selection.");
        }
    }

    public class ExamGroupCreateHandler : IRequestHandler<ExamGroupCreate, int>
    {
        private readonly InterviewContext _interviewContext;
        public ExamGroupCreateHandler(InterviewContext interviewContext)
        {
            _interviewContext = interviewContext;
        }

        public async Task<int> Handle(ExamGroupCreate request, CancellationToken cancellationToken)
        {
            ExamGroup exam = new ExamGroup
            {
                GroupName = request.Name,
                UserId=request.UserId,
                ExamIdJson = JsonConvert.SerializeObject(request.ExamIds)
            };

            _interviewContext.ExamGroup.Add(exam);
            if (await _interviewContext.SaveChangesAsync() > 0)
            {
                return exam.GroupId;
            }
            return 0;
        }
    }
}
