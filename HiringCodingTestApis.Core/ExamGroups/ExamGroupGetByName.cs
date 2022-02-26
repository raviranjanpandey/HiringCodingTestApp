using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using HiringCodingTestApis.Core.DTO;
using HiringCodingTestApis.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.ExamGroups
{
    public class ExamGroupGetByName : IRequest<ExamGroupList>
    {
        public string Name { get; set; }
    }

    public class ExamGroupGetByNameValidator : AbstractValidator<ExamGroupGetByName>
    {
        public ExamGroupGetByNameValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Please provide valid name to fetch.");
        }
    }

    public class ExamGroupGetByNameHandler : IRequestHandler<ExamGroupGetByName, ExamGroupList>
    {
        private readonly InterviewContext _interviewContext;
        public ExamGroupGetByNameHandler(InterviewContext interviewContext)
        {
            _interviewContext = interviewContext;
        }
        public async Task<ExamGroupList> Handle(ExamGroupGetByName request, CancellationToken cancellationToken)
        {
            List<ExamGroupDto> examGroup = new List<ExamGroupDto>();
            var examgroups = await _interviewContext.ExamGroup.Where(x => x.GroupName == request.Name).ToListAsync();
            if (examgroups != null && examgroups.Count > 0)
            {
                var exams = await _interviewContext.ExamMaster.ToListAsync();
                if (exams != null && exams.Count > 0)
                {
                    foreach (var group in examgroups)
                    {
                        var examids = JsonConvert.DeserializeObject<List<int>>(group.ExamIdJson);
                        var filteredExams = exams.Where(x => examids.Contains(x.ExamId)).ToList();

                        var examgroup = new ExamGroupDto
                        {
                            GroupId = group.GroupId,
                            Name = group.GroupName,
                            UserId=group.UserId,
                            Subjects = (from exam in filteredExams
                                        select new ExamMasterDto
                                        {
                                            Description = exam.Description,
                                            ExamId = exam.ExamId,
                                            ExamName = exam.ExamName
                                        }).ToList()
                        };

                        examGroup.Add(examgroup);
                    }
                }
            }

            return new ExamGroupList
            {
                Exams = examGroup
            };
        }
    }
}
