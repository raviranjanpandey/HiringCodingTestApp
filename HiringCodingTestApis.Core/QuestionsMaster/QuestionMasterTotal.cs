using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HiringCodingTestApis.Core.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.QuestionsMaster
{
    public class QuestionMasterTotal : IRequest<int>
    {
        public string UserId { get; set; }
    }
    public class QuestionMasterTotalHandler : IRequestHandler<QuestionMasterTotal, int>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;
        public QuestionMasterTotalHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }
        public async Task<int> Handle(QuestionMasterTotal request, CancellationToken cancellationToken)
        {
            return await _interviewContext.QuestionMaster.CountAsync();

        }

    }
}

