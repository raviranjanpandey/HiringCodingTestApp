using FluentValidation;
using MediatR;
using HiringCodingTestApis.Core.Models;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HiringCodingTestApis.Core.Answer
{
    public class DeleteByExamGroupId : IRequest<bool>
    {
        public int GroupId { get; set; }
    }

    public class DeleteByExamGroupIdValidator : AbstractValidator<DeleteByExamGroupId>
    {
        public DeleteByExamGroupIdValidator()
        {
            RuleFor(x => x.GroupId).NotEmpty().WithMessage("Please select valid exam group to delete.");
        }
    }

    public class DeleteByExamGroupIdHandler : IRequestHandler<DeleteByExamGroupId, bool>
    {
        private readonly InterviewContext _interviewContext;
        public DeleteByExamGroupIdHandler(InterviewContext interviewContext)
        {
            _interviewContext = interviewContext;
        }

        public async Task<bool> Handle(DeleteByExamGroupId request, CancellationToken cancellationToken)
        {
            var examsByGroupId = await _interviewContext.ExamGroup.
                                Where(x => x.GroupId == request.GroupId).Select(x => x.ExamIdJson).ToListAsync();

            if (examsByGroupId != null && examsByGroupId.Count > 0)
            {
                foreach (var examJson in examsByGroupId)
                {
                    var examids = JsonConvert.DeserializeObject<List<int>>(examJson);
                    var existing = await _interviewContext.Answers.
                           Where(x => examids.Contains((int)x.ExamId)).
                           ToListAsync();

                    if (existing != null)
                    {
                        _interviewContext.Answers.RemoveRange(existing);
                    }
                }
            }

            return await _interviewContext.SaveChangesAsync() > 0;
        }
    }
}
