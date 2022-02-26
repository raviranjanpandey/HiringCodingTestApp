using FluentValidation;
using MediatR;
using HiringCodingTestApis.Core.Models;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using HiringCodingTestApis.Core.DTO;
using AutoMapper;

namespace HiringCodingTestApis.Core.Answer
{
    public class GetAnswersByExamGroupId : IRequest<AnswersList>
    {
        public int GroupId { get; set; }
    }

    public class GetAnswersByExamGroupIdValidator : AbstractValidator<GetAnswersByExamGroupId>
    {
        public GetAnswersByExamGroupIdValidator()
        {
            RuleFor(x => x.GroupId).NotEmpty().WithMessage("Please select valid exam group to fetch.");
        }
    }

    public class GetAnswersByExamGroupIdHandler : IRequestHandler<GetAnswersByExamGroupId, AnswersList>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;
        public GetAnswersByExamGroupIdHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }

        public async Task<AnswersList> Handle(GetAnswersByExamGroupId request, CancellationToken cancellationToken)
        {
            List<AnswerDto> answers = new List<AnswerDto>();

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
                        answers.AddRange(_mapper.Map<List<Answers>, List<AnswerDto>>(existing));
                    }
                }
            }

            return new AnswersList
            {
                Answers = answers
            };
        }
    }
}
