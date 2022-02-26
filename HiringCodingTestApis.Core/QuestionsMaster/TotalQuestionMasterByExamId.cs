using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HiringCodingTestApis.Core.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HiringCodingTestApis.Core.QuestionsMaster
{
    public class TotalQuestionMasterByExamId : IRequest<int>
    {
        public int ExamId { get; set; }
        public string UserId { get; set; }
        public TotalQuestionMasterByExamId(string userId, int examId)
        {
            UserId = userId;
            ExamId = examId;
        }
    }

    public class TotalQuestionMasterByExamIdHandler : IRequestHandler<TotalQuestionMasterByExamId, int>
    {
        private readonly InterviewContext _interviewContext;
        private readonly IMapper _mapper;
        public TotalQuestionMasterByExamIdHandler(InterviewContext interviewContext, IMapper mapper)
        {
            _interviewContext = interviewContext;
            _mapper = mapper;
        }
        public async Task<int> Handle(TotalQuestionMasterByExamId request, CancellationToken cancellationToken)
        {
            return await _interviewContext.QuestionMaster.Where(x => x.ExamId == request.ExamId && x.UserId == request.UserId).CountAsync();

        }

    }
}

