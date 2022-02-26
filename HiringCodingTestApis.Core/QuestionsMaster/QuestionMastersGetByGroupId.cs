using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HiringCodingTestApis.Core.DTO;
using HiringCodingTestApis.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.QuestionsMaster
{
    public class QuestionMastersGetByGroupId : IRequest<QuestionMasterList>
    {
        public int GroupId { get; set; }
    }

    public class QuestionMastersGetByGroupIdValidator : AbstractValidator<QuestionMastersGetByGroupId>
    {
        public QuestionMastersGetByGroupIdValidator()
        {
            RuleFor(x => x.GroupId).GreaterThan(0).WithMessage("Group Id can not be zero.");
        }
    }

    public class QuestionMastersGetByGroupIdHandler : IRequestHandler<QuestionMastersGetByGroupId, QuestionMasterList>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;
        public QuestionMastersGetByGroupIdHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }
        public async Task<QuestionMasterList> Handle(QuestionMastersGetByGroupId request, CancellationToken cancellationToken)
        {
            var result = await _interviewContext.QuestionMaster.Where(x => x.GroupId == request.GroupId).ToListAsync();
            if (result != null)
            {
                return new QuestionMasterList
                {
                    QuestionsList = _mapper.Map<List<QuestionMaster>, List<QuestionMastersDto>>(result)
                };
            }
            return null;
        }
    }
}
